using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace FGame
{
    [CreateAssetMenu(menuName = "配置/相机/相机配置", fileName = "New CameraConfig")]
    public class CameraConfig : ScriptableObject
    {
        [LabelText("相机类型")]
        public CameraType cameraType;
        [LabelText("跟随点偏移")]
        public Vector3 CameraPointOffest;
        [LabelText("横向角度限制")]
        public Vector2 HorAngleClamp;
        [LabelText("纵向角度限制")]
        public Vector2 VerAngleClamp;






    }


    public enum CameraType
    { 
        [LabelText("第一人称")]
        FristPerson,
        [LabelText("第三人称")]
        ThirdPerson,
    
    
    
    
    }

}

