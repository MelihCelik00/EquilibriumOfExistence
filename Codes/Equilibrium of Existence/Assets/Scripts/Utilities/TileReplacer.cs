using UnityEngine.Tilemaps;

namespace Utilities
{
    public static class TileReplacer
    {
        public static void PlaceTiles(TileBase tile, Tilemap activeTilemap)
        {
            activeTilemap.CompressBounds();
            foreach (var position in activeTilemap.cellBounds.allPositionsWithin)
            {
                if (!activeTilemap.HasTile(position)) continue;

                activeTilemap.SetTile(position, tile);
            }
        }
    }
}