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


        public Image TestImage;
        public void Init()
        {
            interactionUi.Init();

            



        }



            // Start is called before the first frame update
        void Start()
        {

            LoadImge();
        }



        public void OnDestroy()
        {
            
        }


        public async  void LoadImge()
        {
            Sprite sprite = await FGFramework.Ins.GetCtr<ResourceController>().GetSpriteFromAtlas(SpriteType.ItemSprites, "Axe");
            TestImage.sprite = sprite;
        }



    }

}

