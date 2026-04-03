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

        private List<EquipInventoryPartUi> equipInventoryPartUis = new List<EquipInventoryPartUi>();


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
            throw new NotImplementedException();
        }
    }

}
