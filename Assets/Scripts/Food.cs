using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    [SerializeField] private int _satiety = 1;

    private Animator _animator;
    private CircleCollider2D _collider;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _collider = GetComponent<CircleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && _collider.enabled)
        {
            _collider.enabled = false;

            Bag.Instance.AddFoods(_satiety);

            _animator.SetTrigger("Destroy");
        }
    }
}
