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


        [Title("参数")]
        [SerializeField]
        [LabelText("相机平滑值")]
        private float CameraSmooth =1;

        [SerializeField]
        [LabelText("相机速度")]
        private float CameraSpeed;


        [SerializeField]
        [LabelText("水平灵敏度（度/秒）")]
        [Range(50f, 500f)]
        private float horizontalSensitivity = 200f;

        [SerializeField]
        [LabelText("垂直灵敏度（度/秒）")]
        [Range(30f, 300f)]
        private float verticalSensitivity = 150f;

        [SerializeField]
        [LabelText("最大旋转速度（度/秒）")]
        [Range(0f, 720f)]
        private float maxRotationSpeed = 360f;

        [SerializeField]
        [LabelText("最小输入阈值（度/秒）")]
        private float minInputThreshold = 0.01f;



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


        private Vector3 velocity;
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
            Vector3 currentEuler = CameraFollowPointer.transform.eulerAngles;
            float YawDelta = CameraDelta.x * horizontalSensitivity * Time.deltaTime;
            float PitchDelta = CameraDelta.y * verticalSensitivity * Time.deltaTime;

            YawDelta = Mathf.Clamp(YawDelta,-maxRotationSpeed, maxRotationSpeed);
            PitchDelta = Mathf.Clamp(PitchDelta, -maxRotationSpeed, maxRotationSpeed);

            if (Mathf.Abs(YawDelta) < minInputThreshold)
            {
                YawDelta = 0;
            }
            if (Mathf.Abs(PitchDelta) < minInputThreshold)
            {
                PitchDelta = 0;
            }



            float targetYaw = currentEuler.y + YawDelta;
            float targetPitch = currentEuler.x - PitchDelta;

            targetPitch = AngleUtils.NormalizeAngleSigned(targetPitch);
            targetYaw = AngleUtils.NormalizeAngle(targetYaw);
            // 限制俯仰角
            targetPitch = Mathf.Clamp(targetPitch, -89f, 89f);

            // 使用工具类平滑
            float smoothYaw = AngleUtils.SmoothDampAngle(
                currentEuler.y, targetYaw, ref velocity.y, CameraSmooth
            );

            float smoothPitch = AngleUtils.SmoothDampAngle(
                currentEuler.x, targetPitch, ref velocity.x, CameraSmooth
            );

            CameraFollowPointer.transform.eulerAngles = new Vector3(smoothPitch, smoothYaw, 0);

        }





        #endregion






    }

}
