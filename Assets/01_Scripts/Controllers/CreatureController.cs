using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureController : MonoBehaviour
{
    #region ----- Protected Component

    protected SpriteRenderer _sprite;
    protected Animator _anim;
    protected Rigidbody2D _rb;
    protected Collider2D _collider;

    #endregion

    #region ----- Public

    [SerializeField]
    [Range(0f, 20f)]
    protected float MoveSpeed;

    public float CurHp;
    public float MaxHp;


    public bool IsDie = false;
    #endregion

    private void Awake()
    {
        Init();
    }
    protected virtual void Init()
    {
        _sprite = GetComponent<SpriteRenderer>();
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
    }

    public virtual void OnDamge(float damage)
    {
    }
    
}
