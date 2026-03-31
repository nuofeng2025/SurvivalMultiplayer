using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Object;
using Sirenix.OdinInspector;
using FishNet.Connection;

namespace FGame
{
    public class CharacterController : NetworkBehaviour
    {
        #region 参数

        [SerializeField]
        [LabelText("控制模式")]
        private ControllerMode controllerMode;


        [TabGroup("移动")]
        [SerializeField]
        [LabelText("当前移动速度")]
        private float CurMoveSpeed;

        [TabGroup("移动")]
        [SerializeField]
        [LabelText("加速度")]
        private AnimationCurve accelerationCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

        [TabGroup("重力")]
        [SerializeField]
        [LabelText("启用重力")]
        private bool OpenGravity;

        [TabGroup("重力")]
        [SerializeField]
        [LabelText("重力大小")]
        private float Gravity = 7f;

        [TabGroup("重力")]
        [SerializeField]
        [LabelText("地面层")]
        private LayerMask GroundMask;


        [TabGroup("转向")]
        [SerializeField]
        [LabelText("转向平滑值(值越低转向越快)")]
        [Range(0,1)]
        private float RotateSmooth = 0.05f;



        [TabGroup("基本动画")]



        [FoldoutGroup("基础状态")]
        [SerializeField]
        private bool IsGround;
        [SerializeField]
        private bool IsMove;
        [SerializeField]
        private bool IsRun;
        [SerializeField]
        private bool IsWalk;
        public bool _IsOwner;



        #endregion


        #region 组件
        [LabelText("角色属性")]
        private CharacterState characterState;      

        private CharacterMovement characterMovement;
        private UnityEngine.CharacterController characterController;
        private Rigidbody rigidbody;
        private PlayerInput playerInput;
        private CharacterAnimator characterAnimator;
        private CharacterInteraction characterInteraction;


        [FoldoutGroup("调试")]
        [SerializeField]
        private bool OpenDrawUi;

        #endregion


        #region 生命周期
        private void Awake()
        {
            Init();
        }

        void Start()
        {
            
        }

        public override void OnStartClient()
        {

            base.OnStartClient();
            if (IsOwner)
            {
                SwitchControllerType(ControllerMode.ThirdPerson);
                //playerInput.enabled = true;
            }
            else
            {
                playerInput.enabled = false;
            }

        }



     

        void Update()
        {
            if (IsOwner)
            {

                CheckGround();//地面检测

                HandleInput();//控制输入

                UpdateAnimator();//更新动画
            }
          
        }


        private void LateUpdate()
        {
            if (IsOwner)
            {
                SimuGravity();//重力模拟
                UpdateCamera();//更新相机
            }
            
        }

        #endregion





        #region API
        public void Init()
        {
            characterState = GetComponent<CharacterState>();
            characterController = GetComponent<UnityEngine.CharacterController>();
            rigidbody = GetComponent<Rigidbody>();           
            characterMovement = GetComponent<CharacterMovement>();
            playerInput = GetComponent<PlayerInput>();
            characterAnimator = GetComponent<CharacterAnimator>();
            characterInteraction = GetComponent<CharacterInteraction>();
        }


        public void HandleInput()
        {
            MoveAndRotate();

            HandleUse();


            if (Input.GetKey(KeyCode.Space))
            {
                // 添加这两行
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;

            }
        }


        public void MoveAndRotate()
        {

            //移动角色
            if (playerInput.MoveDir.sqrMagnitude > 0)
            {
                if (!IsGround)
                {
                    return;
                }
                float targetAngle = Mathf.Atan2(playerInput.MoveDir.x, playerInput.MoveDir.y) * Mathf.Rad2Deg + GameManager.Instance.cameraSystem.CameraAngle.y;

                var TargetSpeed = playerInput.IsSpring ? characterState.GetRunSpeed() : characterState.GetWalkSpeed();

                float speedCurve = accelerationCurve.Evaluate(Mathf.Clamp01(playerInput.MoveDir.magnitude));
                CurMoveSpeed = Mathf.Lerp(CurMoveSpeed, TargetSpeed * speedCurve, Time.deltaTime * 8f);
                if (CurMoveSpeed > 0)
                {
                    IsMove = true;
                }
                characterMovement.MoveCharacter(characterController, targetAngle, RotateSmooth, CurMoveSpeed);
            }
            else
            {
                CurMoveSpeed = Mathf.Lerp(CurMoveSpeed, 0, Time.deltaTime * 10f);
                // 当速度足够小时，直接设为 0
                if (CurMoveSpeed < 0.01f)
                {
                    CurMoveSpeed = 0;
                    IsMove = false;
                }
            }



        }


        public void HandleUse()
        {
            if (!characterInteraction || !playerInput) return;
            if (playerInput.IsUse)
            {
                Debug.Log("E");
                characterInteraction.DoInteractive();
            }
        


        }



        /// <summary>
        /// 切换角色控制模式
        /// </summary>
        public void SwitchControllerType(ControllerMode controllerMode)
        {
            this.controllerMode = controllerMode;
            switch (this.controllerMode)
            {
                case ControllerMode.FirstPerson:

                    break;

                case ControllerMode.ThirdPerson:

                    GameManager.Instance.cameraSystem.SetCameraTarget(this.transform, CameraType.ThirdPerson);


                    break;

            }





        }


        /// <summary>
        /// 更新动画系统
        /// </summary>
        public void UpdateAnimator()
        {
            characterAnimator.UpdateBaseMovePar(CurMoveSpeed,IsGround, IsMove);



        }





        /// <summary>
        /// 模拟重力
        /// </summary>
        public void SimuGravity()
        {
            if (!OpenGravity) return;
            if (!IsGround)
            {
                characterController.Move(Vector3.down * Time.deltaTime * Gravity);
            }

        }


        /// <summary>
        /// 地面检测
        /// </summary>
        public void CheckGround()
        {
            if (Physics.Raycast(this.transform.position + new Vector3(0, -0.015f, 0), Vector3.down, 0.03f, GroundMask))
            {
                IsGround = true;
            }
            else
            {
                IsGround = false;
            }
        }


        public void UpdateCamera()
        {
            GameManager.Instance.cameraSystem.SetCameraRotate(playerInput.LookDir);
        }


        public void OnDrawGizmos()
        {
            if (!OpenDrawUi) return;
            Gizmos.color = Color.red;

            // 从当前位置向前绘制射线（方向为向前，长度为5）
            Gizmos.DrawRay(this.transform.position + new Vector3(0, -0.015f, 0), Vector3.down * 0.03f);

        }




        #endregion




    }

    public enum ControllerMode
    { 
        [LabelText("第一人称")]
        FirstPerson,
        [LabelText("第三人称")]
        ThirdPerson,
    
    
    
    
    
    }



}
