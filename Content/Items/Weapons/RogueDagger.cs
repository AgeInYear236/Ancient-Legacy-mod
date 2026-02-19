using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using AncientLegacyMod.Common.Rarity;
using AncientLegacyMod.Content.Items.Projectiles;

namespace AncientLegacyMod.Content.Items.Weapons
{
    public class RogueDagger : ModItem
    {
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.Terragrim);
            Item.damage = 20;
            Item.noUseGraphic = true;
            Item.useTurn = true;
            Item.rare = ModContent.GetInstance<MeleeRarity>().Type;


            Item.scale *= 0.8f;
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                Item.useStyle = ItemUseStyleID.Thrust;
                Item.shoot = ProjectileID.None;
                Item.useTime = 10;
                Item.useAnimation = 10;
                Item.useTurn = true;
                Item.noMelee = true;
                return true;
            }
            else
            {
                Item.DamageType = DamageClass.Melee;
                Item.useStyle = ItemUseStyleID.Shoot;
                Item.shoot = ProjectileID.Terragrim;
                Item.damage = 42;
                Item.useTurn = true;
            }
            return base.CanUseItem(player);
        }


        public override bool? UseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                modPlayer1 modPlayer = player.GetModPlayer<modPlayer1>();

                if (!modPlayer.CanUseRogueDash())
                {
                    return false;
                }

                Vector2 dir = (Main.MouseWorld - player.Center).SafeNormalize(Vector2.UnitX);
                if (dir == Vector2.Zero) dir = new Vector2(player.direction, 0);


                float dashSpeed = 20f;
                int dashDuration = 15;

                player.velocity.X = dir.X * dashSpeed;
                player.velocity.Y = dir.Y * dashSpeed;

                player.immune = true;
                player.immuneNoBlink = true;
                player.immuneTime = dashDuration;

                SoundEngine.PlaySound(SoundID.Item12 with { Pitch = -0.2f }, player.position);
                for (int i = 0; i < 30; i++)
                {
                    Dust.NewDustPerfect(
                        player.Center,
                        DustID.Cloud,
                        dir.RotatedByRandom(0.8f) * Main.rand.NextFloat(2f, 10f),
                        0,
                        new Color(0, 204, 0),
                        1.5f
                    );
                }

                for (int i = 0; i < 15; i++)
                {
                    Dust.NewDustPerfect(
                        player.Center,
                        DustID.TintableDustLighted,
                        dir.RotatedByRandom(0.8f) * Main.rand.NextFloat(2f, 10f),
                        0,
                        new Color(102, 204, 0),
                        1.2f
                    ).noGravity = true;
                }


                modPlayer.UseRogueDash();


                return true;
            }
            else
            {
                return true;
            }

        }

        public override void HoldItem(Player player)
        {
            if (Main.myPlayer == player.whoAmI)
            {
                player.ChangeDir(Main.MouseWorld.X < player.Center.X ? -1 : 1);
            }

            Vector2 dir = (Main.MouseWorld - player.MountedCenter).SafeNormalize(Vector2.UnitX);
            player.itemRotation = dir.ToRotation();
            if (player.direction == -1) player.itemRotation += MathHelper.Pi;
        }
    }
}
