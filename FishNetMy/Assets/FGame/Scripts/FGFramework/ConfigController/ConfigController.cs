using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Newtonsoft.Json;
using System.Linq;


namespace FGame
{
    public class ConfigController : IController
    {
        // ItemData
        private Dictionary<int, ItemData> ItemDataDict = new Dictionary<int, ItemData>();
        // ItemData
        private List<ItemData> ItemDatas;

        public void Init()
        {
            Debug.Log("配置系统初始化");
            LoadItemData();



        }



        private void LoadItemData()
        {
            ItemDataDict = new Dictionary<int, ItemData>();
            Addressables.LoadAssetAsync<TextAsset>("ItemData").Completed += (TextAsset) =>
            {
                var list = JsonConvert.DeserializeObject<ItemData[]>(TextAsset.Result.text);
                ItemDataDict = list.ToDictionary(item => item.ID);
                ItemDatas = ItemDataDict.Values.ToList();
                //TextAsset.Release();
            };
        }

                                       
        public ItemData  GetItemData(int ID)
        {
            ItemData itemData = new ItemData();
            ItemDataDict.TryGetValue(ID,out itemData);
            return itemData;
        }

        public ItemData GetItemData(string Name)
        {
           /* foreach (var i in ItemDatas)
            {
                Debug.Log(i.Name);
            }*/

            return ItemDatas.Where(item => item.Name == Name).FirstOrDefault();
        }





    }

}
