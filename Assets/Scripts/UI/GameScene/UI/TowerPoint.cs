using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPoint : MonoBehaviour
{
    //������������
    private GameObject towerObj = null;
    //������������
    public TowerInfo nowTowerInfo = null;

    //���������������id
    public List<int> chooseIDs;


    public void CreateTower(int id)
    {
        TowerInfo info = GameDataMgr.Instance.towerInfoList[id - 1];
        //���Ǯ���� �Ͳ�����
        if (info.money > GameLevelMgr.Instance.player.money)
            return;
        //��Ǯ
        GameLevelMgr.Instance.player.AddMoney(-info.money);
        //������
        //���ж�֮ǰ�Ƿ����� ����о�ɾ��
        if (towerObj!=null)
        {
            Destroy(towerObj);
            towerObj = null;
        }

        towerObj = Instantiate(Resources.Load<GameObject>(info.res), this.transform.position, Quaternion.identity);
        //��ʼ����
        towerObj.GetComponent<TowerObject>().InitInfo(info);
        //��¼��ǰ��������
        nowTowerInfo = info;

        //��������ϣ�������Ϸ�����ϵ�����
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
        //������������� ��û�б�Ҫ��ʾ������
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
