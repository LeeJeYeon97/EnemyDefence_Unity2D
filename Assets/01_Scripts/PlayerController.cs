using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CreatureController
{

    #region ----- Private Component

    private PlayerInputController _input;
    private SpriteRenderer _sprite;
    private Animator _anim;

    #endregion


    #region ----- Private

    #endregion

    #region ----- Public

    #endregion

    private void Awake()
    {
        MoveSpeed = 3.0f;

        _input = GetComponent<PlayerInputController>();
        _sprite = GetComponent<SpriteRenderer>();
        _anim = GetComponent<Animator>();

    }
    void Start()
    {
    }
    void Update()
    {
        Move();
    }
    private void Move()
    {
        // ���� �������� �� �� ��������Ʈ ������
        if(_input.MoveDir.x != 0)
        {
            _sprite.flipX = _input.MoveDir.x < 0;
        }
        _anim.SetFloat("Speed", _input.MoveDir.magnitude);
        
        transform.Translate(_input.MoveDir.normalized * Time.deltaTime * MoveSpeed);
    }
}
