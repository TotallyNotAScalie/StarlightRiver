﻿using Microsoft.Xna.Framework;
using StarlightRiver.Core.Loaders;
using Terraria.ID;
using Terraria;
using static Terraria.ModLoader.ModContent;

using StarlightRiver.Core;

namespace StarlightRiver.Content.Tiles.Vitric
{
    class BasicVitricTileLoader : TileLoader
    {
        public override string AssetRoot => AssetDirectory.VitricTile;

		public override void Load()
		{
			LoadTile(
				"AncientSandstone",
				"Ancient Sandstone",
				new TileLoadData(
					minPick: 200,
					dustType: DustID.Copper,
					soundType: SoundID.Tink,
					mapColor: new Color(160, 115, 75)
					)
				);

			LoadTile(
				"AncientSandstoneTile",
				"Ancient Sandstone Tile",
				new TileLoadData(
					minPick: 200,
					dustType: DustID.Copper,
					soundType: SoundID.Tink,
					mapColor: new Color(160, 115, 75)
					)
				);

			LoadTile(
				"VitricBrick",
				"Vitric Bricks",
				new TileLoadData(
					minPick: 0,
					dustType: DustType<Dusts.GlassNoGravity>(),
					soundType: SoundID.Shatter,
					mapColor: new Color(190, 255, 245)
				)
			  );

			LoadTile(
				"VitricCactus",
				"Crystal Cactus",
				new TileLoadData(
					minPick: 0,
					dustType: DustType<Dusts.GlassNoGravity>(),
					soundType: SoundID.Shatter,
					mapColor: new Color(190, 255, 245)
				)
			  );

			LoadTile(
				"VitricGlass",
				"Vitric Glass",
				new TileLoadData(
					minPick: 0,
					dustType: DustType<Dusts.GlassNoGravity>(),
					soundType: SoundID.Shatter,
					mapColor: new Color(190, 255, 245)
				)
			  );

			LoadTile(
				"VitricSand",
				"Glassy Sand",
				new TileLoadData(
					minPick: 0,
					dustType: DustType<Content.Dusts.AirGravity>(),
					soundType: SoundID.Dig,
					mapColor: new Color(172, 131, 105)
				)
			  );

			LoadTile(
				"VitricSoftSand",
				"Soft Glassy Sand",
				new TileLoadData(
					minPick: 0,
					dustType: DustType<Content.Dusts.AirGravity>(),
					soundType: SoundID.Dig,
					mapColor: new Color(182, 141, 115)
				)
			  );

			LoadTile(
                "VitricSandPlain",
                "Soft Sand",
                new TileLoadData(
                    minPick: 0,
                    dustType: 32,
                    soundType: SoundID.Dig,
                    mapColor: new Color(192, 145, 110)
                )
              );
		}

        public override void PostLoad()
        {
            AddMerge("VitricSoftSand", "VitricSand");
			AddMerge("VitricSoftSand", "VitricSandPlain");
			AddMerge("VitricSoftSand", "VitricSand");

			var sands = new int[] { TileID.Sand, TileID.Ebonsand, TileID.Crimsand, TileID.Pearlsand,
                TileID.Sandstone, TileID.CorruptSandstone, TileID.CrimsonSandstone, TileID.HallowSandstone,
                TileID.HardenedSand, TileID.CorruptHardenedSand, TileID.CrimsonHardenedSand, TileID.HallowHardenedSand };

			AddMerge("VitricSandPlain", sands);

            AddMerge("VitricSoftSand", sands);

            AddMerge("VitricSand", sands);

			AddMerge("VitricSandPlain", TileType<VitricSpike>());
			AddMerge("VitricSoftSand", TileType<VitricSpike>());
			AddMerge("VitricSand", TileType<VitricSpike>());

            AddMerge("AncientSandstone", "AncientSandstoneTile");
			AddMerge("AncientSandstone", "VitricSoftSand");
			AddMerge("AncientSandstone", "VitricSandPlain");
			AddMerge("AncientSandstone", "VitricSand");
		}
    }
}
