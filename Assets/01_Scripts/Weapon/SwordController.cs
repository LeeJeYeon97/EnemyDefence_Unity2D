using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SwordController : WeaponController
{
    public float[] knockBackPower;

    private void Start()
    {
        Init();    
    }
    protected override void Init()
    {
        base.Init();
        _data = Managers.Data.GetData<WeaponData>(Enum.GetName(typeof(Define.WeaponType), Define.WeaponType.Sword));
        _damage = _data.Damages[Level - 1];
        _attackDelay = _data.attackDelay[Level - 1];
    }
    private void Update()
    {
        SetRotate();
    }
    private void SetRotate()
    {
        Vector2 dir = GameManager.Instance.Player._input.MoveDir;
        float angle = - Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;

        transform.eulerAngles = new Vector3(0, 0, angle);
    }
    public override void LevelUp()
    {
        base.LevelUp();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Monster")) return;

        // 몬스터 넉백 시키기
        collision.GetComponent<CreatureController>().OnDamge(_damage);
        collision.GetComponent<MonsterController>().KnockBackEvt.Invoke(knockBackPower[Level - 1]);
    }
}
