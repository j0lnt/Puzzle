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
    }
}
