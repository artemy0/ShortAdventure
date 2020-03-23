using UnityEngine;
using UnityEngine.UI;

public class MenuUIUpdater : MonoBehaviour
{
    [SerializeField] private Text _bestScoreText;
    [SerializeField] private Text _bestTrophyText;

    public void OnEnable()
    {
        //Debug.Log("MenuUI - OnEnable");

        if (SaveProgress.Instance != null)
        {
            SaveProgress.Instance.OnUpdate += UpdateUI;
        }
    }

    private void OnDestroy()
    {
        //Debug.Log("MenuUI - OnDestroy");

        if (SaveProgress.Instance != null)
        {
            SaveProgress.Instance.OnUpdate -= UpdateUI;
        }
    }

    private void UpdateUI(SaveProgress saveProgress)
    {
        _bestScoreText.text = "Best score is " + saveProgress.BestScore;
        _bestTrophyText.text = "Best trophy is " + saveProgress.BestTrophy;
    }
}
