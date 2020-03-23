using UnityEngine;

public class ButtonHelper : MonoBehaviour
{
    public void LoadLevel(int sceneIndex)
    {
        LevelLoader.Instance.LoadLevel(sceneIndex);
    }

    public void ResetBag()
    {
        Bag.Instance?.ResetBag();
    }
}
