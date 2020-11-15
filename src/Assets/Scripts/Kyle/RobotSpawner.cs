using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class RobotSpawner : MonoBehaviour
{

    [System.Serializable]
    public struct SpawnableEnemy
    {
        public int id;
        public GameObject Enemy;
        public float AfterGlobalTime;
        public float AfterLastSpawn;
        public float DisableAfter;
        public int Quantity;
    }

    public Transform SpawnLocation;
    public Vector3 SpawnableArea = new Vector3(1, 0, 1);
    public List<SpawnableEnemy> spawnableEnemies;
    public List<Transform> InitialAvailableTargets = new List<Transform>();


    protected List<SpawnableEnemy> activelySpawning = new List<SpawnableEnemy>();
    protected Dictionary<int, float> spawnTracker = new Dictionary<int, float>();

    public float TotalCounter = 0;
    public float RecheckSpawnableAfter = 10;
    public float RecheckCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (SpawnLocation == null)
        {
            this.SpawnLocation = this.transform;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Time.fixedTime >= this.RecheckCounter + this.RecheckSpawnableAfter)
        {
            this.RecheckSpawnable();
            this.RecheckCounter = Time.fixedTime;
        }


        // If we are actively popping them, check how long since we last spawned
        foreach(SpawnableEnemy enemy in activelySpawning)
        {
            // Check if it is in the spawn tracker
            if (spawnTracker.ContainsKey(enemy.id))
            {
                // We have already spawned this enemy once, so check the last spawn
                if (Time.fixedTime >= spawnTracker[enemy.id] + enemy.AfterLastSpawn)
                {
                    this.SpawnRobot(enemy.Enemy, enemy.Quantity);
                    spawnTracker[enemy.id] = Time.fixedTime;
                }
            } else {
                this.SpawnRobot(enemy.Enemy, enemy.Quantity);
                this.spawnTracker[enemy.id] = Time.fixedTime;
            }
        }

    }

    public void RecheckSpawnable()
    {
        // Maintain a list of mobs we are actively popping
        foreach (SpawnableEnemy availableEnemy in spawnableEnemies)
        {
            if (Time.fixedTime >= availableEnemy.AfterGlobalTime && !this.isDisabled(availableEnemy))
            {
                activelySpawning.Add(availableEnemy);
            }

            if (this.isDisabled(availableEnemy) && activelySpawning.Contains(availableEnemy))
            {
                activelySpawning.Remove(availableEnemy);

                if (spawnTracker.ContainsKey(availableEnemy.id))
                {
                    spawnTracker.Remove(availableEnemy.id);
                }
            }
        }
    }

    public bool isDisabled(SpawnableEnemy enemy)
    {
        return Time.fixedTime >= enemy.DisableAfter && enemy.DisableAfter != 0;
    }

    public void SpawnRobot(GameObject robot, int quantity = 1)
    {
        for(int i = 0; i<quantity; i++)
        {
            // if it is, and the last spawn was > the after time, spawn
            // if not, what is the total time passed
            GameObject newEnemy = GameObject.Instantiate(robot);
            newEnemy.transform.parent = this.transform;

            Vector3 spawnPosition = new Vector3(
                SpawnLocation.position.x + Random.Range(-1 * (SpawnableArea.x / 2), SpawnableArea.x / 2),
                SpawnLocation.position.y + Random.Range(-1 * (SpawnableArea.y / 2), SpawnableArea.y / 2),
                SpawnLocation.position.z + Random.Range(-1 * (SpawnableArea.z / 2), SpawnableArea.z / 2)
            );

            Transform target = InitialAvailableTargets[Random.Range(0, InitialAvailableTargets.Count - 1)];

            RobotScript script = newEnemy.GetComponent<RobotScript>();
            script.MoveTarget = target;
            script.Load(spawnPosition);
        }
    }
}
