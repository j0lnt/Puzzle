using UnityEngine;

namespace MVVM
{
    internal interface ILevelView
    {
        Canvas MainCanvas { get; }

        void Initialize(ILevelViewModel levelViewModel, int cellCount, bool isEmprty);

        //debug
        bool HideCellByName(string name);
    }
}
