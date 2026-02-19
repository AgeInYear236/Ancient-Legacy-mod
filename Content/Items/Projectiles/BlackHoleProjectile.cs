using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace AncientLegacyMod.Content.Items.Projectiles
{
    public class BlackHoleProjectile : ModProjectile
    {
        private int ChargeTimer { get => (int)Projectile.ai[0]; set => Projectile.ai[0] = value; }
        private int ManaTimer = 0;
        // ! ! !
        private const int MaxChargeTime = 150;

        public override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            if (!player.channel || player.dead)
            {
                Projectile.Kill();
                return;
            }

            ManaTimer++;
            if (ManaTimer >= 60)
            {
                if (player.statMana >= 20)
                {
                    player.statMana -= 20;
                    player.manaRegenDelay = 120; 
                }
                else
                {
                    Projectile.Kill();
                    return;
                }
                ManaTimer = 0;
            }

            player.velocity = Vector2.Zero;
            player.itemAnimation = 2;
            player.itemTime = 2;
            Projectile.Center = player.Center;

            player.immune = true;
            player.immuneTime = 20;

            ChargeTimer++;

            if (ChargeTimer < MaxChargeTime)
            {
                if (Main.rand.NextBool(2))
                {
                    Vector2 offset = Main.rand.NextVector2CircularEdge(150, 150);
                    Dust d = Dust.NewDustPerfect(Projectile.Center + offset, DustID.PurpleCrystalShard, -offset * 0.05f);
                    d.noGravity = true;
                }
                return;
            }


            float pullRadius = 800f; 
            float damageRadius = 50f;

            if (Main.rand.NextBool(5))
            {
                Vector2 darkPos = Projectile.Center + Main.rand.NextVector2CircularEdge(pullRadius, pullRadius);
                Vector2 toCenter = (Projectile.Center - darkPos) * 0.08f;

                Dust d = Dust.NewDustPerfect(darkPos, DustID.ShadowbeamStaff, toCenter, 0, default, 2.5f);
                d.noGravity = true;
                d.fadeIn = 1.2f;

                d.color = new Color(40, 0, 80);
            }

            if (ChargeTimer > MaxChargeTime && Main.rand.NextBool(15))
            {
                for (int i = 0; i < 8; i++)
                {
                    Vector2 speed = Main.rand.NextVector2Circular(4f, 4f);
                    Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.Granite, speed, 0, Color.Purple, 1.8f);
                    d.noGravity = true;
                    d.velocity *= 2f;
                }
            }

            for (int i = 0; i < 3; i++)
            {
                Vector2 beamStart = Projectile.Center + Main.rand.NextVector2CircularEdge(pullRadius, pullRadius);
                for (int j = 0; j < 10; j++)
                { 
                    Vector2 pos = Vector2.Lerp(beamStart, Projectile.Center, j / 10f);
                    Dust d = Dust.NewDustPerfect(pos, DustID.Shadowflame, Vector2.Zero, 150, default, 0.8f);
                    d.noGravity = true;
                }
            }

            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC npc = Main.npc[i];
                if (npc.active && !npc.friendly && !npc.boss && npc.Distance(Projectile.Center) < pullRadius)
                {
                    Vector2 pullDirection = Projectile.Center - npc.Center;
                    float force = 10f; 
                    npc.velocity = Vector2.Normalize(pullDirection) * force;

                    if (npc.Distance(Projectile.Center) < damageRadius)
                    {
                        if (Projectile.timeLeft % 10 == 0)
                        { 
                            npc.SimpleStrikeNPC(Projectile.damage, 0);
                        }
                    }
                }
            }

            if (ChargeTimer > MaxChargeTime + 600) Projectile.Kill();
        }
    }
}