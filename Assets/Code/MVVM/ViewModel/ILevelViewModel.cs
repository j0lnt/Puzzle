using System;


namespace MVVM
{
    internal interface ILevelViewModel
    {
        ILevelModel LevelModel { get; }
        IInputViewModel UIInput { get; }
        IDotsViewModel DotsViewModel { get; }
        bool IsEmpty { get; }
        event Action<int> OnCellChange;

        void InstantiateView(ILevelViewModel levelViewModel, int cellCount, bool isEmpty);
        void InitializeInput(IInputViewModel input);
        void InitializeDots(IDotsViewModel dotsViewModel);
        void RestartButtonHandle();
        void MenuButtonHandle();

    }
}
