using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager
{
    private GameObject[] prefabs;

    private List<GameObject>[] pools;

    private int PoolSize = 100;

    private GameObject _root;
    public void Init()
    {
        _root = new GameObject("@Pool_Root");
        Object.DontDestroyOnLoad(_root);

        // 리플렉션을 이용하여 Enum값의 수만큼 배열을 초기화
        int count = System.Enum.GetValues(typeof(Define.MonsterName)).Length;
        prefabs = new GameObject[count];
        for(int i =0; i<prefabs.Length; i++)
        {
            string prefabName = System.Enum.GetName(typeof(Define.MonsterName), i);
            prefabs[i] = Resources.Load<GameObject>($"Monster/{prefabName}");
        }
        pools = new List<GameObject>[prefabs.Length];

        CreatePool();
    }

    private void CreatePool()
    {
        for (int i = 0; i < pools.Length; i++)
        {
            pools[i] = new List<GameObject>();

            GameObject temp = new GameObject(prefabs[i].name + "s");
            temp.transform.parent = _root.transform;

            for (int j = 0; j < PoolSize; j++)
            {
                GameObject go = Object.Instantiate(prefabs[i], temp.transform);
                go.SetActive(false);
                pools[i].Add(go);
            }
        }
    }

    public GameObject Get(int index)
    {
        if (index > pools.Length)
        {
            Debug.Log("PoolManager Get() Index Error");
            return null;
        }

        foreach(GameObject go in pools[index]) 
        { 
            if(go.activeSelf == false)
            {
                go.SetActive(true);
                return go;
            }
        }
        
        // Active가 꺼져있는 오브젝트가 없으면 새로 생성
        GameObject temp = Object.Instantiate(prefabs[index], _root.transform.Find(prefabs[index].name + "s"));
        temp.SetActive(true);
        pools[index].Add(temp);
        return temp;
    }
}
