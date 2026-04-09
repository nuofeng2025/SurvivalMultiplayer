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
        public LootInventoryUI LootInventoryUi;
        public CharacterInventoryUI characterInventoryUI;
        public EquipemntPlane equipemntPlane;
        public AroundPlane aroundPlane;

        public Image TestImage;
        public void Init()
        {
            interactionUi.Init();

            FGFramework.Ins.GetCtr<EventController>().OpenLootInventory.AddListener(OpenLootInventoryUi);



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



        public void OpenLootInventoryUi(InventoryBase inventoryBase,CharacterInventory characterInventory)
        {
            LootInventoryUi.ShowLootInventory(inventoryBase);

            OpenCharacterInventoryUi(characterInventory);

            aroundPlane.gameObject.SetActive(true);

        }

        public void CloseLootInventoryUi()
        {
            // LootInventoryUi.ShowLootInventory(inventoryBase);

            //OpenCharacterInventoryUi(characterInventory);

            aroundPlane.gameObject.SetActive(false);

        }



        public void OpenCharacterInventoryUi(CharacterInventory characterInventory)
        {
            characterInventoryUI.ShowUi(characterInventory);



        }




        public void OnDestroy()
        {
            FGFramework.Ins.GetCtr<EventController>().OpenLootInventory.RemoveListener(OpenLootInventoryUi);
        }
    }

}

