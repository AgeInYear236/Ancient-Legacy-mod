using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace testMod1.Content.Items.Projectiles
{
    public class KeeperStaffProjectile : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = 1;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 100;
            Projectile.hide = true;
        }

        public override void AI()
        {
            Projectile.rotation += 0.2f;
            Lighting.AddLight(Projectile.Center, new Vector3(0.7f, 0.9f, 1f));

            if (Main.rand.NextBool(3))
            {
                Dust.NewDustPerfect(
                    Projectile.Center,
                    DustID.TintableDustLighted,
                    Vector2.Zero,
                    0,
                    new Color(200, 230, 255),
                    0.8f
                ).noGravity = true;
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Explode();
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Explode();
            return false;
        }

        private void Explode()
        {
            Projectile.Kill();
            SoundEngine.PlaySound(SoundID.Item14, Projectile.position);

            for (int i = 0; i < 15; i++)
            {
                Dust.NewDustPerfect(
                    Projectile.Center,
                    DustID.TintableDustLighted,
                    Main.rand.NextVector2CircularEdge(3f, 3f),
                    0,
                    new Color(200, 230, 255),
                    1.3f
                ).noGravity = true;
            }
        }

        public override void Kill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
            for (int i = 0; i < 15; i++)
            {
                Dust.NewDustPerfect(
                    Projectile.Center,
                    DustID.TintableDustLighted,
                    Main.rand.NextVector2CircularEdge(3f, 3f),
                    0,
                    new Color(200, 230, 255),
                    1.3f
                ).noGravity = true;
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = ModContent.Request<Texture2D>(Texture).Value;
            Vector2 origin = texture.Size() / 2f;
            Main.spriteBatch.Draw(
                texture,
                Projectile.Center - Main.screenPosition,
                null,
                lightColor,
                Projectile.rotation,
                origin,
                Projectile.scale,
                SpriteEffects.None,
                0f
            );
            return false;
        }
    }
}
