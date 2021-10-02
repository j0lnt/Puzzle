namespace MVVM
{
    internal sealed class InputViewModel : IExecutable
    {
        #region Fields

        private readonly IInputProxy _input;

        #endregion


        #region Properties

        internal IInputProxy Input => _input;

        #endregion


        #region ClassLifeCycles

        public InputViewModel(IInputProxy input)
        {
            _input = input;
        }

        #endregion


        #region Methods

        public void Execute(float deltaTime)
        {
            _input.GetAxisOnChange(deltaTime);
            _input.GetTouch(deltaTime);
        }

        #endregion
    }
}
