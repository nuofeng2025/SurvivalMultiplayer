using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace FGame
{
    public class FGFramework
    {
        private static FGFramework _instance;
        public static FGFramework Ins
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
            Debug.Log("FG框架已初始化");
            // 方案A：手动注册（推荐）

            _controllers.Add(new ResourceController());
            _controllers.Add(new ConfigController());
            _controllers.Add(new EventController());
            _controllers.Add(new PoolController());
            foreach (var c in _controllers)
            {
                c.Init();
            
            }

        }

        /// <summary>
        /// 获取系统
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetCtr<T>() where T : class,IController
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
