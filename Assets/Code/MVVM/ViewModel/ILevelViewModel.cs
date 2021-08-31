using System.Collections.Generic;


namespace MVVM
{
    internal interface ILevelViewModel
    {
        ILevelModel LevelModel { get; }

        void GenerateFieldMap(ILevelModel levelModel, bool isEmpty);
        void InstantiateView(ILevelViewModel levelViewModel);
        void RestartButtonHandle();
        void MenuButtonHandle();

        List<LevelView> Views { get; }
        int[,] FieldMap { get; }
        bool IsEmpty { get; }
    }
}
