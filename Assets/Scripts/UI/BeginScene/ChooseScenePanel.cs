using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChooseScenePanel : BasePanel
{
    public Button btnLeft;
    public Button btnRight;
    public Button btnStart;
    public Button btnBack;

    public Text txtInfo;
    public Image imgScene;

    //��ǰ��������
    private int nowIndex;
    //��ǰ��������
    private SceneInfo nowSceneInfo;

    public override void Init()
    {
        btnLeft.onClick.AddListener(() =>
        {
            --nowIndex;
            if (nowIndex < 0)
                nowIndex = GameDataMgr.Instance.sceneInfoList.Count - 1;
            ChangeScene();
        });

        btnRight.onClick.AddListener(() =>
        {
            ++nowIndex;
            if (nowIndex >= GameDataMgr.Instance.sceneInfoList.Count - 1)
                nowIndex = 0;
            ChangeScene();
        });

        btnStart.onClick.AddListener(() =>
        {
            //�����Լ�
            UIManager.Instance.HidePanel<ChooseScenePanel>();
            //�л�����
            AsyncOperation ao = SceneManager.LoadSceneAsync(nowSceneInfo.sceneName);
            //�ؿ���ʼ��
            ao.completed += (obj) =>
            {
                GameLevelMgr.Instance.InitInfo(nowSceneInfo);
            };
            

        });

        btnBack.onClick.AddListener(() =>
        {
            //�����Լ�
            UIManager.Instance.HidePanel<ChooseScenePanel>();
            //����ѡ���ɫ����
            UIManager.Instance.ShowPanel<ChooseHeroPanel>();
        });
        //һ�����ҲҪ��ʼ��һ��
        ChangeScene();
    }

    //�л�������ʾ�ĳ�����Ϣ
    public void ChangeScene()
    {
        nowSceneInfo = GameDataMgr.Instance.sceneInfoList[nowIndex];
        //����ͼƬ��Ϣ
        imgScene.sprite = Resources.Load<Sprite>(nowSceneInfo.imgRes);
        txtInfo.text = "����:\n" + nowSceneInfo.name + "\n" + "����:\n" + nowSceneInfo.tips;
    }
    
}
