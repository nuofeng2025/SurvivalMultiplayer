using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace FGame
{
    public class CharacterAnimator : MonoBehaviour
    {


        #region 参数

        #endregion


        #region 组件

        private Animator animator;

        #endregion


        #region 生命周期

        private void Awake()
        {
            Init();
        }


        #endregion


        #region API
        public void Init()
        {
            animator = GetComponent<Animator>();
        }

    




        #endregion


    }

}
