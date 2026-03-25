using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
namespace FGame
{
    [CreateAssetMenu(fileName ="新 物资池",menuName = "配置/物资池")]
    public class ItemSpawnList : ScriptableObject
    {
        [LabelText("物品池")]
        public List<ItemWeight> SpawnItems = new List<ItemWeight>();



    }



    [System.Serializable]
    public struct ItemWeight
    {
        [LabelText("权重")]
        public int Weight;
        [LabelText("物品名")]
        public string ItemName;
    
    
    
    }




}

