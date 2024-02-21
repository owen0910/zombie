using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class BasePanel : MonoBehaviour
{
    //����͸����
    private CanvasGroup canvasGroup;
    //���뵭�����ٶ�
    private float alphaSpeed = 10;
    //�Ƿ���ʾ
    public bool isShow = false;
    //��������Ϻ���Ҫ������
    private UnityAction hideCallBack = null;

    protected virtual void Awake()
    {
        //��ȡ����ϵ����
        canvasGroup = this.GetComponent<CanvasGroup>();
        //������Ǽ���
        if (canvasGroup==null)
        {
            canvasGroup = this.gameObject.AddComponent<CanvasGroup>();
        }
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        Init();
    }

    /// <summary>
    /// ע��ؼ��¼��ķ��� ������嶼Ҫʵ��
    /// </summary>
    public abstract void Init();

    public virtual void ShowME()
    {
        canvasGroup.alpha = 0;
        isShow = true;
    }

    public virtual void HideMe(UnityAction callBack)
    {
        canvasGroup.alpha = 1;
        isShow = false;
        hideCallBack = callBack;
    }

    // Update is called once per frame
    
    protected virtual void Update()
    {
        //����
        if (isShow&&canvasGroup.alpha!=1)
        {
            canvasGroup.alpha += alphaSpeed * Time.deltaTime;
            if (canvasGroup.alpha>=1)
            {
                canvasGroup.alpha = 1;
            }
        }
        //����
        else if (!isShow&&canvasGroup.alpha!=0)
        {
            canvasGroup.alpha -= alphaSpeed * Time.deltaTime;
            if (canvasGroup.alpha <=0)
            {
                canvasGroup.alpha = 0;
                //������ɺ���ȥִ�е��߼�
                hideCallBack?.Invoke();
            }
        }
    }
}
