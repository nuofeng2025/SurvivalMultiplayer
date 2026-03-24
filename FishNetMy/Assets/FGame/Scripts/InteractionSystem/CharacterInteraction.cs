using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityGameFramework.Runtime;
namespace FGame
{
    public class CharacterInteraction : MonoBehaviour
    {


        #region 参数
        [Title("射线检测")]
        [LabelText("可交互层")]
        [SerializeField]
        private LayerMask InteractionMask;

        [LabelText("中心交互距离")]
        [SerializeField]
        private float ScreenCenterRayDistance;

        [SerializeField]
        [LabelText("当前交互物")]
        private IInteractive CurInteractive;

        [SerializeField]
        [LabelText("当前交互物类型")]
        private InteractiveType CurInteractiveType;


        [TitleGroup("调试")]
        [SerializeField]
        [LabelText("开启调试Ui")]
        private bool OpenDrawUi;

        private RaycastHit ScreenCenterRayHitInfo;
        #endregion


        #region 组件

        #endregion


        #region 生命周期
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            ScreenCenterRayCheck();
        }
        #endregion


        #region API
        /// <summary>
        /// 中心交互检测
        /// </summary>
        private void ScreenCenterRayCheck()
        {
            if (Camera.main == null) return;
            // 获取屏幕中心点坐标（像素坐标）
            Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);
            // 从摄像机经过屏幕中心点发射射线
            Ray ray = Camera.main.ScreenPointToRay(screenCenter);
            if(OpenDrawUi) Debug.DrawRay(ray.origin, ray.direction * ScreenCenterRayDistance, Color.red);

            if (Physics.Raycast(ray, out ScreenCenterRayHitInfo, ScreenCenterRayDistance, InteractionMask))
            {
                CurInteractive = ScreenCenterRayHitInfo.collider.GetComponent<IInteractive>();
                
                if (CurInteractive!=null)
                {
                    switch (CurInteractive.Type)
                    {
                        case InteractiveType.SearchContainer:
                            CurInteractiveType = CurInteractive.Type;


                            break;


                    }
                }
              

            }
        }


        private void OnDrawGizmos()
        {
           /* if (!OpenDrawUi) return;
            Gizmos.color = Color.red;

            // 获取屏幕中心点坐标（像素坐标）
            Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);
            // 从摄像机经过屏幕中心点发射射线
            Ray ray = Camera.main.ScreenPointToRay(screenCenter);


            // 从当前位置向前绘制射线（方向为向前，长度为5）
            Gizmos.DrawRay(ray.origin, ray.direction * ScreenCenterRayDistance);*/

        }

        #endregion








    }
}

