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



        [TitleGroup("物资生成")]
        [LabelText("生成类型")]
        [SerializeField] private LootGenerationType generationType = LootGenerationType.OnOpen;

        [LabelText("物资表")]
        [SerializeField] private ItemSpawnList itemSpawnList;


        [LabelText("能否刷新")]
        [SerializeField] private bool isRespawnable = false;

        [LabelText("刷新时间")]
        [SerializeField] private float respawnTime = 300f; // 5分钟重生

        /// <summary>
        /// 已生成
        /// </summary>
        [SerializeField]
        [LabelText("已生成")]
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
                Debug.Log("预生产物资");
                RandomSpawnItem();
                isLootGenerated = true;
            }
        }


        #endregion


        #region API
        /// <summary>
        /// 服务器生成物资
        /// </summary>
        [Server]
        private void GenerateLoot()
        {
            Debug.Log("预生成物资");




            /*foreach (var loot in lootItems)
            {
                CreateNewItem(loot.ItemId, loot.Quantity);
            }*/
        }


        /// <summary>
        /// 生成随机物品
        /// </summary>
        /// <returns></returns>
        [Server]        
        public void RandomSpawnItem()
        {          
            if (itemSpawnList == null)
            {
                Debug.LogWarning($"{this.name}物资表为空,无法生成物资!");
            }
           
            itemSpawnList.CalWeight();

            //获得随机数量
            int randomCount = GetItemRandomCount();
            Debug.Log(randomCount);
            //获得随机物品
            GetRandomItem(randomCount);

        }


        [Server]
        public int GetItemRandomCount()
        {
            int allweight = itemSpawnList.GetSpawnCountAllWeight();
            float randomCount = Random.Range(0, allweight);
            int CurWeight = 0;
            for (int i=0;i< itemSpawnList.SpawnCountWeight.Count;i++)
            {
                CurWeight += itemSpawnList.SpawnCountWeight[i].Weight;
                if(randomCount <= CurWeight)
                {
                    return itemSpawnList.SpawnCountWeight[i].Count;
                }
            }
            return 0;
        }

        /// <summary>
        /// 获得随机物品
        /// </summary>
        /// <param name="count">随机数量</param>
        /// <param name="CanRepeat">物品能否重复</param>
        /// <returns></returns>
        [Server]
        public void GetRandomItem(int count,bool CanRepeat = true)
        {
            if (count == 0) return;
            var RemainCount = count;

            int allweight = itemSpawnList.GetSpawnItemsAllWeight();

            if (CanRepeat)
            {
                while (RemainCount>0)
                {

                    RemainCount--;
                    float randomCount = Random.Range(0, allweight);
                    Debug.Log(randomCount);
                    int CurWeight = 0;
                    for (int i = 0; i < itemSpawnList.SpawnItems.Count; i++)
                    {
                        CurWeight += itemSpawnList.SpawnItems[i].Weight;
                        Debug.Log(CurWeight);
                        if (randomCount <= CurWeight)
                        {
                            var curItemName = itemSpawnList.SpawnItems[i].ItemName;
                            if (string.IsNullOrEmpty(curItemName)) continue;
                            var item =  FGFramework.Ins.GetCtr<ConfigController>().GetItemData(curItemName.Trim());
                            Debug.Log(curItemName.Trim());
                            Debug.Log(item.Name);

                            //添加进容器
                            AddItem(new Item(item, GlobalItemManager.GenerateId(), 1));
                            break;
                        }
                    }
                    
                }
            
            }









        }



        



        #endregion


    }
    public enum LootGenerationType
    {
        /// <summary>
        /// 预生成
        /// </summary>
        [LabelText("预生成")]
        PreGenerated,    // 预生成（重要地点、Boss宝箱）
        /// <summary>
        /// 接近时生成
        /// </summary>
        [LabelText("接近时生成")]
        OnApproach,      // 接近时生成（普通房屋、野怪营地）
        /// <summary>
        /// 打开时生成"
        /// </summary>
        [LabelText("打开时生成")]
        OnOpen          // 打开时生成（普通箱子、尸体）
    }

    

}
