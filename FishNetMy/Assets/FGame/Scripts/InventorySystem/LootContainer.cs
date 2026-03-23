using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using Sirenix.OdinInspector;
namespace FGame
{
    public class LootContainer : InventoryBase
    {
        #region 参数
        [LabelText("生成类型")]
        [SerializeField] private LootGenerationType generationType = LootGenerationType.OnOpen;

        [LabelText("能否刷新")]
        [SerializeField] private bool isRespawnable = false;

        [LabelText("刷新时间")]
        [SerializeField] private float respawnTime = 300f; // 5分钟重生

        /// <summary>
        /// 已生成
        /// </summary>
        private bool isLootGenerated = false;

        private float lastLootTime = 0;


        #endregion


        #region 组件

        #endregion


        #region 生命周期
        public override void OnStartServer()
        {
            base.OnStartServer();

            if (generationType == LootGenerationType.PreGenerated)
            {
                GenerateLoot();
                isLootGenerated = true;
            }
        }





        /// <summary>
        /// 服务器生成物资
        /// </summary>
        [Server]
        private void GenerateLoot()
        {
            Debug.Log("预生成物资");
            //var lootItems = lootTable.GenerateLoot();

            // 根据玩家调整（如果适用）
            if (generationType == LootGenerationType.OnOpen)
            {
                // 获取打开的玩家，调整战利品
                // AdjustLootByPlayer(currentPlayer);
            }

            /*foreach (var loot in lootItems)
            {
                CreateNewItem(loot.ItemId, loot.Quantity);
            }*/
        }




        #endregion


        #region API


        #endregion


    }
    public enum LootGenerationType
    {
        [LabelText("预生成")]
        PreGenerated,    // 预生成（重要地点、Boss宝箱）
        [LabelText("接近时生成")]
        OnApproach,      // 接近时生成（普通房屋、野怪营地）
        [LabelText("打开时生成")]
        OnOpen          // 打开时生成（普通箱子、尸体）
    }
}
