using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : CreatureController
{

    #region ----- Private Component

    private PlayerInputController _input;

    #endregion

    #region ----- Private

    #endregion

    #region ----- Public

    public float maxExP;
    public float curExp;

    public int level;
    public int kill;

    #endregion

    public Action GetExpAction;

    public List<GameObject> _weaponList = new List<GameObject>();
    protected override void Init()
    {
        base.Init();
        _input = GetComponent<PlayerInputController>();
        MoveSpeed = 3.0f;
        maxExP = 10;
        curExp = 0;
        level = 1;
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
    public void GetExp(int exp)
    {
        curExp += exp;
        if (curExp >= maxExP)
        {
            LevelUp();
            curExp = 0;
            maxExP *= 2;
        }
        GetExpAction.Invoke();
    }
    private void LevelUp()
    {
        level++;

        GameManager.Instance.IsPause = true;

        Managers.UI.ShowPopupUI<UI_LevelUpPopup>("LevelUpPopup");
    }
}
