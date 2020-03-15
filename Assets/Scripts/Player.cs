using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Speeds")]
    [SerializeField] private float _walkSpeed = 3f;
    [SerializeField] private float _jumpForce = 10f;
    [SerializeField] private LayerMask _whatIsGround;

    [SerializeField] private GameObject _spawnPoint;

    private Rigidbody2D _rigidbody;
    private Animator _animatorController;

    private Transform _groundCheck;
    const float GroundedRadius = .3f;
    private bool _grounded;

    private bool _facingRight = true;
    private bool _doubleJumped = false;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animatorController = GetComponent<Animator>();

        _groundCheck = transform.Find("GroundCheck");
    }

    private void FixedUpdate()
    {
        _grounded = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(_groundCheck.position, GroundedRadius, _whatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                _doubleJumped = false;
                _grounded = true;
            }
        }
        _animatorController.SetBool("Ground", _grounded);

        _animatorController.SetBool("DoubleJump", _doubleJumped);

        _animatorController.SetFloat("vSpeed", _rigidbody.velocity.y);

        //Debug.Log(_grounded ? "grounded" : "not grounded"); //remove
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(_groundCheck.position, GroundedRadius);
    }

    public void Move(Vector2 direction)
    {
        if (_grounded)
        {
            if (!_facingRight && direction == Vector2.right)
            {
                Flip();
            }
            else if (_facingRight && direction == Vector2.left)
            {
                Flip();
            }

            float speed = Mathf.Abs(_rigidbody.velocity.x);
            _animatorController.SetFloat("Speed", speed);

            _rigidbody.velocity = direction * _walkSpeed * Time.deltaTime + new Vector2(0, _rigidbody.velocity.y); //called in FixedUpdate method
        }
    }

    public void Jump()
    {
        if (_grounded)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpForce);
        }
        else if (!_grounded && !_doubleJumped)
        {
            _doubleJumped = true;
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpForce);
        }
    }


    /*
    public void Move(Vector2 direction)
    {
        if (_canMove)
        {
            if (!_facingRight && direction == Vector2.right)
            {
                Flip();
            }
            else if (_directionState == DirectionState.Right && direction == Vector2.left)
            {
                Flip();
                _directionState = DirectionState.Left;
            }

            float speed = GetSpeed();
            _animatorController.SetFloat("Speed", speed);

            _rigidbody.velocity = direction * _walkSpeed * Time.deltaTime + new Vector2(0, _rigidbody.velocity.y); //called in FixedUpdate method
        }
    }

    public void Jump()
    {
        if (_canJump && _isGround == true)
        {
            _canJump = false;

            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpForce);
            _animatorController.SetTrigger("Jump");
            _animatorController.SetBool("Fall", false);
        }
        else if (_canDoubleJump && _isGround == false)
        {
            _canDoubleJump = false;
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpForce);
            _animatorController.SetTrigger("DoubleJump");
            _animatorController.SetBool("Fall", false);
        }
    }
    */


    //private void Update()
    //{
    //    if (_isGround == false && _rigidbody.velocity.y < 0f)
    //    {
    //        _animatorController.SetBool("Fall", true);
    //    }
    //    else
    //    {
    //        _animatorController.SetBool("Fall", false);
    //    }
    //}

    private void Flip()
    {
        _facingRight = !_facingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    //private float GetSpeed()
    //{
    //    return Mathf.Abs((float)System.Math.Round(_rigidbody.velocity.x, 1));
    //}

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    Debug.Log("Ender");
    //    if (collision.gameObject.CompareTag("Terrain"))
    //    {
    //        _isGround = true;
    //        _canMove = true;

    //        _canJump = true;
    //        _canDoubleJump = true;

    //        float speed = GetSpeed();
    //        _animatorController.SetFloat("Speed", speed); //idle or wolk anim after landing
    //        _animatorController.SetBool("Fall", false);
    //    }
    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Terrain"))
    //    {
    //        _isGround = false;
    //        _canMove = false;
    //    }
    //}

    public void Respawn()
    {
        StartCoroutine(RespawnEnumerator(1f));
    }

    private IEnumerator RespawnEnumerator(float animationDelay)
    {
        _rigidbody.bodyType = RigidbodyType2D.Static;

        _animatorController.SetTrigger("Desappearing");
        yield return new WaitForSeconds(_animatorController.GetCurrentAnimatorStateInfo(0).length + animationDelay);

        transform.position = _spawnPoint.transform.position;

        _animatorController.SetTrigger("Appearing");
        yield return new WaitForSeconds(_animatorController.GetCurrentAnimatorStateInfo(0).length);

        _rigidbody.bodyType = RigidbodyType2D.Dynamic;
    }
}
