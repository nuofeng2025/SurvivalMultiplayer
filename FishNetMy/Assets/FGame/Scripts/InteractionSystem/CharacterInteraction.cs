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

   
        [TitleGroup("状态")]
        [SerializeField]
        [LabelText("当前交互状态")]
        private float CurInteractiveRemainTime;

        [SerializeField]
        [LabelText("当前交互物类型")]
        private InteractiveType CurInteractiveType;

        [SerializeField]
        [LabelText("当前交互物")]
        private IInteractive _curInteractive;


        [TitleGroup("调试")]
        [SerializeField]
        [LabelText("开启调试Ui")]
        private bool OpenDrawUi;

        private RaycastHit ScreenCenterRayHitInfo;
        private IInteractive LastRayCastInteractive;//最后检测到的交互物，用于显示


        public CharacterController characterController;

        public IInteractive CurInteractive { get => _curInteractive;  }


        #endregion


        #region 组件

        #endregion


        #region 生命周期
        void Start()
        {
            characterController = GetComponent<CharacterController>();
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

        /// <summary>
        /// 与当前物体交互
        /// </summary>
        public void DoInteractive()
        {
            if (LastRayCastInteractive == null) return;//如果准心没有对准交互物，则不交互
            if (_curInteractive != null)
            {
                Debug.Log("当前正在进行其它交互!");
                return;
            }

            _curInteractive = LastRayCastInteractive;

            switch (CurInteractiveType)
            {
                case InteractiveType.SearchContainer:

                    var SearchContainer = LastRayCastInteractive as ContainerInteractive;
                    if (SearchContainer) SearchContainer.Interaction(this);
                    break;


            }



        }

        /// <summary>
        /// 当前是否在进行容器交互
        /// </summary>
        /// <returns></returns>
        public bool IsContainerInteracting()
        {
            if (_curInteractive != null && (_curInteractive as ContainerInteractive) != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 是否正在交互
        /// </summary>
        /// <returns></returns>
        public bool IsInteracting()
        {
            if (_curInteractive != null && CurInteractiveType != InteractiveType.NUll)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 停止当前交互
        /// </summary>
        public void StopInteract()
        {
            _curInteractive = null;






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

