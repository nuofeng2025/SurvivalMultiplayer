using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

namespace FGame
{
    public class AroundPlane : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public RectTransform LockCameraRotateRt;

        // Start is called before the first frame update
        void Start()
        {

        }

        private void OnDisable()
        {
            // 关键：UI隐藏时强制恢复视角
            if (GameManager.Instance != null && GameManager.Instance.inputSystem.LockLookDired)
            {
                GameManager.Instance.inputSystem.OpenLookDir();
            }
        }

        private void OnEnable()
        {
            // 可选：UI显示时确保状态正确
            if (GameManager.Instance != null && !GameManager.Instance.inputSystem.LockLookDired)
            {
                // 如果鼠标当前在UI上，锁定视角
                if (IsPointerOverThisUI())
                {
                    GameManager.Instance.inputSystem.LockLookDir();
                }
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {

            if (!GameManager.Instance.inputSystem.LockLookDired)
            { GameManager.Instance.inputSystem.LockLookDir(); }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (GameManager.Instance.inputSystem.LockLookDired)
            { GameManager.Instance.inputSystem.OpenLookDir(); }               
        }


        private bool IsPointerOverThisUI()
        {
            // 简单检测鼠标是否在当前UI上
            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                GetComponent<RectTransform>(),
                Input.mousePosition,
                null,
                out localPoint
            );
            return GetComponent<RectTransform>().rect.Contains(localPoint);
        }

    }

}
