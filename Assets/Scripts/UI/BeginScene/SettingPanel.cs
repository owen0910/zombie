using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingPanel : BasePanel
{
    public Button btnClose;
    public Toggle togMusic;
    public Toggle togSound;
    public Slider sliderMusic;
    public Slider sliderSound;
    public override void Init()
    {
        //初始化面板显示的内容，根据本地存储的数据
        MusicData data = GameDataMgr.Instance.musicData;

        togMusic.isOn = data.musicOpen;
        togSound.isOn = data.soundOpen;
        sliderMusic.value = data.musicValue;
        sliderSound.value = data.soundValue;

        btnClose.onClick.AddListener(() =>
        {
            //向本地存储数据
            GameDataMgr.Instance.SaveMusicData();
            UIManager.Instance.HidePanel<SettingPanel>();

        });

        togMusic.onValueChanged.AddListener((v) =>
        {
            //设置背景音乐
            BKMusic.Instance.SetIsOpen(v);
            //记录数据
            GameDataMgr.Instance.musicData.musicOpen = v;

        });

        togSound.onValueChanged.AddListener((v) =>
        {
            //记录数据
            GameDataMgr.Instance.musicData.soundOpen = v;
        });

        sliderMusic.onValueChanged.AddListener((v) =>
        {
            //改变背景音乐大小
            BKMusic.Instance.ChangeValue(v);
            GameDataMgr.Instance.musicData.musicValue = v;
        });

        sliderSound.onValueChanged.AddListener((v) =>
        {
            //改变背景音效大小
            
            GameDataMgr.Instance.musicData.soundValue = v;
        });
    }

    
}
