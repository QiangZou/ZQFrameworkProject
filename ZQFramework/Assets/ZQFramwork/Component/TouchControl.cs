using UnityEngine;
using System.Collections;
using System;

namespace ZQFramwork
{
    public class TouchControl : MonoBehaviour
    {
        public delegate void TouchHandler(TouchDirection touchDirection);

        public enum TouchState
        {
            Began = 0,
            Moved = 1,
            End = 2
        }

        /// <summary>
        /// 触摸方向
        /// </summary>
        public enum TouchDirection
        {
            Null = 0,
            Up,
            Down,
            Left,
            Right,
        }

        /// <summary>
        /// 滑动方法
        /// </summary>
        public enum TouchMethod
        {
            /// <summary>
            /// 完整的滑动(通过按下点和抬起点来确定滑动方向)
            /// </summary>
            Complete,
            /// <summary>
            /// 轻松的滑动(通过按下后滑动固定距离来确定滑动方向)
            /// </summary>
            Easy,
        }

        public TouchHandler touchDelegate;

        public TouchMethod touchMethod;

        public TouchState touchState;


        public Vector2 beginPosition;
        public Vector2 endPosition;

        public float distance;

        public void SetTouchDelegate(TouchMethod touchMethod, TouchHandler touchDelegate)
        {
            this.touchDelegate = touchDelegate;
            this.touchMethod = touchMethod;
        }


        void Start()
        {

            distance = Screen.width / 10;

        }

        void Update()
        {
            if (touchDelegate == null)
            {
                return;
            }

            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            {
                touchDelegate(TouchDirection.Up);
            }

            if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            {
                touchDelegate(TouchDirection.Down);
            }

            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                touchDelegate(TouchDirection.Left);
            }

            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                touchDelegate(TouchDirection.Right);
            }

            if (Input.GetMouseButtonDown(0))
            {
                SwitchTouchState(TouchState.Began);
            }

            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                SwitchTouchState(TouchState.Began);
            }


            if (Input.GetMouseButtonUp(0))
            {
                SwitchTouchState(TouchState.End);
            }

            if (Input.touchCount > 0 && (Input.GetTouch(0).phase == TouchPhase.Ended || Input.GetTouch(0).phase == TouchPhase.Canceled))
            {

                SwitchTouchState(TouchState.End);
            }

            if (touchState == TouchState.Moved)
            {
                Moved();
            }
        }




        void SwitchTouchState(TouchState touchState)
        {
            this.touchState = touchState;
            switch (touchState)
            {
                case TouchState.Began:
                    Began();
                    break;
                case TouchState.Moved:
                    break;
                case TouchState.End:
                    break;
                default:
                    break;
            }
        }

        Vector2 GetTouchPosition()
        {
            Vector2 position = Vector2.zero;

            if (Input.touchCount > 0)
            {
                position = Input.GetTouch(0).position;
            }
            else
            {
                position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            }

            return position;
        }

        void Began()
        {
            beginPosition = GetTouchPosition();

            SwitchTouchState(TouchState.Moved);
        }

        void Moved()
        {
            endPosition = GetTouchPosition();


            if (touchMethod == TouchMethod.Easy)
            {
                if (Vector2.Distance(beginPosition, endPosition) > distance)
                {
                    ReturnEvent();

                    SwitchTouchState(TouchState.End);
                }
            }
        }

        void End()
        {
            endPosition = GetTouchPosition();

            if (touchMethod == TouchMethod.Complete)
            {
                ReturnEvent();
            }
        }

        void ReturnEvent()
        {
            double angle = GetAngle(beginPosition, endPosition);

            TouchDirection TouchDirection = GetTouchDirection(angle);

            if (touchDelegate != null)
            {
                touchDelegate(TouchDirection);
            }
        }

        double GetAngle(Vector2 origin, Vector2 target)
        {
            double angle = Math.Atan2((target.y - origin.y), (target.x - origin.x)) * 180 / Math.PI;

            return angle;
        }

        TouchDirection GetTouchDirection(double angle)
        {
            if (angle >= -45 && angle < 45)
            {
                return TouchDirection.Right;
            }
            else if (angle >= 45 && angle < 135)
            {
                return TouchDirection.Up;
            }
            else if (angle >= -135 && angle < -45)
            {
                return TouchDirection.Down;
            }
            else if (angle >= 135 || angle < -135)
            {
                return TouchDirection.Left;
            }
            else
            {
                return TouchDirection.Null;
            }

        }








    }



}
