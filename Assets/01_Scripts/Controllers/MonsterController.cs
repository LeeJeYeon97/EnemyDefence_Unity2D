using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class MonsterController : CreatureController
{
    private GameObject _target;

    protected override void Init()
    {
        base.Init();
    }
    public void SpawnInit(int monsterLevel)
    {
        switch (monsterLevel)
        {
            // bat
            case 0:
                MaxHp = 1;
                MoveSpeed = 1.0f;
                break;
            case 1:
                MaxHp = 2;
                MoveSpeed = 2.0f;
                break;
            case 2:
                MaxHp = 3;
                MoveSpeed = 3.0f;
                break;
            case 3:
                MaxHp = 4;
                MoveSpeed = 4.0f;
                break;
        }
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
                _anim.SetTrigger("Hit");
        }
    }
    public override void OnDamge(float damage)
    {
        CurHp -= damage;

        if(CurHp <= 0)
        {
            IsDie = true;
            _collider.enabled = false;
            _anim.SetTrigger("Die");
            float time = _anim.GetCurrentAnimatorStateInfo(0).length;
            StartCoroutine(CoDie(time));
        }
    }
    IEnumerator CoDie(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }
}
