using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using testMod1.Common.Rarity;

namespace testMod1.Content.Items.Weapons
{
    public class WindrangerBow : ModItem
    {
        // Переменные для отслеживания состояния зарядки
        public int chargeTimer = 0;
        public const int MaxCharge = 120; // 2 секунды

        public override void SetDefaults()
        {
            Item.damage = 65;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 30;
            Item.height = 50;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 4f;
            Item.value = Item.buyPrice(gold: 5);
            Item.rare = ModContent.GetInstance<MagicRarity3>().Type;
            Item.UseSound = SoundID.Item5;
            Item.autoReuse = true;
            Item.shoot = ProjectileID.WoodenArrowFriendly;
            Item.shootSpeed = 12f;
            Item.useAmmo = AmmoID.Arrow;

            Item.scale = 1.7f;
        }
        public override Vector2? HoldoutOffset() => new Vector2(-8, 0);


        public override bool AltFunctionUse(Player player) => true;

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                return false;
            }

            Item.useTime = 15;
            Item.useAnimation = 15;
            return true;
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


            if (Main.mouseRight && !player.mouseInterface)
            {
                chargeTimer++;

                if (chargeTimer % 10 == 0)
                {
                    Dust d = Dust.NewDustDirect(player.Center, 0, 0, DustID.GreenTorch, 0, 0, 100, default, 1.2f);
                    d.velocity *= 2f;
                    d.noGravity = true;
                }

                if (chargeTimer == MaxCharge)
                {
                    Terraria.Audio.SoundEngine.PlaySound(SoundID.MaxMana, player.Center);
                    for (int i = 0; i < 15; i++)
                    {
                        Dust.NewDust(player.position, player.width, player.height, DustID.GreenFairy, 0, 0, 150, default, 1.5f);
                    }
                }

                player.itemAnimation = 2;
                player.itemTime = 2;
            }
            else
            {
                if (chargeTimer >= MaxCharge)
                {
                    FirePowershot(player);
                }
                chargeTimer = 0;
            }
        }

        private void FirePowershot(Player player)
        {
            if (player.whoAmI == Main.myPlayer)
            {
                Vector2 velocity = Main.MouseWorld - player.Center;
                velocity.Normalize();
                velocity *= 24f;

                int p = Projectile.NewProjectile(player.GetSource_ItemUse(Item), player.Center, velocity, ProjectileID.WoodenArrowFriendly, (int)(Item.damage * 2.2f), Item.knockBack * 2f, player.whoAmI);

                Main.projectile[p].ai[1] = 555f;

                Terraria.Audio.SoundEngine.PlaySound(SoundID.Item117, player.Center);
            }
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (player.altFunctionUse != 2)
            {
                damage = (int)(damage * 0.35f);
            }
        }
    }

    public class PowershotGlobalProj : GlobalProjectile
    {
        public override void PostAI(Projectile projectile)
        {
            if (projectile.type == ProjectileID.WoodenArrowFriendly && projectile.ai[1] == 555f)
            {
                projectile.penetrate = -1;
                projectile.maxPenetrate = -1;

                projectile.usesLocalNPCImmunity = true;
                projectile.localNPCHitCooldown = 15;

                if (Main.rand.NextBool(2))
                {
                    Dust d = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, DustID.GreenFairy);
                    d.noGravity = true;
                    d.scale = 1.1f;
                }
            }
        }
    }
}
