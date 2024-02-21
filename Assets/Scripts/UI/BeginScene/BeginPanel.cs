using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeginPanel : BasePanel
{
    public Button btnStart;
    public Button btnSetting;
    public Button btnAbout;
    public Button btnQuit;
    public override void Init()
    {
        btnStart.onClick.AddListener(()=>
        {
            Camera.main.GetComponent<CameraAnimator>().TurnRight(() =>
            {
                UIManager.Instance.ShowPanel<ChooseHeroPanel>();
            });
            //隐藏自己
            UIManager.Instance.HidePanel<BeginPanel>();

        });

        btnSetting.onClick.AddListener(() => 
        {
            //显示设置面板
            UIManager.Instance.ShowPanel<SettingPanel>();
        });

        btnAbout.onClick.AddListener(() => 
        {
            //关于面板
        });

        btnQuit.onClick.AddListener(() => 
        {
            Application.Quit();
        });
    }
}
