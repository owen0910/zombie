using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 组合控件 造塔的按钮
/// </summary>
public class Towerbtn : MonoBehaviour
{
    public Image imgPic;

    public Text txtTip;

    public Text txtMoney;
    
    /// <summary>
    /// 初始化按钮信息
    /// </summary>
    /// <param name="id"></param>
    /// <param name="inputStr"></param>
    public void InitInfo(int id, string inputStr)
    {
        TowerInfo info = GameDataMgr.Instance.towerInfoList[id - 1];
        imgPic.sprite = Resources.Load<Sprite>(info.imgRes);
        txtMoney.text = "￥" + info.money;
        txtTip.text = inputStr;

        if (info.money > GameLevelMgr.Instance.player.money)
            txtMoney.text = "金钱不足";
    }
}
