using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Events;
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
        private IInteractive LastRayCastInteractive;//最后检测到的交互物，用于显示
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

            if (Input.GetKeyDown(KeyCode.E) && CurInteractiveType == InteractiveType.SearchContainer && LastRayCastInteractive!=null)
            {
                var IntObj = LastRayCastInteractive as InteractiveBase;
                IntObj.GetComponent<LootContainer>().OpenInventory();

            }

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
                var CurRayCastInteractive = ScreenCenterRayHitInfo.collider.GetComponent<IInteractive>();

                //确保只执行一次
                if (CurRayCastInteractive != null)
                {

                    if (LastRayCastInteractive != null && LastRayCastInteractive!= CurRayCastInteractive)
                    {                      
                        LastRayCastInteractive = CurRayCastInteractive;
                        FGFramework.Ins.GetCtr<EventController>().InteractionCenterEnter?.Invoke(LastRayCastInteractive);
                        InitCurInteractive(LastRayCastInteractive);

                    }
                    else
                    {
                        LastRayCastInteractive = CurRayCastInteractive;
                        FGFramework.Ins.GetCtr<EventController>().InteractionCenterEnter?.Invoke(LastRayCastInteractive);
                        InitCurInteractive(LastRayCastInteractive);
                    }                
                }
                else
                {
                    //清空数据
                    if (LastRayCastInteractive != null)
                    {
                        FGFramework.Ins.GetCtr<EventController>().InteractionCenterLeave?.Invoke(LastRayCastInteractive);
                        LastRayCastInteractive = null;
                        CurInteractiveType = InteractiveType.NUll;
                    }
                }
            }
            else
            {
                //清空数据
                if (LastRayCastInteractive!=null)
                {
                    FGFramework.Ins.GetCtr<EventController>().InteractionCenterLeave?.Invoke(LastRayCastInteractive);
                    LastRayCastInteractive = null;
                    CurInteractiveType = InteractiveType.NUll;
                }
            }


        }



        public void InitCurInteractive(IInteractive CurRayCastInteractive)
        {
            switch (CurRayCastInteractive.Type)
            {
                case InteractiveType.SearchContainer:
                    CurInteractiveType = InteractiveType.SearchContainer;


                    break;


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

