using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverPanel : BasePanel
{
    public Text txtWin;
    public Text txtInfo;
    public Text txtMoney;

    public Button btnSure;
    public override void Init()
    {
        btnSure.onClick.AddListener(() =>
        {
            UIManager.Instance.HidePanel<GameOverPanel>();
            UIManager.Instance.HidePanel<GamePanel>();

            GameLevelMgr.Instance.ClearInfo();
            //切换场景
            SceneManager.LoadScene("BeginScene");
        });
    }

    public void InitInfo(int money,bool isWin)
    {
        txtWin.text = isWin ? "通关" : "失败";
        txtInfo.text = isWin ? "获得胜利奖励" : "获得失败奖励";
        txtMoney.text = "￥" + money;

        //根据奖励改变玩家数据
        GameDataMgr.Instance.playerData.haveMoney += money;
        GameDataMgr.Instance.SavePlayerData();
    }

    public override void ShowME()
    {
        base.ShowME();
        Cursor.lockState = CursorLockMode.None;
    }

}
