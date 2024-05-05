using UnityEngine;
using UnityEngine.SceneManagement;

namespace BadJuja.UI {
    public class LoadUIScene : MonoBehaviour {
        public int UISceneIndex = 0;
        private void OnEnable()
        {
            Load();
        }

        private void OnDisable()
        {
            Unload();
        }

        public void Load()
        {
            SceneManager.LoadSceneAsync(UISceneIndex, LoadSceneMode.Additive);
        }

        public void Unload()
        {
            SceneManager.UnloadSceneAsync(UISceneIndex);
        }
    }
}
