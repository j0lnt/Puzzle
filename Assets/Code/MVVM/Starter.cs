using UnityEngine;


namespace MVVM
{
    internal sealed class Starter : MonoBehaviour
    {
        #region Fields

        private ResourceLoader _resourceLoader;

        #endregion


        #region UnityMethods

        private void Awake()
        {
            _resourceLoader = new ResourceLoader();
        }

        //private void Update()
        //{
            
        //}

        #endregion


        #region Methods



        #endregion
    }
}
