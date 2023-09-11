using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SwordController : WeaponController
{

    bool isAttack = false;
    float startAngle = 0;
    float endAngle = 0;
    bool isRight = false;
    private void Start()
    {
        Init();    }
    protected override void Init()
    {
        base.Init();
        _data = Managers.Data.GetData<WeaponData>(Enum.GetName(typeof(Define.WeaponType), Define.WeaponType.Sword));
        _damage = _data.Damages[Level - 1];
        _attackDelay = _data.attackDelay[Level - 1];
    }
    private void Update()
    {
        if(!isAttack)
            SetRotate();
        if (isAttack)
            AttackRotate();
        
        Attack();
    }
    private void SetRotate()
    {
        Vector2 dir = GameManager.Instance.Player._input.MoveDir;
        float angle = - Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;

        transform.eulerAngles = new Vector3(0, 0, angle);

        if(transform.eulerAngles.z > 0 && transform.eulerAngles.z < 180) // ����
        {
            startAngle = transform.eulerAngles.z - 40;
            endAngle = transform.eulerAngles.z + 40;
            isRight = false;
            
        }
        else if(transform.eulerAngles.z >= 180 && transform.eulerAngles.z <= 360)
        {
            startAngle = transform.eulerAngles.z + 40;
            endAngle = transform.eulerAngles.z - 40;
            isRight = true;
        }
    }
    private void Attack()
    {
        if (GameManager.Instance.Player.IsDie) return;


        _attackDelay -= Time.deltaTime;
        if (!isAttack && _attackDelay <= 0)
        {
            transform.eulerAngles = new Vector3(0, 0, startAngle);
            // ���� ����
            isAttack = true; 
        }
    }
    private void AttackRotate()
    {
        // ���� �������� ȸ����Ű��
        if(isRight)
        {
            transform.eulerAngles -= new Vector3(0, 0, 500.0f * Time.deltaTime);
            if (transform.eulerAngles.z < endAngle)
            {
                
                isAttack = false;
                _attackDelay = _data.attackDelay[Level - 1];
            }
        }
        else
        {
            transform.eulerAngles += new Vector3(0, 0, 500.0f * Time.deltaTime);
            if (transform.eulerAngles.z > endAngle)
            {
                
                isAttack = false;
                _attackDelay = _data.attackDelay[Level - 1];
            }
        }
    }
    public override void LevelUp()
    {
        base.LevelUp();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Monster")) return;

        // ���� �˹� ��Ű��
        collision.GetComponent<CreatureController>().OnDamge(_damage);
        collision.GetComponent<MonsterController>().KnockBackEvt.Invoke();
    }
}
