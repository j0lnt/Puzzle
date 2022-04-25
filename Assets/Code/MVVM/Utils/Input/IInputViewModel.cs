using System;
using UnityEngine;

namespace MVVM
{
    internal interface IInputViewModel
    {
        event Action<int> OnUITouchBegan;
        event Action<int> OnPlayingFieldTouchBegan;
    }
}