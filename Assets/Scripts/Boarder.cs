using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Boarder : MonoBehaviour
{
    public TerrainGenerator terrainGenerator;

    [SerializeField] private UnityEvent Respawn;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            LevelLoader.Instance.ReloadCurrentScene();
        }
    }
}
