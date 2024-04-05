using System;
using Graph;
using Unity.Muse.Behavior;
using UnityEngine;

#if UNITY_EDITOR
[CreateAssetMenu(menuName = "Muse Behavior/Event Channels/PlayerEnteredRoom")]
#endif
[Serializable]
[EventChannelDescription(name: "PlayerEnteredRoom", message: "[Player] entered the room", category: "Events", id: "55d3e91fa201c5c209a6af4fe42469be")]
public class PlayerEnteredRoom : EventChannelBase
{
    public delegate void PlayerEnteredRoomEventHandler(GameObject Player);
    public event PlayerEnteredRoomEventHandler Event; 

    public void SendEventMessage(GameObject Player)
    {
        Event?.Invoke(Player);
    }

    public override void SendEventMessage(BlackboardVariable[] messageData)
    {
        BlackboardVariable<GameObject> PlayerBlackboardVariable = messageData[0] as BlackboardVariable<GameObject>;
        var Player = PlayerBlackboardVariable != null ? PlayerBlackboardVariable.Value : default(GameObject);

        Event?.Invoke(Player);
    }

    public override Delegate CreateEventHandler(BlackboardVariable[] vars, System.Action callback)
    {
        PlayerEnteredRoomEventHandler del = (Player) =>
        {
            BlackboardVariable<GameObject> var0 = vars[0] as BlackboardVariable<GameObject>;
            if(var0 != null)
                var0.Value = Player;

            callback();
        };
        return del;
    }

    public override void RegisterListener(Delegate del)
    {
        Event += del as PlayerEnteredRoomEventHandler;
    }

    public override void UnregisterListener(Delegate del)
    {
        Event -= del as PlayerEnteredRoomEventHandler;
    }
}

