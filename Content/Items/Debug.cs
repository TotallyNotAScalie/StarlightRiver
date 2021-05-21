using StarlightRiver.Codex;
using StarlightRiver.Content.Tiles.CrashTech;
using StarlightRiver.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.Content.Items
{
    class DebugPotion : ModItem
    {
        public override string Texture => AssetDirectory.VitricItem + "VitricPick";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Debug Stick");
            Tooltip.SetDefault("How did you get this?");
        }

        public override void SetDefaults()
        {
            item.damage = 10;
            item.melee = true;
            item.width = 38;
            item.height = 38;
            item.useTime = 18;
            item.useAnimation = 18;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.knockBack = 5f;
            item.value = 1000;
            item.rare = ItemRarityID.Green;
            item.autoReuse = true;
            item.UseSound = SoundID.Item18;
            item.useTurn = true;
            item.accessory = true;

            //item.createTile = ModContent.TileType<CrashPod>();
        }

		public override void UpdateEquip(Player player)
		{
            player.GetModPlayer<CritMultiPlayer>().AllCritMult += 1;
		}

		public override void UpdateInventory(Player player)
        {
            Tile tile = Framing.GetTileSafely(Player.tileTargetX, Player.tileTargetY);
            Main.NewText(tile.bTileHeader3.ToString("D"));
        }

        public override bool UseItem(Player player)
        {
            Tile tile = Framing.GetTileSafely(Player.tileTargetX, Player.tileTargetY);

            tile.bTileHeader3 |= 0b00100000;

            player.GetModPlayer<CodexHandler>().CodexState = 1;

            foreach (CodexEntry entry in player.GetModPlayer<CodexHandler>().Entries)
                entry.Locked = true;

            return true;
        }
    }
}
