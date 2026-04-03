using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
namespace FGame
{
    public class EquipInventoryPartUi : InventoryPartUi
    {

        [SerializeField]
        private Image equipIcon;
     
        // Start is called before the first frame update
        void Start()
        {

        }






        public async void Show(EquipContainer equipContainer)
        {
            var itemData = FGFramework.Ins.GetCtr<ConfigController>().GetItemData(equipContainer.Item.ItemId);

            var sprite = await FGFramework.Ins.GetCtr<ResourceController>().GetSpriteFromAtlas(SpriteType.ItemSprites, itemData.Icon);

            equipIcon.sprite = sprite;

            base.Show(equipContainer);







        }












    }



}
