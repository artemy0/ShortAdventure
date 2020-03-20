using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUIUpdater : MonoBehaviour
{
    [SerializeField] private Text _foodCount;
    [SerializeField] private Text _trophyCount;
    [SerializeField] private Text _secondSinceStart;

    public void Start()
    {
        InitUI();

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

    private void InitUI()
    {
        UpdateUI(Bag.Instance);
    }

    private void UpdateUI(Bag bag)
    {
        _foodCount.text = bag.FoodCount.ToString();
        _trophyCount.text = bag.TrophyCount.ToString();
    }

    private void UpdateUI(Race race)
    {
        _secondSinceStart.text = ((int)race.TimeSinceStart).ToString();
    }
}
