using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPoint : MonoBehaviour
{
    //怪物有多少波
    public int maxWave;
    //每波怪物有多少只
    public int monsterNumOneWave;
    //当前波还有多少只没有创建
    private int nowNum;

    //怪物ID
    public List<int> monsterIDs;
    //记录当前波创建什么ID的怪物
    private int nowID;

    //单只怪物创建的间隔时间
    public float createOffsetTime;
    //波与波之间的间隔时间
    public float delayTime;
    //第一波怪物创建的间隔时间
    public float firstDelayTime;
    

    // Start is called before the first frame update
    void Start()
    {
        Invoke("CreateWave", firstDelayTime);

        //记录怪物点
        GameLevelMgr.Instance.AddMonsterPoint(this);
        //更新最大波数
        GameLevelMgr.Instance.UpdateMaxNum(maxWave);
    }

    /// <summary>
    /// 开始创建一波怪物
    /// </summary>
    private void CreateWave()
    {
        //当前怪物的ID
        nowID = monsterIDs[Random.Range(0, monsterIDs.Count)];
        //当前怪物有多少只
        nowNum = monsterNumOneWave;
        //创建怪物
        CreateMonster();
        //减少波数
        --maxWave;
        GameLevelMgr.Instance.ChangeNowWaveNum(1);
    }

    private void CreateMonster()
    {
        //取出怪物数据
        MonsterInfo info = GameDataMgr.Instance.monsterInfoList[nowID - 1];
        //创建预设体
        GameObject obj = Instantiate(Resources.Load<GameObject>(info.res), this.transform.position, Quaternion.identity);
        MonsterObject monsterObject = obj.AddComponent<MonsterObject>();
        monsterObject.InitInfo(info);
        //怪物数量+1；
        //GameLevelMgr.Instance.ChangeMonsterNum(1);
        GameLevelMgr.Instance.AddMonster(monsterObject);

        //创建完一只怪物 减去要创建的怪物1
        --nowNum;
        if (nowNum==0)
        {
            if (maxWave>0)
            {
                Invoke("CreateWave", delayTime);
            }
            
        }
        else
        {
            Invoke("CreateMonster", createOffsetTime);
        }
    }

    //是否出怪结束
    public bool CheckOver()
    {
        return nowNum == 0 && maxWave == 0;
    }


}
