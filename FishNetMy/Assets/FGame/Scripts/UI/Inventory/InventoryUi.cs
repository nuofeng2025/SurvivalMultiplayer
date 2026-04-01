using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.UI;
using TMPro;
using System;
using System.Linq;

namespace FGame
{
    public class InventoryUi : MonoBehaviour,IPlane
    {
        [SerializeField]
        private  TextMeshProUGUI InventoryName;

        [SerializeField]
        private Vector2 SlotSize;

        [SerializeField]
        private GridLayoutGroup ItemRoot;

        [SerializeField]
        private Transform MultiItemRoot;

        private string Name;

        [SerializeField]
        private InventoryBase _inventoryBase;

        private List<SlotUi> slotUis = new List<SlotUi>();
        private List<SlotUi> MultislotUis = new List<SlotUi>();

        // Start is called before the first frame update
        void Start()
        {
            


        }


        public void Init()
        {

        }


        public void Show(InventoryBase inventoryBase)
        {
            //Debug.Log("´ò¿ª¿â´æ");
            this.gameObject.SetActive(true);
            _inventoryBase = inventoryBase;
            InventoryName.text = inventoryBase.InventoryName;
            var size = inventoryBase.InventorySize;
            var Items = inventoryBase.ClientItems;

            //Debug.Log(size);
            ItemRoot.constraintCount = size.x;
            int allSlotCount = size.x * size.y;
            for (int i=0;i< allSlotCount;i++)
            {
                var slotUi =  FGFramework.Ins.GetCtr<PoolController>().GetPool<SlotUi>(PoolName.SlotUi.ToString()).Get();
                slotUis.Add(slotUi);
                slotUi.transform.parent = ItemRoot.transform;
                slotUi.transform.localScale = Vector3.one;
                slotUi.Init(new Vector2Int(i % size.x, (i / size.x)));
            }


            for (int i=0;i< slotUis.Count;i++)
            {
                //Debug.Log($"{slotUis[i].Index}:{slotUis[i].Item.ItemId}");
                var curItem = Items[i];
                //Debug.Log(curItem.Position);
                if (curItem.ItemId == 0) continue;
                if (slotUis[i].Item.ItemId ==0 )
                {                                  
                    var itemData = FGFramework.Ins.GetCtr<ConfigController>().GetItemData(curItem.ItemId);
                    int itemSize = itemData.SlotSizeX * itemData.SlotSizeY;
                    if (itemSize == 1)
                    {
                        slotUis[i].UpateUi(curItem);
                    } 
                    else if(itemSize>1)
                    {
                        var roundSlots = GetRoundSlots(slotUis, curItem,new Vector2(itemData.SlotSizeX, itemData.SlotSizeY));
                      
                        for (int n = 0; n < roundSlots.Count; n++)
                        {
                            //Debug.Log(roundSlots[n].Index);
                            roundSlots[n].UpateUi(curItem,false);
                        }
                        //Debug.Log("Get");
                        var MultiSlotUi = FGFramework.Ins.GetCtr<PoolController>().GetPool<SlotUi>(PoolName.SlotUi.ToString()).Get();
                        MultislotUis.Add(MultiSlotUi);
                        MultiSlotUi.transform.parent = MultiItemRoot;
                        MultiSlotUi.transform.localScale = Vector3.one;
                        MultiSlotUi.Init(new Vector2Int(i % size.x, (i / size.x)), new Vector2(itemData.SlotSizeX, itemData.SlotSizeY), SlotSize, curItem.Position);
                        MultiSlotUi.UpateUi(curItem);

                    }
                }
            }
        }


        public List<SlotUi> GetRoundSlots(List<SlotUi> AllItems, Item curItem, Vector2 ItemSize)
        {
            List<SlotUi> items = new List<SlotUi>();
            for (int i = 0; i < ItemSize.x; i++)
            {
                for (int n = 0; n < ItemSize.y; n++)
                {
                    //Debug.Log($"curItem.Position:{curItem.Position}");
                    Vector2 target = new Vector2Int(curItem.Position.x + i, curItem.Position.y + n);              
                    var roundSlot = AllItems.FirstOrDefault(item => item.Index == target);
                    items.Add(roundSlot);
                }
            }

            return items;
        }



        public void ItemNormalSort()
        { 
        
        
        
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

