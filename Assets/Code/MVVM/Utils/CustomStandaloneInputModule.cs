using UnityEngine.EventSystems;
using UnityEngine;
using System.Collections.Generic;


namespace MVVM
{
    internal sealed class CustomStandaloneInputModule : StandaloneInputModule
    {
        #region Methods

        public GameObject GetHovered()
        {
            var mouseEvent = GetLastPointerEventData(0);
            if (mouseEvent == null)
                return null;
            return mouseEvent.pointerCurrentRaycast.gameObject;
        }

        public List<GameObject> GetAllHovered()
        {
            var mouseEvent = GetLastPointerEventData(0);
            if (mouseEvent == null)
                return null;
            return mouseEvent.hovered;
        }

        #endregion
    }
}
