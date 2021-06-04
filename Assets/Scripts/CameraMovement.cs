using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Camera camera;
    [SerializeField] private Vector3 cameraOffset;
    [SerializeField] private Vector3 cameraRotation;
    [SerializeField] private Transform target;

    private void Start()
    {
        camera = Camera.main;
    }

    private void Update()
    {
        camera.transform.position = target.position + cameraOffset;
        Quaternion t = new Quaternion();
        t.eulerAngles = cameraRotation;
        camera.transform.rotation = t;
    }
}