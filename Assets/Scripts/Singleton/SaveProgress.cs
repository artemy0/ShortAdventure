using UnityEngine;
using UnityEngine.UI;

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
            if (value > LoadData("BestScore"))
                SaveData("BestScore", value);
        }
    }

    public int BestTime
    {
        get
        {
            return LoadData("BestTime");
        }
        set
        {
            if (value > LoadData("BestTime"))
                SaveData("BestTime", value);
        }
    }

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance.gameObject != gameObject)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        LoadAllData();
    }

    public void LoadAllData()
    {
        OnUpdate(this);
    }

    public void SaveAllData(int scoreValue, int timeValue)
    {
        BestScore = scoreValue;
        BestTime = timeValue;
    }

    public void ResetAllDate()
    {
        BestScore = 0;
        BestTime = 0;
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
