using UnityEngine;

namespace MVVM
{
    internal interface ILevelModel
    {
        GameObject LevelViewPrefab { get; }
        int [] FieldSize { get; }
    }
}
