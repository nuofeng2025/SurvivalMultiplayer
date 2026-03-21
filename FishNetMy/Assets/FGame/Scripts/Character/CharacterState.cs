using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using FishNet.Object;
namespace FGame
{
    public class CharacterState : NetworkBehaviour
    {
        #region 参数
        [SerializeField]
        [LabelText("角色属性")]
        private CharacterStateEntity state;



        #endregion


        #region 组件

        #endregion


        #region 生命周期

        private void Start()
        {
            Init();

        }





        #endregion


        #region API
        public void Init()
        {
            state = new CharacterStateEntity();
            InitState();




        }


        public void InitState()
        {
            state.BaseWalkSpeed = 1.5f; //移动速度 m/s
            state.BaseRunSpeed = 5f;
            state.BaseHp = 100;
            state.BaseStamina = 100;

       /*     state.MaxMoveSpeed = state.BaseMoveSpeed + state.BuffMoveSpeed;
            state.MaxHp = state.BaseHp + state.BuffHp;
            state.MaxStamina = state.BaseStamina + state.BuffStamina;*/

            state.CurWalkSpeed = state.BaseWalkSpeed;
            state.CurRunSpeed = state.BaseRunSpeed;
            state.CurHp = state.MaxHp;
            state.CurStamina = state.MaxStamina;

        }




        public void UpdateState()
        {
            

        }


        public void BeAttack(float Damage)
        {





        }



       /// <summary>
       /// 获得步行速度
       /// </summary>
       /// <returns></returns>
        public float GetWalkSpeed()
        {
            return state.CurWalkSpeed;


        }


        /// <summary>
        /// 获得步行速度
        /// </summary>
        /// <returns></returns>
        public float GetRunSpeed()
        {
            return state.CurRunSpeed;


        }





        #endregion









    }

}
