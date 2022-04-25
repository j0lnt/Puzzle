using System;
using UnityEngine;

namespace MVVM
{
    internal interface ILevelViewModel
    {
        ILevelModel LevelModel { get; }
        IInputViewModel UIInput { get; }
        IDotsViewModel DotsViewModel { get; }
        event Action<int> OnCellChange;
        event Action<Rect> OnResolutionChanged;
        event Action<Rect> UpdateView;
        event Action<Vector2Int, GameObject, Color> OnBallSpawned;

        void InstantiateView(ILevelViewModel levelViewModel);
        void AssignView(ILevelView levelView);
        void DisassignView(ILevelView levelView);
        void InitializeInput(IInputViewModel input);
        void InitializeDots(IDotsViewModel dotsViewModel);
        void RestartButtonHandle();
        void MenuButtonHandle();
    }
}
