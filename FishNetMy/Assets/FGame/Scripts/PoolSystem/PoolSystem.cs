using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
namespace FGame
{
    public class PoolSystem : MonoBehaviour,ISystem
    {
        [ShowInInspector]
        public List<Pool> UiPools = new List<Pool>();


        // Start is called before the first frame update
        void Start()
        {

        }




        public void Init()
        {
            Debug.Log("对象池初始化");
            foreach (var p in UiPools)
            {

               FGFramework.Ins.GetCtr<PoolController>().CreatePool<SlotUi>(p.PoolName, p.slotUiPrefab.GetComponent<SlotUi>(), null, p.intiSize, p.maxSize);
            }

           

            
        }

       











    }

    [System.Serializable]
    public class Pool
    {
        [LabelText("池名")]
        public string PoolName;
        public GameObject slotUiPrefab;
        public int intiSize;
        public int maxSize;

    }

}
