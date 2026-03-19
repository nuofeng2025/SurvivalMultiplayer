using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Object;
using Sirenix.OdinInspector;

namespace FGame
{
    public class CharacterController : NetworkBehaviour
    {

        [SerializeField]
        [LabelText("控制模式")]
        private ControllerMode controllerMode;







        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }


















    }

    public enum ControllerMode
    { 
        [LabelText("第一人称")]
        FirstPerson,
        [LabelText("第三人称")]
        ThirdPerson,
    
    
    
    
    
    }



}
