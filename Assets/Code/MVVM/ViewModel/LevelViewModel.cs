using System.Collections.Generic;


namespace MVVM
{
    internal sealed class LevelViewModel : ILevelViewModel
    {
        #region Fields

        private System.Random _random = new System.Random();

        #endregion


        #region Properties

        public ILevelModel LevelModel { get; }
        public List<LevelView> Views { get; private set; }
        public int[,] FieldMap { get; private set; }
        public bool IsEmpty { get; }

        #endregion


        #region ClassLifeCycles

        internal LevelViewModel(ILevelModel model, bool isEmpty = false)
        {
            Views = new List<LevelView>();
            
            LevelModel = model;
            IsEmpty = isEmpty;

            GenerateFieldMap(LevelModel, IsEmpty);

            InstantiateView(this);
        }

        #endregion


        #region Methods

        public void GenerateFieldMap(ILevelModel levelModel, bool isEmpty)
        {
            if (!isEmpty) RandomFieldMap(levelModel.FieldSize);
        }

        private void RandomFieldMap(int[] size)
        {
            FieldMap = new int[size[0], size[1]];
            for (int i = 0; i < size[0]; i++)
            {
                for (int j = 0; j < size[1]; j++)
                {
                    FieldMap[i, j] = _random.Next(0, 2);
                }
            }
        }

        public void InstantiateView(ILevelViewModel levelViewModel)
        {
            var view = new LevelView();
            view.Initialize(levelViewModel);

            Views.Add(view);
        }

        public void RestartButtonHandle()
        {
            throw new System.NotImplementedException("Restart button not implemented");
        }

        public void MenuButtonHandle()
        {
            throw new System.NotImplementedException("Menu button not implemented");
        }

        #endregion
    }
}
