using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AncientLegacyMod.Content.Items.Projectiles
{
    public class TheLogSpear : ModProjectile
    {
        public float HoldoutRangeMax => 30f;
        public float HoldoutRangeMin => -10f;

        public Vector2 VisualOffset = new Vector2(12f, 2f);
        public override void SetDefaults()
        {
            Projectile.width = 60;
            Projectile.height = 32;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.ownerHitCheck = true;
            Projectile.extraUpdates = 1;
            Projectile.aiStyle = -1;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            if (!player.active || player.dead)
            {
                Projectile.Kill();
                return;
            }

            int duration = player.itemAnimationMax;
            if (Projectile.timeLeft > duration)
            {
                Projectile.timeLeft = duration;
            }

            player.heldProj = Projectile.whoAmI;

            float progress;
            float halfDuration = duration * 0.5f;
            if (Projectile.timeLeft < halfDuration)
            {
                progress = Projectile.timeLeft / halfDuration;
            }
            else
            {
                progress = (duration - Projectile.timeLeft) / halfDuration;
            }
            float dynamicMin = (player.direction == -1) ? -20f : 10f;
            float dynamicMax = (player.direction == -1) ? 28f : 48f;

            Vector2 directionUnit = Vector2.Normalize(Projectile.velocity);
            float currentDistance = MathHelper.Lerp(dynamicMin, dynamicMax, progress);

            Projectile.Center = player.MountedCenter + (directionUnit * currentDistance);

            Projectile.rotation = directionUnit.ToRotation();

            Projectile.spriteDirection = player.direction;
        }
    }
}