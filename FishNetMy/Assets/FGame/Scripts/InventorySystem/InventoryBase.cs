using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using Sirenix.OdinInspector;
using System.Linq;
namespace FGame
{
    public class InventoryBase : NetworkBehaviour
    {

        #region 参数

        [TitleGroup("库存基础设置")]
        [LabelText("仓库名")]
        public string InventoryName;

        [LabelText("库存大小,x(列数)y(行数)")]
        [SerializeField]
        private Vector2Int InventorySize;

        // 服务端数据（不直接同步）
        [ShowInInspector]
        private Item[] serverItems;

        // 客户端数据缓存
        [ShowInInspector]
        private Item[] clientItems;


        private int nextInstanceId = 1;
        private int secretKey = 0;  // 服务器启动时随机生成



        private List<Item> CheckedSlot = new List<Item>();
        private List<Item> NoCheckeSlot = new List<Item>();

        #endregion




        #region 组件

        #endregion




        #region 生命周期

        public override void OnStartServer()
        {
            base.OnStartServer();


        }


        private void Awake()
        {
            LoaclInit();
        }

        #endregion




        #region API

        public void LoaclInit()
        {
            //初始化格子
            serverItems = new Item[InventorySize.x * InventorySize.y];
            clientItems = new Item[InventorySize.x * InventorySize.y];

            for (int i=0;i< serverItems.Length;i++)
            {
                serverItems[i] = new Item(new Vector2Int(i % InventorySize.x, (i / InventorySize.x)));
            }

            for (int i = 0; i < clientItems.Length; i++)
            {
                clientItems[i] = new Item(new Vector2Int(i % InventorySize.x, (i / InventorySize.x)));
            }

            //如果服务器有存档需要载入存档




        }




        /// <summary>
        /// 添加物品到指定槽位
        /// </summary>
        /// <param name="item"></param>
        /// <param name="Index"></param>
        [Server]
        public void AddItem(Item item,Vector2Int Index)
        {
            


        }

        /// <summary>
        /// 默认添加物品到最前面的格子
        /// </summary>
        /// <param name="item"></param>
        [Server]
        public void AddItem(Item item)
        {
            NoCheckeSlot.Clear();
            CheckedSlot.Clear();
            NoCheckeSlot = serverItems.ToList();

            while (NoCheckeSlot.Count>0)
            { 
                
            
            
            }

            //serverItems.Add(item);


        }



        public List<Item> GetRoundSlots(List<Item> AllItems,Item curItem,Vector2 ItemSize)
        {
            List<Item> items = new List<Item>();
            for (int i=0;i< ItemSize.x; i++)
            {
                for (int n = 0; n < ItemSize.y; n++)
                {
                    var roundSlot=AllItems.Where(item => item.GetPosition() == 
                    new Vector2Int(curItem.Position.x + i , curItem.Position.y + n)).FirstOrDefault();
                }            
            }

            return items;
        }



        [Server]
        public void AddItems(List<Item> items)
        {
            foreach (var item in items)
            {
                //InventoryItems_SyncVar.Add(item);
            }
        }


        [Server]
        public void InsertItem(int index, Item item)
        {
            // 在指定索引位置插入
            //InventoryItems_SyncVar.Insert(index, item);
        }

        [Server]
        public void RemoveItem(Item item)
        {
            //InventoryItems_SyncVar.Remove(item);
        }






        #endregion







    }

    public enum InventoryType
    {
        Wardrobe,

    }



}
