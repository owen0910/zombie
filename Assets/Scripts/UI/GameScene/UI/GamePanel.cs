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

    //hp的初始宽
    public float hpW=500;

    public Button btnQuit;

    public Transform boTrans;

    public List<Towerbtn> towerBtns = new List<Towerbtn>();

    private TowerPoint nowSelTowerPoint;

    //用来标识是否检测造塔输入
    private bool checkInput;

    public override void Init()
    {
        btnQuit.onClick.AddListener(() =>
        {
            UIManager.Instance.HidePanel<GamePanel>();
            //返回开始界面
            SceneManager.LoadScene("BeginScene");
        });

        //一开始隐藏下方造塔UI
        boTrans.gameObject.SetActive(false);
        //锁定鼠标
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void UpdateTowerHp(int hp, int maxHP)
    {
        txtHp.text = hp + "/" + maxHP;
        //更新血条
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
        //根据造塔点的信息决定界面上的显示
        nowSelTowerPoint = point;
        if (nowSelTowerPoint==null)
        {
            checkInput = false;
            //隐藏下方造塔按钮
            boTrans.gameObject.SetActive(false);
        }
        else
        {
            checkInput = true;
            //显示下方造塔按钮
            boTrans.gameObject.SetActive(true);
            //如果没有造过塔
            if (nowSelTowerPoint.nowTowerInfo == null)
            {
                for (int i = 0; i < towerBtns.Count; i++)
                {
                    towerBtns[i].gameObject.SetActive(true);
                    towerBtns[i].InitInfo(nowSelTowerPoint.chooseIDs[i], "数字键" + (i + 1));
                }
            }
            //如果造过塔了 要升级
            else
            {
                for (int i = 0; i < towerBtns.Count; i++)
                {
                    towerBtns[i].gameObject.SetActive(false);
                }
                towerBtns[1].gameObject.SetActive(true);
                towerBtns[1].InitInfo(nowSelTowerPoint.nowTowerInfo.nextLev, "空格键");
            }
        }

        
    }

    protected override void Update()
    {
        base.Update();
        //主要用于键盘造塔
        if (!checkInput)
            return;
        //如果没有造过塔就检测123来造塔
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
        //如果造过了就检测空格去升级
        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                nowSelTowerPoint.CreateTower(nowSelTowerPoint.nowTowerInfo.nextLev);
            }
        }
    }
}
