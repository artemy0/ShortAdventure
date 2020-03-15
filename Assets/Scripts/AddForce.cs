using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForce : MonoBehaviour
{
    [SerializeField] private float _maxForce;

    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        AddForceRandomDirection(_maxForce);
    }

    private void AddForceRandomDirection(float maxForce)
    {
        float randomXForce = Random.Range(-maxForce, maxForce);
        float randomYForce = Random.Range(-maxForce, maxForce);

        Vector2 force = new Vector2(randomXForce, randomYForce);

        _rigidbody.AddForce(force);
    }
}
