using System;
using UnityEngine;


namespace MVVM
{
    internal interface ILevelView
    {
        ViewProperties CurrentViewProperties { get; }
        event Action<Rect> SetUpResolution;

        void Initialize(ILevelViewModel levelViewModel, ViewProperties viewProperties);
    }
}
