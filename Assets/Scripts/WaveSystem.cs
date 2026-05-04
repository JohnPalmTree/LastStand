using UnityEngine;
using System.Collections;

public class WaveSystem : MonoBehaviour
{

    public enum WaveStates { Spawn, Active, Intermission, GameOver };
    public WaveStates currentState;
    public int roundNumber = 0;

    public Transform[] spawnNodes;
    public GameObject zombiePrefab;
    
    [System.Serializable]
    public struct WaveData {
        public int ZombieCount;
        public int HP;
        public int Damage;
        public int Speed;
        public float SpawnRate;
    };

    private int zombiesAlive = 0;
    private int zombiesQueued = 0;

    public WaveData[] waves = new WaveData[] {
        new WaveData { ZombieCount = 6, HP = 60, Damage = 15, Speed = 12, SpawnRate = 5.0f },
        new WaveData { ZombieCount = 8, HP = 80, Damage = 20, Speed = 14, SpawnRate = 4.0f },
        new WaveData { ZombieCount = 10, HP = 100, Damage = 25, Speed = 12, SpawnRate = 3.0f },
    };

    public WaveData incrementData = new WaveData {ZombieCount = 3, HP = 25, Damage = 10, Speed = 1, SpawnRate = 0.25f};

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentState = WaveStates.Spawn;
        startWave(roundNumber);
        //Debug.Log("test");
    }

    // Update is called once per frame
    void Update()
    {
  
    }

    public float getSpawnRate(int length, int incApplied, float incRate) {
        float SpawnRate = 1.0f;

        if (waves[length - 1].SpawnRate >= 1.25) {
            SpawnRate = waves[length - 1].SpawnRate - (incRate * incApplied); // zombies spawn faster as time goes on - but the absolute fastest is 1 zombie per second.
        };

        return SpawnRate;
    }

    WaveData GetWaveData(int waveNum) {
        if (waveNum < waves.Length) {
            return waves[waveNum];
        } else {
            int incrementsApplied = (waveNum) / 3;

            return new WaveData {
                ZombieCount = waves[waves.Length - 1].ZombieCount + (incrementData.ZombieCount * incrementsApplied),
                HP = waves[waves.Length - 1].HP + (incrementData.HP * incrementsApplied),
                Damage = waves[waves.Length - 1].Damage + (incrementData.Damage * incrementsApplied),
                Speed = waves[waves.Length - 1].Speed + (incrementData.Speed * incrementsApplied),
                SpawnRate = getSpawnRate(waves.Length, incrementsApplied, incrementData.SpawnRate),
            };
        }
    }

    public void startWave(int waveNum) {
        //Debug.Log("test");
        int currentWave = waveNum;
        WaveData data = GetWaveData(waveNum);
        zombiesQueued = data.ZombieCount;
        zombiesAlive = data.ZombieCount;

        Debug.Log("Starting wave " + waveNum + " - Zombies: " + data.ZombieCount + " / Queue: " + zombiesQueued);

        StartCoroutine(SpawnWave(data, zombiesQueued));
    }

    IEnumerator IntermissionWaitForWave() {
        Debug.Log("Waiting for wave...");
        currentState = WaveStates.Spawn;
        roundNumber++;
        yield return new WaitForSeconds(15.0f);
        startWave(roundNumber);
    }

    IEnumerator SpawnWave(WaveData data, int zombiesQueued) {
        //Debug.Log("test1.1");

        while (zombiesQueued > 0) {
            //Debug.Log("test2");
            zombiesQueued--;
            this.zombiesQueued--;
            SpawnZombie(data, zombiesQueued);

            yield return new WaitForSeconds(data.SpawnRate);
        }
    }

    void SpawnZombie(WaveData data, int zombiesQueued) {
        Transform node = spawnNodes[Random.Range(0, spawnNodes.Length)];

        GameObject zombie = Instantiate(zombiePrefab, node.position, node.rotation);

        ZombieHealth hp = zombie.GetComponent<ZombieHealth>();
        ZombieAI ai = zombie.GetComponent<ZombieAI>();

        if (hp != null) {
            hp.health = data.HP;
        }

        if (ai != null) {
            ai.damage = data.Damage;
            ai.agent.speed = data.Speed * 0.3f;
        }

        Debug.Log("Zombie spawned! || Zombies Remaining: " + zombiesQueued);
    }

    public void OnZombieDied() {
        zombiesAlive--;
        Debug.Log("Zombies remaining: " + zombiesAlive + " | Zombies queued: " + zombiesQueued);

        if (zombiesAlive <= 0 && zombiesQueued <= 0) {
            StartIntermission();
        }
    }

    public void StartIntermission() {
        currentState = WaveStates.Intermission;
        Debug.Log("Wave complete! Starting intermission...");

        StartCoroutine(IntermissionWaitForWave());
    }
/*
    public void onZombieDied() {
        zombiesAlive--;
        Debug.Log("Zombies Remaining: " + zombiesAlive);

        if (zombiesAlive == 0 && zombiesQueued == 0) {
            StartIntermission();
        }
    }

    public void StartIntermission() {
        currentState = waveStates.Intermission;
        waveNum++;
        wait(15);
        startWave(waveNum);
    }
    */
}

