using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ��Ͽؼ� �����İ�ť
/// </summary>
public class Towerbtn : MonoBehaviour
{
    public Image imgPic;

    public Text txtTip;

    public Text txtMoney;
    
    /// <summary>
    /// ��ʼ����ť��Ϣ
    /// </summary>
    /// <param name="id"></param>
    /// <param name="inputStr"></param>
    public void InitInfo(int id, string inputStr)
    {
        TowerInfo info = GameDataMgr.Instance.towerInfoList[id - 1];
        imgPic.sprite = Resources.Load<Sprite>(info.imgRes);
        txtMoney.text = "��" + info.money;
        txtTip.text = inputStr;

        if (info.money > GameLevelMgr.Instance.player.money)
            txtMoney.text = "��Ǯ����";
    }
}
