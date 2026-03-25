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
                TextAsset.Release();
            };
        }

                                       
        public ItemData  GetItemData(int ID)
        {
            ItemData itemData = null;
            ItemDataDict.TryGetValue(ID,out itemData);
            return itemData;
        }







    }

}
