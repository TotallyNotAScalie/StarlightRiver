using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using static Terraria.ModLoader.ModContent;

using StarlightRiver.Core;

namespace StarlightRiver.Content.Tiles.JungleHoly
{
    public class VineJungleHoly : ModVine
    {
        public VineJungleHoly() : base(new string[] { "GrassJungleHoly" }, 14, new Color(48, 141, 128), 2, path: AssetDirectory.JungleHolyTile) { }

        public override void NearbyEffects(int i, int j, bool closer) => Grow(i, j, 120);//grows quickly if nearby
    }
}