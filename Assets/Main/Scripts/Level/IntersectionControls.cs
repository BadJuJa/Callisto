using UnityEngine;

public class IntersectionControls : MonoBehaviour, IGeneralRoomControl 
{
    public Transform playerSpawnPoint;

    public Transform UpgradeSpawnPosition;
    public GameObject UpgradePrefab;

    public Transform PlayerSpawnPoint => playerSpawnPoint;

    public void SpawnUpgrade()
    {
        if (UpgradePrefab != null)
        {
            Instantiate(UpgradePrefab, position: UpgradeSpawnPosition.position, Quaternion.identity);
        }
    }
}
