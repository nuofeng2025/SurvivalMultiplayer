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
        private Vector2 _moveDir;

        [LabelText("鼠标偏移量")]
        [SerializeField]
        private Vector2 _lookDir;

        [LabelText("是否奔跑")]
        [SerializeField]
        private bool _isSpring;

        [LabelText("交互")]
        [SerializeField]
        private bool _isF;

        [LabelText("交互")]
        [SerializeField]
        private bool _isTab;

        public Vector2 MoveDir { get => _moveDir; }
        public Vector2 LookDir { get => _lookDir; }
        public bool IsSpring { get => _isSpring; }

        public bool IsUse { get => _isF; }

        public bool IsTab { get => _isTab; }

        //public static MainInput mainInput;
        #endregion


        #region 组件




        #endregion


        #region 生命周期
        private void Awake()
        {
        }



        void Start()
        {

        }


        void Update()
        {
            _moveDir = GameManager.Instance.inputSystem.MoveDir;
            _lookDir = GameManager.Instance.inputSystem.LookDir;
            _isSpring = GameManager.Instance.inputSystem.IsSpring;
            _isF = GameManager.Instance.inputSystem.IsUse;
            _isTab = GameManager.Instance.inputSystem.IsTab;
        }

        #endregion


        #region API


        #endregion







    }

}

