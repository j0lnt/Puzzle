namespace MVVM
{
    internal interface ILevelViewModel
    {
        ILevelModel LevelModel { get; }

        void RestartButtonHandle();
        void MenuButtonHadle();
    }
}
