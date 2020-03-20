using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private int _sceneIndexForPortal;

    private bool _isTrigger;

    private void Start()
    {
        _isTrigger = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && _isTrigger == false)
        {
            _isTrigger = true;
            LevelLoader.Instance.LoadLevel(_sceneIndexForPortal); //0 is MainMenu
        }
    }
}
