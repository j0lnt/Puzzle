namespace MVVM
{
    internal interface IPlayingFieldModel
    {
        int[] FieldSize { get; }
        bool IsMoveAllowed { get; }
    }
}