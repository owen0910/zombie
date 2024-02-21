using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePanel : BasePanel
{
    public Image imgHp;
    public Text txtHp;
    public Text txtWave;
    public Text txtMoney;

    //hp�ĳ�ʼ��
    public float hpW=500;

    public Button btnQuit;

    public Transform boTrans;

    public List<Towerbtn> towerBtns = new List<Towerbtn>();

    private TowerPoint nowSelTowerPoint;

    //������ʶ�Ƿ�����������
    private bool checkInput;

    public override void Init()
    {
        btnQuit.onClick.AddListener(() =>
        {
            UIManager.Instance.HidePanel<GamePanel>();
            //���ؿ�ʼ����
            SceneManager.LoadScene("BeginScene");
        });

        //һ��ʼ�����·�����UI
        boTrans.gameObject.SetActive(false);
        //�������
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void UpdateTowerHp(int hp, int maxHP)
    {
        txtHp.text = hp + "/" + maxHP;
        //����Ѫ��
        (imgHp.transform as RectTransform).sizeDelta = new Vector2((float)hp / maxHP * hpW, 38);

    }

    public void UpdateWaveNum(int nowNum,int maxNum)
    {
        txtWave.text = nowNum + "/" + maxNum;
    }

    public void UpdateMoney(int money)
    {
        txtMoney.text = money.ToString();
    }

    public void UpdateSelTower(TowerPoint point)
    {
        //�������������Ϣ���������ϵ���ʾ
        nowSelTowerPoint = point;
        if (nowSelTowerPoint==null)
        {
            checkInput = false;
            //�����·�������ť
            boTrans.gameObject.SetActive(false);
        }
        else
        {
            checkInput = true;
            //��ʾ�·�������ť
            boTrans.gameObject.SetActive(true);
            //���û�������
            if (nowSelTowerPoint.nowTowerInfo == null)
            {
                for (int i = 0; i < towerBtns.Count; i++)
                {
                    towerBtns[i].gameObject.SetActive(true);
                    towerBtns[i].InitInfo(nowSelTowerPoint.chooseIDs[i], "���ּ�" + (i + 1));
                }
            }
            //���������� Ҫ����
            else
            {
                for (int i = 0; i < towerBtns.Count; i++)
                {
                    towerBtns[i].gameObject.SetActive(false);
                }
                towerBtns[1].gameObject.SetActive(true);
                towerBtns[1].InitInfo(nowSelTowerPoint.nowTowerInfo.nextLev, "�ո��");
            }
        }

        
    }

    protected override void Update()
    {
        base.Update();
        //��Ҫ���ڼ�������
        if (!checkInput)
            return;
        //���û��������ͼ��123������
        if (nowSelTowerPoint.nowTowerInfo==null)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                nowSelTowerPoint.CreateTower(nowSelTowerPoint.chooseIDs[0]);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                nowSelTowerPoint.CreateTower(nowSelTowerPoint.chooseIDs[1]);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                nowSelTowerPoint.CreateTower(nowSelTowerPoint.chooseIDs[2]);
            }
        }
        //�������˾ͼ��ո�ȥ����
        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                nowSelTowerPoint.CreateTower(nowSelTowerPoint.nowTowerInfo.nextLev);
            }
        }
    }
}
