using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLevelMgr 
{
    private static GameLevelMgr instance = new GameLevelMgr();
    public static GameLevelMgr Instance => instance;

    public PlayerObject player;

    //所有的出怪点
    private List<MonsterPoint> points=new List<MonsterPoint>();
    //当前还有多少波怪物
    private int nowWaveNum = 0;
    //一共有多少波怪物
    private int maxWaveNum = 0;
    //场景上的怪物数量
    //public int nowMonsterNum = 0;

    //记录场景上怪物的列表
    private List<MonsterObject> monsterList = new List<MonsterObject>();

    private GameLevelMgr()
    {

    }

    public void InitInfo(SceneInfo info)
    {
        //显示游戏界面
        UIManager.Instance.ShowPanel<GamePanel>();
        //创建玩家
        RoleInfo roleInfo = GameDataMgr.Instance.nowSelRole;
        //获取场景中的出生位置
        Transform heroPos = GameObject.Find("HeroBornPos").transform;

        GameObject heroObj = GameObject.Instantiate(Resources.Load<GameObject>(roleInfo.res), heroPos.position, heroPos.rotation);
        //对玩家进行初始化
        player = heroObj.GetComponent<PlayerObject>();
        player.InitPlayerInfo(roleInfo.atk, info.money);

        //让摄像机看向动态创建的玩家
        Camera.main.GetComponent<CameraMove>().SetTarget(heroObj.transform);
        //初始化中央保护区的血量
        MainTowerObject.Instance.UpdateHp(info.towerHp, info.towerHp);
    }

    public void AddMonsterPoint(MonsterPoint point)
    {
        points.Add(point);
    }

    /// <summary>
    /// 更新一共有多少波怪
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
        //更新界面
        UIManager.Instance.GetPanel<GamePanel>().UpdateWaveNum(nowWaveNum, maxWaveNum);
    }

    /// <summary>
    /// 检测是否胜利
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
    /// 改变当前场景上怪物的数量
    /// </summary>
    /// <param name="num"></param>
    //public void ChangeMonsterNum(int num)
    //{
    //    monsterList.Count += num;
    //}
    //清空当前关卡数据

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
        //在怪物列表中找到满足距离条件的怪物返回出去
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
        //在怪物列表中找到满足距离条件的所有怪物返回出去
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
