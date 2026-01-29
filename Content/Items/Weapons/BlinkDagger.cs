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

namespace testMod1.Content.Items.Weapons
{
    public class BlinkDagger : ModItem
    {
        public override void SetDefaults() {
            Item.damage = 25;
            Item.DamageType = DamageClass.Melee;
            Item.width = 40;
            Item.height = 40;
            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.useStyle = ItemUseStyleID.Thrust;
            Item.knockBack = 1;
            Item.value = 10000;
            Item.rare = ItemRarityID.Purple;
            Item.autoReuse = true;
            Item.UseSound = SoundID.Item1;
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
                Item.useTime = 10;
                Item.useAnimation = 10;
                Item.noMelee = true; 
            }
            else
            {
                Item.useStyle = ItemUseStyleID.Thrust;
                Item.crit = 20;
                Item.useTime = 15;
                Item.useAnimation = 15;
                Item.noMelee = false;
            
            }
            return base.CanUseItem(player);
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (player.altFunctionUse != 2)
            {
                Vector2 direction = (target.Center - player.Center).SafeNormalize(Vector2.UnitX);
                float bladeLength = 40f;

                for (int i = 0; i < 5; i++)
                {
                    float progress = i / 4f;
                    Vector2 particlePos = player.MountedCenter + direction * (bladeLength * progress);

                    Dust.NewDustPerfect(
                        particlePos,
                        DustID.Electric,
                        Vector2.Zero,
                        0,
                        default,
                        1.1f
                    ).noGravity = true;
                }
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

        public override bool? UseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                modPlayer1 modPlayer = player.GetModPlayer<modPlayer1>();
                if (!modPlayer.CanUseBlinkDash()) return false;

                Vector2 dir = (Main.MouseWorld - player.Center).SafeNormalize(Vector2.UnitX);
                if (dir == Vector2.Zero) dir = new Vector2(player.direction, 0);

                float maxDistance = 300f;
                float actualDistance = Vector2.Distance(Main.MouseWorld, player.Center);

                Vector2 targetCenter;
                if (actualDistance > maxDistance)
                {
                    targetCenter = player.Center + dir * maxDistance;
                }
                else
                {
                    targetCenter = Main.MouseWorld;
                }

                int tileX = (int)(targetCenter.X / 16f);
                int tileY = (int)(targetCenter.Y / 16f);

                if (WorldGen.SolidTile(tileX, tileY) || Main.tile[tileX, tileY].LiquidAmount > 0)
                {
                    //Main.NewText("Cannot teleport into solid block!", Color.Red);
                    return false;
                }

                if (Collision.SolidCollision(new Vector2(targetCenter.X, targetCenter.Y), player.width, player.height))
                {
                    //Main.NewText("Not enough space!", Color.Red);
                    return false;
                }

                player.Teleport(targetCenter, TeleportationStyleID.RodOfDiscord);
                if (Main.netMode != NetmodeID.SinglePlayer)
                {
                    NetMessage.SendData(MessageID.TeleportEntity, -1, -1, null, 0, player.whoAmI, targetCenter.X, targetCenter.Y, 1);
                }

                SoundEngine.PlaySound(SoundID.Item12 with { Pitch = 0.2f }, targetCenter);
                for (int i = 0; i < 30; i++)
                {
                    Dust.NewDustPerfect(
                        targetCenter,
                        DustID.ApprenticeStorm,
                        dir.RotatedByRandom(0.8f) * Main.rand.NextFloat(2f, 10f),
                        0,
                        new Color(51, 153, 255),
                        1.5f
                    );
                }

                modPlayer.UseBlinkDash();
                return true;
            }
            return null;
        }
    }
}
