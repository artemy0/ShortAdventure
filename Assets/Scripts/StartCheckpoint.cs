using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCheckpoint : MonoBehaviour
{
    private BoxCollider2D _collider;

    private void Start()
    {
        _collider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision) //start race
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (transform.position.x > collision.transform.position.x) //is the player to the left of the start
            {
                Race.Instance.StartRace();
            }
            else
            {
                _collider.isTrigger = false; //make the object impossible to pass
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        _collider.isTrigger = true; //return the ability to pass through an object
    }
}
