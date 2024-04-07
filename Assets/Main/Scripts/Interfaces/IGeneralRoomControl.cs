using UnityEngine;

public interface IGeneralRoomControl
{
    public Transform PlayerSpawnPoint { get; }
    
    public void SpawnUpgrade();

}
