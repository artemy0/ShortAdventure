using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuUIUpdater : MonoBehaviour
{
    [SerializeField] private Text _bestScoreText;
    [SerializeField] private Text _bestTimeText;

    public void Start()
    {
        InitUI();

        SaveProgress.Instance.OnUpdate += UpdateUI;
    }

    private void OnDestroy()
    {
        if (SaveProgress.Instance != null)
        {
            SaveProgress.Instance.OnUpdate -= UpdateUI;
        }
    }

    private void InitUI()
    {
        UpdateUI(SaveProgress.Instance);
    }

    private void UpdateUI(SaveProgress saveProgress)
    {
        _bestScoreText.text = "Best score is " + saveProgress.BestScore;

        _bestTimeText.text = "Best time is " + saveProgress.BestTime;
    }
}
