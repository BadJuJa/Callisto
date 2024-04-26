using System;
using UnityEngine;

namespace BadJuja.Core.Events {
    public static class EnemyRelatedEvents {
        
        #region Events

        public static Action<float> OnEnemyDied;

        #endregion

        #region Senders
        
        public static void Send_OnEnemyDied(float xpValue) => OnEnemyDied?.Invoke(xpValue);
        
        #endregion
    }

    public static class LevelLoadingRelatedEvents
    {
        #region Events

        public static Action OnScreenFadeInStarted;
        public static Action OnScreenFadeOutStarted;
        public static Action OnScreenFadeInFinished;
        public static Action OnScreenFadeOutFinished;

        public static Action<Collider> OnNewCameraConfinerColliderAvailable;

        #endregion

        #region Senders 

        public static void Send_OnScreenFadeInStarted() => OnScreenFadeInStarted?.Invoke();
        public static void Send_OnScreenFadeOutStarted() => OnScreenFadeOutStarted?.Invoke();
        public static void Send_OnScreenFadeInFinished() => OnScreenFadeInFinished?.Invoke();
        public static void Send_OnScreenFadeOutFinished() => OnScreenFadeOutFinished?.Invoke();

        public static void Send_OnNewCameraConfinderColliderAvailable(Collider newCollider) => OnNewCameraConfinerColliderAvailable?.Invoke(newCollider);

        #endregion
    }

    public static class PlayerRelatedEvents
    {
        #region Events

        public static Action PlayerInitiated;

        public static Action OnPlayerAttacked;
        public static Action OnPlayerAttackEnded;
        public static Action OnPlayerAttackPrepare;
        public static Action OnPlayerAttackDisableVFX;

        public static Action<int> OnSwitchToAttackType;
        public static Action OnPlayerChangedAttackType;

        public static Action OnPlayerEnteredLevel;
        public static Action PlayerClearedTheRoom;
        public static Action OnPlayerExitedRoom;

        public static Action<float, float> OnHealthChange;
        public static Action<float, float> OnExperienceChange;
        public static Action OnLevelIncrease;


        #endregion

        #region Senders

        public static void Send_PlayerInitiated() => PlayerInitiated?.Invoke();

        public static void Send_OnPlayerAttack() => OnPlayerAttacked?.Invoke();
        public static void Send_OnPlayerAttackEnded() => OnPlayerAttackEnded?.Invoke();
        public static void Send_OnPlayerAttackPrepare() => OnPlayerAttackPrepare?.Invoke();
        public static void Send_OnPlayerAttackDisableVFX() => OnPlayerAttackDisableVFX?.Invoke();

        public static void Send_OnPlayerChangedAttackType() => OnPlayerChangedAttackType?.Invoke();
        public static void Send_OnSwitchToAttackType(int value) => OnSwitchToAttackType?.Invoke(value);

        public static void Send_OnPlayerExitedRoom() => OnPlayerExitedRoom?.Invoke();
        public static void Send_PlayerEnteredLevel() => OnPlayerEnteredLevel?.Invoke();

        public static void Send_OnPlayerHealthChanged(float currentValue, float maxValue) => OnHealthChange?.Invoke(currentValue, maxValue);
        public static void Send_OnPlayerExperienceChanged(float currentValue, float maxValue) => OnExperienceChange?.Invoke(currentValue, maxValue);
        
        public static void Send_OnPlayerClearedTheRoom() => PlayerClearedTheRoom?.Invoke();
        public static void Send_OnPlayerLevelIncreased() => OnLevelIncrease?.Invoke();
        
        #endregion
    }
}