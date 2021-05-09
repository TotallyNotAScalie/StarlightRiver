using Microsoft.Xna.Framework;
using StarlightRiver.Content.Items.BaseTypes;
using StarlightRiver.Content.Items.Misc;
using StarlightRiver.Core;
using System;
using Terraria;
using Terraria.ModLoader;

namespace StarlightRiver.Content.Items.Misc
{
    public class Ironheart : SmartAccessory
    {
        public override string Texture => AssetDirectory.MiscItem + Name;
        public override bool Autoload(ref string name)
        {
            StarlightPlayer.OnHitNPCEvent += OnHit;
            return base.Autoload(ref name);
        }
        public Ironheart() : base("Ironheart", "NaN") { }

        private void OnHit(Player player, Item item, NPC target, int damage, float knockback, bool crit)
        {
            if(Equipped(player))
                player.GetModPlayer<StarlightPlayer>().SetIronHeart(damage);
        }
    }

    public class IronheartBuff : ModBuff
    {
        public override bool Autoload(ref string name, ref string texture)
        {
            texture = AssetDirectory.MiscItem + name;
            return true;
        }

        public override void SetDefaults()
        {
            DisplayName.SetDefault("Ironheart");
            //Description.SetDefault("TODO: desc");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            StarlightPlayer mp = Main.LocalPlayer.GetModPlayer<StarlightPlayer>();

            int level = mp.ironheartHighestLevel - mp.ironheartLevel;
            mp.ironheartTimer += level + 1;

            if(mp.ironheartTimer >= 60)
            {
                mp.ironheartTimer -= 60;
                mp.ironheartLevel--;//doesn't need a cap because buff removes itself when time is below zero

                if (mp.ironheartLevel <= 0)
                    mp.ResetIronHeart();
            }

            player.statDefense += mp.ironheartLevel;

            float val = (float)level / StarlightPlayer.IronheartMaxLevel;

            player.GetModPlayer<ShieldPlayer>().OvershieldDrainRate = (int)MathHelper.Lerp(0, 60, val);
            Main.NewText((int)MathHelper.Lerp(0, 10, val));

            player.buffTime[buffIndex] = mp.ironheartLevel * 60;//visual time value
        }
    }
}

namespace StarlightRiver.Core
{
    public partial class StarlightPlayer : ModPlayer
    {
        public const int IronheartMaxLevel = 15;
        public const int IronheartMaxDamage = 75;

        public int ironheartHighestLevel = 0;
        public int ironheartLevel = 0;
        public int ironheartTimer = 0;

        public void SetIronHeart(int damage)
        {
            int buffType = ModContent.BuffType<IronheartBuff>();

            if (!player.HasBuff(buffType))
                ResetIronHeart();

                //Main.NewText("IronSet " + (Math.Min(damage, IronheartMaxDamage) / 15));
            int level = Math.Min(damage, IronheartMaxDamage) / 15;
            ironheartLevel += level;//increases level
            ironheartLevel = ironheartLevel > IronheartMaxLevel ? IronheartMaxLevel : ironheartLevel;//caps value


            Main.NewText("IronLevel " + ironheartLevel);
            Main.NewText("IronMax " + ironheartHighestLevel);
            if (ironheartLevel > 0)
            {
                if (ironheartLevel > ironheartHighestLevel)//if level was increased
                {
                    Main.NewText((ironheartLevel - ironheartHighestLevel));
                    player.GetModPlayer<ShieldPlayer>().Shield += (ironheartLevel - ironheartHighestLevel) * 2;
                    ironheartHighestLevel = ironheartLevel;//increases
                }
                else if(ironheartLevel < IronheartMaxLevel)
                    player.GetModPlayer<ShieldPlayer>().Shield += 1;

                player.AddBuff(buffType, 1);
            }
        }

        public void ResetIronHeart()
        {
            ironheartLevel = 0;
            ironheartHighestLevel = 0;
        }
    }
}