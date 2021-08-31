namespace MVVM
{
    internal sealed class LevelModel : ILevelModel
    {
        #region Properties

        public int[] FieldSize { get; private set; }

        #endregion


        #region ClassLifeCycles

        internal LevelModel(int lines = 10, int columns = 10)
        {
            FieldSize = new int[2] { lines, columns };
        }

        #endregion


        #region Methods



        #endregion
    }
}
