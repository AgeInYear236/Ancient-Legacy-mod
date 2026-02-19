using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AncientLegacyMod.Common.Rarity;

namespace AncientLegacyMod.Content.Items.Weapons
{
    public class ShadowrazeBook : ModItem
    {
        float[] offsets = { 0f, 100f, 200f };
        int raze_count = 0;

        public override void SetDefaults()
        {
            Item.damage = 50;
            Item.DamageType = DamageClass.Magic;
            Item.width = 28;
            Item.height = 30;
            Item.useTime = 40;
            Item.useAnimation = 40;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.value = Item.buyPrice(gold: 3);
            Item.rare = ModContent.GetInstance<MagicRarity2>().Type;
            Item.UseSound = SoundID.Item45;
            Item.autoReuse = true;
            Item.mana = 30;
            Item.scale = 0.6f;

            Item.shoot = ModContent.ProjectileType<Projectiles.ShadowrazeProjectile>();
        }

        public override Vector2? HoldoutOffset() => new Vector2(-2, 4);

        public override bool Shoot(Player player, Terraria.DataStructures.EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float baseDistance = Vector2.Distance(player.Center, Main.MouseWorld);

            float finalDistance = baseDistance + offsets[raze_count];

            Vector2 mouseDir = (Main.MouseWorld - player.Center).SafeNormalize(Vector2.UnitX);
            Vector2 spawnPos = player.Center + mouseDir * finalDistance;

            Projectile.NewProjectile(source, spawnPos, Vector2.Zero, type, damage, knockback, player.whoAmI);

            raze_count++;
            if (raze_count >= 3) raze_count = 0;

            return false;
        }

        public override void HoldItem(Player player)
        {
            if (Main.myPlayer == player.whoAmI && !Main.dedServ)
            {
                float baseDistance = Vector2.Distance(player.Center, Main.MouseWorld);
                Vector2 mouseDir = (Main.MouseWorld - player.Center).SafeNormalize(Vector2.UnitX);

                for (int i = 0; i < offsets.Length; i++)
                {
                    Vector2 targetPos = player.Center + mouseDir * (baseDistance + offsets[i]);

                    float scale = (i == raze_count) ? 1.2f : 0.6f;
                    int alpha = (i == raze_count) ? 50 : 180;

                    if (Main.rand.NextBool(2))
                    {
                        Dust d = Dust.NewDustPerfect(
                            targetPos + Main.rand.NextVector2Circular(15, 15),
                            DustID.Shadowflame,
                            Vector2.Zero,
                            alpha,
                            new Color(121, 7, 23) * (i == raze_count ? 1f : 0.5f),
                            scale
                        );
                        d.noGravity = true;
                    }
                }
            }
        }
    }
}