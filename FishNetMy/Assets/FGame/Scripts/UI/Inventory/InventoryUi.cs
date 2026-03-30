using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.UI;
namespace FGame
{
    public class InventoryUi : MonoBehaviour
    {
        public string Name;

        [SerializeField]
        private InventoryBase _inventoryBase;




        // Start is called before the first frame update
        void Start()
        {

        }



        public void Show(InventoryBase inventoryBase)
        {
            this.gameObject.SetActive(true);
            _inventoryBase = inventoryBase;







        }
        public void Show(Item[] items,string InventoryName)
        {
            this.gameObject.SetActive(true);
           // _inventoryBase = inventoryBase;




        }





    }

}

