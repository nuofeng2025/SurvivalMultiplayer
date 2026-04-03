using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using FishNet.Object;
namespace FGame
{

    public class ContainerInteractive : InteractiveBase
    {
        #region 参数
        [SerializeField]
        protected InventoryBase inventoryBase;



        #endregion


        #region 组件

        #endregion


        #region 生命周期

        #endregion


        #region API






        
        public override void Interaction(CharacterInteraction characterInteraction)
        {
            base.Interaction(characterInteraction);

            switch (interactiveType)
            {
                case InteractiveType.SearchContainer:

                    inventoryBase = GetComponent<InventoryBase>();
                    inventoryBase?.OpenInventory(characterInteraction);
                    break;


            }




        }




        #endregion












    }


}

