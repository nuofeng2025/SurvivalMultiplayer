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

        [TitleGroup("库存基础设置")]
        [LabelText("仓库名")]
        public string InventoryName;

        [LabelText("库存大小")]
        [SerializeField]
        private int InventorySize;


        [TitleGroup("同步数据")]
        [ShowInInspector]
        [LabelText("当前库存大小")]
        private readonly SyncVar<int> InventorySize_SyncVar = new SyncVar<int>();

        [LabelText("当前库存")]
        [ShowInInspector]
        private readonly SyncList<InventoryItem> InventoryItems_SyncVar = new();


        private int nextInstanceId = 1;
        private int secretKey = 0;  // 服务器启动时随机生成





        #endregion




        #region 组件

        #endregion




        #region 生命周期

        public override void OnStartServer()
        {
            base.OnStartServer();
            InventorySize_SyncVar.Value = InventorySize;


        }




        #endregion




        #region API



        [ServerRpc]
        public void AddItemServerRpc(InventoryItem item)
        {



        }









        #endregion







    }

    public enum InventoryType
    {
        Wardrobe,

    }



}
