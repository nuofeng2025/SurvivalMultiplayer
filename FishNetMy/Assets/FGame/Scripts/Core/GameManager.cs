using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
namespace FGame
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        [Title("游戏状态")]
        private bool ShowCursored;

        #region 组件

        public CameraSystem cameraSystem;
        public InputSystem inputSystem;
        public UiSystem uiSystem;
        public PoolSystem poolSystem;


        #endregion

        #region 生命周期

        protected void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this.gameObject);
            }


            FGFramework.Ins.Init();//初始化框架

            Init();

        }



        private void Start()
        {
           


           
        }




        protected void OnDestroy()
        {
            // 清理静态引用，防止野指针
            if (Instance == this)
            {
                Instance = null;
            }
        }
        #endregion




        #region API

        public void Init()
        {
         
            cameraSystem.Init();
            inputSystem.Init();
            uiSystem.Init();
            poolSystem.Init();





        }








        public void ShowCursor()
        {

            // 添加这两行
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            ShowCursored = Cursor.visible;


        }

        public void HideCursor()
        {

            // 添加这两行
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            ShowCursored = Cursor.visible;
        }





        #endregion






    }

}
