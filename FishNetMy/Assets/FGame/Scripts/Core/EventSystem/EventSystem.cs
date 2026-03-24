using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;
namespace FGame
{
    public static class EventController 
    {



        //中心交互检测事件

        public static UnityEvent<IInteractive> InteractionCenterEnter = new UnityEvent<IInteractive>();//进入到检测


        public static UnityEvent<IInteractive> InteractionCenterLeave = new UnityEvent<IInteractive>();//离开检测












    }
}


