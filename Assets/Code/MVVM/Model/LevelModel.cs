namespace MVVM
{
    internal sealed class LevelModel : ILevelModel
    {
        #region Properties

        public int[] FieldSize { get; private set; }
        public int[,] FieldMap { get; private set; }

        #endregion


        #region ClassLifeCycles

        internal LevelModel(int lines = 10, int columns = 10)
        {
            System.Random rnd = new System.Random();
            FieldSize = new int[2] { lines, columns };
            GenerateRandomMap(FieldSize, rnd);
        }

        #endregion


        #region Methods

        private void GenerateRandomMap(int[] size, System.Random rnd)
        {
            FieldMap = new int[size[0], size[1]];
            for (int i = 0; i < size[0]; i++)
            {
                for (int j = 0; j < size[1]; j++)
                {
                    FieldMap[i, j] = rnd.Next(0,2);
                }
            }
        }

        #endregion
    }
}
