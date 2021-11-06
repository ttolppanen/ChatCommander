using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance;

    [SerializeField] private GameObject[] enemyPrefabs;
    public List<GameObject> enemiesOnField { get; private set; } = new List<GameObject>();
    public int spawnCount;
    public float spawnX;
    public float spawnY;
    public float waveTime;
    public Text waveText;
    public Text timeText;
    int wave = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        waveText.text = wave.ToString();
    }

    private void Update()
    {
        timeText.text = Mathf.Round(Time.time).ToString();
        if ((wave + 1) * waveTime < Time.time)
        {
            StartNewWave();
        }
    }

    private void StartNewWave()
    {
        wave++;
        waveText.text = wave.ToString();
        MoneySystem.ins.GiveMoney(1);
        int enemiesToSpawn = Random.Range((int)(wave / 5) + 1, spawnCount + (int)(wave / 5));
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Vector2 spawnPoint = (Vector2)transform.position + new Vector2(Random.Range(-1f, 1f) * spawnX, Random.Range(-1f, 1f) * spawnY);
            GameObject enemyInstance = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)], spawnPoint, Quaternion.identity);
            enemiesOnField.Add(enemyInstance);
        }
    }
}
