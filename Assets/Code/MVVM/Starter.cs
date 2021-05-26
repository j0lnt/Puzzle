using UnityEngine;


namespace MVVM
{
    internal sealed class Starter : MonoBehaviour
    {
        #region Fields

        private ResourceLoader _resourceLoader;
        private ILevelModel _levelModel;
        private ILevelViewModel _levelViewModel;

        #endregion


        #region UnityMethods

        private void Awake()
        {
            _resourceLoader = new ResourceLoader();

            _levelModel = new LevelModel();
            _levelViewModel = new LevelViewModel(_levelModel);
        }

        //private void Update()
        //{
            
        //}

        #endregion


        #region Methods



        #endregion
    }
}
