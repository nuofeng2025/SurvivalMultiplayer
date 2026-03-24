using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityGameFramework.Runtime;
namespace FGame
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;


        #region 组件

        public CameraSystem cameraSystem;
        public InputSystem inputSystem;




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

            GameEntry.GetComponent<UIComponent>().OpenUIForm("UiName","Bottom");

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
            


        }

        #endregion






    }

}
