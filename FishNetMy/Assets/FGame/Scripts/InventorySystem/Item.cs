using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
namespace FGame
{
    [System.Serializable]
    public struct Item
    {
        public int ItemId;
        public int Quantity;
        public int InstanceId;          // Î¨Ò»ÊµÀýID
        public int StackCount;

        public Item(int id, int qty, int instanceId, int count)
        {
            ItemId = id;
            Quantity = qty;
            InstanceId = instanceId;
            StackCount = count;
        }
    }

}

