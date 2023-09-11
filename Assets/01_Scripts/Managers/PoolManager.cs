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
        // Ǯ���� ������Ʈ�� �����뵵�� ���ӿ�����Ʈ
        _root = new GameObject("@Pool_Root");
        Object.DontDestroyOnLoad(_root);

        // ���÷����� �̿��Ͽ� Enum���� ����ŭ �迭�� �ʱ�ȭ
        int count = System.Enum.GetValues(typeof(Define.PoolObject)).Length;
        prefabs = new GameObject[count];

        // ������ �ҷ�����
        for(int i = 0; i < prefabs.Length; i++)
        {
            string prefabName = System.Enum.GetName(typeof(Define.PoolObject), i);
            prefabs[i] = Resources.Load<GameObject>($"PoolObject/{prefabName}");
        }

        // ������Ʈ ����
        CreatePool();
    }

    private void CreatePool()
    {
        for (int i = 0; i < prefabs.Length; i++)
        {
            // ������Ʈ�� �����뵵�� ������Ʈ ����
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
        // Active�� �����ִ� ������Ʈ�� ������ ���� ����
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
