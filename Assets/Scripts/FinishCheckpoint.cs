using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishCheckpoint : MonoBehaviour
{
    private BoxCollider2D _collider;
    private Animator _animator;

    private void Start()
    {
        _collider = GetComponent<BoxCollider2D>();
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && _collider.enabled)
        {
            _collider.enabled = false;

            Bag.Instance.AddTrophy();

            Race.Instance.StopRace();
            _animator.SetTrigger("Destroy");
        }
    }
}
