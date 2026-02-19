using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AncientLegacyMod.Content.Items.Projectiles
{
    public class BoneHookProjectile : ModProjectile
    {

        public override void SetDefaults()
        {
            Projectile.width = 22;
            Projectile.height = 22;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;

            Projectile.aiStyle = 13;
            AIType = ProjectileID.TheRottedFork;

            Projectile.penetrate = -1;
        }

        public override void AI()
        {
           // Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;

            if (Main.rand.NextBool(4))
            {
                Dust d = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Bone);
                d.noGravity = false;
                d.scale = 0.8f;
                d.velocity *= 0.5f;
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            for (int i = 0; i < 5; i++)
            {
                Dust.NewDust(target.position, target.width, target.height, DustID.Bone);
            }
            Terraria.Audio.SoundEngine.PlaySound(SoundID.NPCDeath2 with { Pitch = 0.5f, Volume = 0.8f }, target.Center);

            Player player = Main.player[Projectile.owner];
            if (!target.boss && target.knockBackResist > 0f)
            {
                Vector2 directionToPlayer = player.Center - target.Center;
                directionToPlayer.Normalize();
                target.velocity = directionToPlayer * 15f;
            }

            Projectile.ai[0] = 1f;
        }
        public override bool PreDrawExtras()
        {
            return false;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D chainTexture = ModContent.Request<Texture2D>("AncientLegacyMod/Content/Items/Misc/BoneHook_Chain").Value;

            Vector2 playerCenter = Main.player[Projectile.owner].MountedCenter;
            Vector2 center = Projectile.Center;
            Vector2 directionToPlayer = playerCenter - center;
            float chainRotation = directionToPlayer.ToRotation();
            float distanceToPlayer = directionToPlayer.Length();

            while (distanceToPlayer > 16f && !float.IsNaN(distanceToPlayer))
            {
                directionToPlayer.Normalize();
                directionToPlayer *= 8f;
                center += directionToPlayer;
                directionToPlayer = playerCenter - center;
                distanceToPlayer = directionToPlayer.Length();

                Color drawColor = Lighting.GetColor((int)center.X / 16, (int)(center.Y / 16));

                Main.EntitySpriteDraw(chainTexture, center - Main.screenPosition,
                    null, drawColor, chainRotation, chainTexture.Size() * 0.5f, 1f, SpriteEffects.None, 0);
            }

            Texture2D projTexture = TextureAssets.Projectile[Projectile.type].Value;
            Vector2 drawOrigin = projTexture.Size() * 0.5f;

            Main.EntitySpriteDraw(projTexture, Projectile.Center - Main.screenPosition,
                null, Projectile.GetAlpha(lightColor), Projectile.velocity.ToRotation() + MathHelper.PiOver2, drawOrigin, Projectile.scale, SpriteEffects.None, 0);

            return false; 
        }

    }
}