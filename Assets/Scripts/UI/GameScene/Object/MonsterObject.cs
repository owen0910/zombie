using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterObject : MonoBehaviour
{
    //动画
    private Animator animator;

    //寻路组件
    private NavMeshAgent agent;

    //怪物数据
    private MonsterInfo monsterInfo;

    private int hp;
    public bool isDead = false;

    //记录上一次攻击的时间
    private float frontTime = 0;
    private void Awake()
    {
        agent = this.GetComponent<NavMeshAgent>();
        animator = this.GetComponent<Animator>();
    }

    //初始化怪物
    public void InitInfo(MonsterInfo info)
    {
        monsterInfo = info;
        //状态机加载
        animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(info.animator);
        //要变的血量
        hp = info.hp;
        //速度和加速度初始化
        agent.speed =agent.acceleration =info.moveSpeed;
        //旋转速度
        agent.angularSpeed = info.roundSpeed;

    }
    
    //受伤
    public void Wound(int dmg)
    {
        if (isDead)
            return;
        //减少血量
        hp -= dmg;
        //播放受伤动画
        animator.SetTrigger("Wound");

        if (hp <= 0)
        {
            //死亡
            Dead();
        }
        else
        {
            //播放受伤音效
            //播放音效
            GameDataMgr.Instance.PlaySound("Music/Wound");
        }

    }

    //死亡
    public void Dead()
    {
        isDead = true;
        //停止移动
        //agent.isStopped = true;
        agent.enabled = false;
        //播放死亡动画
        animator.SetBool("Dead", true);
        //播放音效
        GameDataMgr.Instance.PlaySound("Music/dead");
        //加钱
        GameLevelMgr.Instance.player.AddMoney(150);
        //移除对象
    }

    //死亡播放完毕后会调用的方法
    public void DeadEvent()
    {
        //移除对象
        //GameLevelMgr.Instance.ChangeMonsterNum(-1);
        GameLevelMgr.Instance.RemoveMonster(this);
        Destroy(this.gameObject);

        //怪物死亡时检测游戏胜利
        if (GameLevelMgr.Instance.CheckOver())
        {
            GameOverPanel panel = UIManager.Instance.ShowPanel<GameOverPanel>();
            panel.InitInfo((int)(GameLevelMgr.Instance.player.money), true);
        }
        
       
    }
    //出生过后的事件
    public void BornOver()
    {
        agent.SetDestination(MainTowerObject.Instance.transform.position);

        //播放移动动画
        animator.SetBool("Run", true);
    }

    private void Update()
    {
        //检测什么时候停下来攻击
        if (isDead)
            return;
        animator.SetBool("Run", agent.velocity != Vector3.zero);
        //检测攻击
        if (Vector3.Distance(this.transform.position,MainTowerObject.Instance.transform.position)<5&&
            Time.time-frontTime>=monsterInfo.atkOffset)
        {
            //记录这一次攻击时的时间
            frontTime = Time.deltaTime;
            animator.SetTrigger("Atk");
        }
    }

    //伤害范围检测
    public void AtkEvent()
    {
        //进行伤害检测
        Collider[] colliders = Physics.OverlapSphere(this.transform.position + this.transform.forward + this.transform.up, 1, 1 << LayerMask.NameToLayer("MainTower"));
        //播放音效
        GameDataMgr.Instance.PlaySound("Music/Eat");
        for (int i = 0; i < colliders.Length; i++)
        {
            //得到保护区
            if (MainTowerObject.Instance.gameObject==colliders[i].gameObject)
            {
                MainTowerObject.Instance.Wound(monsterInfo.atk);
            }
        }
    }
}
