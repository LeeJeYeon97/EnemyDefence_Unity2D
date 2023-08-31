using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class Spawner : MonoBehaviour
{
    private Transform[] _spawnPoints;
    public float spawnTime;
    
    void Start()
    {
        Transform spawnPointGroup = GameManager.Instance.Player.transform.Find("SpawnPointGroup");

        _spawnPoints = new Transform[spawnPointGroup.childCount];
        for (int i = 0; i < spawnPointGroup.childCount; i++)
        {
            _spawnPoints[i] = spawnPointGroup.GetChild(i).transform;
        }
        
        spawnTime = 1.0f;
        StartCoroutine(CoSpawn());
    }
    IEnumerator CoSpawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnTime);
            Spawn();
        }
    }
    private void Spawn()
    {
        int randPosIndex = Random.Range(0, _spawnPoints.Length);
        GameObject go = Managers.Pool.Get("Monster");
        go.GetComponent<MonsterController>().SpawnInit(GameManager.Instance.level);
        go.transform.position = _spawnPoints[randPosIndex].position;
    }
}
