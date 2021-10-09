namespace MVVM
{
    internal sealed class DotsModel : IDotsModel
    {
        #region Properties

        public DotsProperties DotsProperties { get; }

        #endregion


        #region ClassLifeCycles

        internal DotsModel(DotsProperties dotsProperties)
        {
            DotsProperties = dotsProperties;
        }

        #endregion
    }
}
