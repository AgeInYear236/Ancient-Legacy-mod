using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using testMod1.Common.Rarity;
using testMod1.Content.Buffs;
using testMod1.Content.Items.Projectiles;

namespace testMod1.Content.Items.Accessories
{
    public class Chronosphere : ModItem
    {
        public static bool isActive = false;

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.accessory = true;
            Item.rare = ModContent.GetInstance<AccRarityAura>().Type;
            Item.value = Item.sellPrice(gold: 5);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (isActive && !player.HasBuff(ModContent.BuffType<ChronosphereCooldownBuff>()))
            {
                player.AddBuff(ModContent.BuffType<ChronosphereBuff>(), 10 * 60);
            }
        }
    }

    public class ChronoPlayer : ModPlayer
    {

        public override void PostUpdate()
        {
            if (Player.HasBuff(ModContent.BuffType<ChronosphereBuff>()))
            {
                Player.AddBuff(ModContent.BuffType<ChronosphereCooldownBuff>(), 1 * 60); 

                if (Player.whoAmI == Main.myPlayer)
                {
                    Projectile.NewProjectile(Player.GetSource_Accessory(Player.HeldItem), Player.Center, Vector2.Zero, ModContent.ProjectileType<ChronosphereProjectile>(), 0, 0f, Player.whoAmI);
                }

                Terraria.Audio.SoundEngine.PlaySound(SoundID.Item117, Player.Center);
            }
        }
    }
}