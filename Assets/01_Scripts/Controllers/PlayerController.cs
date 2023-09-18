using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : CreatureController
{

    #region ----- Private

    public PlayerInputController _input;

    #endregion

    #region ----- Public
    public List<GameObject> _weaponList = new List<GameObject>();

    public PlayerStatController _stat;

    public int kill;

    public CircleCollider2D itemCollider;
    private float colliderTime = 0.1f;
    #endregion

    
    protected override void Init()
    {
        base.Init();
        _input = GetComponent<PlayerInputController>();
        _stat = GetComponent<PlayerStatController>();

    }

    private void FixedUpdate()
    {
        if (!GameManager.Instance.GameStart) return;
        if (IsDie) return;
        
        Move();
        if(itemCollider.radius == 100.0f)
        {
            colliderTime -= Time.fixedDeltaTime;
            if(colliderTime <= 0)
            {
                itemCollider.radius = 0.5f;
                colliderTime = 0.1f;
            }
        }
    }
    private void Move()
    {
        // 왼쪽 방향으로 갈 때 스프라이트 뒤집기
        if(_input.MoveDir.x != 0)
        {
            _sprite.flipX = _input.MoveDir.x < 0;
        }
        _anim.SetFloat("Speed", _input.MoveDir.magnitude);

        _rb.MovePosition(_rb.position + _input.MoveDir.normalized * Time.fixedDeltaTime * _stat.moveSpeed);
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision == null) return;

        if (!collision.gameObject.CompareTag("Player"))
            

        if (!_anim.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
            _anim.SetTrigger("Hit");
    }
    public override void OnDamge(float damage)
    {
        if (IsDie) return;

        _stat.curHp -= damage;
        SoundManager.instance.PlaySfx(Define.Sfx.Hit0);

        if(_stat.curHp <= 0)
        {
            // 게임 종료
            IsDie = true;

            SoundManager.instance.PlaySfx(Define.Sfx.Dead);
            _anim.SetTrigger("Die");
            Managers.Pool.ClearPool();
            foreach (var weapon in _weaponList)
            {
                weapon.SetActive(false);
            }
        }
    }
    public void PlayerDieEvent()
    {
        Managers.UI.ShowPopupUI<UI_ResultPopup>("ResultPopup");
        GameManager.Instance.IsPause = true;
    }

}
