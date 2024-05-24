using BadJuja.Core.Data;
using UnityEngine;

namespace BadJuja.Core
{
    public class MainSceneEntryPoint : MonoBehaviour
    {
        public GameManager GameManager;

        [Space]
        public PlayerCurrentEquipment PlayerCurrentEquipment;

        [Space]
        public Player.Player Player;
        public LevelManagement.LevelManager LevelManager;

        private Player.Movement _playerMovement;
        private Player.Combat _playerCombat;

        private void Start()
        {
            Initialize();
        }

        public void Initialize()
        {
            GameManager.Initialize();

            if (Player != null)
                Player.Initialize(PlayerCurrentEquipment);

            Player.Visuals.Model playerModelComponent = Player.InstantiateModel(PlayerCurrentEquipment.Model);

            _playerMovement = Player.gameObject.GetComponent<Player.Movement>();
            _playerMovement.Initialize(playerModelComponent.GetModelTransform());

            if (_playerCombat == null)
                _playerCombat = Player.GetComponentInChildren<Player.Combat>();
            _playerCombat.Initialize(playerModelComponent.GetFiringPoint(), PlayerCurrentEquipment);

            if (LevelManager != null)
                LevelManager.Initialize(Player.transform);
            
        }
    }
}
