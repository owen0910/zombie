using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainTowerObject : MonoBehaviour
{
    private int hp;
    private int maxHp;

    private bool isDead;

    private static MainTowerObject instance;
    public static MainTowerObject Instance => instance;
   

    private void Awake()
    {
        instance = this;
    }

    //����Ѫ��
    public void UpdateHp(int hp,int maxHp)
    {
        this.hp = hp;
        this.maxHp = maxHp;
        //���½�����Ѫ������ʾ
        UIManager.Instance.GetPanel<GamePanel>().UpdateTowerHp(hp, maxHp);
    }

    //�ܵ��˺�
    public void Wound(int dmg)
    {
        if (isDead)
        {
            return;
        }
        hp -= dmg;
        if (hp<=0)
        {
            hp = 0;
            isDead = true;
            //��Ϸ����
            GameOverPanel panel = UIManager.Instance.ShowPanel<GameOverPanel>();
            panel.InitInfo((int)(GameLevelMgr.Instance.player.money * 0.5f), false);
        }
        UpdateHp(hp, maxHp);
    }

    //�����˿��ٻ�ȡ��λ��

    //������ɾ��
    private void OnDestroy()
    {
        instance = null;
    }
}
