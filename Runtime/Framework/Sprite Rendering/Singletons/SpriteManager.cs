using Rich.System;
using System.Linq;
using UnityEngine;

namespace Rich.SpriteRendering
{
    public class SpriteManager : Singleton<SpriteManager>
    {
        private static GameObject[] _sprites;

        void Awake()
        {
            _sprites = Resources.LoadAll<GameObject>("").Where(x => x.GetComponent<SpriteSheetRenderer>() != null).ToArray();
        }

        public static SpriteSheetRenderer CreateSprite(string spriteName, Vector3 position)
        {
            var spritePrefab = _sprites.FirstOrDefault(x => x.name == spriteName);
            var sprite = Instantiate(spritePrefab).GetComponent<SpriteSheetRenderer>();
            sprite.transform.position = position;
            return sprite;
        }
    }
}