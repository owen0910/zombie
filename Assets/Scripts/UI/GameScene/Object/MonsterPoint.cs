using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPoint : MonoBehaviour
{
    //�����ж��ٲ�
    public int maxWave;
    //ÿ�������ж���ֻ
    public int monsterNumOneWave;
    //��ǰ�����ж���ֻû�д���
    private int nowNum;

    //����ID
    public List<int> monsterIDs;
    //��¼��ǰ������ʲôID�Ĺ���
    private int nowID;

    //��ֻ���ﴴ���ļ��ʱ��
    public float createOffsetTime;
    //���벨֮��ļ��ʱ��
    public float delayTime;
    //��һ�����ﴴ���ļ��ʱ��
    public float firstDelayTime;
    

    // Start is called before the first frame update
    void Start()
    {
        Invoke("CreateWave", firstDelayTime);

        //��¼�����
        GameLevelMgr.Instance.AddMonsterPoint(this);
        //���������
        GameLevelMgr.Instance.UpdateMaxNum(maxWave);
    }

    /// <summary>
    /// ��ʼ����һ������
    /// </summary>
    private void CreateWave()
    {
        //��ǰ�����ID
        nowID = monsterIDs[Random.Range(0, monsterIDs.Count)];
        //��ǰ�����ж���ֻ
        nowNum = monsterNumOneWave;
        //��������
        CreateMonster();
        //���ٲ���
        --maxWave;
        GameLevelMgr.Instance.ChangeNowWaveNum(1);
    }

    private void CreateMonster()
    {
        //ȡ����������
        MonsterInfo info = GameDataMgr.Instance.monsterInfoList[nowID - 1];
        //����Ԥ����
        GameObject obj = Instantiate(Resources.Load<GameObject>(info.res), this.transform.position, Quaternion.identity);
        MonsterObject monsterObject = obj.AddComponent<MonsterObject>();
        monsterObject.InitInfo(info);
        //��������+1��
        //GameLevelMgr.Instance.ChangeMonsterNum(1);
        GameLevelMgr.Instance.AddMonster(monsterObject);

        //������һֻ���� ��ȥҪ�����Ĺ���1
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

    //�Ƿ���ֽ���
    public bool CheckOver()
    {
        return nowNum == 0 && maxWave == 0;
    }


}
