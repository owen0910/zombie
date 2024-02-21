using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterObject : MonoBehaviour
{
    //����
    private Animator animator;

    //Ѱ·���
    private NavMeshAgent agent;

    //��������
    private MonsterInfo monsterInfo;

    private int hp;
    public bool isDead = false;

    //��¼��һ�ι�����ʱ��
    private float frontTime = 0;
    private void Awake()
    {
        agent = this.GetComponent<NavMeshAgent>();
        animator = this.GetComponent<Animator>();
    }

    //��ʼ������
    public void InitInfo(MonsterInfo info)
    {
        monsterInfo = info;
        //״̬������
        animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(info.animator);
        //Ҫ���Ѫ��
        hp = info.hp;
        //�ٶȺͼ��ٶȳ�ʼ��
        agent.speed =agent.acceleration =info.moveSpeed;
        //��ת�ٶ�
        agent.angularSpeed = info.roundSpeed;

    }
    
    //����
    public void Wound(int dmg)
    {
        if (isDead)
            return;
        //����Ѫ��
        hp -= dmg;
        //�������˶���
        animator.SetTrigger("Wound");

        if (hp <= 0)
        {
            //����
            Dead();
        }
        else
        {
            //����������Ч
            //������Ч
            GameDataMgr.Instance.PlaySound("Music/Wound");
        }

    }

    //����
    public void Dead()
    {
        isDead = true;
        //ֹͣ�ƶ�
        //agent.isStopped = true;
        agent.enabled = false;
        //������������
        animator.SetBool("Dead", true);
        //������Ч
        GameDataMgr.Instance.PlaySound("Music/dead");
        //��Ǯ
        GameLevelMgr.Instance.player.AddMoney(150);
        //�Ƴ�����
    }

    //����������Ϻ����õķ���
    public void DeadEvent()
    {
        //�Ƴ�����
        //GameLevelMgr.Instance.ChangeMonsterNum(-1);
        GameLevelMgr.Instance.RemoveMonster(this);
        Destroy(this.gameObject);

        //��������ʱ�����Ϸʤ��
        if (GameLevelMgr.Instance.CheckOver())
        {
            GameOverPanel panel = UIManager.Instance.ShowPanel<GameOverPanel>();
            panel.InitInfo((int)(GameLevelMgr.Instance.player.money), true);
        }
        
       
    }
    //����������¼�
    public void BornOver()
    {
        agent.SetDestination(MainTowerObject.Instance.transform.position);

        //�����ƶ�����
        animator.SetBool("Run", true);
    }

    private void Update()
    {
        //���ʲôʱ��ͣ��������
        if (isDead)
            return;
        animator.SetBool("Run", agent.velocity != Vector3.zero);
        //��⹥��
        if (Vector3.Distance(this.transform.position,MainTowerObject.Instance.transform.position)<5&&
            Time.time-frontTime>=monsterInfo.atkOffset)
        {
            //��¼��һ�ι���ʱ��ʱ��
            frontTime = Time.deltaTime;
            animator.SetTrigger("Atk");
        }
    }

    //�˺���Χ���
    public void AtkEvent()
    {
        //�����˺����
        Collider[] colliders = Physics.OverlapSphere(this.transform.position + this.transform.forward + this.transform.up, 1, 1 << LayerMask.NameToLayer("MainTower"));
        //������Ч
        GameDataMgr.Instance.PlaySound("Music/Eat");
        for (int i = 0; i < colliders.Length; i++)
        {
            //�õ�������
            if (MainTowerObject.Instance.gameObject==colliders[i].gameObject)
            {
                MainTowerObject.Instance.Wound(monsterInfo.atk);
            }
        }
    }
}
