public static class GlobalEvents
{
    #region Delegates
    public delegate void delegate_OnPlayerAttacked();
    public delegate void delegate_OnPlayerChangedAttackType(PlayerAttackTypes newType);
    public delegate void delegate_OnSwitchToAttackType(int value);
    public delegate void delegate_OnPlayerExitedRoom();
    public delegate void delegate_OnScreenFadeInStarted();
    public delegate void delegate_OnScreenFadeOutStarted();
    public delegate void delegate_OnScreenFadeInFinished();
    public delegate void delegate_OnScreenFadeOutFinished();
    #endregion

    #region Events
    public static delegate_OnPlayerAttacked Event_OnPlayerAttacked;
    public static delegate_OnPlayerChangedAttackType Event_OnPlayerChangedAttackType;

    public static delegate_OnSwitchToAttackType Event_OnSwitchToAttackType;

    public static delegate_OnScreenFadeInStarted Event_OnScreenFadeInStarted;
    public static delegate_OnScreenFadeOutStarted Event_OnScreenFadeOutStarted;
    public static delegate_OnScreenFadeInFinished Event_OnScreenFadeInFinished;
    public static delegate_OnScreenFadeOutFinished Event_OnScreenFadeOutFinished;

    public static delegate_OnPlayerExitedRoom Event_OnPlayerExitedRoom;
    #endregion

    #region Senders
    public static void Send_OnPlayerAttack() => Event_OnPlayerAttacked?.Invoke();
    public static void Send_OnPlayerChangedAttackType(PlayerAttackTypes newType) => Event_OnPlayerChangedAttackType?.Invoke(newType);

    public static void Send_OnSwitchToAttackType(int value) => Event_OnSwitchToAttackType?.Invoke(value);

    public static void Send_OnScreenFadeInStarted() => Event_OnScreenFadeInStarted?.Invoke();
    public static void Send_OnScreenFadeOutStarted() => Event_OnScreenFadeOutStarted?.Invoke();
    public static void Send_OnScreenFadeInFinished() => Event_OnScreenFadeInFinished?.Invoke();
    public static void Send_OnScreenFadeOutFinished() => Event_OnScreenFadeOutFinished?.Invoke();

    public static void Send_OnPlayerExitedRoom() => Event_OnPlayerExitedRoom?.Invoke();
    #endregion
}
