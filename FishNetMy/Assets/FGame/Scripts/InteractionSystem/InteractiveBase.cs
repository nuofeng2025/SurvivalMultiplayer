using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
namespace FGame
{
    public class InteractiveBase : MonoBehaviour, IInteractive
    {

        #region 参数
        [TitleGroup("交互物设置")]
        [SerializeField]
        [LabelText("交互类型")]
        private InteractiveType interactiveType;

        [SerializeField]
        [LabelText("交互者")]
        private GameObject CurInteractionObj;

        InteractiveType IInteractive.Type => interactiveType;

        #endregion


        #region 组件

        #endregion


        #region 生命周期

        #endregion


        #region API
        public  virtual void Interaction(GameObject obj)
        {
            CurInteractionObj = obj;



        }



        #endregion



    }


}
