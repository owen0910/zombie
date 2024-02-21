using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerObject : MonoBehaviour
{
    //炮台头部
    public Transform head;
    //开火点
    public Transform gunPoint;

    //头部旋转速度
    private float roundSpeed = 20;
    //炮台关联的数据
    private TowerInfo info;

    //当前要攻击的目标
    private MonsterObject targetObj;

    //当前要攻击的目标们
    private List<MonsterObject> targetObjs;

    //用于攻击计时的
    private float nowTime;

    //用于记录怪物位置
    private Vector3 monsterPos;

    //初始化炮台信息
    public void InitInfo(TowerInfo info)
    {
        this.info = info;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //单体攻击
        if (info.atkType==1)
        {
            if (targetObj == null ||
                targetObj.isDead ||
                Vector3.Distance(this.transform.position, targetObj.transform.position) > info.atkRange)
            {
                //寻找目标
                targetObj = GameLevelMgr.Instance.FindMonster(this.transform.position, info.atkRange);
            }

            if (targetObj == null)
                return;
            monsterPos = targetObj.transform.position;
            monsterPos.y = head.transform.position.y;

            head.rotation = Quaternion.Slerp(head.rotation, Quaternion.LookRotation(monsterPos - head.position), roundSpeed * Time.deltaTime);
            if (Vector3.Angle(head.forward,monsterPos-head.position)<5&&Time.time-nowTime>=info.offsetTime)
            {
                //让目标受伤
                targetObj.Wound(info.atk);
                //播放音效
                GameDataMgr.Instance.PlaySound("Music/Tower");
                //创建开火特效
                GameObject effObj = Instantiate(Resources.Load<GameObject>(info.eff), gunPoint.position, gunPoint.rotation);
                //延迟移除特效
                Destroy(effObj, 0.2f);

                nowTime = Time.time;

            }
            
        }
        //群体攻击
        else
        {
            targetObjs = GameLevelMgr.Instance.FindMonsters(this.transform.position, info.atkRange);
            if (targetObjs.Count>0&&Time.time-nowTime>=info.offsetTime)
            {
                //创建开火特效
                GameObject effObj = Instantiate(Resources.Load<GameObject>(info.eff), gunPoint.position, gunPoint.rotation);
                //延迟移除特效
                Destroy(effObj, 0.2f);

                //让目标受伤
                for (int i = 0; i < targetObjs.Count; i++)
                {
                    targetObjs[i].Wound(info.atk);
                }
                nowTime = Time.time;
            }
        }
    }
}
