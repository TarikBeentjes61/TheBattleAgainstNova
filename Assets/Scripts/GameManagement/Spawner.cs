using System.Collections;
using UnityEngine;
using Random = System.Random;

public class Spawner : MonoBehaviour {
    public GameObject SpawnObject;

    public float TimeBetweenSpawns = 4;
    public bool SpawnTimeMultiplier = true;
    [Range(1,0)]
    public float SpawnTimeMultiplierOverTime = 0.5f;
    public float TimeTillCap = 60f;
    public int MaxSpawns;

    private Vector3[] _spawnPositions;
    private float _timer;
    private int _spawns;
    private bool _firstSpawn;

    //Zoek alle posities die in het gameobject zitten door te loopen door de child objects
    private void Start() {
        Transform[] positions = transform.GetComponentsInChildren<Transform>();
        _spawnPositions = new Vector3[positions.Length];
        for (int i = 0; i < positions.Length; i++) {
            _spawnPositions[i] = positions[i].position;
        }

        if (SpawnTimeMultiplier) {
            StartCoroutine(StartSpawnTimeMultiplier());
        }
    }
    //Spawn het gegeven object na een gegeven tijd en kies dan een random positie.
    //Als de Maxposities is aangegeven stop dan na zoveel spawns
    void Update() {
        _timer += Time.deltaTime;
        if (_timer > TimeBetweenSpawns) {
            int random = new Random().Next(0, _spawnPositions.Length);
            Instantiate(SpawnObject, _spawnPositions[random], Quaternion.identity);
            _firstSpawn = true;
            _timer = 0;
            if (MaxSpawns != 0) {
                _spawns++;
                if (_spawns == MaxSpawns) {
                    Destroy(gameObject);
                }
            }
            
        }
    }

    private IEnumerator StartSpawnTimeMultiplier() {
        while (TimeBetweenSpawns > TimeBetweenSpawns * SpawnTimeMultiplierOverTime) {
            if (_firstSpawn) {

                TimeBetweenSpawns = Mathf.Lerp(TimeBetweenSpawns, TimeBetweenSpawns * SpawnTimeMultiplierOverTime,
                    Time.deltaTime / TimeTillCap);
            }
            yield return null;
        }
    }
}
