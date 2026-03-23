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
        [LabelText("水平灵敏度（度/秒）")]
        [Range(1f, 20f)]
        private float horizontalSensitivity = 1f;

        [SerializeField]
        [LabelText("垂直灵敏度（度/秒）")]
        [Range(1f, 20f)]
        private float verticalSensitivity = 1f;

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

        /// <summary>
        /// 返回摄像机当前角度
        /// </summary>
        public Vector3 CameraAngle { get {
                return CameraFollowPointer.eulerAngles;
            }}

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
                CameraFollowPointer.transform.position = CalculateRightShoulderPosition();
            }

        }



        #endregion


        #region API

        /// <summary>
        /// 设置相机目标
        /// </summary>
        /// <param name="Target"></param>
        /// <param name="cameraType"></param>
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


        /// <summary>
        /// 旋转相机
        /// </summary>
        /// <param name="CameraDelta"></param>
        public void SetCameraRotate(Vector2 CameraDelta)
        {      
            Vector3 currentEuler = CameraFollowPointer.transform.eulerAngles;
            float YawDelta = CameraDelta.x * horizontalSensitivity ;
            float PitchDelta = CameraDelta.y * verticalSensitivity ;

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


        public Vector3 GetCameraCurrentAngle()
        {
            return CameraFollowPointer.transform.eulerAngles;    
        }



        public Vector3 CalculateRightShoulderPosition()
        {

            // 获取相机跟随点的前向和右向向量（基于相机旋转）
            Vector3 cameraForward = CameraFollowPointer.forward;
            Vector3 cameraRight = CameraFollowPointer.right;

            // 获取角色的上方向（通常是世界Y轴，但为了更准确，使用角色的上方向）
            Vector3 characterUp = CameraTarget.up;

            // 计算偏移方向：基于相机旋转的右侧，同时投影到水平面以防止倾斜
            Vector3 horizontalRight = Vector3.ProjectOnPlane(cameraRight, characterUp).normalized;
            Vector3 horizontalForward = Vector3.ProjectOnPlane(cameraForward, characterUp).normalized;

            // 如果水平方向向量太小，使用默认值
            if (horizontalRight.magnitude < 0.01f)
                horizontalRight = CameraTarget.right;
            if (horizontalForward.magnitude < 0.01f)
                horizontalForward = CameraTarget.forward;

            // 计算右肩偏移（基于相机旋转）
            Vector3 offset = horizontalRight * CurCameraConfig.CameraPointOffest.x;

            // 添加高度偏移（在世界Y轴上）
            offset += Vector3.up * CurCameraConfig.CameraPointOffest.y;

           /* // 可选：添加向前的偏移，使相机跟随点略微在角色前方
            if (CurCameraConfig.ForwardOffset > 0)
            {
                offset += horizontalForward * CurCameraConfig.ForwardOffset;
            }*/

            // 返回角色位置加上计算出的偏移
            return CameraTarget.position + offset;
        }



        #endregion






    }

}
