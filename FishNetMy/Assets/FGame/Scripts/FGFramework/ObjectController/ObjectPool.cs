using System.Collections.Generic;
using UnityEngine;


namespace FGame
{
    /// <summary>
    /// 通用对象池
    /// </summary>
    public class ObjectPool<T> where T : Component
    {
        private readonly T prefab;
        private readonly Transform parent;
        private readonly Queue<T> pool = new Queue<T>();
        private readonly int defaultCapacity;
        private readonly int maxSize;
        private float lastUseTime;

        public int Count => pool.Count;
        public int TotalCreated { get; private set; }

        /// <summary>
        /// 对象池构造函数
        /// </summary>
        /// <param name="prefab">预制体</param>
        /// <param name="parent">父物体</param>
        /// <param name="defaultCapacity">默认容量</param>
        /// <param name="maxSize">最大容量</param>
        public ObjectPool(T prefab, Transform parent, int defaultCapacity = 10, int maxSize = 100)
        {
            this.prefab = prefab;
            this.parent = parent;
            this.defaultCapacity = defaultCapacity;
            this.maxSize = maxSize;

            // 预创建对象
            for (int i = 0; i < defaultCapacity; i++)
            {
                CreateNewObject();
            }
        }

        /// <summary>
        /// 创建新对象
        /// </summary>
        private T CreateNewObject()
        {
            if (TotalCreated >= maxSize)
            {
                Debug.LogWarning($"对象池已达到最大容量 {maxSize}，无法创建新对象");
                return null;
            }

            T obj = UnityEngine.Object.Instantiate(prefab, parent);
            obj.gameObject.SetActive(false);
            pool.Enqueue(obj);
            TotalCreated++;
            return obj;
        }

        /// <summary>
        /// 从池中获取对象
        /// </summary>
        public T Get()
        {
            lastUseTime = Time.time;
            if (pool.Count > 0)
            {
                T obj = pool.Dequeue();
                if (obj != null)
                {
                    Debug.Log("池中找到物体");
                    obj.gameObject.SetActive(true);
                    return obj;
                }
            }

            // 池中无可用对象，创建新对象
            T newObj = CreateNewObject();
            if (newObj != null)
            {
                newObj.gameObject.SetActive(true);
                newObj = pool.Dequeue();
                return newObj;
            }

            return null;
        }

        /// <summary>
        /// 归还对象到池中
        /// </summary>
        public void Return(T obj)
        {
            if (obj == null) return;
            if (pool.Contains(obj)) return;


            obj.gameObject.SetActive(false);
            obj.transform.SetParent(parent);

            // 可选：重置对象位置和状态
            obj.transform.localPosition = Vector3.zero;

            pool.Enqueue(obj);
        }

        /// <summary>
        /// 清空对象池
        /// </summary>
        public void Clear()
        {
            while (pool.Count > 0)
            {
                T obj = pool.Dequeue();
                if (obj != null)
                {
                    UnityEngine.Object.Destroy(obj.gameObject);
                }
            }
            TotalCreated = 0;
        }
    }
}
