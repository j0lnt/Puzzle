using UnityEngine;

namespace MVVM
{
    internal sealed class LevelModel : ILevelModel
    {
        #region Properties

        public GameObject LevelViewPrefab { get; private set; }
        public int[] FieldSize { get; private set; }

        #endregion


        #region ClassLifeCycles

        internal LevelModel(GameObject levelPrefab, int lines = 10, int columns = 10)
        {
            LevelViewPrefab = levelPrefab;
            FieldSize = new int[2] { lines, columns };
        }

        #endregion


        #region Methods



        #endregion
    }
}
