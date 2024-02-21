using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CameraMove : MonoBehaviour
{
    //摄像机看向的目标点
    public Transform target;
    //摄像机相对于目标的偏移位置
    public Vector3 offsetPos;

    public float bodyHeight;

    //移动和旋转速度
    public float moveSpeed;
    public float rotationSpeed;

    private Vector3 targetPos;
    private Quaternion targetRotation;

  
    // Update is called once per frame
    void Update()
    {
        //根据目标对象来计算摄像机的位置和角度
        if (target == null)
            return;
        //向后偏移z坐标
        targetPos = target.position + target.forward * offsetPos.z;
        //向上偏移y坐标
        targetPos += Vector3.up * offsetPos.y;
        //左右偏移x坐标
        targetPos += target.right * offsetPos.x;
        //插值运算让摄像机向目标点靠拢
        this.transform.position = Vector3.Lerp(this.transform.position, targetPos, moveSpeed * Time.deltaTime);

        //旋转的计算
        targetRotation = Quaternion.LookRotation(target.position + Vector3.up * bodyHeight - this.transform.position);
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    public void SetTarget(Transform player)
    {
        target = player;
    }
}
