using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
namespace FGame
{
    public class UiSystem : MonoBehaviour,ISystem
    {
        [TitleGroup("组件")]
        public InteractionUi interactionUi;
        public GameObject crosshair;
        public LootInventoryUI LootInventoryUi;
        public CharacterInventoryUI characterInventoryUI;
        public EquipemntPlane equipemntPlane;
        public AroundPlane aroundPlane;




        [TitleGroup("状态")]
        private bool OpenLootInventoryUied;
        public UiPlane CurUiPlane;


        public List<RectTransform> LockCameraUIs_OpenLootInventoryUi;
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



        public void SwitchPlayerInventory()
        {
            switch (CurUiPlane)
            {

                case UiPlane.Close:

                    //CloseLootInventoryUi();

                    break;
                case UiPlane.Loot_Inventory:

                    CloseLootInventoryUi();

                    break;
                case UiPlane.Player_Inventory:




                    break;

            }
        
        
        
        }


        public void OpenLootInventoryUi(InventoryBase inventoryBase,CharacterInventory characterInventory)
        {
            CurUiPlane = UiPlane.Loot_Inventory;

            OpenLootInventoryUied = true;

            LootInventoryUi.ShowLootInventory(inventoryBase);

            OpenCharacterInventoryUi(characterInventory);

            aroundPlane.gameObject.SetActive(true);

            GameManager.Instance.ShowCursor();

            GameManager.Instance.inputSystem.LockLookDir();
        }

        public void CloseLootInventoryUi()
        {
            CurUiPlane = UiPlane.Close;

            LootInventoryUi.HideLootInventory();

            CloseCharacterInventoryUi();

            aroundPlane.gameObject.SetActive(false);

        }



        public void OpenCharacterInventoryUi(CharacterInventory characterInventory)
        {
            characterInventoryUI.ShowUi(characterInventory);

        }


        public void CloseCharacterInventoryUi()
        {
            characterInventoryUI.Close();

        }

        public void OnDestroy()
        {
            FGFramework.Ins.GetCtr<EventController>().OpenLootInventory.RemoveListener(OpenLootInventoryUi);
        }



        bool IsPointerOverSpecificUI(List<RectTransform> rectTransforms)
        {
            foreach (RectTransform rectTransform in rectTransforms)
            {
                Vector2 localPoint;
                RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    rectTransform,
                    Input.mousePosition,
                    null,
                    out localPoint
                );

                if (rectTransform.rect.Contains(localPoint))
                {
                    return true; // 只要有一个包含鼠标就返回 true
                }
            }
            return false;
        }




    }


    public enum UiPlane
    { 
        Close,
        Loot_Inventory,
        Player_Inventory,





    }



}

