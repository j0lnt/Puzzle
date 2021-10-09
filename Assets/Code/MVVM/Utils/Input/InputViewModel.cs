using System;
using UnityEngine;


namespace MVVM
{
    internal sealed class InputViewModel : IExecutable, IInputViewModel
    {
        #region Properties

        internal IInputProxy Input { get; }
        public event Action<int> OnUITouchBegan;
        public event Action<int> OnPlayingFieldTouchBegan;

        #endregion


        #region ClassLifeCycles

        public InputViewModel(IInputProxy input)
        {
            Input = input;
            Input.TouchOnComplete += TouchHandler;
        }

        ~InputViewModel()
        {
            Input.TouchOnComplete -= TouchHandler;
        }

        #endregion


        #region Methods

        public void Execute(float deltaTime)
        {
            Input.GetTouch();
        }

        private void TouchHandler(Touch[] touches)
        {
            for (int i = 0; i < touches.Length; i++)
            {
                if (touches[i].phase.Equals(TouchPhase.Began))
                {
                    var go = Input.StandaloneInputModule.GetRaycastFromPointer().gameObject;
                    if (go == null)
                        throw new NullReferenceException($"This no GameObjects at raycast result");
                    else
                    {
                        switch (go.layer)
                        {
                            case 5:
                                OnUITouchBegan(go.GetInstanceID());
                                break;
                            case 8:
                                OnPlayingFieldTouchBegan(go.GetInstanceID());
                                break;
                            default:
                                break;
                        }
                    }

                }
            }
        }

        #endregion
    }
}
