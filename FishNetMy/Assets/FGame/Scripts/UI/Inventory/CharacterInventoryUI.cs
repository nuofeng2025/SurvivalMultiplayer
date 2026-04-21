using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FGame
{
    /// <summary>
    /// ½ÇÉ«¿â´æÏÔÊ¾
    /// </summary>
    public class CharacterInventoryUI : MonoBehaviour,IPlane
    {
        [SerializeField]
        private  EquipInventoryPartUi equipInventoryPartUiPrefab;

        [SerializeField]
        private Transform Root;

        private List<EquipInventoryPartUi> equipInventoryPartUis = new List<EquipInventoryPartUi>();

        public RectTransform LockCameraRotateRt;
        public void Start()
        {
            


        }

        /// <summary>
        /// ÏÔÊ¾½ÇÉ«¿â´æ
        /// </summary>
        /// <param name="characterInventory"></param>
        public void ShowUi(CharacterInventory characterInventory)
        {
            this.gameObject.SetActive(true);
            for (int i=0;i< characterInventory.equipContainers.Count;i++)
            {
                var equipContainer = characterInventory.equipContainers[i];
                var equipInventoryPartUi =  Instantiate(equipInventoryPartUiPrefab,Root);
                equipInventoryPartUi.Show(equipContainer);

                equipInventoryPartUis.Add(equipInventoryPartUi);

            }
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        }

        public void Open(Action<IPlane> action)
        {
            throw new NotImplementedException();
        }

        public void Init()
        {
            throw new NotImplementedException();
        }

        public void Close()
        {
            this.gameObject.SetActive(false);



        }
    }

}
