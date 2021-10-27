using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{

    public enum TargetCamera
    {
        MainCamera,
        SecondCamera
    }

    public TargetCamera targetCamera;
    [SerializeField] private float xAngleMax;
    [SerializeField] private float yAngleMax;
    [SerializeField] private float sens;
    [SerializeField,Range(0f,1f)] private float bodyRotateSens;
    
    public bool isActive;
    private float _rotationX = 0F;
    private float _rotationY = 0F;
    private Quaternion _originalRotation;
    private float reactionZone = 0.5f;
    
    
    
    

    private void Start()
    {
        _originalRotation = transform.localRotation;
    }

    private void Update()
    {
        if (isActive)
        {
            _rotationX += Input.GetAxis("Mouse X") * sens;
            Debug.Log(Input.GetAxis("Mouse X") + "inputValue");
            
            _rotationY += Input.GetAxis("Mouse Y") * sens;
            _rotationX = ClampAngle (_rotationX, -xAngleMax, xAngleMax);
            Debug.Log("rotatoinx" + _rotationX);
            
            
            _rotationY = ClampAngle (_rotationY, -yAngleMax, yAngleMax);
            Quaternion xQuaternion = Quaternion.AngleAxis (_rotationX, Vector3.up);
            Quaternion yQuaternion = Quaternion.AngleAxis (_rotationY, -Vector3.right);
            transform.localRotation = _originalRotation * xQuaternion * yQuaternion;
        }

        if (targetCamera == TargetCamera.MainCamera)
        {
            if (Math.Abs(_rotationX) > (xAngleMax - reactionZone))
            {
                gameObject.GetComponentInParent<PlayerController>().Rotate(Input.GetAxisRaw("Mouse X")*sens*bodyRotateSens);
            }
        }
    }

    private float ClampAngle(float angle, float minimumX, float maximumX)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp (angle, minimumX, maximumX);
    }
}
