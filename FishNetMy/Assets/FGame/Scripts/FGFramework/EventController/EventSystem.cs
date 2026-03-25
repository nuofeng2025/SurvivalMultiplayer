using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;
namespace FGame
{
    public  class EventController :IController
    {
        public void Init()
        {
            Debug.Log("事件系统初始化");
        }






        //中心交互检测事件

        public UnityEvent<IInteractive> InteractionCenterEnter = new UnityEvent<IInteractive>();//进入到检测


        public UnityEvent<IInteractive> InteractionCenterLeave = new UnityEvent<IInteractive>();//离开检测

      
    }
}


