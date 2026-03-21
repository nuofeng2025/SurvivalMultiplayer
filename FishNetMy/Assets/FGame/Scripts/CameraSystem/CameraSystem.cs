using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Cinemachine;
using System.Linq;
namespace FGame
{
    public class CameraSystem : MonoBehaviour, ISystem
    {




        #region 参数


        [BoxGroup("参数")]
        [SerializeField]
        [LabelText("相机平滑值")]
        private float CameraSmooth;


        [Title("状态")]
        [LabelText("当前配置")]
        [SerializeField]
        private CameraConfig CurCameraConfig;

        [LabelText("当前类型")]
        [SerializeField]
        private CameraType CurCameraType;


        [LabelText("当前相机")]
        [SerializeReference]
        private CinemachineVirtualCamera CurCamera;


        private Vector3 velocity = Vector3.zero;
        private Vector3 TargetRotation;


        #endregion


        #region 组件
        [Title("组件")]
        [LabelText("预备相机")]
        [SerializeReference]
        private List<CinemachineVirtualCamera> Cameras;


        [SerializeField]
        [Required]
        protected Transform CameraFollowPointer;//相机跟随点

        [SerializeField]
        protected Transform CameraTarget;//相机锁定目标

        [SerializeField]
        [Required]
        [LabelText("相机配置")]
        protected List<CameraConfig> cameraConfigs;



        #endregion


        #region 生命周期
        public void Init()
        {
            Cameras = GetComponentsInChildren<CinemachineVirtualCamera>().ToList();

        }


        public void LateUpdate()
        {
            //相机跟随目标
            if (CameraTarget!=null)
            {
                CameraFollowPointer.transform.position = CameraTarget.transform.position + CurCameraConfig.CameraPointOffest;
            }

            //相机平滑移动至目标值
            if (TargetRotation != CameraFollowPointer.transform.eulerAngles)
            {
                CameraFollowPointer.transform.eulerAngles = Vector3.SmoothDamp(
                    CameraFollowPointer.transform.eulerAngles,
                    TargetRotation,
                    ref velocity,
                    CameraSmooth
                );
            }



        }



        #endregion


        #region API

        public void SetCameraTarget(Transform Target,CameraType cameraType)
        {
            CameraTarget = Target;
            CurCameraType = cameraType;
            CurCameraConfig = cameraConfigs.Where(item => item.cameraType == cameraType).FirstOrDefault();

            SwitchCamera(cameraType);
            CurCamera.m_LookAt = CameraFollowPointer;
            CurCamera.m_Follow = CameraFollowPointer;

        }


        /// <summary>
        /// 切换相机
        /// </summary>
        /// <param name="cameraType">相机类型</param>
        public void SwitchCamera(CameraType cameraType)
        {
            foreach (var camera in Cameras)
            {
                if (camera.GetComponent<CameraTag>()?.cameraType == cameraType)
                {
                    CurCamera = camera;
                    CurCamera.gameObject.SetActive(true);
                }
                else
                {
                    camera.gameObject.SetActive(false);
                }
            }    
        }



        public void SetCameraRotate(Vector2 CameraDelta)
        {
            var CurEulerAngles = CameraFollowPointer.transform.eulerAngles;

            var YAngleDelta = CameraDelta.x;
            var XAngleDelta = CameraDelta.y;

            TargetRotation = CurEulerAngles + new Vector3(XAngleDelta, YAngleDelta, 0); 

        }







        #endregion






    }

}
