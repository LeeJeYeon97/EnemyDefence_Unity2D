using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;

public class MonsterController : CreatureController
{
    private PlayerController _target;
    public RuntimeAnimatorController[] RunAnim;
    private Define.MonsterName _monsterName;

    public Slider hpBar;
    public bool isKnockBack;
    public float knockBackTime;
    WaitForFixedUpdate wait;
    public Action KnockBackEvt;
    public float damage;

    protected override void Init()
    {
        base.Init();
    }
    public void SpawnInit(int monsterLevel)
    {
        string key = Enum.GetName(typeof(Define.MonsterName), monsterLevel);
        MonsterData data = Managers.Data.GetData<MonsterData>(key);
        MaxHp = data.MaxHp;
        MoveSpeed = data.MoveSpeed;
        _monsterName = data._name;
        damage = data.Damage;

        _anim.runtimeAnimatorController = data.anim;
        
        _target = GameManager.Instance.Player;
        _collider.enabled = true;
        IsDie = false;
        CurHp = MaxHp;

        hpBar.value = CurHp / MaxHp;

        wait = new WaitForFixedUpdate();
        KnockBackEvt -= (() => { StartCoroutine(CoKnockBack()); });
        KnockBackEvt += (() => { StartCoroutine(CoKnockBack()); });

        
    }
    private void FixedUpdate()
    {
        //if (IsDie || _anim.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
        //{
        //    return;
        //}
        if (!GameManager.Instance.GameStart) return;
        if (IsDie)
            return;

        Move();
        
        hpBar.value = CurHp / MaxHp;

    }
    private void Move()
    {
        Vector2 dir = _target.GetComponent<Rigidbody2D>().position - _rb.position;
        if(dir.x != 0)
        {
            // 스프라이트 이미지가 뒤집혀있어 플레이어와 반대로 적용한다.
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
            {
                _anim.SetTrigger("Hit");
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (IsDie) return;
        if (!collision.gameObject.CompareTag("Player"))
            return;

        collision.gameObject.GetComponent<PlayerController>().OnDamge(damage);
    }
    IEnumerator CoKnockBack()
    {
        yield return wait;

        Vector3 playerPos = GameManager.Instance.Player.transform.position;
        Vector3 dirVec = transform.position - playerPos;

        _rb.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse);
    }
    public override void OnDamge(float damage)
    {
        if (IsDie) return;

        CurHp -= damage;

        hpBar.value = CurHp / MaxHp;
        SoundManager.instance.PlaySfx(Define.Sfx.Hit1);

        if (CurHp <= 0)
        {
            SoundManager.instance.PlaySfx(Define.Sfx.Dead, 0.5f);
            IsDie = true;
            _collider.enabled = false;

            _anim.SetTrigger("Die");
            _target.kill++;
            _target._stat.GetExpAction.Invoke();
        }
    }
    // 애니메이션 이벤트에서 호출
    public void DieEvent()
    {
        // Item생성
        GameObject go = Managers.Pool.Get("Item");
        go.GetComponent<ItemController>().SpawnInit((int)_monsterName);
        go.transform.position = transform.position;
        gameObject.SetActive(false);
    }
}
