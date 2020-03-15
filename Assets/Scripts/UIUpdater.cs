using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIUpdater : MonoBehaviour
{
    [SerializeField] private Text FoodCount;
    [SerializeField] private Text TrophyCount;
    [SerializeField] private Text SecondSinceStart;

    public void Start()
    {
        Bag.Instance.OnUpdate += UpdateUI;
        Race.Instance.OnUpdate += UpdateUI;
    }

    private void OnDestroy()
    {
        if(Bag.Instance != null && Race.Instance != null)
        {
            Bag.Instance.OnUpdate -= UpdateUI;
            Race.Instance.OnUpdate -= UpdateUI;
        }
    }

    private void UpdateUI(Bag bag)
    {
        FoodCount.text = bag.FoodCount.ToString();
        TrophyCount.text = bag.TrophyCount.ToString();
    }

    private void UpdateUI(Race race)
    {
        SecondSinceStart.text = ((int)race.TimeSinceStart).ToString();
    }
}
