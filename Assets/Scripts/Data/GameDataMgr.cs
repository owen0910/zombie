using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataMgr 
{
    private static GameDataMgr instance = new GameDataMgr();
    public static GameDataMgr Instance => instance;

    public MusicData musicData;

    //���н�ɫ������
    public List<RoleInfo> roleInfoList;

    //����������
    public PlayerData playerData;

    //��¼��ǰѡ��Ľ�ɫ���� ����֮����Ϸ�����д���
    public RoleInfo nowSelRole;

    //���г�������
    public List<SceneInfo> sceneInfoList;

    //���еĹ�������
    public List<MonsterInfo> monsterInfoList;

    //������������
    public List<TowerInfo> towerInfoList;

    private GameDataMgr()
    {
        //��ʼ������
        musicData = JsonMgr.Instance.LoadData<MusicData>("MusicData");
        //��ȡ��ɫ����
        roleInfoList = JsonMgr.Instance.LoadData<List<RoleInfo>>("RoleInfo");
        //�������
        playerData = JsonMgr.Instance.LoadData<PlayerData>("PlayerData");
        //��ȡ��������
        sceneInfoList = JsonMgr.Instance.LoadData<List<SceneInfo>>("SceneInfo");
        //��ȡ��������
        monsterInfoList = JsonMgr.Instance.LoadData<List<MonsterInfo>>("MonsterInfo");
        //��ȡ��������
        towerInfoList = JsonMgr.Instance.LoadData<List<TowerInfo>>("TowerInfo");
    }

    public void SaveMusicData()
    {
        JsonMgr.Instance.SaveData(musicData, "MusicData");
    }
    
    public void SavePlayerData()
    {
        JsonMgr.Instance.SaveData(playerData, "PlayerData");
    }

    /// <summary>
    /// ������Ч����
    /// </summary>
    /// <param name="resName"></param>
    public void PlaySound(string resName)
    {
        GameObject musicObj = new GameObject();
        AudioSource a = musicObj.AddComponent<AudioSource>();
        a.clip = Resources.Load<AudioClip>(resName);
        a.volume = musicData.soundValue;
        a.mute = !musicData.soundOpen;
        a.Play();
        GameObject.Destroy(musicObj, 1);
    }
}
