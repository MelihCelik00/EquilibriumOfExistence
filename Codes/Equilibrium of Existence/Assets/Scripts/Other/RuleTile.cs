using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Eoe.Other
{
    public class RuleTile : MonoBehaviour
    {
        private Tilemap tilemap;
        [SerializeField] private List<TileBase> tile;
        [SerializeField] private bool button;

        private void PlaceTiles()
        {
            tilemap = GetComponent<Tilemap>();
            tilemap.CompressBounds();
            foreach (var position in tilemap.cellBounds.allPositionsWithin) {
            
                if (tilemap.HasTile(position))
                {
                    var neighbours = Neighbours((Vector2Int) position);

                    if (!neighbours[0] && !neighbours[2] && !neighbours[4] && !neighbours[6])
                    {
                        tilemap.SetTile(position,tile[16]);
                    }

                    else if (neighbours[0] && !neighbours[2] && !neighbours[4] && !neighbours[6])
                    {
                        tilemap.SetTile(position,tile[13]);
                    }

                    else if (neighbours[0] && !neighbours[2] && neighbours[4] && !neighbours[6])
                    {
                        tilemap.SetTile(position,tile[14]);
                    }

                    else if (!neighbours[0] && !neighbours[2] && neighbours[4] && !neighbours[6])
                    {
                        tilemap.SetTile(position,tile[15]);
                    }
                
                    // 8
                    else if (neighbours[0] && !neighbours[1] && neighbours[2] && neighbours[4] && neighbours[6])
                    {
                        tilemap.SetTile(position,tile[8]);
                    }
                
                    // 9
                    else if (neighbours[0] && neighbours[2] && !neighbours[3] && neighbours[4] && neighbours[6])
                    {
                        tilemap.SetTile(position,tile[9]);
                    }
                
                    // 10
                    else if (neighbours[0] && neighbours[2] && neighbours[4] && !neighbours[5] && neighbours[6])
                    {
                        tilemap.SetTile(position,tile[10]);
                    }
                
                    // 11
                    else if (neighbours[0] && neighbours[2] && neighbours[4] && neighbours[6] && !neighbours[7] )
                    {
                        tilemap.SetTile(position,tile[11]);
                    }
                
                    // 0
                    else if (!neighbours[0] && neighbours[2] && neighbours[4] && neighbours[6])
                    {
                        tilemap.SetTile(position,tile[0]);
                    }
                
                    // 1
                    else if (!neighbours[0] && !neighbours[2] && neighbours[4] && neighbours[6])
                    {
                        tilemap.SetTile(position,tile[1]);
                    }
                
                    // 2
                    else if (neighbours[0] && !neighbours[2] && neighbours[4] && neighbours[6])
                    {
                        tilemap.SetTile(position,tile[2]);
                    }
                
                    // 3
                    else if (neighbours[0] && !neighbours[2] && !neighbours[4] && neighbours[6])
                    {
                        tilemap.SetTile(position,tile[3]);
                    }
                
                    // 4
                    else if (neighbours[0] && neighbours[2] && !neighbours[4] && neighbours[6])
                    {
                        tilemap.SetTile(position,tile[4]);
                    }
                
                    // 5
                    else if (neighbours[0] && neighbours[2] && !neighbours[4] && !neighbours[6])
                    {
                        tilemap.SetTile(position,tile[5]);
                    }
                
                    // 6
                    else if (neighbours[0] && neighbours[2] && neighbours[4] && !neighbours[6])
                    {
                        tilemap.SetTile(position,tile[6]);
                    }
                
                    // 7
                    else if (!neighbours[0] && neighbours[2] && neighbours[4] && !neighbours[6])
                    {
                        tilemap.SetTile(position,tile[7]);
                    }
                
                    // 12
                    else
                    {
                        tilemap.SetTile(position,tile[12]);
                    }
                }
            }
        }

        private bool[] Neighbours(Vector2Int pos)
        {
            bool [] neighbours = new bool [8];
            neighbours[0] = IsFilled(pos,Vector2Int.right);
            neighbours[1] = IsFilled(pos,new Vector2Int(1,1));
            neighbours[2] = IsFilled(pos,Vector2Int.up);
            neighbours[3] = IsFilled(pos,new Vector2Int(-1,1));
            neighbours[4] = IsFilled(pos,Vector2Int.left);
            neighbours[5] = IsFilled(pos,new Vector2Int(-1,-1));
            neighbours[6] = IsFilled(pos,Vector2Int.down);
            neighbours[7] = IsFilled(pos,new Vector2Int(1,-1));
            return neighbours;
        }

        private bool IsFilled(Vector2Int pos, Vector2Int dir)
        {
            return tilemap.HasTile((Vector3Int)(pos + dir));
        }

        private void OnValidate()
        {
            if (!button) return;
            button = false;
            Debug.Log("Tiles are placed!");
            PlaceTiles();
        }
    }
}
