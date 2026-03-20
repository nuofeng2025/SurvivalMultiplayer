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
        [LabelText("相机类型")]
        private CameraType CurCameraType;


        [LabelText("相机类型")]
        private CameraConfig CurCameraConfig;


        #endregion


        #region 组件
        [SerializeField]
        protected CinemachineVirtualCamera virtualCamera;
        [SerializeField]
        protected Transform CameraFollowPointer;
        [SerializeField]
        protected Transform CameraTarget;
        [SerializeField]
        protected List<CameraConfig> cameraConfigs;



        #endregion


        #region 生命周期
        public void Init()
        { 
        
        
        
        
        }

        public void LateUpdate()
        {
            if (CameraTarget!=null)
            {
                CameraFollowPointer.transform.position = CameraTarget.transform.position;
            }



        }



        #endregion


        #region API

        public void SetCameraTarget(Transform transform,CameraType cameraType)
        {
            CurCameraType = cameraType;
            
            virtualCamera.m_LookAt = CameraTarget;
            virtualCamera.m_Follow = CameraTarget;

        }










        #endregion






    }

}
