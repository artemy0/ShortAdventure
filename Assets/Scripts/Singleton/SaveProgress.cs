using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveProgress : MonoBehaviour
{
    public static SaveProgress Instance { get; private set; }

    public event System.Action<SaveProgress> OnUpdate;

    public int BestScore
    {
        get
        {
            return LoadData("BestScore");
        }
        set
        {
            SaveData("BestScore", value);
        }
    }
    public int BestTrophy
    {
        get
        {
            return LoadData("BestTrophy");
        }
        set
        {
            SaveData("BestTrophy", value);
        }
    }

    private void Awake()
    {
        //Debug.Log("SaveProgress - Awake");

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
        //Debug.Log("SaveProgress - Start");

        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            OnUpdate?.Invoke(this);
        }
    }

    private void OnLevelWasLoaded(int level)
    {
        //Debug.Log("SaveProgress - OnLevelWasLoaded - " + level + " level");

        if (level == 0)
        {
            OnUpdate?.Invoke(this);
        }
    }

    public void SaveAllData(int scoreValue, int bestTrophy)
    {
        if(scoreValue > BestScore)
        {
            BestScore = scoreValue;

            OnUpdate?.Invoke(this);
        }
        if(bestTrophy > BestTrophy)
        {
            BestTrophy = bestTrophy;

            OnUpdate?.Invoke(this);
        }
    }

    public void ResetAllDate()
    {
        BestScore = 0;
        BestTrophy = 0;

        OnUpdate?.Invoke(this);
    }

    private int LoadData(string dataName)
    {
        return PlayerPrefs.GetInt(dataName, 0);
    }

    private void SaveData(string dataName, int dataValue)
    {
        PlayerPrefs.SetInt(dataName, dataValue);
    }
}
