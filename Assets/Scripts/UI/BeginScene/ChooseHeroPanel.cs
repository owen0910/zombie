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

    //��ɫ����
    public Text txtName;

    //��Ҫ������λ��
    public Transform heroPos;
    //��ǰ��������ʾ�Ķ���
    private GameObject heroObj;
    //��ǰʹ�õ�����
    private RoleInfo nowRoleData;
    //��ǰʹ�õ����ݵ�����
    private int nowIndex;

    public override void Init()
    {
        heroPos = GameObject.Find("HeroPos").transform;

        //�������Ͻǵ�Ǯ
        txtMoney.text = GameDataMgr.Instance.playerData.haveMoney.ToString();

        btnLeft.onClick.AddListener(() =>
        {
            --nowIndex;
            if (nowIndex < 0)
                nowIndex = GameDataMgr.Instance.roleInfoList.Count - 1;
            //ģ�͵ĸ���
            ChangeHero();
        });

        btnRight.onClick.AddListener(() =>
        {
            ++nowIndex;
            if (nowIndex >= GameDataMgr.Instance.roleInfoList.Count)
                nowIndex = 0;
            //ģ�͵ĸ���
            ChangeHero();
        });

        btnUnlock.onClick.AddListener(() =>
        {
            //���������ť
            PlayerData data = GameDataMgr.Instance.playerData;
            if (data.haveMoney>=nowRoleData.lockMoney)
            {
                //����Ӣ��
                data.haveMoney -= nowRoleData.lockMoney;
                //������ʾ����
                txtMoney.text = data.haveMoney.ToString();
                //��¼�����id
                data.buyHero.Add(nowRoleData.id);
                //��������
                GameDataMgr.Instance.SavePlayerData();

                //���½�����ť
                UpdateLockBtn();
                //��ʾ��� ��ʾ����ɹ�
                UIManager.Instance.ShowPanel<TipPanel>().ChangeInfo("����ɹ�");
            }
            else
            {
                //��ʾ�����ʾ��Ǯ����
                UIManager.Instance.ShowPanel<TipPanel>().ChangeInfo("��Ǯ����");
            }
        });

        btnStart.onClick.AddListener(() =>
        {
            //��¼��ǰѡ��Ľ�ɫ
            GameDataMgr.Instance.nowSelRole = nowRoleData;
            //�����Լ���ʾ����ѡ�����
            UIManager.Instance.HidePanel<ChooseHeroPanel>();
            UIManager.Instance.ShowPanel<ChooseScenePanel>();
        });

        btnBack.onClick.AddListener(() =>
        {
            //�����Լ���ʾ��ʼ����
            UIManager.Instance.HidePanel<ChooseHeroPanel>();
            Camera.main.GetComponent<CameraAnimator>().TurnLeft(() =>
            {
                UIManager.Instance.ShowPanel<BeginPanel>();
            });
        });
        //����ģ����ʾ
        ChangeHero();
    }

    /// <summary>
    /// ���³�����Ҫ��ʾ��ģ��
    /// </summary>
    private void ChangeHero()
    {
        if (heroObj!=null)
        {
            Destroy(heroObj);
            heroObj = null;
        }
        nowRoleData = GameDataMgr.Instance.roleInfoList[nowIndex];
        //ʵ�������󲢼�¼ �������´��л�ʱɾ��
        heroObj = Instantiate(Resources.Load<GameObject>(nowRoleData.res), heroPos.position, heroPos.rotation);
        txtName.text = nowRoleData.tips;
        //�Ƴ��ű�
        Destroy(heroObj.GetComponent<PlayerObject>());

        //���������������Ƿ���ʾ������ť
        UpdateLockBtn();
    }

    //���������������Ƿ���ʾ������ť
    private void UpdateLockBtn()
    {
        //����ý�ɫ��Ҫ������û�н���
        if(nowRoleData.lockMoney>0&&!GameDataMgr.Instance.playerData.buyHero.Contains(nowRoleData.id))
        {
            btnUnlock.gameObject.SetActive(true);
            txtUnlock.text = "��" + nowRoleData.lockMoney;
            //���ؿ�ʼ��ť����Ϊû�н���
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
