using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace FGame
{
    public interface IPlane
    {
        void Open(Action<IPlane> action);
        void Init();

        void Close();



        
    }

}
