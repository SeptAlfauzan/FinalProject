using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerController : MonoBehaviour
{
    [SerializeField] GameObject terrain;
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] ParticleSystem smokeParticle;
    
    [Range(1, 10)]
    [SerializeField] int enemySpawnedUnitRange;
    [SerializeField] int currentEnemySpawned;
    [SerializeField] int maxEnemiesSpawnAtSameTIme;
    [SerializeField] bool hasLimitSpawn = false;
    [SerializeField] bool spawnOnlyonNight = false;
    [SerializeField] SceneInfo sceneInfo;
    private Dictionary<string, Vector3> boundariesPos;

    private void Start() {
        boundariesPos = GetBoundaryPositions(terrain);
        
    }

    private void Update() {
        if(sceneInfo.dayTime < 18 && spawnOnlyonNight) return;///when time is not night yet, dont spawn enemies;
        
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemies");
        if(enemies.Length < maxEnemiesSpawnAtSameTIme){
            // stop spawn enemy when reach the limit
            if(hasLimitSpawn && currentEnemySpawned >= enemySpawnedUnitRange) return;

            currentEnemySpawned += maxEnemiesSpawnAtSameTIme;
            float maxX = boundariesPos["top right"].x;
            float minX = boundariesPos["top left"].x;
            float maxZ = boundariesPos["top left"].z;
            float minZ = boundariesPos["bottom left"].z;

            Vector3 randomPositionSpawn = new Vector3(Random.Range(minX, maxX), 1, Random.Range(minZ, maxZ));
            // init smoke particle then destroy it
            ParticleSystem smoke = Instantiate(smokeParticle, randomPositionSpawn, Quaternion.Euler(Vector3.zero));
            smoke.Play();
            Destroy(smoke);

            Instantiate(enemyPrefab, randomPositionSpawn, Quaternion.Euler(Vector3.zero));
        }
    }

    private Dictionary<string, Vector3> GetBoundaryPositions(GameObject gameObject){
        Matrix4x4 localToWorld = gameObject.transform.localToWorldMatrix; //Create a Matrix in the planes LocalSpace
        Vector3 topLeftPos = localToWorld.MultiplyPoint3x4(new Vector3(-5, 0, 5)); //Get the Top Left Position in WorldSpace
        Vector3 topRightPos = localToWorld.MultiplyPoint3x4(new Vector3(5, 0, 5)); //Get the Top Right Position in WorldSpace
        Vector3 bottomLeftPos = localToWorld.MultiplyPoint3x4(new Vector3(-5, 0, -5)); //Get the Bottom Left Position in WorldSpace
        Vector3 bottomRightPos = localToWorld.MultiplyPoint3x4(new Vector3(5, 0, -5)); 

        return new Dictionary<string, Vector3>(){
            {"top left", topLeftPos},
            {"top right", topRightPos},
            {"bottom left", bottomLeftPos},
            {"bottom right", bottomRightPos},
        };
    }
}
