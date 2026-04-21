using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace FGame
{
    public class LootInventoryUI : MonoBehaviour,IPlane
    {
        [SerializeField]
        private InventoryPartUi inventoryPartUi;


        public RectTransform LockCameraRotateRt;
        public void Close()
        {
            throw new NotImplementedException();
        }

        public void Init()
        {
            throw new NotImplementedException();
        }

        public void Open(Action<IPlane> action)
        {
            throw new NotImplementedException();
        }

        public void ShowLootInventory(InventoryBase inventoryBase)
        {
            this.gameObject.SetActive(true);
            inventoryPartUi.Show(inventoryBase);
        }

        public void HideLootInventory()
        {
            inventoryPartUi.Close();
            this.gameObject.SetActive(false);
            //inventoryPartUi.Show(inventoryBase);
        }

    }

}
