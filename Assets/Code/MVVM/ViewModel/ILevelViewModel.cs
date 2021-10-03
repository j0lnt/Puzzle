using System.Collections.Generic;


namespace MVVM
{
    internal interface ILevelViewModel
    {
        ILevelModel LevelModel { get; }
        IInputProxy PlayerInput { get; }

        void InstantiateView(ILevelViewModel levelViewModel, int cellCount, bool isEmpty);
        void InitializeInput(IInputProxy input);
        void RestartButtonHandle();
        void MenuButtonHandle();

        List<ILevelView> Views { get; }
        int[,] FieldMap { get; }
        bool IsEmpty { get; }
    }
}
