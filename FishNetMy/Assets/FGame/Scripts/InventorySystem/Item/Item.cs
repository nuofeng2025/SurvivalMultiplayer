using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
namespace FGame
{
    [System.Serializable]
    public struct Item
    {
        public ushort ItemId;
        public ushort Quantity;
        public int InstanceId;// 唯一实例ID
        public ushort CurStack;//当前堆叠数

        /// <summary>
        /// 物品所占大小
        /// </summary>
        [System.NonSerialized]
        public Vector2Int SlotSize;

        /// <summary>
        /// 物品栏内的位置
        /// </summary>
        [System.NonSerialized]
        [ShowInInspector]
        public Vector2Int Position;


        public Item(Vector2Int Position)
        {
            ItemId = 0;
            Quantity = 0;
            InstanceId = 0;
            CurStack = 0;
            SlotSize = Vector2Int.zero;
            this.Position = Position;


        }


        public Item(ItemData itemData, int instanceId, int curStack)
        {
            ItemId = (ushort)itemData.ID;
            Quantity = (ushort)itemData.Quality;
            InstanceId = instanceId;
            CurStack = (ushort)curStack;
            Position = Vector2Int.zero;
            SlotSize = new Vector2Int(itemData.SlotSizeX,itemData.SlotSizeY)  ;
           
        }

        public void SetPosition(Vector2Int position)
        {
            Position = position;
        }

        public Vector2Int GetPosition()
        {
            return Position;
        }


        public Vector2Int GetSize()
        {
            return SlotSize;
        }


    }

}

