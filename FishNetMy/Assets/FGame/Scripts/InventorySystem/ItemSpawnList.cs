using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace FGame
{
    /// <summary>
    /// 物资表，配置容器能生成的物资
    /// </summary>
    [CreateAssetMenu(fileName ="新 物资池",menuName = "配置/物资池")]
    public class ItemSpawnList : ScriptableObject
    {

        // List<>

        [LabelText("物品池")]
        public List<ItemWeight> SpawnItems = new List<ItemWeight>();

        [LabelText("物品池")]
        public List<CountWeight> SpawnCountWeight = new List<CountWeight>();


        private int SpawnItemsAllWeight;

        private int SpawnCountAllWeight;


        [Button("计算权重")]
        public void CalWeight()
        {
            SpawnItemsAllWeight = 0;

            for (int i=0;i< SpawnItems.Count;i++)
            {
                //ItemWeight itemWeight = SpawnItems[i];
                SpawnItemsAllWeight += SpawnItems[i].Weight;
            }
            for (int i = 0; i < SpawnItems.Count; i++)
            {
                ItemWeight itemWeight = SpawnItems[i];
                itemWeight.CurWeight = (float)System.Math.Round((float)SpawnItems[i].Weight / (float)SpawnItemsAllWeight * 100f, 2); 


                SpawnItems[i] = itemWeight;
            }


            SpawnCountAllWeight = 0;

            for (int i = 0; i < SpawnCountWeight.Count; i++)
            {
                //ItemWeight itemWeight = SpawnItems[i];
                SpawnCountAllWeight += SpawnCountWeight[i].Weight;
            }
            for (int i = 0; i < SpawnCountWeight.Count; i++)
            {
                CountWeight countWeight = SpawnCountWeight[i];
                countWeight.CurWeight = (float)System.Math.Round((float)SpawnCountWeight[i].Weight / (float)SpawnCountAllWeight * 100f, 2);


                SpawnCountWeight[i] = countWeight;
            }




        }

        public int GetSpawnItemsAllWeight()
        {
            SpawnItemsAllWeight = 0;

            for (int i = 0; i < SpawnItems.Count; i++)
            {
                //ItemWeight itemWeight = SpawnItems[i];
                SpawnItemsAllWeight += SpawnItems[i].Weight;
            }
            return SpawnItemsAllWeight;

        }



        public int GetSpawnCountAllWeight()
        {

            SpawnCountAllWeight = 0;

            for (int i = 0; i < SpawnCountWeight.Count; i++)
            {
                //ItemWeight itemWeight = SpawnItems[i];
                SpawnCountAllWeight += SpawnCountWeight[i].Weight;
            }

            return SpawnCountAllWeight;

        }



    }

    /// <summary>
    /// 生成数量配置
    /// </summary>
    [System.Serializable]
    public struct CountWeight
    {
        [LabelText("权重")]
        public int Weight;
        [LabelText("数量")]
        public int Count;
        [LabelText("当前概率%")]
        public float CurWeight;


    }


    /// <summary>
    /// 物品生成概率配置
    /// </summary>
    [System.Serializable]
    public struct ItemWeight
    {
        [LabelText("权重")]
        public int Weight;
        [LabelText("物品名")]
        public string ItemName;
        [LabelText("当前概率%")]
        public float CurWeight;


    }




}

