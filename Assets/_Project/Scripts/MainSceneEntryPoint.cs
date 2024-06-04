using BadJuja.Core.CharacterStats;
using BadJuja.Core.Data;
using BadJuja.Player;
using BadJuja.Player.Visuals;
using System;
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

        private Movement _playerMovement;
        private Combat _playerCombat;
        private ExperienceContainer _experienceContainer;
        private void Start()
        {
            Initialize();
        }

        public void Initialize()
        {
            GameManager.Initialize();

            if (Player != null)
                Player.Initialize(PlayerCurrentEquipment);
            else
                throw new NullReferenceException();

            Model playerModelComponent = Model.InstantiateModel(PlayerCurrentEquipment.Model, Player.transform);

            _playerMovement = Player.gameObject.GetComponent<Movement>();
            _playerMovement.Initialize(playerModelComponent.GetModelTransform(), Player.GetComponent<IStats>());

            _playerCombat = Player.GetComponentInChildren<Combat>();
            _playerCombat.Initialize(playerModelComponent.GetFiringPoint(), PlayerCurrentEquipment);

            _experienceContainer = Player.GetComponentInChildren<ExperienceContainer>();
            _experienceContainer.Initialize();

            LevelManager.Initialize(Player.transform);
            
        }
    }
}
