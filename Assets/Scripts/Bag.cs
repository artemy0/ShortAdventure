using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bag : MonoBehaviour
{
    public int FoodCount { get; private set; }
    public int TrophyCount { get; private set; }

    public static Bag Instance { get; private set; }

    public event System.Action<Bag> OnUpdate;

    private void Awake()
    {   
        if(Instance == null)
        {
            Instance = this;
        }
        else if (Instance.gameObject != gameObject)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    private void OnDestroy()
    {
        if(Instance.gameObject == gameObject)
        {
            Instance = null;
        }
    }

    public void AddFoods(int count)
    {
        FoodCount += count;

        if (OnUpdate != null)
        {
            OnUpdate(this);
        }
    }

    public void AddTrophy()
    {
        TrophyCount++;

        if (OnUpdate != null)
        {
            OnUpdate(this);
        }
    }
}
