using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLevelMgr 
{
    private static GameLevelMgr instance = new GameLevelMgr();
    public static GameLevelMgr Instance => instance;

    public PlayerObject player;

    //���еĳ��ֵ�
    private List<MonsterPoint> points=new List<MonsterPoint>();
    //��ǰ���ж��ٲ�����
    private int nowWaveNum = 0;
    //һ���ж��ٲ�����
    private int maxWaveNum = 0;
    //�����ϵĹ�������
    //public int nowMonsterNum = 0;

    //��¼�����Ϲ�����б�
    private List<MonsterObject> monsterList = new List<MonsterObject>();

    private GameLevelMgr()
    {

    }

    public void InitInfo(SceneInfo info)
    {
        //��ʾ��Ϸ����
        UIManager.Instance.ShowPanel<GamePanel>();
        //�������
        RoleInfo roleInfo = GameDataMgr.Instance.nowSelRole;
        //��ȡ�����еĳ���λ��
        Transform heroPos = GameObject.Find("HeroBornPos").transform;

        GameObject heroObj = GameObject.Instantiate(Resources.Load<GameObject>(roleInfo.res), heroPos.position, heroPos.rotation);
        //����ҽ��г�ʼ��
        player = heroObj.GetComponent<PlayerObject>();
        player.InitPlayerInfo(roleInfo.atk, info.money);

        //�����������̬���������
        Camera.main.GetComponent<CameraMove>().SetTarget(heroObj.transform);
        //��ʼ�����뱣������Ѫ��
        MainTowerObject.Instance.UpdateHp(info.towerHp, info.towerHp);
    }

    public void AddMonsterPoint(MonsterPoint point)
    {
        points.Add(point);
    }

    /// <summary>
    /// ����һ���ж��ٲ���
    /// </summary>
    /// <param name="num"></param>
    public void UpdateMaxNum(int num)
    {
        maxWaveNum += num;
        nowWaveNum = maxWaveNum;
        UIManager.Instance.GetPanel<GamePanel>().UpdateWaveNum(nowWaveNum, maxWaveNum);
    }

    public void ChangeNowWaveNum(int num)
    {
        nowWaveNum -= num;
        //���½���
        UIManager.Instance.GetPanel<GamePanel>().UpdateWaveNum(nowWaveNum, maxWaveNum);
    }

    /// <summary>
    /// ����Ƿ�ʤ��
    /// </summary>
    /// <returns></returns>
    public bool CheckOver()
    {
        for (int i = 0; i < points.Count; i++)
        {
            if (!points[i].CheckOver())
            {
                return false;
            }
        }

        if (monsterList.Count> 0)
        {
            return false;
        }
        Debug.Log("Win");
        return true;
    }
    /// <summary>
    /// �ı䵱ǰ�����Ϲ��������
    /// </summary>
    /// <param name="num"></param>
    //public void ChangeMonsterNum(int num)
    //{
    //    monsterList.Count += num;
    //}
    //��յ�ǰ�ؿ�����

    public void AddMonster(MonsterObject obj)
    {
        monsterList.Add(obj);
    }

    public void RemoveMonster(MonsterObject obj)
    {
        monsterList.Remove(obj);
    }


    public void ClearInfo()
    {
        points.Clear();
        monsterList.Clear();
        nowWaveNum = maxWaveNum = 0;
        player = null;
    }

    public MonsterObject FindMonster(Vector3 pos,int Range)
    {
        //�ڹ����б����ҵ�������������Ĺ��ﷵ�س�ȥ
        for (int i = 0; i < monsterList.Count; i++)
        {
            if ( !monsterList[i].isDead && Vector3.Distance(pos,monsterList[i].transform.position)<=Range)
            {
                return monsterList[i];
            }
        }
        return null;
    }
    public List<MonsterObject> FindMonsters(Vector3 pos, int Range)
    {
        List<MonsterObject> list = new List<MonsterObject>();
        //�ڹ����б����ҵ�����������������й��ﷵ�س�ȥ
        for (int i = 0; i < monsterList.Count; i++)
        {
            if (!monsterList[i].isDead && Vector3.Distance(pos, monsterList[i].transform.position) <= Range)
            {
                list.Add(monsterList[i]);
            }
        }
        return list;
    }

}
