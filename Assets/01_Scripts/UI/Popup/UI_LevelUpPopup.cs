using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_LevelUpPopup : UI_Popup
{
    enum GameObjects
    {
        LevelUpWeaponPanel
    }
    private void Start()
    {
        Init();
    }
    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));
        GameObject panel = Get<GameObject>((int)GameObjects.LevelUpWeaponPanel);

    }
}
