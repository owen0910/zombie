using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BKMusic : MonoBehaviour
{
    private static BKMusic instance;

    public static BKMusic Instance => instance;

    private AudioSource bkSource;

    private void Awake()
    {
        instance = this;
        bkSource = this.GetComponent<AudioSource>();

        //ͨ���������������ִ�С�Ϳ���
        MusicData data = GameDataMgr.Instance.musicData;
        SetIsOpen(data.musicOpen);
        ChangeValue(data.musicValue);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //���ر������ֵķ���
    public void SetIsOpen(bool isOpen)
    {
        bkSource.mute = !isOpen;
    }

    //�����������ִ�С
    public void ChangeValue(float v)
    {
        bkSource.volume = v;
    }

}
