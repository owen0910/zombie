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

    //更新血量
    public void UpdateHp(int hp,int maxHp)
    {
        this.hp = hp;
        this.maxHp = maxHp;
        //更新界面上血量的显示
        UIManager.Instance.GetPanel<GamePanel>().UpdateTowerHp(hp, maxHp);
    }

    //受到伤害
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
            //游戏结束
            GameOverPanel panel = UIManager.Instance.ShowPanel<GameOverPanel>();
            panel.InitInfo((int)(GameLevelMgr.Instance.player.money * 0.5f), false);
        }
        UpdateHp(hp, maxHp);
    }

    //被别人快速获取到位置

    //过场景删除
    private void OnDestroy()
    {
        instance = null;
    }
}
