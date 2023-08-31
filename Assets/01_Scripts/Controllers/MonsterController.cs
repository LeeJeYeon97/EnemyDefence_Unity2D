using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class MonsterController : CreatureController
{
    private PlayerController _target;
    public RuntimeAnimatorController[] RunAnim;
    private Define.MonsterName _monsterName;

    protected override void Init()
    {
        base.Init();
    }
    public void SpawnInit(int monsterLevel)
    {
        switch (monsterLevel)
        {
            // bat
            case (int)Define.MonsterName.Bat:
                MaxHp = 1;
                MoveSpeed = 1.0f;
                _monsterName = Define.MonsterName.Bat;
                break;
            case (int)Define.MonsterName.Chicken:
                MaxHp = 2;
                MoveSpeed = 2.0f;
                _monsterName = Define.MonsterName.Chicken;
                break;
            case (int)Define.MonsterName.Bunny:
                MaxHp = 3;
                MoveSpeed = 3.0f;
                _monsterName = Define.MonsterName.Bunny;
                break;
            case (int)Define.MonsterName.Rino:
                MaxHp = 4;
                MoveSpeed = 4.0f;
                _monsterName = Define.MonsterName.Rino;
                break;
        }
        _anim.runtimeAnimatorController = RunAnim[monsterLevel];
        _target = GameManager.Instance.Player;
        _collider.enabled = true;
        IsDie = false;
        CurHp = MaxHp;
    }
    private void FixedUpdate()
    {
        
        if (IsDie) return;

        Move();
    }
    private void Move()
    {
        Vector2 dir = _target.GetComponent<Rigidbody2D>().position - _rb.position;
        if(dir.x != 0)
        {
            // ��������Ʈ �̹����� �������־� �÷��̾�� �ݴ�� �����Ѵ�.
            _sprite.flipX = dir.x > 0;
        }
        _anim.SetFloat("Speed", dir.magnitude);
        _rb.MovePosition(_rb.position + dir.normalized * Time.fixedDeltaTime * MoveSpeed);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsDie) return;

        if(collision.CompareTag("Weapon"))
        {
            if (!_anim.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
                _anim.SetTrigger("Hit");
        }
    }
    public override void OnDamge(float damage)
    {
        if (IsDie) return;

        CurHp -= damage;

        if(CurHp <= 0)
        {
            IsDie = true;
            _collider.enabled = false;

            _anim.SetTrigger("Die");
            _target.kill++;
            _target.GetExpAction.Invoke();
        }
    }
    public void DieEvent()
    {
        // Item����
        GameObject go = Managers.Pool.Get("Item");
        go.GetComponent<ItemController>().SpawnInit((int)_monsterName);
        go.transform.position = transform.position;
        gameObject.SetActive(false);
    }
}
