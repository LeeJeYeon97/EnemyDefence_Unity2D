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

    void Start()
    {
        MoveSpeed = 10.0f;
        _input = GetComponent<PlayerInputController>();    
    }
    void Update()
    {
        Move();
    }
    private void Move()
    {
        transform.Translate(_input.MoveDir * Time.deltaTime * MoveSpeed);
    }
}
