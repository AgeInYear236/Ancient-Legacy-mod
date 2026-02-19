using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AncientLegacyMod.Content.Buffs;
using AncientLegacyMod.Content.Items.Armor;

namespace AncientLegacyMod.Content.Items.Pets
{
    public class LightPetProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 1;
            Main.projPet[Projectile.type] = true;
        }

        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.ZephyrFish);
            Projectile.width = 32;
            Projectile.height = 32;
            Projectile.aiStyle = ProjAIStyleID.Pet;
            AIType = ProjectileID.ZephyrFish;
            Projectile.friendly = true;
            Projectile.netImportant = true;
            Projectile.timeLeft = 2;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = ModContent.Request<Texture2D>(Texture).Value;
            Vector2 origin = new Vector2(texture.Width / 2f, texture.Height / 2f);

            Main.spriteBatch.Draw(
                texture,
                Projectile.Center - Main.screenPosition,
                null,
                Color.White * 0.9f,
                Projectile.rotation,
                origin,
                Projectile.scale * 0.3f,
                SpriteEffects.None,
                0f
            );

            return false;
        }

        public override void PostDraw(Color lightColor)
        {
            if (Projectile.localAI[1] != 0)
                return;

            bool useMask1 = (Main.GameUpdateCount / 60) % 2 == 0;

            string maskSuffix = useMask1 ? "_Mask1" : "_Mask2";
            string fullMaskPath = Texture + maskSuffix;

            Texture2D maskTexture = null;

            try
            {
                maskTexture = ModContent.Request<Texture2D>(fullMaskPath).Value;
            }
            catch
            {
                // Debug logic
            }

            if (maskTexture != null)
            {
                Vector2 origin = new Vector2(maskTexture.Width / 2f, maskTexture.Height / 2f);
                Vector2 drawPos = Projectile.Center - Main.screenPosition;

                Main.EntitySpriteDraw(
                    maskTexture,
                    drawPos,
                    null,
                    Color.White,
                    Projectile.rotation,
                    origin,
                    Projectile.scale * 0.3f,
                    SpriteEffects.None,
                    0f
                );
            }
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            if (!player.active || player.dead || player.ghost || !player.HasBuff(ModContent.BuffType<LightPetBuff>()))
            {
                Projectile.Kill();
                return;
            }

            float targetX = player.Center.X;
            float targetY = player.Center.Y - 80f; 

            float sway = (float)System.Math.Sin(Main.GlobalTimeWrappedHourly * 3f) * 20f;
            targetX += sway;

            Vector2 targetPos = new Vector2(targetX, targetY);

            float speed = 0.1f; 
            Projectile.velocity = (targetPos - Projectile.Center) * speed;
            Projectile.position += Projectile.velocity;

            if (Projectile.velocity.Length() > 0.1f)
            {
                Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
            }

            /*if (Main.rand.NextBool(5))
            {
                Dust dust = Dust.NewDustPerfect(
                    Projectile.Center,
                    DustID.TintableDustLighted,
                    Vector2.Zero,
                    0,
                    new Color(245, 255, 144),
                    0.8f
                );
                dust.noGravity = true;
                dust.fadeIn = 1.2f;
                dust.velocity *= 0.1f;
            }*/

            /*if (Main.rand.NextBool(10))
            {
                Dust dust = Dust.NewDustPerfect(
                    Projectile.Center,
                    DustID.TintableDustLighted,
                    Vector2.Zero,
                    0,
                    new Color(245, 245, 189),
                    1.1f
                );
                dust.noGravity = false;
                dust.fadeIn = 1.5f;
                dust.velocity.Y *= 3f;
                
            }*/

            if (Main.rand.NextBool(15))
            {
                Dust spark = Dust.NewDustPerfect(
                    Projectile.Center,
                    DustID.YellowStarDust,
                    new Vector2(Main.rand.NextFloat(-3f, 3f), Main.rand.NextFloat(-3f, 3f)) * 2f,
                    0,
                    new Color(255, 255, 51),
                    1.3f
                );
                spark.noGravity = true;
                spark.scale *= 1.3f;
            }



            Lighting.AddLight(Projectile.Center, new Vector3(0.7f, 0.9f, 1f));

            Projectile.timeLeft = 2;

            if (++Projectile.localAI[1] >= 30)
            {
                Projectile.localAI[1] = 0;
            }
        }
    }
}
