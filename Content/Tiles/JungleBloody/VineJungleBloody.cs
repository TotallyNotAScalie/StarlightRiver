using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using static Terraria.ModLoader.ModContent;

using StarlightRiver.Core;

namespace StarlightRiver.Content.Tiles.JungleBloody
{
    public class VineJungleBloody : ModVine
    {
        public VineJungleBloody() : base(new string[] { "GrassJungleBloody" }, 14, new Color(122, 38, 38), 2, path: AssetDirectory.JungleBloodyTile) { }

        public override void NearbyEffects(int i, int j, bool closer) => Grow(i, j, 120);//grows quickly if nearby
    }
}