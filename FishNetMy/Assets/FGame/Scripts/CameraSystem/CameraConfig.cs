using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace FGame
{
    [CreateAssetMenu(menuName = "配置/相机/相机配置", fileName = "New CameraConfig")]
    public class CameraConfig : ScriptableObject
    {
        public CameraType cameraType;

        public Vector3 CameraPointOffest;











    }


    public enum CameraType
    { 
        [LabelText("第一人称")]
        FristPerson,
        [LabelText("第三人称")]
        ThirdPerson,
    
    
    
    
    }

}

