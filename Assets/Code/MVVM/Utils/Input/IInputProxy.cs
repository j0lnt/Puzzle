using UnityEngine;
using System;
using UnityEngine.EventSystems;


namespace MVVM
{
    internal interface IInputProxy
    {
        CustomStandaloneInputModule StandaloneInputModule { get; }
        event Action<Touch[]> TouchOnComplete;
        void GetTouch();
    }
}
