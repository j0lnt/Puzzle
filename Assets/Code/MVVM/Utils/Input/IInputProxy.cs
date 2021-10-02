using UnityEngine;
using System;

namespace MVVM
{
    internal interface IInputProxy
    {
        event Action<float, float> AxisOnChange;
        event Action<Vector3> TouchAxisOnChange;
        event Action<Touch> TouchOnChange;
        void GetAxisOnChange(float deltaTime);
        void GetTouch(float deltaTime);
    }
}
