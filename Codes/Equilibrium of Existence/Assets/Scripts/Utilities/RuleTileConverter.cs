using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Eoe.Utilities
{
    public static class RuleTileConverter
    {
        public static void ConvertTiles(Tilemap[] tilemaps)
        {
            foreach (var tilemap in tilemaps)
            {
                var spritesDict = new Dictionary<Sprite, List<Vector3Int>>();
                foreach (var position in tilemap.cellBounds.allPositionsWithin)
                {
                    var sprite = tilemap.GetSprite(position);

                    if (sprite != null)
                    {
                        if (!spritesDict.ContainsKey(sprite)) spritesDict.Add(sprite, new List<Vector3Int>());

                        spritesDict[sprite].Add(position);
                    }
                }

                foreach (var pair in spritesDict)
                {
                    var tile = ScriptableObject.CreateInstance<Tile>();
                    tile.sprite = pair.Key;
                    foreach (var position in pair.Value) tilemap.SetTile(position, tile);
                }
            }
        }
    }
}