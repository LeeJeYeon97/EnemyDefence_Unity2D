using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CreatureController
{

    #region ----- Private Component

    private PlayerInputController _input;
    
    #endregion

    #region ----- Private

    #endregion

    #region ----- Public

    #endregion


    protected override void Init()
    {
        base.Init();
        _input = GetComponent<PlayerInputController>();
        MoveSpeed = 3.0f;
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

        _rb.MovePosition(_rb.position + _input.MoveDir.normalized * Time.fixedDeltaTime * MoveSpeed);
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision == null) return;

        if (!collision.gameObject.CompareTag("Player"))
            

        if (!_anim.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
            _anim.SetTrigger("Hit");
    }
}
