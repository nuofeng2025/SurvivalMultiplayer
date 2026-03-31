using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.UI;
using TMPro;
using System;

namespace FGame
{
    public class InventoryUi : MonoBehaviour,IPlane
    {
        [SerializeField]
        private  TextMeshProUGUI InventoryName;

        [SerializeField]
        private GridLayoutGroup ItemRoot;

        private string Name;

        [SerializeField]
        private InventoryBase _inventoryBase;

        private List<SlotUi> slotUis = new List<SlotUi>();


        // Start is called before the first frame update
        void Start()
        {
            


        }


        public void Init()
        {

        }
        public void Show(InventoryBase inventoryBase)
        {
            Debug.Log("´ò¿ª¿â´æ");
            this.gameObject.SetActive(true);
            _inventoryBase = inventoryBase;
            var size = inventoryBase.InventorySize;
            Debug.Log(size);
            ItemRoot.constraintCount = size.x;
            int allSlotCount = size.x * size.y;
            for (int i=0;i< allSlotCount;i++)
            {
                var slotUi =  FGFramework.Ins.GetCtr<PoolController>().GetPool<SlotUi>(PoolName.SlotUi.ToString()).Get();
                slotUis.Add(slotUi);
                slotUi.transform.parent = ItemRoot.transform;
            }




        }
        public void Show(Item[] items,string InventoryName)
        {
            this.gameObject.SetActive(true);
           // _inventoryBase = inventoryBase;




        }

       

       

        public void Close()
        {
            foreach (var s in slotUis)
            {
                FGFramework.Ins.GetCtr<PoolController>().Return<SlotUi>(PoolName.SlotUi.ToString(), s);
            }
            slotUis.Clear();



        }



        private void OnDestroy()
        {
            foreach (var s in slotUis)
            {
                FGFramework.Ins.GetCtr<PoolController>().Return<SlotUi>(PoolName.SlotUi.ToString(), s);
            }
            slotUis.Clear();




        }

        public void Open(Action<IPlane> action)
        {
            action.Invoke(this);
        }
    }

}

