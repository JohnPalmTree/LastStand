using UnityEngine;

public class WaveSystem : MonoBehaviour
{

    public enum WaveStates { Spawn, Active, Intermission, GameOver };
    public int roundNumber = 0;
    
    [System.Serializable]
    public struct WaveData {
        public int ZombieCount;
        public int HP;
        public int Damage;
        public int Speed;
        public float spawnRate;
    };

    public WaveData[] waves = new WaveData[] {
        new WaveData[] {ZombieCount = 6, HP = 60, Damage = 15, Speed = 12, SpawnRate = 5.0f};
        new WaveData[] {ZombieCount = 8, HP = 80, Damage = 20, Speed = 14, SpawnRate = 4.0f};
        new WaveData[] {ZombieCount = 10, HP = 100, Damage = 25, Speed = 12, SpawnRate = 3.0f};
    };

    public WaveData incrementData = new WaveData {ZombieCount = 3, HP = 25, Damage = 10, Speed = 1, SpawnRate = 0.25f};

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentState = WaveStates.Spawn;
        // spawn()
    }

    // Update is called once per frame
    void Update()
    {
  
    }

    WaveData GetWaveData(int waveNum) {
        if (waveNum <= waves.length) {
            return waves[number - 1];
        } else {
            int incrementsApplied = (waveNumber - 1) / 3;

            return new WaveData {
                ZombieCount = waves[waves.length - 1].ZombieCount + (incrementData.ZombieCount * incrementsApplied);
                HP = waves[waves.length - 1].HP + (incrementData.HP * incrementsApplied);
                Damage = waves[waves.length - 1].Damage + (incrementData.Damage * incrementsApplied);
                Speed = waves[waves.length - 1].Speed + (incrementData.Speed * incrementsApplied);

                if (waves[waves.length - 1].SpawnRate >= 1.25) {
                    SpawnRate = waves[waves.length - 1].SpawnRate - (incrementData.SpawnRate * incrementsApplied); // zombies spawn faster as time goes on - but the absolute fastest is 1 zombie per second.
                };
            };
        }
    }

    /*
    public void startWave(int waveNum) {
        currentWave = waveNum;
        data = GetWaveData(waveNum);
        zombiesQueued = data.ZombieCount;
        zombiesAlive = data.ZombieCount;

        Debug.Log("Starting wave " + waveNum + " - Zombies: " + data.ZombieCount);

    };

    IEnumerator SpawnWave(WaveData data) {
        while (zombiesQueued > 0) {
            spawnZombie();
            zombiesQueued--;
            yield return new WaitForSeconds(SpawnRate)
        }
    };

    void SpawnZombie(WaveData data) {
        pick a node
        create the zombie, transform to position
        apply wave states (hp/speed/dmg)
    };

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
    };
    */
