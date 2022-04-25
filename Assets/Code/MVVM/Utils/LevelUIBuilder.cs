using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace MVVM
{
    internal sealed class LevelUIBuilder
    {
        #region Fields

        private GameObject _gameObject;
        private List<Component> _uiComponents;        

        #endregion


        #region Methods

        internal LevelUIBuilder() => _gameObject = new GameObject();
        internal LevelUIBuilder(GameObject gameObject) => _gameObject = gameObject;

        public static implicit operator GameObject(LevelUIBuilder builder)
        {
            return builder._gameObject;
        }

        private T GetOrAddComponent<T>() where T : Component
        {
            var result = _gameObject.GetComponent<T>();
            if (!result)
            {
                result = _gameObject.AddComponent<T>();
            }
            return result;
        }

        internal LevelUIBuilder Name(string name)
        {
            _gameObject.name = name;
            return this;
        }

        internal LevelUIBuilder Tag(string tag)
        {
            _gameObject.tag = tag;
            return this;
        }

        internal LevelUIBuilder Layer(LayerMask layerMask)
        {
            _gameObject.layer = layerMask;
            return this;
        }


        internal LevelUIBuilder UI_Canvases()
        {
            if (_uiComponents == null) _uiComponents = new List<Component>();
            else _uiComponents.Clear();

            _uiComponents.Add(GetOrAddComponent<RectTransform>());
            _uiComponents.Add(GetOrAddComponent<Canvas>());
            _uiComponents.Add(GetOrAddComponent<CanvasScaler>());
            _uiComponents.Add(GetOrAddComponent<GraphicRaycaster>());

            return this;
        }

        internal LevelUIBuilder LevelView()
        {
            if (_uiComponents == null) UI_Canvases();
            GetOrAddComponent<LevelView>();
            return this;
        }

        #endregion
    }
}
