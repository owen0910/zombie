using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    private static UIManager instance = new UIManager();
    public static UIManager Instance => instance;

    //��ʾ���ʹ����ֵ�
    //�������ͻ�ȡ�ֵ��е�����������
    private Dictionary<string, BasePanel> panelDic = new Dictionary<string, BasePanel>();

    private Transform canvasTrans;

    private UIManager()
    {
        //�õ������е�canvas����
        GameObject canvas = GameObject.Instantiate(Resources.Load<GameObject>("UI/Canvas"));
        canvasTrans = canvas.transform;
        //���������Ƴ�
        GameObject.DontDestroyOnLoad(canvas);
    }

    //��ʾ���
    public T ShowPanel<T>()where T:BasePanel
    {
        //��Ҫ��֤����T�����ͺ����Ԥ��������һ��
        string panelName = typeof(T).Name;

        //�ж��ֵ����Ƿ��Ѿ���ʾ�����
        if (panelDic.ContainsKey(panelName))
            return panelDic[panelName] as T;

        //��ʾ��� ��̬����Ԥ����
        GameObject panelObj = GameObject.Instantiate(Resources.Load<GameObject>("UI/" + panelName));
        panelObj.transform.SetParent(canvasTrans, false);
        //ִ������ϵ���ʾ�߼����������ֵ�
        T panel = panelObj.GetComponent<T>();
        panelDic.Add(panelName, panel);
        panel.ShowME();
        return panel;
    }

    //�������
    public void HidePanel<T>(bool isFade=true) where T:BasePanel
    {
        string panelName = typeof(T).Name;
        //�жϵ�ǰ��ʾ���������û��Ҫ���ص�
        if (panelDic.ContainsKey(panelName))
        {
            if (isFade)
            {
                panelDic[panelName].HideMe(() =>
                {
                    GameObject.Destroy(panelDic[panelName].gameObject);
                    //ɾ���ֵ��е����
                    panelDic.Remove(panelName);
                });
            }
            else
            {
                GameObject.Destroy(panelDic[panelName].gameObject);
                //ɾ���ֵ��е����
                panelDic.Remove(panelName);
            }
            
        }
    }
    //�õ����
    public T GetPanel<T>()where T:BasePanel
    {
        string panelName = typeof(T).Name;
        if (panelDic.ContainsKey(panelName))
            return panelDic[panelName] as T;

        return null;
    }
}

