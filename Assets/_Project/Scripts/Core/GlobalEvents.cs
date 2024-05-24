using BadJuja.Core.CharacterStats;
using System;
using UnityEngine;

namespace BadJuja.Core.Events {

    public static class GameRelatedEvents
    {
        public static Action OnPause;

        public static void Send_OnPause() => OnPause?.Invoke();
    }

    public static class ModifiersRelaterEvents
    {
        #region Events

        public static Action<AllCharacterStats, StatModifier, object> ApplyModifierToPlayer;
        public static Action<AllCharacterStats, object> RemovePlayerModifier;
        public static Action<StatModifier, object> ApplyModifierToTarget;

        #endregion

        #region Senders

        public static void Send_TargetModifier(StatModifier modifier, object target) => ApplyModifierToTarget?.Invoke(modifier, target);
        public static void Send_AddPlayerModifier(AllCharacterStats stat, StatModifier modifier, object source) => ApplyModifierToPlayer?.Invoke(stat, modifier, source);
        public static void Send_RemovePlayerModifier(AllCharacterStats stat, object source) => RemovePlayerModifier?.Invoke(stat, source);

        #endregion

    }

    public static class EnemyRelatedEvents {
        
        #region Events

        public static Action<int> OnEnemyDied;
        public static Action<Transform> PriorityTargetChanged;

        #endregion

        #region Senders
        
        public static void Send_OnEnemyDied(int xpValue) => OnEnemyDied?.Invoke(xpValue);
        public static void Send_PriorityTargetChanged(Transform newTarget) => PriorityTargetChanged?.Invoke(newTarget);
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

    public static class PlayerRelatedEvents {
        #region Events

        public static Action OnDeath;

        public static Action OnAttacked;
        public static Action OnAttackEnded;
        public static Action OnAttackPrepare;
        public static Action OnAttackDisableVFX;

        public static Action<int> OnSwitchToAttackType;
        public static Action OnChangedAttackType;

        public static Action OnEnteredLevel;
        public static Action PlayerClearedTheRoom;
        public static Action OnPlayerExitedRoom;

        public static Action<float, float> OnHealthChange;
        public static Action<float, float> OnExperienceChange;
        public static Action OnLevelIncrease;


        #endregion

        #region Senders

        public static void Send_OnAttack() => OnAttacked?.Invoke();
        public static void Send_OnAttackEnded() => OnAttackEnded?.Invoke();
        public static void Send_OnAttackPrepare() => OnAttackPrepare?.Invoke();
        public static void Send_OnAttackDisableVFX() => OnAttackDisableVFX?.Invoke();
        public static void Send_OnChangedAttackType() => OnChangedAttackType?.Invoke();
        public static void Send_OnSwitchToAttackType(int value) => OnSwitchToAttackType?.Invoke(value);
        public static void Send_EnteredLevel() => OnEnteredLevel?.Invoke();
        public static void Send_OnHealthChanged(float currentValue, float maxValue) => OnHealthChange?.Invoke(currentValue, maxValue);
        public static void Send_OnExperienceChanged(float currentValue, float maxValue) => OnExperienceChange?.Invoke(currentValue, maxValue);
        public static void Send_OnClearedTheRoom() => PlayerClearedTheRoom?.Invoke();
        public static void Send_OnExitedRoom() => OnPlayerExitedRoom?.Invoke();
        public static void Send_OnLevelIncreased() => OnLevelIncrease?.Invoke();
        public static void Send_OnDeath() => OnDeath?.Invoke();
        #endregion
    }
}