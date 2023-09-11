using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class PoolManager
{
    private GameObject[] prefabs;

    private Dictionary<string,List<GameObject>> pools = new Dictionary<string,List<GameObject>>();

    private int PoolSize = 500;

    private GameObject _root;
    public void Init()
    {
        // 풀링한 오브젝트들 정리용도의 게임오브젝트
        _root = new GameObject("@Pool_Root");
        Object.DontDestroyOnLoad(_root);

        // 리플렉션을 이용하여 Enum값의 수만큼 배열을 초기화
        int count = System.Enum.GetValues(typeof(Define.PoolObject)).Length;
        prefabs = new GameObject[count];

        // 프리팹 불러오기
        for(int i = 0; i < prefabs.Length; i++)
        {
            string prefabName = System.Enum.GetName(typeof(Define.PoolObject), i);
            prefabs[i] = Resources.Load<GameObject>($"PoolObject/{prefabName}");
        }

        // 오브젝트 생성
        CreatePool();
    }

    private void CreatePool()
    {
        for (int i = 0; i < prefabs.Length; i++)
        {
            // 오브젝트들 정리용도의 오브젝트 생성
            GameObject temp = new GameObject(prefabs[i].name + "s");
            temp.transform.parent = _root.transform;

            List<GameObject> list = new List<GameObject>();

            for (int j = 0; j < PoolSize; j++)
            {
                GameObject go = Object.Instantiate(prefabs[i], temp.transform);
                go.SetActive(false);
                list.Add(go);
            }
            pools[prefabs[i].name] = list;
        }
    }

    public GameObject Get(string key)
    {
        if (pools.ContainsKey(key) == false)
        {
            Debug.Log("PoolManager Get() Index Error");
            return null;
        }

        foreach(GameObject go in pools[key]) 
        { 
            if(go.activeSelf == false)
            {
                go.SetActive(true);
                return go;
            }
        }
        int index = 0;
        // Active가 꺼져있는 오브젝트가 없으면 새로 생성
        for (int i =0; i < prefabs.Length; i++)
        {
            if (prefabs[i].name == key)
                index = i;
        }
        
        GameObject temp = Object.Instantiate(prefabs[index], _root.transform.Find(prefabs[index].name + "s"));    
        temp.SetActive(true);
        pools[key].Add(temp);
        return temp;
    }
    public void ClearPool()
    {
        foreach(List<GameObject> pool in pools.Values)
        {
            foreach(GameObject go in pool)
            {
                if (go.activeSelf == true)
                {
                    go.SetActive(false);
                }   
            }
        }
    }
}
