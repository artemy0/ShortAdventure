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

            SaveProgress.Instance.SaveAllData(Bag.Instance.FoodCount, Bag.Instance.TrophyCount);

            LevelLoader.Instance.LoadLevel(_sceneIndexForPortal); //0 is MainMenu
        }
    }
}
