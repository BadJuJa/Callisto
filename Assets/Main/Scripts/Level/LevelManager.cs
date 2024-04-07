using System.Collections;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject[] Rooms;
    public GameObject[] Intersections;
    public Transform[] SpawnPoints;

    public Transform PlayerTransform;

    private GameObject previousRoom;
    private GameObject currentRoom;

    private int roomIndex = 0;
    private int spawnPointIndex = 0;
    private Vector3 roomPlayerSpawnpoint;

    private bool forceIntersection = false;

    private void OnEnable()
    {
        GlobalEvents.OnPlayerExitedRoom += Proceed_p1;
        GlobalEvents.OnScreenFadeOutFinished += Proceed_p2;
        GlobalEvents.OnPlayerLevelIncreased += (value) => forceIntersection = value;
    }

    private void OnDisable()
    {
        GlobalEvents.OnPlayerExitedRoom -= Proceed_p1;
        GlobalEvents.OnScreenFadeOutFinished -= Proceed_p2;
        GlobalEvents.OnPlayerLevelIncreased -= (value) => forceIntersection = value;
    }


    private void Proceed_p1()
    {
        if (PlayerTransform == null) return;

        PlayerTransform.gameObject.SetActive(false);

        // Затенить экран
        FadeOut();
    }

    private void Proceed_p2 () {
        SpawnNewRoom();
        
        if (currentRoom.TryGetComponent(out IGeneralRoomControl controls))
        {
            roomPlayerSpawnpoint = controls.PlayerSpawnPoint.position;
            
            if (forceIntersection)
                controls.SpawnUpgrade();
        } 
        

        PlayerTransform.position = roomPlayerSpawnpoint;
        
        Destroy(previousRoom);

        PlayerTransform.gameObject.SetActive(true);

        IEnumerator _()
        {
            yield return new WaitForSeconds(.5f);
            FadeIn();
        }

        StartCoroutine(_());

        GlobalEvents.Send_PlayerEnteredLevel();
        GlobalEvents.Send_OnPlayerLevelIncreased(false);
    }

    private GameObject GetRandomRoom()
    {
        return Rooms[Random.Range(0, Rooms.Length)];
    }

    private GameObject GetRandomIntersection()
    {
        return Intersections[Random.Range(0, Intersections.Length)];
    }

    private void SpawnNewRoom()
    {
        previousRoom = currentRoom;

        GameObject roomToSpawn;

        if (forceIntersection)
        {
            roomToSpawn = GetRandomIntersection();
        }
        else
        {
            roomToSpawn = (roomIndex % 6 == 5) ? GetRandomIntersection() : GetRandomRoom();
        }

        spawnPointIndex = spawnPointIndex == 0 ? 1 : 0;
        currentRoom = Instantiate(roomToSpawn, SpawnPoints[spawnPointIndex].position, Quaternion.identity);
        roomIndex++;
    }

    private void FadeIn()
    {
        GlobalEvents.Send_OnScreenFadeInStarted();
    }

    private void FadeOut()
    {
        GlobalEvents.Send_OnScreenFadeOutStarted();
    }

    public void ResetPlayerPosition()
    {
        PlayerTransform.position = roomPlayerSpawnpoint;
    }
}
