using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Terraria.Audio;

namespace testMod1.Content.Items.Weapons
{
    public class AghanimScepter : ModItem
    {
        private static readonly int[] Spells = {
            ProjectileID.Typhoon,
            ProjectileID.NebulaArcanum,
            ProjectileID.Blizzard,
            ProjectileID.LunarFlare,
            ProjectileID.ShadowBeamFriendly,
            ProjectileID.InfernoFriendlyBlast,
            ProjectileID.SpectreWrath,
            ProjectileID.ApprenticeStaffT3Shot
        };

        private int currentSpellIndex = 0;
        private int shootTimer = 0;

        public override void SetDefaults()
        {
            Item.damage = 150;
            Item.DamageType = DamageClass.Magic;
            Item.mana = 4;
            Item.width = 40;
            Item.height = 40;

            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.HoldUp;

            Item.knockBack = 5;
            Item.rare = ItemRarityID.Purple;
            Item.value = Item.sellPrice(0, 50, 0, 0);
            Item.autoReuse = true;
            Item.noMelee = true;

            Item.shoot = ProjectileID.None;
        }

        public override void HoldItem(Player player)
        {
            if (Main.myPlayer == player.whoAmI)
            {
                player.ChangeDir(Main.MouseWorld.X < player.Center.X ? -1 : 1);
            }

            if (!player.controlUseItem)
            {
                Vector2 dir = (Main.MouseWorld - player.MountedCenter).SafeNormalize(Vector2.UnitX);
                float rotationOffset = MathHelper.PiOver4;
                player.itemRotation = dir.ToRotation() + (rotationOffset * player.direction);

                if (player.direction == -1) player.itemRotation += MathHelper.Pi;
                player.itemLocation = player.MountedCenter + dir * 10f;
                shootTimer = 3;
            }
            else if (Main.myPlayer == player.whoAmI)
            {
                shootTimer++;
                if (shootTimer >= 4)
                {
                    if (player.CheckMana(Item.mana, true))
                    {
                        Vector2 tipPosition = player.MountedCenter + new Vector2(25f * player.direction, -30f);
                        Vector2 targetDir = (Main.MouseWorld - tipPosition).SafeNormalize(Vector2.UnitX);

                        ShootSpells(player, targetDir, tipPosition);
                        shootTimer = 0;
                    }
                }
            }
        }

        private void ShootSpells(Player player, Vector2 direction, Vector2 spawnPos)
        {
            int projType = Spells[currentSpellIndex];
            currentSpellIndex = (currentSpellIndex + 1) % Spells.Length;

            Vector2 velocity = direction * 16f;
            Vector2 perturbedSpeed = velocity.RotatedByRandom(MathHelper.ToRadians(10));

            Projectile.NewProjectile(player.GetSource_ItemUse(Item), spawnPos, perturbedSpeed, projType, Item.damage, Item.knockBack, player.whoAmI);

            if (Main.rand.NextBool(4))
            {
                SoundEngine.PlaySound(SoundID.Item117 with { Pitch = 0.1f, Volume = 0.3f }, player.Center);
            }

            Lighting.AddLight(spawnPos, 0.8f, 0.4f, 1.0f);
        }

        public override bool? UseItem(Player player) => true;
    }
}