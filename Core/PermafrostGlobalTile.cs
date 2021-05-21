using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using On.Terraria.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace StarlightRiver.Core
{
	class PermafrostGlobalTile : ModWorld
	{
		public override bool Autoload(ref string name)
		{
			IL.Terraria.IO.WorldFile.SaveWorldTiles += SaveExtraBits;
			IL.Terraria.IO.WorldFile.LoadWorldTiles += LoadExtraBits;
			return base.Autoload(ref name);
		}

		private void SaveExtraBits(ILContext il)
		{
			ILCursor c = new ILCursor(il);
			if( c.TryGotoNext(n => n.MatchLdloc(0), n => n.MatchLdloc(12), n => n.MatchLdloc(7), n => n.MatchStelemI1()) )
			{
				c.Index += 3;

				c.Emit(OpCodes.Ldloc, 5);
				c.Emit(OpCodes.Ldfld, typeof(Tile).GetField("bTileHeader3"));
				c.Emit(OpCodes.Ldc_I4_1);
				c.Emit(OpCodes.Shl);
				c.Emit(OpCodes.Ldc_I4, 0b11000001);
				c.Emit(OpCodes.And);
				c.Emit(OpCodes.Or);
				c.Emit(OpCodes.Conv_U1);

				c.TryGotoPrev(n => n.MatchLdloc(8));
				ILLabel label = il.DefineLabel(c.Next);

				c.TryGotoPrev(n => n.MatchLdloc(7));
				c.Emit(OpCodes.Br, label);
			}
		}

		private void LoadExtraBits(ILContext il)
		{
			ILCursor c = new ILCursor(il);

			if( c.TryGotoNext(n => n.MatchLdloc(5), n => n.MatchLdcI4(2), n => n.MatchAnd(), n => n.MatchLdcI4(2) ))
			{
				c.Emit(OpCodes.Ldloc, 7);
				c.Emit(OpCodes.Dup);
				c.Emit(OpCodes.Ldfld, typeof(Tile).GetField("bTileHeader3"));

				c.Emit(OpCodes.Ldloc, 5);
				c.Emit(OpCodes.Ldc_I4_1);
				c.Emit(OpCodes.Shr_Un);
				c.Emit(OpCodes.Ldc_I4, 0b11100000);
				c.Emit(OpCodes.And);
				c.Emit(OpCodes.Conv_U1);
				c.Emit(OpCodes.Or);
				c.Emit(OpCodes.Conv_U1);
				c.Emit(OpCodes.Stfld, typeof(Tile).GetField("bTileHeader3"));
			}
		}

		public override void PostDrawTiles()
		{
			Main.spriteBatch.Begin();

			for (int i = -2 + (int)(Main.screenPosition.X) / 16; i <= 2 + (int)(Main.screenPosition.X + Main.screenWidth) / 16; i++)
				for (int j = -2 + (int)(Main.screenPosition.Y) / 16; j <= 2 + (int)(Main.screenPosition.Y + Main.screenHeight) / 16; j++)
				{
					if (WorldGen.InWorld(i, j))
					{
						Tile tile = Framing.GetTileSafely(i, j);
						if ((tile.bTileHeader3 & 0b11100000) >> 5 == 1)
						{
							Rectangle target = new Rectangle((int)(i * 16 - Main.screenPosition.X), (int)(j * 16 - Main.screenPosition.Y), 16, 16);
							Main.spriteBatch.Draw(Main.magicPixel, target, Color.White * 0.5f);
						}
					}
				}

			Main.spriteBatch.End();
		}
	}
}
