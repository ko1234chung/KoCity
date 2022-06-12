using System.Collections;
using UnityEngine;
using KoCity.System;
using UnityEngine.Tilemaps;

namespace KoCity.Game
{
    public class AssetTest : MonoBehaviour
    {
        public Tilemap TestTilemap;
        public TileBase TestTileBase;
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            /*
            if (Input.GetKeyDown(KeyCode.A))
            {
                var collector = AssetLoadSystem.Instance.GetCollector("tiles");
                collector.LoadAssetAsync<TileBase>("Assets/BundleAsset/Tiles/TilesTexture/GrassTile1.asset", HandleLoadTileBack);
            }
            */
        }

        private void HandleLoadTileBack(TileBase tileBase)
        {
            Debug.Log("Load tile end");
            if (tileBase == null)
            {
                Debug.LogError("Tile Base for null");
                return;
            }
            TestTileBase = tileBase;
            TestTilemap.SetTile(new Vector3Int(0, 0, 0), tileBase);

        }
    }
}