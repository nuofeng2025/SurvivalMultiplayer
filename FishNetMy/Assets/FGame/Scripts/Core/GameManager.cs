using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
namespace FGame
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;


        #region 组件

        public CameraSystem cameraSystem;
        public InputSystem inputSystem;
        public UiSystem uiSystem;



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


        }

        #endregion






    }

}
