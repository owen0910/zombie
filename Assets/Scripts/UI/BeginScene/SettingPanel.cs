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
        //��ʼ�������ʾ�����ݣ����ݱ��ش洢������
        MusicData data = GameDataMgr.Instance.musicData;

        togMusic.isOn = data.musicOpen;
        togSound.isOn = data.soundOpen;
        sliderMusic.value = data.musicValue;
        sliderSound.value = data.soundValue;

        btnClose.onClick.AddListener(() =>
        {
            //�򱾵ش洢����
            GameDataMgr.Instance.SaveMusicData();
            UIManager.Instance.HidePanel<SettingPanel>();

        });

        togMusic.onValueChanged.AddListener((v) =>
        {
            //���ñ�������
            BKMusic.Instance.SetIsOpen(v);
            //��¼����
            GameDataMgr.Instance.musicData.musicOpen = v;

        });

        togSound.onValueChanged.AddListener((v) =>
        {
            //��¼����
            GameDataMgr.Instance.musicData.soundOpen = v;
        });

        sliderMusic.onValueChanged.AddListener((v) =>
        {
            //�ı䱳�����ִ�С
            BKMusic.Instance.ChangeValue(v);
            GameDataMgr.Instance.musicData.musicValue = v;
        });

        sliderSound.onValueChanged.AddListener((v) =>
        {
            //�ı䱳����Ч��С
            
            GameDataMgr.Instance.musicData.soundValue = v;
        });
    }

    
}
