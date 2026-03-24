using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FGame
{
    public class UiSystem : MonoBehaviour,ISystem
    {
        public InteractionUi interactionUi;
        public GameObject crosshair;



        public void Init()
        {
            interactionUi.Init();

     



        }



            // Start is called before the first frame update
        void Start()
        {

        }



        public void OnDestroy()
        {
            
        }






    }

}

