using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private LevelLoader _levelLoader;

    private void Awake()
    {
        if(_levelLoader == null)
        {
            _levelLoader = this;

            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
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
