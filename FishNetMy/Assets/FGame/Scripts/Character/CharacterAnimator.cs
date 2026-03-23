using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace FGame
{
    public class CharacterAnimator : MonoBehaviour
    {

        #region 参数
        [Title("动画参数")]
        [SerializeField]
        private string MoveSpeed_ParName;

        [SerializeField]
        private string IsGround_ParName;

        [SerializeField]
        private string IsMove_ParName;




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


        public void UpdateBaseMovePar(float MoveSpeed,bool isGround,bool isMove)
        {
            animator.SetFloat(MoveSpeed_ParName, MoveSpeed);
            animator.SetBool(IsGround_ParName, isGround);
            animator.SetBool(IsMove_ParName, isMove);
        }





        #endregion


    }

}
