using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObject : MonoBehaviour
{
    private Animator animator;

    private int atk;
    public int money;

    // 旋转速度
    private float roundSpeed = 50;

    //持枪对象的开火点
    public Transform gunPoint;



    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();
    }

    public void InitPlayerInfo(int atk,int money)
    {
        this.atk = atk;
        this.money = money;
        UpdateMoney();

    }
    // Update is called once per frame
    void Update()
    {
        //移动变化
        animator.SetFloat("VSpeed", Input.GetAxis("Vertical"));
        animator.SetFloat("HSpeed", Input.GetAxis("Horizontal"));
        //旋转
        this.transform.Rotate(Vector3.up, Input.GetAxis("Mouse X") * roundSpeed * Time.deltaTime);
        
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            animator.SetLayerWeight(1, 1);
        }
        else if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            animator.SetLayerWeight(1, 0);
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            animator.SetTrigger("Roll");
        }

        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("Fire");
        }

        
    }

    /// <summary>
    /// 刀的事件
    /// </summary>
    public void KnifeEvent()
    {
        //进行伤害检测
        Collider[] colliders= Physics.OverlapSphere(this.transform.position + this.transform.forward + this.transform.up, 1, 1 << LayerMask.NameToLayer("Monster"));

        //播放音效
        GameDataMgr.Instance.PlaySound("Music/Knife");
        for (int i = 0; i < colliders.Length; i++)
        {
            //得到碰撞对象的怪物脚本。让其受伤
            MonsterObject monster= colliders[i].gameObject.GetComponent<MonsterObject>();
            if (monster!=null&&!monster.isDead)
            {
                monster.Wound(this.atk);
                break;
            }
        }
    }
    /// <summary>
    /// 枪的事件
    /// </summary>
    public void ShootEvent()
    {
        RaycastHit[] hits = Physics.RaycastAll(new Ray(gunPoint.position, this.transform.forward), 1000, 1 << LayerMask.NameToLayer("Monster"));

        //播放音效
        GameDataMgr.Instance.PlaySound("Music/Gun");
        for (int i = 0; i < hits.Length; i++)
        {
            
            //得到碰撞对象的怪物脚本。让其受伤
            MonsterObject monster = hits[i].collider.gameObject.GetComponent<MonsterObject>();
            if (monster != null && !monster.isDead)
            {
                GameObject effObj = Instantiate(Resources.Load<GameObject>(GameDataMgr.Instance.nowSelRole.hitEff));
                effObj.transform.position = hits[i].point;
                effObj.transform.rotation = Quaternion.LookRotation(hits[i].normal);
                Destroy(effObj, 1);

                monster.Wound(this.atk);
                break;
            }
        }
    }

    public void UpdateMoney()
    {
        UIManager.Instance.GetPanel<GamePanel>().UpdateMoney(money);
    }

    //提供给外部加钱的方法
    public void AddMoney(int money)
    {
        this.money += money;
        UpdateMoney();
    }

}
