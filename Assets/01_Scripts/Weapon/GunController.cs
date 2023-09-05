using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : WeaponController
{
    public float scanRange;
    public LayerMask monsterLayer;
    public Transform nearestTarget;
    public Collider2D[] targets;

    void Start()
    {
        Init();
    }

    protected override void Init()
    {
        base.Init();
        scanRange = 5.0f;
        monsterLayer = LayerMask.GetMask("Monster");
        _data = Managers.Data.GetData<WeaponData>(Enum.GetName(typeof(Define.WeaponType), Define.WeaponType.Gun));
        _damage = _data.Damages[Level - 1];
        _attackDelay = _data.attackDelay[Level - 1];
    }
    private void Update()
    {
        ScanRange();
        GetNearestTarget();

        Fire();
    }
    private void ScanRange()
    {
        targets = Physics2D.OverlapCircleAll(transform.position, scanRange, monsterLayer);
    }
    private void GetNearestTarget()
    {
        if (targets.Length == 0) return;

        float maxDis = scanRange;

        foreach(Collider2D coll in targets)
        {
            Vector3 myPos = transform.position;
            Vector3 targetPos = coll.transform.position;
            float curDis = Vector3.Distance(myPos, targetPos);
            if (curDis <= maxDis)
            {
                maxDis = curDis;
                nearestTarget = coll.transform;
            } 
        }
    }
    private void Fire()
    {
        if (GameManager.Instance.Player.IsDie) return;
        if (targets.Length == 0) return;
        if (nearestTarget == null) return;

        
        _attackDelay -= Time.deltaTime;
        if (_attackDelay <= 0)
        {
            if(Level == 5)
            {
                GameObject[] bullets = new GameObject[3];
                for(int i = 0; i < 3; i++)
                {
                    GameObject bullet = Managers.Pool.Get(Enum.GetName(typeof(Define.PoolObject), Define.PoolObject.Bullet));
                    bullets[i] = bullet;
                }
                bullets[0].GetComponent<BulletController>().SetTarget(_data, Level, nearestTarget,bullets);
            }
            else
            {
                GameObject bullet = Managers.Pool.Get(Enum.GetName(typeof(Define.PoolObject), Define.PoolObject.Bullet));
                bullet.GetComponent<BulletController>().SetTarget(_data, Level, nearestTarget);
            }
            
            _attackDelay = _data.attackDelay[Level - 1];
        }
    }
    public override void LevelUp()
    {
        base.LevelUp();

    }
}
