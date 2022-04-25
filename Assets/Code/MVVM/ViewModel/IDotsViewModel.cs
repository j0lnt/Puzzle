namespace MVVM
{
    internal interface IDotsViewModel
    {
        IDotsModel DotsModel { get; }
        ILevelViewModel LevelViewModel { get; }
        void InstantiateView();
    }
}
