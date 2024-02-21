using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerObject : MonoBehaviour
{
    //��̨ͷ��
    public Transform head;
    //�����
    public Transform gunPoint;

    //ͷ����ת�ٶ�
    private float roundSpeed = 20;
    //��̨����������
    private TowerInfo info;

    //��ǰҪ������Ŀ��
    private MonsterObject targetObj;

    //��ǰҪ������Ŀ����
    private List<MonsterObject> targetObjs;

    //���ڹ�����ʱ��
    private float nowTime;

    //���ڼ�¼����λ��
    private Vector3 monsterPos;

    //��ʼ����̨��Ϣ
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
        //���幥��
        if (info.atkType==1)
        {
            if (targetObj == null ||
                targetObj.isDead ||
                Vector3.Distance(this.transform.position, targetObj.transform.position) > info.atkRange)
            {
                //Ѱ��Ŀ��
                targetObj = GameLevelMgr.Instance.FindMonster(this.transform.position, info.atkRange);
            }

            if (targetObj == null)
                return;
            monsterPos = targetObj.transform.position;
            monsterPos.y = head.transform.position.y;

            head.rotation = Quaternion.Slerp(head.rotation, Quaternion.LookRotation(monsterPos - head.position), roundSpeed * Time.deltaTime);
            if (Vector3.Angle(head.forward,monsterPos-head.position)<5&&Time.time-nowTime>=info.offsetTime)
            {
                //��Ŀ������
                targetObj.Wound(info.atk);
                //������Ч
                GameDataMgr.Instance.PlaySound("Music/Tower");
                //����������Ч
                GameObject effObj = Instantiate(Resources.Load<GameObject>(info.eff), gunPoint.position, gunPoint.rotation);
                //�ӳ��Ƴ���Ч
                Destroy(effObj, 0.2f);

                nowTime = Time.time;

            }
            
        }
        //Ⱥ�幥��
        else
        {
            targetObjs = GameLevelMgr.Instance.FindMonsters(this.transform.position, info.atkRange);
            if (targetObjs.Count>0&&Time.time-nowTime>=info.offsetTime)
            {
                //����������Ч
                GameObject effObj = Instantiate(Resources.Load<GameObject>(info.eff), gunPoint.position, gunPoint.rotation);
                //�ӳ��Ƴ���Ч
                Destroy(effObj, 0.2f);

                //��Ŀ������
                for (int i = 0; i < targetObjs.Count; i++)
                {
                    targetObjs[i].Wound(info.atk);
                }
                nowTime = Time.time;
            }
        }
    }
}
