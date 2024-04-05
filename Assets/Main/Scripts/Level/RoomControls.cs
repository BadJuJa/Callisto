using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomControls : MonoBehaviour {
    public GameObject enemyPrefab;
    public Transform playerSpawnPoint;
    public List<Transform> spawnPoints;
    public int enemiesToSpawn = 5;
    public float timeBetweenSpawns = 2f;
    public Transform Enemies;

    public BoxCollider CameraConfinderCollider;

    private Camera playerCamera;

    private void Awake()
    {
        if (CameraConfinderCollider != null)
        {
            GlobalEvents.OnPlayerEnteredLevel += () => GlobalEvents.Send_OnNewCameraConfinderColliderAvailable(CameraConfinderCollider);
        }
        
    }
    private void OnEnable()
    {
        
    }
    private void OnDisable()
    {
        GlobalEvents.OnPlayerEnteredLevel -= () => GlobalEvents.Send_OnNewCameraConfinderColliderAvailable(CameraConfinderCollider);
    }

    private void Start()
    {
        playerCamera = Camera.main;
        StartCoroutine(WaitUntilSpawnEnemies(5f));
    }

    private IEnumerator SpawnEnemies()
    {
        List<Transform> validSpawnPoints = new List<Transform>(spawnPoints);

        for (int i = 0; i < enemiesToSpawn; i++)
        {
            int index = Random.Range(0, validSpawnPoints.Count);
            Transform spawnPoint = validSpawnPoints[index];

            if (!IsInView(spawnPoint.position))
            {
                Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation, Enemies);
            }

            yield return new WaitForSeconds(timeBetweenSpawns);
        }
        StartCoroutine(CheckEnemyCount());
    }

    bool IsInView(Vector3 position)
    {
        Vector3 viewportPoint = playerCamera.WorldToViewportPoint(position);
        return viewportPoint.z > 0 && viewportPoint.x > 0 && viewportPoint.x < 1 && viewportPoint.y > 0 && viewportPoint.y < 1;
    }

    private IEnumerator WaitUntilSpawnEnemies(float value)
    {
        yield return new WaitForSeconds(value);
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator CheckEnemyCount()
    {
        while (true)
        {
            if (Enemies.childCount < 1)
            {
                GlobalEvents.Send_OnPlayerClearedTheRoom();
                break;
            }
            yield return new WaitForSeconds(.5f);
        }
    }
}
