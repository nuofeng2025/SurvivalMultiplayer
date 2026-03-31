using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Sprites;
using UnityEngine.U2D;
using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System;
namespace FGame
{
    /// <summary>
    /// 资源管理
    /// </summary>
    public class ResourceController : IController
    {

        private Dictionary<string, SpriteAtlas> atlasCache = new Dictionary<string, SpriteAtlas>();
        private Dictionary<string, Sprite> singleSpriteCache = new Dictionary<string, Sprite>();


        private List<string> loadingTasks = new(); //正在加载的资源   关键：防止并发重复加载
        public void Init()
        {
            Debug.Log("资源系统初始化");
            PreloadAtlases();
        }




        /// <summary>
        /// 预加载常用图集
        /// </summary>
        private void PreloadAtlases()
        {
     /*       // item图集
            LoadAtlas(SpriteType.ItemSprites.ToString());*/
            

        }

        /// <summary>
        /// 直接加载图集
        /// </summary>
        /// <param name="atlasName"></param>
        public void LoadAtlas(string atlasName)
        {
            if (atlasCache.ContainsKey(atlasName)) return;

            if (!loadingTasks.Contains(atlasName))
            {
                loadingTasks.Add(atlasName);
                Addressables.LoadAssetAsync<SpriteAtlas>(atlasName).Completed += (handle) =>
                {
                    if (handle.Result != null)
                    {
                        if (atlasCache != null)
                        {
                            atlasCache[atlasName] = handle.Result;
                            Debug.Log(atlasName);
                        }
                    }
                    else
                    {
                        handle.Release();
                        Debug.Log($"加载图集失败:{atlasName}");
                    }


                    loadingTasks.Remove(atlasName);
                };
            }
     
        }


        /// <summary>
        /// 异步加载图集
        /// </summary>
        public async Task<SpriteAtlas> AsyncLoadAtlas(string atlasName)
        {
            // 1. 检查缓存
            if (atlasCache.TryGetValue(atlasName, out SpriteAtlas cachedAtlas))
                return cachedAtlas;

            // Addressables加载
            AsyncOperationHandle<SpriteAtlas> handle = default;

            if (loadingTasks.Contains(atlasName)) return null;

            try
            {
                loadingTasks.Add(atlasName);

                handle = Addressables.LoadAssetAsync<SpriteAtlas>(atlasName);

                SpriteAtlas atlas = await handle.Task;

                if (handle.Result != null)
                {
                    if (atlasCache != null)
                    {
                        atlasCache[atlasName] = handle.Result;
                    }
                }
                return atlas;
            }
            catch (Exception ex)
            {
                Debug.LogError($"加载图集失败 [{atlasName}]: {ex.Message}");
                throw; // 重新抛出，让调用者处理
            }
            finally
            {

                loadingTasks.Remove(atlasName);
                // 注意：这里不能释放 handle，因为 atlas 被缓存了
                // 只在加载失败时释放
                if (handle.IsValid() && !atlasCache.ContainsKey(atlasName))
                {
                    Addressables.Release(handle);
                }

            }
           
        }



        /// <summary>
        /// 从图集中获取Sprite
        /// </summary>
        public async Task<Sprite>  GetSpriteFromAtlas(SpriteType atlasType, string spriteName)
        {
            string atlasKey = atlasType.ToString();

            if (atlasCache.TryGetValue(atlasKey, out SpriteAtlas atlas))
            {
                Sprite sprite = atlas.GetSprite(spriteName);
                if (sprite == null)
                    Debug.LogWarning($"在图集 {atlasType} 中未找到图片: {spriteName}");
                return sprite;
            }

            // 未加载的情况
            atlas = await AsyncLoadAtlas(atlasKey);
            atlasCache[atlasKey] = atlas;

            Sprite result = atlas.GetSprite(spriteName);
            if (result == null)
                Debug.LogWarning($"在图集 {atlasType} 中未找到图片: {spriteName}");
            return result;
        }












    }

    public enum SpriteType
    {
        /// <summary>
        /// 道具
        /// </summary>
        ItemSprites,
        /// <summary>
        /// 通用
        /// </summary>
        Common,
        /// <summary>
        /// Ui元素
        /// </summary>
        UiElement,




    }


}
