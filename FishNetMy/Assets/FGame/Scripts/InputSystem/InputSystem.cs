using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace FGame
{

    public class InputSystem : MonoBehaviour,ISystem
    {

        #region 参数

        public InputMode CurInputMode;

        [SerializeField]
        private Vector2 _MoveDir;

        [SerializeField]
        private Vector2 _LookDir;

        [SerializeField]
        private bool _IsSpring;


        [SerializeField]
        private bool _F;


        [SerializeField]
        private bool _Tab;

        #endregion


        #region 组件
        private MainInput _mainInput;

        public Vector2 MoveDir { get => _MoveDir;}
        public Vector2 LookDir { get => _LookDir;}
        public bool IsSpring { get => _IsSpring; }
        public bool IsUse { get => _F; }
        public bool IsTab { get => _Tab; }
        #endregion


        #region 生命周期
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            _MoveDir = _mainInput.PlayerController.Move.ReadValue<Vector2>();
            _LookDir = _mainInput.PlayerController.LookDir.ReadValue<Vector2>();
            _IsSpring = _mainInput.PlayerController.Spring.IsPressed();
            _F = _mainInput.PlayerController.F.WasPressedThisFrame();
            _Tab = _mainInput.PlayerController.Tab.WasPressedThisFrame();
        }

        #endregion


        #region API
        public void Init()
        {
            _mainInput = new MainInput();
            _mainInput.PlayerController.Enable();
        }





        #endregion

    }

    public enum InputMode
    { 
        PC,
        MOBILE,
    
    
    
    }



}
