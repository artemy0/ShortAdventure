using UnityEngine;
using UnityEngine.SceneManagement;

public class Bag : MonoBehaviour
{
    public static Bag Instance { get; private set; }

    public event System.Action<Bag> OnUpdate;

    public int FoodCount { get; private set; }
    public int TrophyCount { get; private set; }

    private void Awake()
    {
        //Debug.Log("Bag - Awake");

        if (Instance == null)
        {
            Instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else if (Instance.gameObject != gameObject)
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start() //OnLevelWasLoaded вызывается только тогда когда сцена загружается, но если мы с неё стартуем то метод OnLevelWasLoaded не вызывается, а вместо него вызывается Start (если ОЧЕНЬ кратко)
    {
        //Debug.Log("Bag - Start");

        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            OnUpdate?.Invoke(this);
        }
    }

    private void OnLevelWasLoaded(int level)
    {
        //Debug.Log("Bag - OnLevelWasLoaded - " + level + " level");

        if (level != 0)
        {
            OnUpdate?.Invoke(this);
        }
    }

    public void AddFoods(int count)
    {
        FoodCount += count;

        OnUpdate?.Invoke(this);
    }

    public void AddTrophy()
    {
        TrophyCount++;

        OnUpdate?.Invoke(this);
    }

    public void ResetBag()
    {
        FoodCount = 0;
        TrophyCount = 0;

        OnUpdate?.Invoke(this);
    }
}
