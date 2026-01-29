using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Graphics;
using Terraria.ID;
using Terraria.ModLoader;
using testMod1.Content.Buffs;
using testMod1.Content.Items.Armor;
using testMod1.Content.Items.Projectiles;
using testMod1.Content.Items.Weapons;

namespace testMod1.Content
{
    public class modPlayer1 : ModPlayer
    {
        // Axe Armor
        public int previousHealth;
        public bool isAxed = false;
        public bool hasHitWithAxeAxeThisUse = false;
        int c = 0;

        // Magic Staff projectiles
        public List<Vector2> orbOffsets = new List<Vector2>();

        // Blink stuff
        public int blinkDashCooldown = 0;
        public const int MaxBlinkDashCooldown = 10;

        // Rogue stuff
        public int rogueDashCooldown = 0;
        public const int MaxRogueDashCooldown = 10;

        // Huskar blood
        public bool isBlooded = false;
        public float missingHealthPercent = 0f;

        //PA dagger
        public int critChanceCounter = 0; 
        public const int CritChancePerHit = 5;
        public const int MaxBonusCritChance = 100;

        //Bach
        public bool bachAccEquipped = false;

        public override void ResetEffects()
        {
            bachAccEquipped = false;
        }

        public override void OnHitByNPC(NPC npc, Player.HurtInfo hurtInfo) // Analize if hit, and apply thorns attack
        {
            if (!Player.dead && hurtInfo.Damage > 0 && isAxed)
            {
                int thornsDamage = (int)(hurtInfo.Damage * 0.3f);
                thornsDamage = Math.Max(thornsDamage, 1);

                npc.SimpleStrikeNPC(thornsDamage, 0);

            }
        }

        public override void PreUpdate()
        {
            previousHealth = Player.statLife;
            bool hasHelmet = Player.head == ModContent.ItemType<AxeHelm>();
            bool hasChestplate = Player.body == ModContent.ItemType<AxeArmor>();
            bool hasLeggings = Player.legs == ModContent.ItemType<AxeBoots>();

            isAxed = hasHelmet && hasChestplate && hasLeggings;
        }

        public override void PostUpdate()
        {
            if (blinkDashCooldown > 0)
            {
                Main.NewText(blinkDashCooldown);
                blinkDashCooldown--;
            }


            //Speed Cap if Armor equipped
            if (Player.whoAmI == Main.myPlayer && isAxed)
            {
                if (Math.Abs(Player.velocity.X) > 5f) //26 mph
                {
                    Player.velocity.X = Math.Sign(Player.velocity.X) * 5f;
                }

                if (Player.statLife < previousHealth)
                {
                    c++;
                    if (c == 5)
                    {
                        int numProjectiles = 20;
                        float spread = MathHelper.TwoPi;
                        float speed = 16f;
                        int damage = AxeHelm.setDamage;
                        float knockback = 3f;

                        for (int i = 0; i < numProjectiles; i++)
                        {
                            // Calculate direction for each projectile in the circle
                            Vector2 velocity = Vector2.UnitX.RotatedBy(spread * i / numProjectiles) * speed;

                            Projectile.NewProjectile(
                                Player.GetSource_FromThis(),
                                Player.Center,
                                velocity,
                                ModContent.ProjectileType<AxeArmorProjectile>(),
                                damage,
                                knockback,
                                Player.whoAmI
                            );
                        }
                        Terraria.Audio.SoundEngine.PlaySound(SoundID.Item37);

                        c = 0;
                    }
                }
            }


            if (Player.dead || Player.ghost || orbOffsets.Count == 0)
                return;

            for (int i = 0; i < orbOffsets.Count; i++)
            {
                float radius = orbOffsets[i].Length();
                float currentAngle = orbOffsets[i].ToRotation() + 0.05f;
                orbOffsets[i] = new Vector2(
                    (float)System.Math.Cos(currentAngle),
                    (float)System.Math.Sin(currentAngle)
                ) * radius;
            }

            foreach (Vector2 worldPos in GetOrbWorldPositions())
            {
                if (Main.rand.NextBool(3))
                {
                    Dust dust = Dust.NewDustPerfect(
                        worldPos,
                        DustID.TintableDustLighted,
                        Vector2.Zero,
                        0,
                        new Color(200, 230, 255),
                        1.1f
                    );
                    dust.noGravity = true;
                    dust.fadeIn = 1.2f;
                }
            }

        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (bachAccEquipped)
            {
                if (Main.rand.NextFloat() > 0.92f)
                {
                    target.AddBuff(ModContent.BuffType<StunBuff>(), 60);
                    Player.AddBuff(ModContent.BuffType<MagnusCooldown>(), 150);
                    Main.NewText("Bashed!");
                }
            }
        }

        public override void PostUpdateEquips()
        {
            /*if (hasZoomAccessory)
            {
                Main.GameZoomTarget = 0.8f;
                return;
            }
            else
            {
                Main.GameZoomTarget = 1f;
            }*/
        }
        public void LaunchOrbs(Vector2 launchDirection)
        {
            foreach (Vector2 offset in orbOffsets)
            {
                Vector2 spawnPos = Player.Center + offset;
                Vector2 velocity = launchDirection * 14f;
                Projectile.NewProjectile(
                    Player.GetSource_ItemUse(Player.inventory[Player.selectedItem]),
                    spawnPos,
                    velocity,
                    ModContent.ProjectileType<KeeperStaffProjectile>(),
                    Player.GetWeaponDamage(Player.inventory[Player.selectedItem]),
                    4f,
                    Player.whoAmI
                );
            }
            orbOffsets.Clear();
        }

        public List<Vector2> GetOrbWorldPositions()
        {
            List<Vector2> positions = new List<Vector2>();
            foreach (Vector2 offset in orbOffsets)
            {
                positions.Add(Player.Center + offset);
            }
            return positions;
        }

        public bool CanUseBlinkDash() => blinkDashCooldown <= 0;

        public void UseBlinkDash()
        {
            Player.velocity = new Vector2(0, 0);
            blinkDashCooldown = MaxBlinkDashCooldown;
        }

        public bool CanUseRogueDash() => blinkDashCooldown <= 0;

        public void UseRogueDash()
        {
            blinkDashCooldown = MaxBlinkDashCooldown;
        }

        public int GetBonusCritChance()
        {
            return (int)MathHelper.Min(critChanceCounter * CritChancePerHit, MaxBonusCritChance);
        }

        public override void PostUpdateMiscEffects()
        {
            if (Player.HeldItem.type != ModContent.ItemType<PADagger>())
            {
                critChanceCounter = 0;
            }
        }


    }
}
