using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

        spawnTime = 1.0f - (GameManager.Instance.level /GameManager.Instance.maxLevel);
    }
    public void Update()
    {
        if (!GameManager.Instance.GameStart) return;
        
        spawnTime -= Time.deltaTime;
        if(spawnTime <= 0.0f)
        {
            Spawn();
            if (GameManager.Instance.level == 0)
            {
                spawnTime = 1.0f;
            }
                
            else
            {
                spawnTime = 1.0f - ((float)GameManager.Instance.level / (float)GameManager.Instance.maxLevel);
            }
            
        }
    }
    
    private void Spawn()
    {
        int randPosIndex = Random.Range(0, _spawnPoints.Length);
        GameObject go = Managers.Pool.Get("Monster");
        int spawnLevel = GameManager.Instance.level;
        if (GameManager.Instance.level >= 3)
            spawnLevel = 3;
        go.GetComponent<MonsterController>().SpawnInit(spawnLevel);
        go.transform.position = _spawnPoints[randPosIndex].position;
    }
}
