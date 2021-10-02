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
        public List<ILevelView> Views { get; private set; }
        public int[,] FieldMap { get; private set; } 
        public bool IsEmpty { get; }

        #endregion


        #region ClassLifeCycles

        internal LevelViewModel(ILevelModel model, bool isEmpty = true)
        {
            Views = new List<ILevelView>();
            
            LevelModel = model;
            IsEmpty = isEmpty;

            var cellCount = LevelModel.FieldSize[0] * LevelModel.FieldSize[1];

            InstantiateView(this, cellCount, IsEmpty);
        }

        #endregion


        #region Methods

        public void InstantiateView(ILevelViewModel levelViewModel, int cellCount, bool isEmpty)
        {
            
            var go = UnityEngine.GameObject.Instantiate(levelViewModel.LevelModel.LevelViewPrefab);
            var view = go.GetComponent<ILevelView>();
            //view.MainCanvas.enabled = false;
            view.Initialize(levelViewModel, cellCount, isEmpty);

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
