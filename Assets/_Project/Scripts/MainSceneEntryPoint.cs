using BadJuja.Core.Data;
using BadJuja.Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BadJuja.Core
{
    public class MainSceneEntryPoint : MonoBehaviour
    {
        public int UISceneIndex = 0;

        public PlayerCurrentEquipment PlayerCurrentEquipment;

        public Player.Player Player;
        public PlayerCombat PlayerCombat;
        public PlayerMovement PlayerMovement;

        public LevelManagement.LevelManager LevelManager;
        private void Awake()
        {
#if !UNITY_EDITOR
            SceneManager.LoadSceneAsync(UISceneIndex, LoadSceneMode.Additive);
#endif
        }

        private void Start()
        {
            Initialize();
        }

        public void Initialize()
        {
            if (Player != null)
                Player.Initialize(PlayerCurrentEquipment);
            
            PlayerModel playerModelComponent = Player.InstantiateModel(PlayerCurrentEquipment.Model);

            PlayerMovement = Player.gameObject.GetComponent<PlayerMovement>();
            PlayerMovement.Initialize(playerModelComponent.GetModelTransform());

            if (PlayerCombat != null)
                PlayerCombat.Initialize(playerModelComponent.GetFiringPoint(), PlayerCurrentEquipment);

            if (LevelManager != null)
                LevelManager.Initialize();
            
        }

        private void OnDestroy()
        {
#if !UNITY_EDITOR
            SceneManager.UnloadSceneAsync(UISceneIndex);
#endif
        }
    }
}
