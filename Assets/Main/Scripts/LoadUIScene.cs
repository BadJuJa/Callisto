using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadUIScene : MonoBehaviour
{
    SceneManager sceneManager;

    private void OnEnable()
    {
        SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
    }

    private void OnDisable()
    {
        SceneManager.UnloadSceneAsync(1);
    }
}
