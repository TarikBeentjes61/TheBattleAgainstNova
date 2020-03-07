using UnityEngine;
using Random = System.Random;

public class Spawner : MonoBehaviour {
    public GameObject SpawnObject;

    public float TimeBetweenSpawns;
    public int MaxSpawns;

    private Vector3[] _spawnPositions;
    private float _timer;
    private int _spawns;

    //Zoek alle posities die in het gameobject zitten door te loopen door de child objects
    private void Start() {
        Transform[] positions = transform.GetComponentsInChildren<Transform>();
        _spawnPositions = new Vector3[positions.Length];
        for (int i = 0; i < positions.Length; i++) {
            _spawnPositions[i] = positions[i].position;
        }
    }
    //Spawn het gegeven object na een gegeven tijd en kies dan een random positie.
    //Als de Maxposities is aangegeven stop dan na zoveel spawns
    void Update() {
        _timer += Time.deltaTime;
        if (_timer > TimeBetweenSpawns) {
            int random = new Random().Next(0, _spawnPositions.Length);
            Instantiate(SpawnObject, _spawnPositions[random], Quaternion.identity);

            _timer = 0;
            if (MaxSpawns != 0) {
                _spawns++;
                if (_spawns == MaxSpawns) {
                    Destroy(gameObject);
                }
            }
            
        }
    }
}
