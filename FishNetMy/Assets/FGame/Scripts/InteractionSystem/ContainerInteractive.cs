using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using FishNet.Object;
namespace FGame
{

    public class ContainerInteractive : InteractiveBase
    {
        #region 参数




        #endregion


        #region 组件

        #endregion


        #region 生命周期

        #endregion


        #region API






        [ServerRpc]
        public override void Interaction(GameObject obj)
        {
            base.Interaction(obj);

            Debug.Log("调用服务器交互");


        }




        #endregion












    }


}

