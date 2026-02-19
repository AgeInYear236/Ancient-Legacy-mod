using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AncientLegacyMod.Content.Items.Accessories;

namespace AncientLegacyMod.Content.Items.Projectiles
{
    public class WispProjectile : ModProjectile
    {

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

        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            if (player.dead || !player.active)
            {
                Projectile.Kill();
                return;
            }

            bool isEquipped = false;
            for (int i = 3; i < 10; i++)
            {
                if (player.armor[i].type == ModContent.ItemType<WispBottle>())
                {
                    isEquipped = true;
                    break;
                }
            }

            if (!isEquipped)
            {
                Projectile.Kill();
                return;
            }



            float time = (float)Main.timeForVisualEffects * 0.05f;
            Vector2 wobble = new Vector2(
                (float)Math.Sin(time) * 30,
                (float)Math.Cos(time * 0.7f) * 20
            );

            Vector2 idlePosition = player.Center + new Vector2(player.direction * -40, -50) + wobble;

            Vector2 vectorToIdlePosition = idlePosition - Projectile.Center;
            float distance = vectorToIdlePosition.Length();

            if (distance > 2000f)
            {
                Projectile.Center = player.Center;
            }

            float speedMult = 0.06f;
            if (distance < 5f)
            {
                Projectile.velocity *= 0.9f;
            }
            else
            {
                Projectile.velocity += vectorToIdlePosition * speedMult;

                float maxSpeed = 7f;
                if (Projectile.velocity.Length() > maxSpeed)
                {
                    Projectile.velocity.Normalize();
                    Projectile.velocity *= maxSpeed;
                }

                Projectile.velocity *= 0.94f;
            }

            Projectile.rotation += Projectile.velocity.X * 0.05f;

            float pulse = (float)Math.Sin(Main.timeForVisualEffects * 0.1f) * 0.1f;
            Lighting.AddLight(Projectile.Center, 0.3f + pulse, 0.6f + pulse, 1.0f);

            Projectile.ai[1]++;
            if (Projectile.ai[1] >= 120)
            {
                if (player.statLife < player.statLifeMax2)
                {
                    int heal = 5;
                    player.statLife = Math.Min(player.statLife + heal, player.statLifeMax2);
                    player.HealEffect(heal);
                    if (Main.netMode != NetmodeID.SinglePlayer)
                        NetMessage.SendData(MessageID.SpiritHeal, -1, -1, null, player.whoAmI, heal);
                }
                Projectile.ai[1] = 0;
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Player player = Main.player[Projectile.owner];

            Texture2D beamTex = ModContent.Request<Texture2D>("AncientLegacyMod/Content/Items/Misc/WispTether_Chain").Value;
            Vector2 start = Projectile.Center;
            Vector2 end = player.Center;
            Vector2 dist = end - start;
            float beamRot = dist.ToRotation();
            float length = dist.Length();

            for (float i = 0; i < length; i += 8f)
            {
                Vector2 pos = start + Vector2.Normalize(dist) * i;
                float alpha = 0.4f + (float)Math.Sin(Main.timeForVisualEffects * 0.2f) * 0.2f;
                Main.EntitySpriteDraw(beamTex, pos - Main.screenPosition, null, Color.Cyan * alpha, beamRot, beamTex.Size() / 2, 0.7f, SpriteEffects.None, 0);
            }

            Texture2D tex = ModContent.Request<Texture2D>(Texture).Value;
            Main.EntitySpriteDraw(tex, Projectile.Center - Main.screenPosition, null, Color.White, Projectile.rotation, tex.Size() / 2, Projectile.scale * 1.5f, SpriteEffects.None, 0);

            return false;
        }
    }
}