using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class UI_SelectPopup : UI_Popup
{
    enum GameObjects
    {
        SelectWeaponPanel,
    }

    private void Start()
    {
        Init();
    }
    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));
        GameObject panel = Get<GameObject>((int)GameObjects.SelectWeaponPanel);
    }
}
