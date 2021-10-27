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
         mainCam.enabled = false;
         mainCam.gameObject.GetComponent<MouseLook>().isActive = false;
         secondCam.enabled = true;
         secondCam.gameObject.GetComponent<MouseLook>().isActive = true;
      }
      else
      {
         secondCam.enabled = false;
         secondCam.gameObject.GetComponent<MouseLook>().isActive = false;
         mainCam.enabled = true;
         mainCam.gameObject.GetComponent<MouseLook>().isActive = true;

      }
   }

   public void Rotate(float angle)
   {
      transform.Rotate(0f,angle*rotateSpeed*Time.deltaTime,0f);
   }


   private void Move(Vector2 move)
   {
      Vector3 targetMove = new Vector3(move.x, 0, move.y);
      transform.Translate(targetMove*speed*Time.deltaTime/mass);
   }
}
