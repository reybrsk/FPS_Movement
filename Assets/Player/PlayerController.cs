using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
   
   [SerializeField]private float speed = 10f;
   [SerializeField]private float mass = 1f;
   [SerializeField]private float rotateSpeed = 100f;
   [SerializeField]private Camera secondCam;
   [SerializeField]private Camera mainCam;
   [SerializeField] private Animator animator;
   


   private void Start()
   {
      mainCam.enabled = true;
      secondCam.enabled = false;
   }

   private void Update()
   {
      if (!animator.GetBool("isSecondCameraActive"))
      {
         if (Input.GetAxis("Vertical") != 0f || Input.GetAxis("Horizontal") != 0f)
         {
            var vector2 = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            Move(vector2);
         }

         if (Input.GetAxis("Rotate") != 0f)
         {
            Rotate(Input.GetAxisRaw("Rotate"));
         }
      }
      if (Input.GetKeyDown(KeyCode.LeftAlt))
      {
         SecondCamera(true);
      }
      if (Input.GetKeyUp(KeyCode.LeftAlt))
      {
         SecondCamera(false);
      }
      
      
   }

   private void SecondCamera(bool onOff)
   {
      animator.SetBool("isSecondCameraActive",onOff);
      if (onOff)
      {
         secondCam.enabled = true;
         mainCam.enabled = false;
      }
      else
      {
         secondCam.enabled = false;
         mainCam.enabled = true;
      }
   }

   private void Rotate(float getAxisRaw)
   {
      transform.Rotate(0f,getAxisRaw*rotateSpeed*Time.deltaTime,0f);
   }


   private void Move(Vector2 move)
   {
      Vector3 targetMove = new Vector3(move.x, 0, move.y);
      transform.Translate(targetMove*speed*Time.deltaTime/mass);
   }
}
