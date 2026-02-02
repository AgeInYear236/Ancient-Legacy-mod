using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using testMod1.Content.Buffs;

namespace testMod1.Content.Items.Accessories
{
    public class TAShield : ModItem
    {
        public static bool isActive1 = false;
        public static bool isActive2 = false;

        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 24;
            Item.accessory = true;
            Item.rare = ItemRarityID.LightRed;
            Item.value = 10000;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statLifeMax2 -= 100;

            if (!isActive1 && !isActive2) return;

            var refPlayer = player.GetModPlayer<TAShieldPlayer>();
            var meldPlayer = player.GetModPlayer<MeldPlayer>();


            if (isActive1 && !player.HasBuff(ModContent.BuffType<TA1CooldownBuff>()))
            {
                refPlayer.shieldCharges = 5;
                player.AddBuff(ModContent.BuffType<TA1Buff>(), 30 * 60);
                player.AddBuff(ModContent.BuffType<TA1CooldownBuff>(), 60 * 60);

                Terraria.Audio.SoundEngine.PlaySound(SoundID.Item37, player.Center);
            }
            if (isActive2 && !player.HasBuff(ModContent.BuffType<TA2Buff>()) && !player.HasBuff(ModContent.BuffType<TA2CooldownBuff>()))
            {
                 meldPlayer.isAmbushActive = true;
                 meldPlayer.ambushTimer = 0;
                 player.AddBuff(ModContent.BuffType<TA2Buff>(), 11 * 60);
            }

        }
    }
}
