using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace testMod1.Content.Items.Projectiles
{
    public class KeeperStaffProjectile2 : ModProjectile
    {
        private const int HoldTime = 60; // 1 секунда задержки
        private bool hasLaunched = false;
        private Vector2 launchDirection = Vector2.Zero;

        public override void SetDefaults()
        {
            Projectile.width = 18;
            Projectile.height = 18;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = -1;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.tileCollide = true;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 10;
            Projectile.timeLeft = 180;
            Projectile.hide = false;
            Projectile.alpha = 0;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            if (!player.active || player.dead)
            {
                Projectile.Kill();
                return;
            }

            Vector2 dirToMouse = (Main.MouseWorld - player.MountedCenter).SafeNormalize(Vector2.UnitX);

            if (Main.myPlayer == player.whoAmI)
            {
                player.ChangeDir(Main.MouseWorld.X < player.Center.X ? -1 : 1);
                player.itemRotation = dirToMouse.ToRotation();
                if (player.direction == -1) player.itemRotation += MathHelper.Pi;
                player.itemLocation = player.MountedCenter + new Vector2(0f, -22f);
            }

            if (Projectile.timeLeft > (180 - HoldTime) && !hasLaunched)
            {
                float length = 45f;
                Projectile.Center = player.MountedCenter + dirToMouse * length;
                Projectile.rotation = dirToMouse.ToRotation() + MathHelper.PiOver2;
                Projectile.velocity = Vector2.Zero;

                Lighting.AddLight(Projectile.Center, new Vector3(0.5f, 0.7f, 1f));
                if (Main.rand.NextBool(3))
                {
                    Dust dust = Dust.NewDustPerfect(
                        Projectile.Center,
                        DustID.Cloud,
                        Vector2.Zero,
                        0,
                        new Color(180, 220, 230),
                        1.5f
                    );
                    dust.noGravity = true;
                }
            }
            else if (!hasLaunched)
            {
                hasLaunched = true;

                launchDirection = dirToMouse;
                float speed = 20f; 
                Projectile.velocity = launchDirection * speed;
                Projectile.rotation = launchDirection.ToRotation() + MathHelper.PiOver2;

                if (Main.myPlayer == player.whoAmI)
                {
                    SoundEngine.PlaySound(SoundID.Item12, Projectile.position);
                }
            }

            if (hasLaunched)
            {
                Lighting.AddLight(Projectile.Center, new Vector3(0.8f, 0.9f, 1f));
                if (Main.rand.NextBool(2))
                {
                    Dust dust = Dust.NewDustPerfect(
                        Projectile.Center - Projectile.velocity * 0.3f,
                        DustID.TintableDustLighted,
                        Vector2.Zero,
                        0,
                        new Color(200, 230, 255),
                        0.8f
                    );
                    dust.noGravity = true;
                }
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = ModContent.Request<Texture2D>(Projectile.ModProjectile.Texture).Value;
            Vector2 origin = new Vector2(texture.Width / 2f, texture.Height / 2f);

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

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            for (int i = 0; i < 12; i++)
            {
                Dust dust = Dust.NewDustPerfect(
                    target.Center,
                    DustID.TintableDustLighted,
                    new Vector2(Main.rand.NextFloat(-2f, 2f), Main.rand.NextFloat(-2f, 2f)),
                    0,
                    new Color(200, 230, 255),
                    1.1f
                );
                dust.noGravity = true;
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.Kill();
            return false;
        }

        public override void Kill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item14, Projectile.position);

            for (int i = 0; i < 20; i++)
            {
                Vector2 speed = Main.rand.NextVector2CircularEdge(3f, 3f);
                Dust dust = Dust.NewDustPerfect(
                    Projectile.Center,
                    DustID.TintableDustLighted,
                    speed,
                    0,
                    new Color(200, 230, 255),
                    1.3f
                );
                dust.noGravity = true;
            }

            for (int i = 0; i < 8; i++)
            {
                Dust spark = Dust.NewDustPerfect(
                    Projectile.Center,
                    DustID.Electric,
                    Main.rand.NextVector2Circular(2f, 2f),
                    0,
                    default,
                    1.1f
                );
                spark.noGravity = true;
            }
        }
    }
}