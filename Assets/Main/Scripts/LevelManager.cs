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

    private void OnEnable()
    {
        GlobalEvents.OnPlayerExitedRoom += Proceed_p1;
        GlobalEvents.OnScreenFadeOutFinished += Proceed_p2;
    }

    private void OnDisable()
    {
        GlobalEvents.OnPlayerExitedRoom -= Proceed_p1;
        GlobalEvents.OnScreenFadeOutFinished -= Proceed_p2;
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
        
        if (currentRoom.TryGetComponent(out RoomControls controls))
        {
            roomPlayerSpawnpoint = controls.playerSpawnPoint.position;
        } else if (currentRoom.TryGetComponent(out IntersectionControls controls2))
        {
            roomPlayerSpawnpoint = controls2.playerSpawnPoint.position;
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

        GameObject roomToSpawn = (roomIndex % 2 == 1) ? GetRandomIntersection() : GetRandomRoom();
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
