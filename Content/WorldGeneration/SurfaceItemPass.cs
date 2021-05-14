using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.World.Generation;
using static Terraria.ModLoader.ModContent;

using StarlightRiver.Core;
using StarlightRiver.Helpers;
using StarlightRiver.Content.Tiles.Forest;
using StarlightRiver.Content.Tiles.Herbology;
using System;
using StarlightRiver.Content.Items.Beach;

namespace StarlightRiver.Core
{
    public partial class StarlightWorld : ModWorld
    {
        private void SurfaceItemPass(GenerationProgress progress)
        {
            progress.Message = "Placing treasures";
            //Seaglass ring
            PlaceSeaglassRing(0, 300);
            PlaceSeaglassRing(Main.maxTilesX - 300, Main.maxTilesY);
        }

        private bool PlaceSeaglassRing(int xStart, int xEnd)
		{
            for (int x = xStart; x < xEnd; x++)
                for (int y = 200; y < Main.maxTilesY; y++)
                {
                    var tile = Framing.GetTileSafely(x, y);

                    if (tile.active() && tile.type != TileID.Sand || tile.liquid > 0)
                        break;
                    else if (tile.active() && tile.type == TileID.Sand && WorldGen.genRand.Next(40) == 0)
                    {
                        WorldGen.PlaceTile(x, y - 1, TileType<SeaglassRingTile>());
                        return true;
                    }
                }

            return false;
        }
    }
}