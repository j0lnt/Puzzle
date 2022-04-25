using System;
using UnityEngine;
using UnityEngine.EventSystems;


namespace MVVM
{
    internal sealed class AndroidInput : IInputProxy
    {
        #region Properties

        public event Action<Touch[]> TouchOnComplete;
        public CustomStandaloneInputModule StandaloneInputModule { get; }

        #endregion


        #region ClassLifeCycles

        internal AndroidInput(CustomStandaloneInputModule standaloneInputModule)
        {
            StandaloneInputModule = standaloneInputModule;
        }

        #endregion


        #region Methods

        public void GetTouch()
        {
            TouchOnComplete.Invoke(Input.touches);
        }

        #endregion
    }
}
