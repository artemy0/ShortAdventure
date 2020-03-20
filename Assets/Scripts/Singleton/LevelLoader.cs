using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private Animator _foregroundAnimator;

    public static LevelLoader Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else if(Instance.gameObject != gameObject)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public void ReloadCurrentScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        StartCoroutine(LoadLevelCoroutine(currentSceneIndex));
    }

    public void LoadLevel(int sceneIndex)
    {
        StartCoroutine(LoadLevelCoroutine(sceneIndex));
    }

    private IEnumerator LoadLevelCoroutine(int sceneIndex)
    {
        _foregroundAnimator.SetTrigger("GoIn");
        yield return new WaitForSeconds(_foregroundAnimator.GetCurrentAnimatorClipInfo(0).Length);

        SceneManager.LoadScene(sceneIndex);
        while (!SceneManager.GetActiveScene().isLoaded)
            yield return null;

        _foregroundAnimator.SetTrigger("GoOut");
    }
}
