using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
public class Spawner : NetworkBehaviour
{
    public float spawnTime = 5f;        // The amount of time between each spawn.
    public float spawnDelay = 3f;       // The amount of time before spawning starts.
    public GameObject[] enemies;        // Array of enemy prefabs.


    public override void OnStartServer()
    {
        // Start calling the Spawn function repeatedly after a delay .
        InvokeRepeating("Spawn", spawnDelay, spawnTime);
    }


    void Spawn()
    {
        CmdSpawn();

    }

    [Command]
    void CmdSpawn()
    {
        // Instantiate a random enemy.
        int enemyIndex = Random.Range(0, enemies.Length);

        var creep = (GameObject)Instantiate(enemies[enemyIndex], transform.position, transform.rotation);
        NetworkServer.Spawn(creep);
        RpcSpawnParticles();
    }

    [ClientRpc]
    void RpcSpawnParticles()
    {
        // Play the spawning effect from all of the particle systems.
        foreach (ParticleSystem p in GetComponentsInChildren<ParticleSystem>())
        {
            p.Play();
        }
    }
}
