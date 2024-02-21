using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class BasePanel : MonoBehaviour
{
    //控制透明度
    private CanvasGroup canvasGroup;
    //淡入淡出的速度
    private float alphaSpeed = 10;
    //是否显示
    public bool isShow = false;
    //当隐藏完毕后想要做的事
    private UnityAction hideCallBack = null;

    protected virtual void Awake()
    {
        //获取面板上的组件
        canvasGroup = this.GetComponent<CanvasGroup>();
        //如果忘记加了
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
    /// 注册控件事件的方法 所有面板都要实现
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
        //淡入
        if (isShow&&canvasGroup.alpha!=1)
        {
            canvasGroup.alpha += alphaSpeed * Time.deltaTime;
            if (canvasGroup.alpha>=1)
            {
                canvasGroup.alpha = 1;
            }
        }
        //淡出
        else if (!isShow&&canvasGroup.alpha!=0)
        {
            canvasGroup.alpha -= alphaSpeed * Time.deltaTime;
            if (canvasGroup.alpha <=0)
            {
                canvasGroup.alpha = 0;
                //淡出完成后再去执行的逻辑
                hideCallBack?.Invoke();
            }
        }
    }
}
