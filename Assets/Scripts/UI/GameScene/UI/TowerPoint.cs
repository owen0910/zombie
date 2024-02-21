using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPoint : MonoBehaviour
{
    //关联的塔对象
    private GameObject towerObj = null;
    //关联的塔数据
    public TowerInfo nowTowerInfo = null;

    //可以造的三个塔的id
    public List<int> chooseIDs;


    public void CreateTower(int id)
    {
        TowerInfo info = GameDataMgr.Instance.towerInfoList[id - 1];
        //如果钱不够 就不能造
        if (info.money > GameLevelMgr.Instance.player.money)
            return;
        //扣钱
        GameLevelMgr.Instance.player.AddMoney(-info.money);
        //创建塔
        //先判断之前是否有塔 如果有就删除
        if (towerObj!=null)
        {
            Destroy(towerObj);
            towerObj = null;
        }

        towerObj = Instantiate(Resources.Load<GameObject>(info.res), this.transform.position, Quaternion.identity);
        //初始化塔
        towerObj.GetComponent<TowerObject>().InitInfo(info);
        //记录当前塔的数据
        nowTowerInfo = info;

        //塔建造完毕，更新游戏界面上的内容
        if (nowTowerInfo.nextLev!=0)
        {
            UIManager.Instance.GetPanel<GamePanel>().UpdateSelTower(this);
        }
        else
        {
            UIManager.Instance.GetPanel<GamePanel>().UpdateSelTower(null);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //如果现在有塔了 就没有必要显示界面了
        if (nowTowerInfo!=null&&nowTowerInfo.nextLev==0)
        {
            return;
        }
        UIManager.Instance.GetPanel<GamePanel>().UpdateSelTower(this);
    }

    private void OnTriggerExit(Collider other)
    {
        UIManager.Instance.GetPanel<GamePanel>().UpdateSelTower(null);
    }
}
