using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace FGame
{
    public class UiSystem : MonoBehaviour,ISystem
    {
        public InteractionUi interactionUi;
        public GameObject crosshair;
        public InventoryUi LootInventoryUi;

        public Image TestImage;
        public void Init()
        {
            interactionUi.Init();

            FGFramework.Ins.GetCtr<EventController>().OpenInventory.AddListener(OpenInventoryUi);



        }



            // Start is called before the first frame update
        void Start()
        {

            LoadImge();
        }



      


        public async void LoadImge()
        {
            Sprite sprite = await FGFramework.Ins.GetCtr<ResourceController>().GetSpriteFromAtlas(SpriteType.ItemSprites, "Axe");
            TestImage.sprite = sprite;
        }



        public void OpenInventoryUi(InventoryBase inventoryBase)
        {
            LootInventoryUi.Show(inventoryBase);



        }



        public void OnDestroy()
        {
            FGFramework.Ins.GetCtr<EventController>().OpenInventory.RemoveListener(OpenInventoryUi);
        }
    }

}

