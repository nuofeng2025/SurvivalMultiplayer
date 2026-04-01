using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace FGame
{
    public class LootInventoryUI : MonoBehaviour
    {
        [SerializeField]
        private InventoryPartUi inventoryPartUi;

        public void ShowLootInventory(InventoryBase inventoryBase)
        {
            this.gameObject.SetActive(true);
            inventoryPartUi.Show(inventoryBase);
        }



    }

}
