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

        [TabGroup("重力")]
        [SerializeField]
        private bool OpenGravity;

        [TabGroup("重力")]
        [SerializeField]
        [LabelText("重力大小")]
        private float Gravity = 7f;

        [TabGroup("重力")]
        [SerializeField]
        [LabelText("地面层")]
        private LayerMask GroundMask;



        [TabGroup("基本动画")]



        [BoxGroup("基础状态")]
        [SerializeField]
        [LabelText("在地面")]
        private bool IsGround;



        #endregion


        #region 组件
        [LabelText("角色属性")]
        private CharacterState characterState;

        [BoxGroup("组件")]
        public UnityEngine.CharacterController characterController;
        [BoxGroup("组件")]
        public  Rigidbody rigidbody;
        [BoxGroup("组件")]
        public Animator animator;

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
                GameManager.Instance.cameraSystem.SetCameraTarget(this.transform, CameraType.ThirdPerson);
                controllerMode = ControllerMode.ThirdPerson;
            }

        }



     

        void Update()
        {
            CheckGround();//地面检测

           




        }


        private void LateUpdate()
        {
            SimuGravity();//重力模拟
        }


        #endregion





        #region API
        public void Init()
        {
            characterState = GetComponent<CharacterState>();
            characterController = GetComponent<UnityEngine.CharacterController>();
            rigidbody = GetComponent<Rigidbody>();
            animator = GetComponent<Animator>();

        }


        public void MoveCharacter(Vector2 dir)
        {

            // 移动角色
            characterController.Move(new Vector3(dir.x,0, dir.y) * characterState.GetWalkSpeed() * Time.deltaTime);


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
