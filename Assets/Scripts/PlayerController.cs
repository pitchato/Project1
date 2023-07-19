using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 direction;
    public float forwardSpeed;
    private int desiredLane = 1;//0:left 1:middle 2:right
    public float laneDistance = 4;//the distance between two lanes
    private bool fingerDown;
    
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (fingerDown == false && Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            direction = Input.touches[0].position;
            fingerDown = true;
        }
            direction.z = forwardSpeed;
        if (Input.GetKeyDown(KeyCode.RightArrow) || (Input.touches[0].position.x >= direction.x + desiredLane))
        {
            fingerDown = false;
            desiredLane++;
            if(desiredLane == 3)
            desiredLane = 2;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) || (Input.touches[0].position.x <= direction.x - desiredLane))
        {
            fingerDown = false;
            desiredLane--;
            if(desiredLane == -1)
            desiredLane = 0;
        }
        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;
        if (desiredLane == 0)
        {
            targetPosition += Vector3.left * laneDistance;
        }else if (desiredLane == 2)
        {
            targetPosition += Vector3.right * laneDistance;
        }
        transform.position = Vector3.Lerp(transform.position,targetPosition,80*Time.deltaTime);
    }
    private void FixedUpdate()
    {
        controller.Move(direction*Time.fixedDeltaTime);
    }
}
//Vector3 startPos;
//float minSwipeDistX, minSwipeDistY;
//bool isJump = false;

//void Start()
//{
    //minSwipeDistX = minSwipeDistY = Screen.width / 6;
//}

//bool isSwipe = false;
//bool isTouch = false;
//void Update()
//{
  //  if (Input.touchCount > 0)
    //{
      //  Touch touch = Input.touches[0];
        //switch (touch.phase)
        //{
          //  case TouchPhase.Began:
          //      startPos = touch.position;
           //     break;
           // case TouchPhase.Moved:
             //   isTouch = true;
               // float swipeDistHorizontal = (new Vector3(touch.position.x, 0, 0) - new Vector3(startPos.x, 0, 0)).magnitude;
               // float swipeDistVertical = (new Vector3(0, touch.position.y, 0) - new Vector3(0, startPos.y, 0)).magnitude;
               // if (swipeDistHorizontal > minSwipeDistX)
               // {
                 //   float swipeValue = Mathf.Sign(touch.position.x - startPos.x);
                   // if (swipeValue > 0 && !isSwipe)//to right swipe
                   // {
                     //   isTouch = false;
                       // isSwipe = true;
                      //  Debug.Log("Right");
                   // }
                   // else if (swipeValue < 0 && !isSwipe)//to left swipe
                   // {
                      //  isTouch = false;
                      //  isSwipe = true;
                    //    Debug.Log("Left");
                   // }
               // }
                // add swipe to up
               // if (swipeDistVertical > minSwipeDistY)
               // {
                 //   float swipeValueY = Mathf.Sign(touch.position.y - startPos.y);
                  //  if (swipeValueY > 0 && !isSwipe)
                   // {
                     //   isTouch = false;
                      //  isSwipe = true;
                      //  Debug.Log("Up");
                   // }
               // }
              //  break;
          //  case TouchPhase.Stationary:
            //    isJump = true;
              //  break;
           // case TouchPhase.Ended:
           // case TouchPhase.Canceled:
             //   isTouch = false;
              //  isSwipe = false;
        //        break;
    //    }
  //  }
//}

//void FixedUpdate()
//{
  //  if (isJump && !isTouch)
    //{
      //  Debug.Log("Tap");
       // isJump = false;
   // }
//}