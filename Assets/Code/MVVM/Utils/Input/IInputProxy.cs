using UnityEngine;
using System;

namespace MVVM
{
    internal interface IInputProxy
    {
        event Action<Touch[]> TouchOnComplete;
        void GetTouch();
    }
}
