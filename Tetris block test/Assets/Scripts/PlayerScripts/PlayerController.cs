using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TetrisRunnerSpace
{
    namespace PlayerSpace
    {
        public class PlayerController : MonoBehaviour, IRotatable, ICameraControllable
        {
            Vector3 _startTouchPos;
            Vector3 _endTouchPos;       
            public Transform Position => transform;

            void Update()
            {
                ControlPlayer();              
            }

            void ControlPlayer()
            {
#if UNITY_EDITOR
                if (Input.GetMouseButtonDown(0))
                    _startTouchPos = Input.mousePosition;
                if (Input.GetMouseButtonUp(0))
                {
                    _endTouchPos = Input.mousePosition;

                    Vector3 dirToRot = (Vector3.right * (_endTouchPos.x - _startTouchPos.x)).normalized;
                    float yAxisRotation = 90 * dirToRot.x;
                    Rotate(yAxisRotation);
                }

#else
               if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
                    _startTouchPos = Input.GetTouch(0).position;
                
                if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
                    _endTouchPos = Input.GetTouch(0).position;
                {
                    Vector3 dirToRot = (Vector3.right * (_endTouchPos.x - _startTouchPos.x)).normalized;
                    float yAxisRotation = 90 * dirToRot.x;
                    Rotate(yAxisRotation);
                }
#endif
            }


            public void Rotate(float angle)
            {
                transform.rotation = Quaternion.AngleAxis(transform.eulerAngles.y + angle, Vector3.up);
                //transform.Rotate(0, transform.eulerAngles.y + angle, 0, Space.Self); //rotate
            }          
        }
    }
}
