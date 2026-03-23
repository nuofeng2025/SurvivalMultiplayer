using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using Sirenix.OdinInspector;

namespace FGame
{
    public class InventoryBase : NetworkBehaviour
    {

        #region 参数

        private int nextInstanceId = 1;
        private int secretKey = 0;  // 服务器启动时随机生成





        #endregion




        #region 组件

        #endregion




        #region 生命周期






        #endregion




        #region API


        #endregion







    }

    public enum InventoryType
    {
        Wardrobe,

    }



}
