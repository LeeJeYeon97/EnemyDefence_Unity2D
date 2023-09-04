using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : CreatureController
{

    #region ----- Private

    private PlayerInputController _input;

    #endregion

    #region ----- Public
    public List<GameObject> _weaponList = new List<GameObject>();

    public PlayerStatController _stat;

    public int kill;

    public CircleCollider2D itemCollider;
    #endregion

    
    protected override void Init()
    {
        base.Init();
        _input = GetComponent<PlayerInputController>();
        _stat = GetComponent<PlayerStatController>();

    }

    private void FixedUpdate()
    {
        
        if (IsDie) return;
        
        Move();
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
    
}
