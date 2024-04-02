using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

[RequireComponent(typeof(Camera))]
[ExecuteInEditMode]
public class MirrorCamera : MonoBehaviour
{
    public GameObject mainCamera;
    private GameObject mirrorCamera;  
    public GameObject mirrorPlane;

    public bool flipHorizontal;
    private void Awake()
    {
        mirrorCamera = gameObject;
    }

    private void Start()
    {
        Camera camera = mirrorCamera.GetComponent<Camera>();

        Matrix4x4 mat = camera.projectionMatrix;

        mat *= Matrix4x4.Scale(new UnityEngine.Vector3(-1, 1, 1));

        camera.projectionMatrix = mat;
    }

    void Update()
    {
        Vector3 normal = mirrorPlane.transform.forward;

        // 计算主摄像机到镜子平面的距离
        float distance = Vector3.Dot(mirrorPlane.transform.position - mainCamera.transform.position, normal);

        // 计算MirrorCamera的位置
        Vector3 mirrorCameraPos = mainCamera.transform.position + normal * distance * 2f;

        // 将MirrorCamera的位置沿镜子平面的法线方向偏移
        mirrorCamera.transform.position = mirrorCameraPos;
        // 计算MirrorCamera的旋转
        Vector3 lookAtPos = mirrorCameraPos + (mainCamera.transform.position - mirrorCameraPos).normalized;
        mirrorCamera.transform.LookAt(lookAtPos, Vector3.up);

        // 将MirrorCamera的旋转沿镜子平面的法线方向翻转
        Vector3 mirrorCameraRotation = mirrorCamera.transform.rotation.eulerAngles;
        mirrorCameraRotation.y += 180f;
        mirrorCamera.transform.rotation = Quaternion.Euler(mirrorCameraRotation);
        
    }
}
