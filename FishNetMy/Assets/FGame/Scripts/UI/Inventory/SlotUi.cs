using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using TMPro;
namespace FGame
{
    public class SlotUi : MonoBehaviour
    {
        [SerializeField]
        private Item item;

        [SerializeField]
        private Image ItemIcon;

        [SerializeField]
        private TextMeshProUGUI ItenName;

        [SerializeField]
        private TextMeshProUGUI ItenCount;

        [SerializeField]
        private Vector2 _index;

        [SerializeField]
        private List<Item> items;

        [SerializeField]
        private RectTransform rectTransform;

        public Vector2 Index { get => _index; }
        public List<Item> Items { get => items; }
        public Item Item { get => item;}

        // Start is called before the first frame update
        void Start()
        {

        }

        public void Init(Vector2 Index)
        {
            _index = Index;
            ItemIcon.gameObject.SetActive(false);
            ItenName.text = "";
            ItenCount.text = "";
            items.Clear();
            this.item = new Item();
        }


        public void Init(Vector2 Index, Vector2 ItemSize, Vector2 SlotSize, Vector2 positin)
        {
            _index = Index;
            ItemIcon.gameObject.SetActive(false);
            ItenName.text = "";
            ItenCount.text = "";
            items.Clear();
            this.item = new Item();

            rectTransform.sizeDelta = new Vector2(SlotSize.x * ItemSize.x, SlotSize.y * ItemSize.y);
            rectTransform.anchoredPosition = new Vector3(positin.x * SlotSize.x, -(positin.y * SlotSize.y), 0);

        }



        public async void UpateUi(Item item,bool ShowIcon = true)
        {
            //Debug.Log(10);
            this.item = item;
            if (ShowIcon == false) return;
            var itemData = FGFramework.Ins.GetCtr<ConfigController>().GetItemData(item.ItemId);

            ItenName.text = itemData.Name;
            ItenCount.text = item.CurStack.ToString();

            Sprite sp =  await FGFramework.Ins.GetCtr<ResourceController>().GetSpriteFromAtlas(SpriteType.ItemSprites, itemData.Icon);
            //Debug.Log(11); Debug.Log(itemData.Name);
            if (sp!=null)
            {
                //Debug.Log(12);
                ItemIcon.sprite = sp;
                ItemIcon.gameObject.SetActive(true);
            }          
        }



        public void UpateUi(List<Item> items)
        {
            this.items = items;

            /*this.item = item;
            var itemData = FGFramework.Ins.GetCtr<ConfigController>().GetItemData(item.ItemId);
            Sprite sp = FGFramework.Ins.GetCtr<ResourceController>().GetSpriteFromAtlas(SpriteType.ItemSprites, itemData.Name).Result;

            if (sp != null)
            {
                ItemIcon.sprite = sp;
                ItemIcon.gameObject.SetActive(true);
            }*/


        }


 
    }

}
