using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ZombieSpawnerController : MonoBehaviour
{
    public int initialZombiePerWave = 5;
    public int currentZombiePerWave;
    public int maxZombiePerWave = 10;
    public float spawnDelay = 0.5f;

    public int currentWave = 0;
    public float waveCooldown = 10f;

    private bool inCoolDown;
    public float coolDownCounter = 0;
    public GameObject zombiePrefab;

    public List<ZombieScript> currentZombieALive;

    public TextMeshProUGUI waveOverGUI;
    public TextMeshProUGUI coolDownCounterGUI;

    public Transform player;
    public float spawnDistance = 10f;
    public float requiredMoveDistance = 100f;
    private Vector3 lastPlayerPosition;

    private void Start()
    {
        currentZombiePerWave = Random.Range(initialZombiePerWave, maxZombiePerWave + 1);
        lastPlayerPosition = player.position;
        StartNextWave();
    }

    private void StartNextWave()
    {
        currentZombieALive.Clear();
        currentWave++;
        StartCoroutine(SpawnWave());
    }

    private IEnumerator SpawnWave()
    {
        for (int i = 0; i < currentZombiePerWave; i++)
        {
            Vector3 spawnPosition = GetSpawnPosition();
            var zombie = Instantiate(zombiePrefab, spawnPosition, Quaternion.identity);
            var enemyScript = zombie.GetComponent<ZombieScript>();

            currentZombieALive.Add(enemyScript);
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    private Vector3 GetSpawnPosition()
    {
        Vector3 direction = (player.position - lastPlayerPosition).normalized;
        Vector3 spawnPosition = player.position + direction * spawnDistance;
        spawnPosition += new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)); // Add some randomness
        return spawnPosition;
    }

    private void Update()
    {
        CheckPlayerMovement();

        var zombieToRemove = new List<ZombieScript>();

        foreach (var zombie in currentZombieALive)
        {
            if (zombie.isDead)
            {
                zombieToRemove.Add(zombie);
            }
        }

        foreach (var zombie in zombieToRemove)
        {
            currentZombieALive.Remove(zombie);
        }
        zombieToRemove.Clear();

        if (currentZombieALive.Count == 0 && !inCoolDown)
        {
            waveOverGUI.gameObject.SetActive(true);
            StartCoroutine(WaveCoolDown());
        }

        if (inCoolDown)
        {
            coolDownCounter -= Time.deltaTime;
        }
        else
        {
            coolDownCounter = waveCooldown;
        }

        coolDownCounterGUI.text = coolDownCounter.ToString("F0");
    }

    private void CheckPlayerMovement()
    {
        if (currentZombieALive.Count == 0 && Vector3.Distance(player.position, lastPlayerPosition) > requiredMoveDistance)
        {
            lastPlayerPosition = player.position;
            waveOverGUI.gameObject.SetActive(false);
            StartNextWave();
        }
    }

    private IEnumerator WaveCoolDown()
    {
        inCoolDown = true;
        yield return new WaitForSeconds(waveCooldown);
        inCoolDown = false;
        currentZombiePerWave = Random.Range(initialZombiePerWave, maxZombiePerWave + 1);
    }
}
