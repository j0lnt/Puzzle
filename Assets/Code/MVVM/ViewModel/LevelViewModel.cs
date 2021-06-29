namespace MVVM
{
    internal sealed class LevelViewModel : ILevelViewModel
    {
        #region Properties

        public ILevelModel LevelModel { get; }

        #endregion


        #region ClassLifeCycles

        internal LevelViewModel(ILevelModel model)
        {
            LevelModel = model;
        }

        #endregion


        #region Methods


        public void RestartButtonHandle()
        {
            throw new System.NotImplementedException("Restart button not implemented");
        }

        public void MenuButtonHadle()
        {
            throw new System.NotImplementedException("Menu button not implemented");
        }

        #endregion
    }
}
