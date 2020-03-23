using UnityEngine;
using UnityEngine.UI;

public class LevelUIUpdater : MonoBehaviour
{
    [SerializeField] private Race _race;

    [SerializeField] private Text _foodCountText;
    [SerializeField] private Text _trophyCountText;
    [SerializeField] private Text _secondSinceStartText;

    public void OnEnable()
    {
        //Debug.Log("LevlelUI - OnEnable");

        if (Bag.Instance != null && _race != null)
        {
            Bag.Instance.OnUpdate += UpdateUI;
            _race.OnUpdate += UpdateUI;
        }
    }

    private void OnDestroy()
    {
        //Debug.Log("LevelUI - OnDestroy");

        if (Bag.Instance != null && _race != null)
        {
            Bag.Instance.OnUpdate -= UpdateUI;
            _race.OnUpdate -= UpdateUI;
        }
    }

    private void UpdateUI(Bag bag)
    {
        if(_foodCountText != null && _trophyCountText != null)
        {
            _foodCountText.text = bag.FoodCount.ToString();
            _trophyCountText.text = bag.TrophyCount.ToString();
        }
    }

    private void UpdateUI(Race race)
    {
        _secondSinceStartText.text = ((int)race.TimeSinceStart).ToString();
    }
}
