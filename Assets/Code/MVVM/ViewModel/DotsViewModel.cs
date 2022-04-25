namespace MVVM
{
    internal sealed class DotsViewModel : IDotsViewModel
    {
        #region Properties

        public IDotsModel DotsModel { get; }
        public ILevelViewModel LevelViewModel { get; }

        #endregion


        #region ClassLifeCycles

        internal DotsViewModel(IDotsModel dotsModel, ILevelViewModel levelViewModel)
        {
            DotsModel = dotsModel;
            LevelViewModel = levelViewModel;
        }

        #endregion


        #region Methods

        public void InstantiateView()
        {
            LevelViewModel.InitializeDots(this);
        }

        #endregion
    }
}
