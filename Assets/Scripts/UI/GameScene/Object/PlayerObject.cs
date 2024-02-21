using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObject : MonoBehaviour
{
    private Animator animator;

    private int atk;
    public int money;

    // ��ת�ٶ�
    private float roundSpeed = 50;

    //��ǹ����Ŀ����
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
        //�ƶ��仯
        animator.SetFloat("VSpeed", Input.GetAxis("Vertical"));
        animator.SetFloat("HSpeed", Input.GetAxis("Horizontal"));
        //��ת
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
    /// �����¼�
    /// </summary>
    public void KnifeEvent()
    {
        //�����˺����
        Collider[] colliders= Physics.OverlapSphere(this.transform.position + this.transform.forward + this.transform.up, 1, 1 << LayerMask.NameToLayer("Monster"));

        //������Ч
        GameDataMgr.Instance.PlaySound("Music/Knife");
        for (int i = 0; i < colliders.Length; i++)
        {
            //�õ���ײ����Ĺ���ű�����������
            MonsterObject monster= colliders[i].gameObject.GetComponent<MonsterObject>();
            if (monster!=null&&!monster.isDead)
            {
                monster.Wound(this.atk);
                break;
            }
        }
    }
    /// <summary>
    /// ǹ���¼�
    /// </summary>
    public void ShootEvent()
    {
        RaycastHit[] hits = Physics.RaycastAll(new Ray(gunPoint.position, this.transform.forward), 1000, 1 << LayerMask.NameToLayer("Monster"));

        //������Ч
        GameDataMgr.Instance.PlaySound("Music/Gun");
        for (int i = 0; i < hits.Length; i++)
        {
            
            //�õ���ײ����Ĺ���ű�����������
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

    //�ṩ���ⲿ��Ǯ�ķ���
    public void AddMoney(int money)
    {
        this.money += money;
        UpdateMoney();
    }

}
