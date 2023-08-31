using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers s_instance;
    static Managers Instance { get { return s_instance; } }

    PoolManager _pool = new PoolManager();
    UIManager _ui = new UIManager();
    DataManager _data = new DataManager();
    public static PoolManager Pool { get { return s_instance._pool; } }
    public static UIManager UI { get { return s_instance._ui; } }
    public static DataManager Data { get { return s_instance._data; } }

    private void Awake()
    {
        Init();    
    }
    
    private static void Init()
    {
        if(s_instance == null)
        {
            GameObject go = GameObject.Find("@Managers");
            if(go == null)
            {
                go = new GameObject { name = "@Managers" };
                go.AddComponent<Managers>();
            }
            DontDestroyOnLoad(go);
            s_instance = go.GetComponent<Managers>();

            s_instance._pool.Init();
            s_instance._data.Init();
        }
    }
}
