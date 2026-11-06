
using UnityEngine;
using System.Collections.Generic;

public class TrackSpawner : MonoBehaviour
{
    [Header("Track Settings")]
    public GameObject trackTilePrefab;
    public Transform player;
    public float tileLength = 30f;
    public int tilesOnScreen = 4;

    [Header("Obstacle Prefabs")]
    public GameObject flyingBookPrefab;
    public GameObject fallenBookshelfPrefab;
    public GameObject magicalBarrierPrefab;

    [Header("Power-Up Prefabs")]
    public GameObject magicShieldPrefab;
    public GameObject speedBoostPrefab;
    public GameObject orbMagnetPrefab;

    [Header("Power-Up Settings")]
    [Range(0f, 1f)]
    public float powerUpSpawnChance = 0.4f;

    [Header("Obstacle Difficulty")]
    [Range(1, 3)]
    public int startingObstacleCount = 2;

    [Range(1, 3)]
    public int maximumObstacleCount = 3;

    public float difficultyIncreaseDistance = 150f;

    [Header("Obstacle Spacing")]
    public float firstObstacleMinZ = 7f;
    public float firstObstacleMaxZ = 11f;
    public float obstacleSpacing = 9f;

    private float spawnZ = 0f;
    private List<GameObject> activeTiles = new List<GameObject>();

    void Start()
    {
        for (int i = 0; i < tilesOnScreen; i++)
        {
            SpawnTile();
        }
    }

    void Update()
    {
        if (player == null) return;

        if (player.position.z >
            spawnZ - (tilesOnScreen - 2) * tileLength)
        {
            SpawnTile();
            DeleteOldTile();
        }
    }

    void SpawnTile()
    {
        float tileStartZ = spawnZ;

        GameObject tile = Instantiate(
            trackTilePrefab,
            new Vector3(0, 0, spawnZ + tileLength / 2f),
            Quaternion.identity
        );

        activeTiles.Add(tile);

        SpawnObstacles(tile.transform, tileStartZ);
        SpawnPowerUp(tile.transform, tileStartZ);

        spawnZ += tileLength;
    }

    void SpawnObstacles(Transform tile, float tileStartZ)
    {
        int obstacleCount = startingObstacleCount;

        int extraObstacles = Mathf.FloorToInt(
            player.position.z / difficultyIncreaseDistance
        );

        obstacleCount = Mathf.Min(
            startingObstacleCount + extraObstacles,
            maximumObstacleCount
        );

        obstacleCount = Mathf.Clamp(obstacleCount, 1, 3);

        // Shuffle the three lanes.
        List<int> lanes = new List<int> { 0, 1, 2 };

        for (int i = 0; i < lanes.Count; i++)
        {
            int randomIndex = Random.Range(i, lanes.Count);
            int temp = lanes[i];
            lanes[i] = lanes[randomIndex];
            lanes[randomIndex] = temp;
        }

        float obstacleZ = tileStartZ +
            Random.Range(firstObstacleMinZ, firstObstacleMaxZ);

        for (int i = 0; i < obstacleCount; i++)
        {
            SpawnObstacle(tile, lanes[i], obstacleZ);

            obstacleZ += obstacleSpacing;
        }
    }

    void SpawnObstacle(Transform tile, int lane, float obstacleZ)
    {
        int obstacleType = Random.Range(0, 3);

        float laneX = (lane - 1) * 3f;
        float obstacleY = 1.5f;

        GameObject selectedObstacle = null;

        if (obstacleType == 0)
        {
            selectedObstacle = flyingBookPrefab;
            obstacleY = 1.5f;
        }
        else if (obstacleType == 1)
        {
            selectedObstacle = fallenBookshelfPrefab;
            obstacleY = 1.75f;
        }
        else
        {
            selectedObstacle = magicalBarrierPrefab;
            obstacleY = 2.5f;
        }

        if (selectedObstacle == null) return;

        GameObject obstacle = Instantiate(
            selectedObstacle,
            new Vector3(laneX, obstacleY, obstacleZ),
            Quaternion.identity
        );

        obstacle.transform.SetParent(tile, true);
    }

    void SpawnPowerUp(Transform tile, float tileStartZ)
    {
        if (Random.value > powerUpSpawnChance)
            return;

        int powerUpType = Random.Range(0, 3);
        int lane = Random.Range(0, 3);

        float laneX = (lane - 1) * 3f;
        float powerUpZ = tileStartZ + Random.Range(18f, 25f);

        GameObject selectedPowerUp = null;

        if (powerUpType == 0)
            selectedPowerUp = magicShieldPrefab;
        else if (powerUpType == 1)
            selectedPowerUp = speedBoostPrefab;
        else
            selectedPowerUp = orbMagnetPrefab;

        if (selectedPowerUp == null) return;

        GameObject powerUp = Instantiate(
            selectedPowerUp,
            new Vector3(laneX, 1.5f, powerUpZ),
            Quaternion.identity
        );

        powerUp.transform.SetParent(tile, true);
    }

    void DeleteOldTile()
    {
        if (activeTiles.Count > tilesOnScreen)
        {
            Destroy(activeTiles[0]);
            activeTiles.RemoveAt(0);
        }
    }
}
