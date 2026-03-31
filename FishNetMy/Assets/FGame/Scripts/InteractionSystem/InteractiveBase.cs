using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using FishNet.Object;

namespace FGame
{
    public class InteractiveBase : NetworkBehaviour, IInteractive
    {

        #region 参数
        [TitleGroup("交互物设置")]
        [SerializeField]
        [LabelText("交互类型")]
        protected InteractiveType interactiveType;

        [SerializeField]
        [LabelText("交互时间(为0瞬间完成交互)")]
        protected float InteractionTime;

        [SerializeField]
        [LabelText("能否交互")]
        protected bool OpenInteraction;


        [TitleGroup("交互状态")]
        [SerializeField]
        [LabelText("当前交互者")]
        protected GameObject CurInteractionObj;

        [SerializeField]
        [LabelText("剩余交互时间")]
        protected float RemainInteractionTime;



        InteractiveType IInteractive.Type => interactiveType;

    


        #endregion


        #region 组件

        #endregion


        #region 生命周期

        #endregion


        #region API


        public virtual bool CanInteraction( GameObject obj)
        {
            
            if (!OpenInteraction)
            {
                return false;

            }
            //当前正在交互且非自己
            if (CurInteractionObj != null)
            { 
            
            
            
            }
            else
            {
                return true;


            }





            return false;
        }

        public virtual void Interaction(GameObject obj)
        {




        }


        #endregion



    }


}
