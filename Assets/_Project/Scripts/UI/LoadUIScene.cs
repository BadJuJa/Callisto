using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadUIScene : MonoBehaviour
{
    public int UISceneIndex = 0;
    private void OnEnable()
    {
        SceneManager.LoadSceneAsync(UISceneIndex, LoadSceneMode.Additive);
    }

    private void OnDisable()
    {
        SceneManager.UnloadSceneAsync(UISceneIndex);
    }
}
