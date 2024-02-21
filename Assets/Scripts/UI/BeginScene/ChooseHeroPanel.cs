using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ChooseHeroPanel : BasePanel
{
    public Button btnLeft;
    public Button btnRight;

    public Button btnUnlock;
    public Text txtUnlock;

    public Button btnStart;
    public Button btnBack;

    public Text txtMoney;

    //角色名字
    public Text txtName;

    //需要创建的位置
    public Transform heroPos;
    //当前场景中显示的对象
    private GameObject heroObj;
    //当前使用的数据
    private RoleInfo nowRoleData;
    //当前使用的数据的索引
    private int nowIndex;

    public override void Init()
    {
        heroPos = GameObject.Find("HeroPos").transform;

        //更新左上角的钱
        txtMoney.text = GameDataMgr.Instance.playerData.haveMoney.ToString();

        btnLeft.onClick.AddListener(() =>
        {
            --nowIndex;
            if (nowIndex < 0)
                nowIndex = GameDataMgr.Instance.roleInfoList.Count - 1;
            //模型的更新
            ChangeHero();
        });

        btnRight.onClick.AddListener(() =>
        {
            ++nowIndex;
            if (nowIndex >= GameDataMgr.Instance.roleInfoList.Count)
                nowIndex = 0;
            //模型的更新
            ChangeHero();
        });

        btnUnlock.onClick.AddListener(() =>
        {
            //点击解锁按钮
            PlayerData data = GameDataMgr.Instance.playerData;
            if (data.haveMoney>=nowRoleData.lockMoney)
            {
                //购买英雄
                data.haveMoney -= nowRoleData.lockMoney;
                //更新显示界面
                txtMoney.text = data.haveMoney.ToString();
                //记录购买的id
                data.buyHero.Add(nowRoleData.id);
                //保存数据
                GameDataMgr.Instance.SavePlayerData();

                //更新解锁按钮
                UpdateLockBtn();
                //提示面板 显示购买成功
                UIManager.Instance.ShowPanel<TipPanel>().ChangeInfo("购买成功");
            }
            else
            {
                //提示面板显示金钱不足
                UIManager.Instance.ShowPanel<TipPanel>().ChangeInfo("金钱不足");
            }
        });

        btnStart.onClick.AddListener(() =>
        {
            //记录当前选择的角色
            GameDataMgr.Instance.nowSelRole = nowRoleData;
            //隐藏自己显示场景选择界面
            UIManager.Instance.HidePanel<ChooseHeroPanel>();
            UIManager.Instance.ShowPanel<ChooseScenePanel>();
        });

        btnBack.onClick.AddListener(() =>
        {
            //隐藏自己显示开始界面
            UIManager.Instance.HidePanel<ChooseHeroPanel>();
            Camera.main.GetComponent<CameraAnimator>().TurnLeft(() =>
            {
                UIManager.Instance.ShowPanel<BeginPanel>();
            });
        });
        //更新模型显示
        ChangeHero();
    }

    /// <summary>
    /// 更新场景上要显示的模型
    /// </summary>
    private void ChangeHero()
    {
        if (heroObj!=null)
        {
            Destroy(heroObj);
            heroObj = null;
        }
        nowRoleData = GameDataMgr.Instance.roleInfoList[nowIndex];
        //实例化对象并记录 并用于下次切换时删除
        heroObj = Instantiate(Resources.Load<GameObject>(nowRoleData.res), heroPos.position, heroPos.rotation);
        txtName.text = nowRoleData.tips;
        //移除脚本
        Destroy(heroObj.GetComponent<PlayerObject>());

        //根据数据来决定是否显示解锁按钮
        UpdateLockBtn();
    }

    //根据数据来决定是否显示解锁按钮
    private void UpdateLockBtn()
    {
        //如果该角色需要解锁并没有解锁
        if(nowRoleData.lockMoney>0&&!GameDataMgr.Instance.playerData.buyHero.Contains(nowRoleData.id))
        {
            btnUnlock.gameObject.SetActive(true);
            txtUnlock.text = "￥" + nowRoleData.lockMoney;
            //隐藏开始按钮，因为没有解锁
            btnStart.gameObject.SetActive(false);
        }
        else
        {
            btnUnlock.gameObject.SetActive(false);
            btnStart.gameObject.SetActive(true);
        }
    }
    public override void HideMe(UnityAction callBack)
    {
        base.HideMe(callBack);
        if (heroObj!=null)
        {
            DestroyImmediate(heroObj);
            heroObj = null;
        }
    }
}
