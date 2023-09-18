using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Menu : UI_Base
{
    enum Buttons
    {
        LoginButton,
        LogOutButton,
        LeaderButton,
        AchievementsButton,
        
        AdButton,
        LoadButton,
        SaveButton,
        ExitButton,
    }

    string log;
    private Button loginButton;
    private Button logOutButton;
    private Button leaderButton;
    private Button achievementButton;
    private Button eventButton;
    private Button adButton;
    private Button saveButton;
    private Button loadButton;

    private Button exitButton;

    private void Start()
    {
        Init();
    }
    public override void Init()
    {
        Bind<Button>(typeof(Buttons));

        loginButton = Get<Button>((int)Buttons.LoginButton);
        loginButton.onClick.AddListener(() => OnClickLogin());

        logOutButton = Get<Button>((int)Buttons.LogOutButton);
        logOutButton.onClick.AddListener(() => OnClickLogOut());

        leaderButton = Get<Button>((int)Buttons.LeaderButton);
        leaderButton.onClick.AddListener(() => OnClickLeader());

        achievementButton = Get<Button>((int)Buttons.AchievementsButton);
        achievementButton.onClick.AddListener(() => OnClickAchivement());

        adButton = Get<Button>((int)Buttons.AdButton);

        saveButton = Get<Button>((int)Buttons.SaveButton);
        saveButton.onClick.AddListener(() => OnClickSaveData());

        loadButton = Get<Button>((int)Buttons.LoadButton);
        loadButton.onClick.AddListener(() => OnClickLoadData());

        exitButton = Get<Button>((int)Buttons.ExitButton);
        exitButton.onClick.AddListener(() => OnClickExit());

    }
    public void OnClickExit()
    {
        gameObject.SetActive(false);
    }
    public void OnClickLogin()
    {
        GPGSBinder.Inst.Login((success, localUser) =>
           log = $"{success}, {localUser.userName}, {localUser.id}, {localUser.state}, {localUser.underage}");
    }
    public void OnClickLogOut()
    {
        GPGSBinder.Inst.Logout();
    }
    public void OnClickSaveData()
    {
        GPGSBinder.Inst.SaveCloud("mysave", "want data", success => log = $"{success}");
    }
    public void OnClickLoadData()
    {
        GPGSBinder.Inst.LoadCloud("mysave", (success, data) => log = $"{success}, {data}");
    }
    public void OnClickDeleteData()
    {
        GPGSBinder.Inst.DeleteCloud("mysave", success => log = $"{success}");
    }
    public void OnClickLeader()
    {
        GPGSBinder.Inst.ShowAllLeaderboardUI();
    }
    public void OnClickAchivement()
    {
        GPGSBinder.Inst.ShowAchievementUI();
    }
    public void OnClickEvent()
    {
        
    }
    public void OnClickAd()
    {

    }

}
