using System;
using UnityEngine;

namespace MVVM
{
    internal sealed class AndroidInput : IInputProxy
    {
        #region Properties

        public event Action<float, float> AxisOnChange;
        public event Action<Vector3> TouchAxisOnChange;
        public event Action<Touch> TouchOnChange;

        #endregion


        #region Methods

        public void GetAxisOnChange(float deltaTime)
        {
            //throw new NotImplementedException();
        }

        public void GetTouch(float deltaTime)
        {
            TouchOnChange.Invoke(Input.GetTouch(0));
        }

        #endregion
    }
}
