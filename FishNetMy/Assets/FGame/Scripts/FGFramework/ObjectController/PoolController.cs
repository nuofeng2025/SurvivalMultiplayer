using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FGame
{
    public class PoolController : IController
    {
        private Dictionary<string, object> pools = new Dictionary<string, object>();
        private Dictionary<string, Transform> poolParents = new Dictionary<string, Transform>();


        public void Init()
        {
            
        }

        /// <summary>
        /// 创建对象池（需要传入父物体Transform）
        /// </summary>
        public ObjectPool<T> CreatePool<T>(string poolName, T prefab, Transform parent,
            int defaultCapacity = 10, int maxSize = 100) where T : Component
        {
            if (pools.ContainsKey(poolName))
            {
                Debug.LogWarning($"对象池 {poolName} 已存在，返回现有池");
                return pools[poolName] as ObjectPool<T>;
            }

            // 创建池的父物体
            Transform poolParent;
            if (parent != null)
            {
                poolParent = new GameObject($"Pool_{poolName}").transform;
                poolParent.SetParent(parent);
            }
            else
            {
                // 如果没有指定父物体，创建根目录下的物体
                GameObject root = GameObject.Find("PoolRoot");
                if (root == null)
                {
                    root = new GameObject("PoolRoot");
                    UnityEngine.Object.DontDestroyOnLoad(root);
                }
                poolParent = new GameObject($"Pool_{poolName}").transform;
                poolParent.SetParent(root.transform);
            }

            var pool = new ObjectPool<T>(prefab, poolParent, defaultCapacity, maxSize);
            pools.Add(poolName, pool);
            poolParents.Add(poolName, poolParent);

            return pool;
        }




        /// <summary>
        /// 获取对象池
        /// </summary>
        public ObjectPool<T> GetPool<T>(string poolName) where T : Component
        {
            if (pools.TryGetValue(poolName, out object pool))
            {
                return pool as ObjectPool<T>;
            }

            Debug.LogError($"对象池 {poolName} 不存在");
            return null;
        }

        /// <summary>
        /// 从池中获取对象
        /// </summary>
        public T Get<T>(string poolName) where T : Component
        {
            var pool = GetPool<T>(poolName);
            return pool != null ? pool.Get() : null;
        }

        /// <summary>
        /// 归还对象到池
        /// </summary>
        public void Return<T>(string poolName, T obj) where T : Component
        {
            var pool = GetPool<T>(poolName);
            pool?.Return(obj);
        }
        /// <summary>
        /// 检查对象池是否存在
        /// </summary>
        public bool HasPool(string poolName)
        {
            return pools.ContainsKey(poolName);
        }

        /// <summary>
        /// 删除对象池
        /// </summary>
        public bool RemovePool(string poolName)
        {
            if (pools.TryGetValue(poolName, out object pool))
            {
                // 清空池中所有对象
                var clearMethod = pool.GetType().GetMethod("Clear");
                clearMethod?.Invoke(pool, null);

                // 删除父物体
                if (poolParents.TryGetValue(poolName, out Transform parent))
                {
                    if (parent != null)
                    {
                        UnityEngine.Object.Destroy(parent.gameObject);
                    }
                }

                pools.Remove(poolName);
                poolParents.Remove(poolName);
                return true;
            }

            return false;
        }

        /// <summary>
        /// 清空所有对象池
        /// </summary>
        public void ClearAllPools()
        {
            foreach (var kvp in pools)
            {
                var clearMethod = kvp.Value.GetType().GetMethod("Clear");
                clearMethod?.Invoke(kvp.Value, null);
            }

            // 销毁所有父物体
            foreach (var parent in poolParents.Values)
            {
                if (parent != null)
                {
                    UnityEngine.Object.Destroy(parent.gameObject);
                }
            }

            pools.Clear();
            poolParents.Clear();
        }

        /// <summary>
        /// 获取所有池的信息
        /// </summary>
        public Dictionary<string, PoolInfo> GetAllPoolInfo()
        {
            var info = new Dictionary<string, PoolInfo>();

            foreach (var kvp in pools)
            {
                var poolType = kvp.Value.GetType();
                var countProperty = poolType.GetProperty("Count");
                var totalCreatedProperty = poolType.GetProperty("TotalCreated");

                int count = countProperty != null ? (int)countProperty.GetValue(kvp.Value) : 0;
                int totalCreated = totalCreatedProperty != null ? (int)totalCreatedProperty.GetValue(kvp.Value) : 0;

                info.Add(kvp.Key, new PoolInfo
                {
                    PoolName = kvp.Key,
                    AvailableCount = count,
                    TotalCreated = totalCreated
                });
            }

            return info;
        }

    }

    /// <summary>
    /// 池信息结构体
    /// </summary>
    public struct PoolInfo
    {
        public string PoolName;
        public int AvailableCount;
        public int TotalCreated;

        public override string ToString()
        {
            return $"{PoolName}: Available={AvailableCount}, Total={TotalCreated}";
        }
    }
}
