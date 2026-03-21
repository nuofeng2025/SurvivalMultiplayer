using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FGame
{
    [System.Serializable]
    public class CharacterStateEntity
    {
        ///基础属性
        public float BaseWalkSpeed;
        public float BaseRunSpeed;
        public float BaseHp;
        public float BaseStamina;



        ///加成属性
        public float BuffMoveSpeed;
        public float BuffHp;
        public float BuffStamina;


        ///最大属性
        public float MaxMoveSpeed;
        public float MaxHp;
        public float MaxStamina;

        ///当前属性
        public float CurWalkSpeed;
        public float CurRunSpeed;
        public float CurHp;
        public float CurStamina;

    }



}
