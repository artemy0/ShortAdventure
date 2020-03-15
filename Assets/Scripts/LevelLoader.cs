using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private Animator _animator;

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
        StartCoroutine(LoadLevelCoroutine(SceneManager.GetActiveScene().buildIndex));
    }

    public void LoadLevel(int sceneIndex)
    {
        //if(SceneManager.GetSceneByBuildIndex(sceneIndex) != null)
        //{
        //    Debug.Log($"scene with index {sceneIndex} not found!");
        //    return;
        //}

        StartCoroutine(LoadLevelCoroutine(sceneIndex));
    }

    private IEnumerator LoadLevelCoroutine(int sceneIndex)
    {
        _animator.SetTrigger("GoIn");
        yield return new WaitForSeconds(_animator.GetCurrentAnimatorClipInfo(0).Length);

        SceneManager.LoadScene(sceneIndex);
        while (!SceneManager.GetActiveScene().isLoaded)
            yield return null;

        _animator.SetTrigger("GoOut");
    }
}
