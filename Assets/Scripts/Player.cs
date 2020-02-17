using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Speeds")]
    public float WalkSpeed = 3;
    public float JumpForce = 10;

    [SerializeField]private GameObject SpawnPoint;

    private Transform _transform;
    private Rigidbody2D _rigidbody;
    private Animator _animatorController;

    private DirectionState _directionState;
    private bool _canMove;
    private bool _isGround;
    private bool _canDoubleJump;
    private bool _canJump;

    private void Start()
    {
        _transform = GetComponent<Transform>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _animatorController = GetComponent<Animator>();
        _directionState = transform.localScale.x > 0 ? DirectionState.Right : DirectionState.Left;
    }

    public void Move(Vector2 direction)
    {
        if (_canMove)
        {
            if (_directionState == DirectionState.Left && direction == Vector2.right)
            {
                Rotate();
                _directionState = DirectionState.Right;
            }
            else if (_directionState == DirectionState.Right && direction == Vector2.left)
            {
                Rotate();
                _directionState = DirectionState.Left;
            }

            float speed = GetSpeed();
            _animatorController.SetFloat("Speed", speed);

            _rigidbody.velocity = direction * WalkSpeed * Time.deltaTime + new Vector2(0, _rigidbody.velocity.y); //called in FixedUpdate method
        }
    }

    public void Jump()
    {
        if (_canJump && _isGround == true)
        {
            _canJump = false;

            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, JumpForce);
            _animatorController.SetTrigger("Jump");
            _animatorController.SetBool("Fall", false);
        }
        else if (_canDoubleJump && _isGround == false)
        {
            _canDoubleJump = false;
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, JumpForce);
            _animatorController.SetTrigger("DoubleJump");
            _animatorController.SetBool("Fall", false);
        }
    }

    private void Update()
    {
        if (_isGround == false && _rigidbody.velocity.y < 0f)
        {
            _animatorController.SetBool("Fall", true);
        }
        else
        {
            _animatorController.SetBool("Fall", false);
        }
    }

    private void Rotate()
    {
        _transform.localScale = new Vector3(-_transform.localScale.x, _transform.localScale.y, _transform.localScale.z);
    }

    private float GetSpeed()
    {
        return Mathf.Abs((float)System.Math.Round(_rigidbody.velocity.x, 1));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Ender");
        if (collision.gameObject.CompareTag("Terrain"))
        {
            _isGround = true;
            _canMove = true;

            _canJump = true;
            _canDoubleJump = true;

            float speed = GetSpeed();
            _animatorController.SetFloat("Speed", speed); //idle or wolk anim after landing
            _animatorController.SetBool("Fall", false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Terrain"))
        {
            _isGround = false;
            _canMove = false;
        }
    }

    public void Respawn()
    {
        StartCoroutine(RespawnEnumerator(1f));
    }

    private IEnumerator RespawnEnumerator(float animationDelay)
    {
        _rigidbody.bodyType = RigidbodyType2D.Static;

        _animatorController.SetTrigger("Desappearing");
        yield return new WaitForSeconds(_animatorController.GetCurrentAnimatorStateInfo(0).length + animationDelay);

        transform.position = SpawnPoint.transform.position;

        _animatorController.SetTrigger("Appearing");
        yield return new WaitForSeconds(_animatorController.GetCurrentAnimatorStateInfo(0).length);

        _rigidbody.bodyType = RigidbodyType2D.Dynamic;
    }

    enum DirectionState
    {
        Right,
        Left
    }
}
