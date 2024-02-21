using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CameraMove : MonoBehaviour
{
    //����������Ŀ���
    public Transform target;
    //����������Ŀ���ƫ��λ��
    public Vector3 offsetPos;

    public float bodyHeight;

    //�ƶ�����ת�ٶ�
    public float moveSpeed;
    public float rotationSpeed;

    private Vector3 targetPos;
    private Quaternion targetRotation;

  
    // Update is called once per frame
    void Update()
    {
        //����Ŀ������������������λ�úͽǶ�
        if (target == null)
            return;
        //���ƫ��z����
        targetPos = target.position + target.forward * offsetPos.z;
        //����ƫ��y����
        targetPos += Vector3.up * offsetPos.y;
        //����ƫ��x����
        targetPos += target.right * offsetPos.x;
        //��ֵ�������������Ŀ��㿿£
        this.transform.position = Vector3.Lerp(this.transform.position, targetPos, moveSpeed * Time.deltaTime);

        //��ת�ļ���
        targetRotation = Quaternion.LookRotation(target.position + Vector3.up * bodyHeight - this.transform.position);
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    public void SetTarget(Transform player)
    {
        target = player;
    }
}
