using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using testMod1.Content.Items.Accessories;

namespace testMod1.Content.Items.Projectiles
{
    public class WispProjectile : ModProjectile
    {
        private float rotationSpeed;
        private int rotationDirection;

        public override void SetStaticDefaults()
        {
            Main.projPet[Projectile.type] = true;
        }

        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 30;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.netImportant = true;

            rotationSpeed = Main.rand.NextFloat(0.05f, 0.15f);
            rotationDirection = Main.rand.NextBool() ? 1 : -1;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            if (player.dead || !player.active)
            {
                Projectile.Kill();
                return;
            }

            Vector2 idlePosition = player.Center + new Vector2(player.direction * -50, -60);
            Vector2 vectorToIdlePosition = idlePosition - Projectile.Center;
            float distance = vectorToIdlePosition.Length();

            if (distance > 2000f)
            {
                Projectile.Center = player.Center;
            }

            float speed = 8f;
            float inertia = 20f;
            vectorToIdlePosition.Normalize();
            vectorToIdlePosition *= speed;
            Projectile.velocity = (Projectile.velocity * (inertia - 1f) + vectorToIdlePosition) / inertia;

            Projectile.rotation += rotationSpeed * rotationDirection;

            float pulse = (float)Math.Sin(Main.timeForVisualEffects * 0.1f) * 0.2f;
            Lighting.AddLight(Projectile.Center, 0.3f + pulse, 0.6f + pulse, 1.0f);

            Projectile.ai[1]++;
            if (Projectile.ai[1] >= 120)
            {
                if (player.statLife < player.statLifeMax2)
                {
                    player.statLife += 3;
                    player.HealEffect(3);
                }
                Projectile.ai[1] = 0;
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Player player = Main.player[Projectile.owner];

            Texture2D beamTex = ModContent.Request<Texture2D>("testMod1/Content/Items/Misc/WispTether_Chain").Value;
            Vector2 start = Projectile.Center;
            Vector2 end = player.Center;
            Vector2 dist = end - start;
            float beamRot = dist.ToRotation();
            float length = dist.Length();

            for (float i = 0; i < length; i += 8f)
            {
                Vector2 pos = start + Vector2.Normalize(dist) * i;
                float alpha = 0.4f + (float)Math.Sin(Main.timeForVisualEffects * 0.2f) * 0.2f;
                Main.EntitySpriteDraw(beamTex, pos - Main.screenPosition, null, Color.Cyan * alpha, beamRot, beamTex.Size() / 2, 0.5f, SpriteEffects.None, 0);
            }

            Texture2D tex = ModContent.Request<Texture2D>(Texture).Value;
            Main.EntitySpriteDraw(tex, Projectile.Center - Main.screenPosition, null, Color.White, Projectile.rotation, tex.Size() / 2, Projectile.scale, SpriteEffects.None, 0);

            return false;
        }
    }
}