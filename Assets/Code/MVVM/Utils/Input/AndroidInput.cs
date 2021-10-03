using System;
using UnityEngine;


namespace MVVM
{
    internal sealed class AndroidInput : IInputProxy
    {
        #region Properties

        public event Action<Touch[]> TouchOnComplete;

        #endregion


        #region Methods

        public void GetTouch()
        {
            TouchOnComplete.Invoke(Input.touches);
        }

        #endregion
    }
}
