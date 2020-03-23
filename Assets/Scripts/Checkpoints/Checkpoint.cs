using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    protected BoxCollider2D _collider;
    protected Animator _animator;

    protected static Race _race;

    private bool _isTriggered;

    private void Awake()
    {
        _collider = GetComponent<BoxCollider2D>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        if(_race == null)
        {
            _race = gameObject.GetComponentInParent<Race>();
        }
    }

    protected virtual void Enter(Collider2D collision)
    {
    }

    protected virtual void Exit(Collider2D collision)
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && _collider.enabled == true)
        {
            Enter(collision);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Exit(collision);
        }
    }
}
