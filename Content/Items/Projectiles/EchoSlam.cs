using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace AncientLegacyMod.Content.Items.Projectiles
{
    public class EchoSlam : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 250;
            Projectile.height = 250;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 5;
            Projectile.hide = true;
        }

        public override void OnSpawn(Terraria.DataStructures.IEntitySource source)
        {
            Player player = Main.player[Projectile.owner];
            int enemyCount = 0;

            foreach (NPC npc in Main.npc)
            {
                if (npc.active && !npc.friendly && npc.lifeMax > 5 && Vector2.Distance(Projectile.Center, npc.Center) < 250f)
                {
                    enemyCount++;
                }
            }

            float multiplier = 1f + (enemyCount * 0.25f);
            Projectile.damage = (int)(Projectile.damage * multiplier);


            for (int i = 0; i < 60; i++)
            {
                Vector2 speed = Main.rand.NextVector2Unit() * Main.rand.NextFloat(6f, 18f);

                Dust dMagic = Dust.NewDustDirect(Projectile.Center, 0, 0, DustID.Stone, speed.X, speed.Y, 100, Color.DarkRed, 2.5f);
                dMagic.noGravity = true;
                dMagic.velocity *= 1.2f;
            }

            for (int i = 0; i < 40; i++)
            {
                Vector2 speed = Main.rand.NextVector2Unit() * Main.rand.NextFloat(2f, 10f);

                Dust dWood = Dust.NewDustDirect(Projectile.Center, 0, 0, DustID.WoodFurniture, speed.X, speed.Y, 0, default, 1.5f);

                Dust dSmoke = Dust.NewDustDirect(Projectile.Center, 0, 0, DustID.Smoke, speed.X * 0.5f, speed.Y * 0.5f, 150, Color.Gray, 1.2f);
            }

            for (int i = 0; i < 10; i++)
            {
                Dust dFlash = Dust.NewDustDirect(Projectile.Center, 0, 0, DustID.Flare, 0, 0, 100, Color.White, 3f);
                dFlash.noGravity = true;
            }

            Terraria.Audio.SoundEngine.PlaySound(SoundID.Item14, Projectile.Center);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            return false;
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            modifiers.HitDirectionOverride = (target.Center.X > Main.player[Projectile.owner].Center.X) ? 1 : -1;
        }
    }
}