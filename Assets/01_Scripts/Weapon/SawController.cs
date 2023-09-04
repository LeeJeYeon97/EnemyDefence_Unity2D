using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class SawController : WeaponController
{

    public float RotationSpeed;
    public float sawRotationSpeed;

    private Transform[] saws;

    GameObject prefab;
    private void Awake()
    {
        Init();
    }
    void Update()
    {
        
        SawsRotation();
    }

    protected override void Init()
    {
        base.Init();

        _data = Managers.Data.GetData<WeaponData>(Enum.GetName(typeof(Define.WeaponType),Define.WeaponType.Saw));
        _damage = _data.Damages[Level - 1];

        RotationSpeed = 200.0f;
        sawRotationSpeed = 700.0f;
        saws = new Transform[_maxLevel];

        prefab = Resources.Load<GameObject>("Weapon/Saw");
        CreateSaws();
    }
    private void SawsRotation()
    {
        transform.Rotate(Vector3.back * RotationSpeed * Time.deltaTime);
        // Ä®³¯ È¸Àü
        for (int i = 0; i< Level; i++)
        {
            if (saws[i] == null) return;

            saws[i].Rotate(Vector3.forward * sawRotationSpeed * Time.deltaTime);
        }
    }
    private void CreateSaws()
    {
        foreach(Transform t in saws)
        {
            if (t == null) break;
            Destroy(t.gameObject);
        }

        for (int i = 0; i < Level; i++)
        {
            GameObject saw = Instantiate(prefab);

            saw.transform.parent = transform;
            Vector3 rotVec = Vector3.forward * 360 * i / Level;
            saw.transform.localRotation = Quaternion.Euler(rotVec);
            
            Vector3 posVec = saw.transform.up * 1.5f;
            saw.transform.localPosition = posVec;
            saws[i] = saw.transform;
        }
    } 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))
            collision.GetComponent<CreatureController>().OnDamge(_damage);
    }

    public override void LevelUp()
    {
        base.LevelUp();

        if (Level == _maxLevel)
            RotationSpeed = 1000.0f;

        CreateSaws();
    }
}
