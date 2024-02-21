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
            //�л�����
            SceneManager.LoadScene("BeginScene");
        });
    }

    public void InitInfo(int money,bool isWin)
    {
        txtWin.text = isWin ? "ͨ��" : "ʧ��";
        txtInfo.text = isWin ? "���ʤ������" : "���ʧ�ܽ���";
        txtMoney.text = "��" + money;

        //���ݽ����ı��������
        GameDataMgr.Instance.playerData.haveMoney += money;
        GameDataMgr.Instance.SavePlayerData();
    }

    public override void ShowME()
    {
        base.ShowME();
        Cursor.lockState = CursorLockMode.None;
    }

}
