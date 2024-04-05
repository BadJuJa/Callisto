using System;
using UnityEngine;

public static class GlobalEvents
{
    #region Events
    public static Action OnPlayerAttacked;
    public static Action<PlayerAttackTypes> OnPlayerChangedAttackType;

    public static Action<int> OnSwitchToAttackType;

    public static Action OnScreenFadeInStarted;
    public static Action OnScreenFadeOutStarted;
    public static Action OnScreenFadeInFinished;
    public static Action OnScreenFadeOutFinished;

    public static Action OnPlayerExitedRoom;
    public static Action OnPlayerEnteredLevel;
    public static Action<Collider> OnNewCameraConfinerColliderAvailable;

    public static Action<float> OnPlayerHealthChanged;

    public static Action OnPlayerClearedTheRoom;
    #endregion

    #region Senders
    public static void Send_OnPlayerAttack() => OnPlayerAttacked?.Invoke();
    public static void Send_OnPlayerChangedAttackType(PlayerAttackTypes newType) => OnPlayerChangedAttackType?.Invoke(newType);

    public static void Send_OnSwitchToAttackType(int value) => OnSwitchToAttackType?.Invoke(value);

    public static void Send_OnScreenFadeInStarted() => OnScreenFadeInStarted?.Invoke();
    public static void Send_OnScreenFadeOutStarted() => OnScreenFadeOutStarted?.Invoke();
    public static void Send_OnScreenFadeInFinished() => OnScreenFadeInFinished?.Invoke();
    public static void Send_OnScreenFadeOutFinished() => OnScreenFadeOutFinished?.Invoke();

    public static void Send_OnPlayerExitedRoom() => OnPlayerExitedRoom?.Invoke();
    public static void Send_PlayerEnteredLevel() => OnPlayerEnteredLevel?.Invoke();
    public static void Send_OnNewCameraConfinderColliderAvailable(Collider newCollider) => OnNewCameraConfinerColliderAvailable?.Invoke(newCollider);
    public static void Send_OnPlayerHealthChanged(float newPerventage) => OnPlayerHealthChanged?.Invoke(newPerventage);

    public static void Send_OnPlayerClearedTheRoom() => OnPlayerClearedTheRoom?.Invoke();
    #endregion
}
