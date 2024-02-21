using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataMgr 
{
    private static GameDataMgr instance = new GameDataMgr();
    public static GameDataMgr Instance => instance;

    public MusicData musicData;

    //所有角色的数据
    public List<RoleInfo> roleInfoList;

    //玩家相关数据
    public PlayerData playerData;

    //记录当前选择的角色数据 用于之后游戏场景中创建
    public RoleInfo nowSelRole;

    //所有场景数据
    public List<SceneInfo> sceneInfoList;

    //所有的怪物数据
    public List<MonsterInfo> monsterInfoList;

    //所有塔的数据
    public List<TowerInfo> towerInfoList;

    private GameDataMgr()
    {
        //初始化数据
        musicData = JsonMgr.Instance.LoadData<MusicData>("MusicData");
        //读取角色数据
        roleInfoList = JsonMgr.Instance.LoadData<List<RoleInfo>>("RoleInfo");
        //玩家数据
        playerData = JsonMgr.Instance.LoadData<PlayerData>("PlayerData");
        //读取场景数据
        sceneInfoList = JsonMgr.Instance.LoadData<List<SceneInfo>>("SceneInfo");
        //读取怪物数据
        monsterInfoList = JsonMgr.Instance.LoadData<List<MonsterInfo>>("MonsterInfo");
        //读取塔的数据
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
    /// 播放音效方法
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
