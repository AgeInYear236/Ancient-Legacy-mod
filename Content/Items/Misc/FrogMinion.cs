using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AncientLegacyMod.Content.Items.Armor;

namespace AncientLegacyMod.Content.Items.Misc
{
    public class FrogMinion : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projPet[Projectile.type] = true;
            ProjectileID.Sets.MinionSacrificable[Projectile.type] = false;
        }

        public override void SetDefaults()
        {
            Projectile.width = 24;
            Projectile.height = 20;
            Projectile.friendly = true;
            Projectile.minion = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 2;
            Projectile.tileCollide = true;
            Projectile.scale = 0.7f;
        }


        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            FrogPlayer modPlayer = player.GetModPlayer<FrogPlayer>();

            if (player.dead || !player.active || !modPlayer.frogSet)
            {
                Projectile.Kill();
                return;
            }

            Projectile.timeLeft = 2;

            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                Projectile other = Main.projectile[i];
                if (i != Projectile.whoAmI && other.active && other.type == Projectile.type && Math.Abs(Projectile.position.X - other.position.X) < 20 && Math.Abs(Projectile.position.Y - other.position.Y) < 20)
                {
                    if (Projectile.position.X < other.position.X) Projectile.velocity.X -= 0.2f;
                    else Projectile.velocity.X += 0.2f;
                }
            }

            Projectile.velocity.Y += 0.4f;
            if (Projectile.velocity.Y > 10f) Projectile.velocity.Y = 10f;

            float dist = Vector2.Distance(player.Center, Projectile.Center);

            if (dist > 1000f)
            {
                Projectile.Center = player.Center;
            }

            if (dist > 60f)
            {
                float moveSpeed = 4f;
                int direction = (player.Center.X > Projectile.Center.X) ? 1 : -1;

                if (Projectile.velocity.Y == 0.4f)
                {
                    Projectile.velocity.Y = -5f;
                    Projectile.velocity.X = direction * moveSpeed;
                }
            }
            else
            {
                Projectile.velocity.X *= 0.9f;
            }

            if (Projectile.velocity.X != 0)
            {
                Projectile.spriteDirection = (Projectile.velocity.X > 0) ? -1 : 1;
            }

            Projectile.gfxOffY = -6;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            return false;
        }

    }
}