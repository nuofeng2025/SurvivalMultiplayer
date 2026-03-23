using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace FGame
{
    public class FGFramework
    {
        private static FGFramework _instance;
        public static FGFramework Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new FGFramework();
                }
                return _instance;
            }
        }

        private FGFramework() // 私有构造函数，防止外部实例化
        {
            // 初始化代码
            //Init();
        }

        private List<IController> _controllers = new List<IController>();


        /// <summary>
        /// 初始化框架
        /// </summary>
        public void Init()
        {
            // 方案A：手动注册（推荐）

            _controllers.Add(new ResourceController());


            foreach (var c in _controllers)
            {
                c.Init();
            
            }

        }


        public T GetController<T>() where T : class,IController
        {
            foreach (var c in _controllers)
            {
                if (c is T)
                {
                    return c as T;
                }
            
            }

            return null;
                
        }





    }

}
