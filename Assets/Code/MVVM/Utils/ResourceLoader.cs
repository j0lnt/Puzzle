using System.Collections.Generic;
using UnityEngine;


namespace MVVM
{
    internal sealed class ResourceLoader
    {
        #region Fields

        private Dictionary<string, GameObject> _prefabs;
        private Dictionary<string, Sprite> _sprites;

        private string _prefabsPath;
        private string _spritesPath;

        #endregion


        #region ClassLifeCycles

        internal ResourceLoader(string prefabsPath = "Prefabs/", string spritesPath = "Sprites/")
        {
            _prefabs = new Dictionary<string, GameObject>();
            _sprites = new Dictionary<string, Sprite>();

            _prefabsPath = prefabsPath;
            _spritesPath = spritesPath;
        }

        #endregion


        #region Methods

        internal GameObject LoadPrefab(string name)
        {
            if (!_prefabs.ContainsKey(name)) _prefabs.Add(name, Resources.Load<GameObject>(_prefabsPath + name));
            return _prefabs[name];
        }

        internal Sprite LoadSprite(string name)
        {
            if (!_sprites.ContainsKey(name)) _sprites.Add(name, Resources.Load<Sprite>(_spritesPath + name));
            return _sprites[name];
        }

        #endregion
    }
}