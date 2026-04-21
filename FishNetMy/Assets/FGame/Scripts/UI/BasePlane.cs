using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;


namespace FGame
{
    public class BasePlane : MonoBehaviour, IPlane, IPointerEnterHandler, IPointerExitHandler
    {
        public bool OpenLockCamera;
        public RectTransform LockCameraRotateRt;



        protected virtual void OnDisable()
        {
            // 关键：UI隐藏时强制恢复视角
            if (OpenLockCamera && GameManager.Instance != null && GameManager.Instance.inputSystem.LockLookDired)
            {
                GameManager.Instance.inputSystem.OpenLookDir();
                Cursor.visible = false;
            }
        }

        protected virtual void OnEnable()
        {
            // 可选：UI显示时确保状态正确
            if (OpenLockCamera && GameManager.Instance != null && !GameManager.Instance.inputSystem.LockLookDired)
            {
                // 如果鼠标当前在UI上，锁定视角
                if (Uitils.IsPointerOverThisUI(LockCameraRotateRt))
                {
                    GameManager.Instance.inputSystem.LockLookDir();
                    Cursor.visible = true;
                }
            }
        }

        public virtual void OnPointerEnter(PointerEventData eventData)
        {

            if (OpenLockCamera && !GameManager.Instance.inputSystem.LockLookDired)
            {   
                GameManager.Instance.inputSystem.LockLookDir();
                Cursor.visible = true;
            }
        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {
            if (OpenLockCamera && GameManager.Instance.inputSystem.LockLookDired)
            {
                GameManager.Instance.inputSystem.OpenLookDir();
                Cursor.visible = false;
            }





        }



        public void Close()
        {
            throw new NotImplementedException();
        }

        public void Init()
        {
            throw new NotImplementedException();
        }

        public void Open(Action<IPlane> action)
        {
            throw new NotImplementedException();
        }
    }
}

