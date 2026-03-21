using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
namespace FGame
{

    public class InputSystem : MonoBehaviour,ISystem
    {

        #region 参数
        



        [SerializeField]
        private Vector2 MoveDir;

        [SerializeField]
        private Vector2 LookDir;




        #endregion


        #region 组件
        private MainInput _mainInput;


        #endregion


        #region 生命周期
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            MoveDir = _mainInput.PlayerController.Move.ReadValue<Vector2>();
            LookDir = _mainInput.PlayerController.LookDir.ReadValue<Vector2>();


        }

        #endregion


        #region API
        public void Init()
        {
            _mainInput = new MainInput();
            _mainInput.PlayerController.Enable();
        }


        public Vector2 GetMoveDir()
        {
            return MoveDir.normalized;
        }

        public Vector2 GetLookDir()
        {
            return LookDir;
        }







        #endregion

    }


}
