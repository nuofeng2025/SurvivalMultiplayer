using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
namespace FGame
{
    public interface IInteractive
    {
        InteractiveType Type { get; }  // 添加类型属性

        /// <summary>
        /// 交互
        /// </summary>
        public void Interaction(CharacterInteraction characterInteraction);

        /// <summary>
        /// 判断当前交互物能否交互
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool CanInteraction(CharacterInteraction characterInteraction);
    }


    // 定义交互物体类型枚举
    public enum InteractiveType
    {
        /// <summary>
        /// 无
        /// </summary>
        [LabelText("无")]
        NUll,


        /// <summary>
        /// 可搜索容器
        /// </summary>
        [LabelText("可搜索容器")]
        SearchContainer,

        /// <summary>
        /// 桌子
        /// </summary>
        [LabelText("桌子")]
        Table,
        Cabinet,
        /// <summary>
        /// 门
        /// </summary>
        [LabelText("桌子")]
        Door,
        Item,
        /// <summary>
        /// 角色
        /// </summary>
        [LabelText("角色")]
        Character,
        // ... 其他类型
    }
}

