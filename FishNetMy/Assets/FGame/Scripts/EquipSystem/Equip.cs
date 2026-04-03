using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FGame
{
    public class Equip
    {
        private ItemData _itemData;

        public ItemData ItemData { get => _itemData;}



        public void InitEquip(ItemData itemData)
        {
            _itemData = itemData;

        }









    }

    public enum EquipType
    { 
        Weapon,
        Wear,
    }

}
