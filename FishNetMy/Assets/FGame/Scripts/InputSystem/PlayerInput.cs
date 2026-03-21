using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.InputSystem;

namespace FGame
{
    public class PlayerInput : MonoBehaviour
    {

        #region 参数
        [LabelText("移动方向")]
        [SerializeField]
        private Vector2 MoveDir;

        [LabelText("鼠标偏移量")]
        [SerializeField]
        private Vector2 LookDir;

        //public static MainInput mainInput;
        #endregion


        #region 组件
        private CharacterController characterController;


        #endregion


        #region 生命周期
        private void Awake()
        {
            characterController = GetComponent<CharacterController>();
        }



        void Start()
        {

        }


        void Update()
        {
            MoveDir = GameManager.Instance.inputSystem.GetMoveDir();
            LookDir = GameManager.Instance.inputSystem.GetLookDir();

            if (MoveDir.sqrMagnitude > 0)
            {
                characterController.MoveCharacter(MoveDir);
            }

            if (LookDir.sqrMagnitude>0.1f)
            {
                GameManager.Instance.cameraSystem.SetCameraRotate(LookDir);
            }




        }
        #endregion


        #region API


        #endregion







    }

}

