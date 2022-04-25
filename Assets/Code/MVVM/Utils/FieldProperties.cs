using System.Collections.Generic;
using UnityEngine;


namespace MVVM
{
    internal struct FieldProperties
    {
        internal Vector2Int FieldSize;
        internal Dictionary<Vector2Int, CellState> FieldMap;
    }
}
