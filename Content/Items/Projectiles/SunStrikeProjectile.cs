using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace AncientLegacyMod.Content.Items.Projectiles
{
    public class SunStrikeProjectile : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 120;
            Projectile.height = 120; 
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 100;
            Projectile.hide = true;
        }

        public override bool? CanDamage() => Projectile.ai[0] >= 60 && Projectile.ai[0] <= 70;

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            if (Projectile.ai[0] >= 60 && Projectile.ai[0] <= 70)
            {
                float beamWidth = 50f;
                float beamHeight = 2000f;

                Rectangle beamArea = new Rectangle(
                    (int)(Projectile.Center.X - beamWidth / 2),
                    (int)(Projectile.Center.Y - beamHeight),
                    (int)beamWidth,
                    (int)beamHeight
                );

                return targetHitbox.Intersects(beamArea);
            }
            return false;
        }
        public override void AI()
        {
            if (Projectile.localAI[0] == 0)
            {
                Vector2 startScan = Projectile.Center - new Vector2(0, 1000);

                for (float y = startScan.Y; y < Projectile.Center.Y; y += 16f)
                {
                    Vector2 checkPos = new Vector2(Projectile.Center.X, y);
                    Point tileCoords = checkPos.ToTileCoordinates();

                    if (WorldGen.InWorld(tileCoords.X, tileCoords.Y) && Main.tile[tileCoords.X, tileCoords.Y].HasTile && Main.tileSolid[Main.tile[tileCoords.X, tileCoords.Y].TileType])
                    {
                        Projectile.Center = new Vector2(Projectile.Center.X, tileCoords.Y * 16f);
                        break;
                    }
                }

                Projectile.localAI[0] = 1;
            }

            Projectile.ai[0]++;

            if (Projectile.ai[0] < 60)
            {
                float radius = (60f - Projectile.ai[0]) * 1.5f;
                for (int i = 0; i < 2; i++)
                {
                    Vector2 offset = Main.rand.NextVector2CircularEdge(radius, radius);
                    Dust d = Dust.NewDustDirect(Projectile.Center + offset, 0, 0, DustID.SolarFlare);
                    d.noGravity = true;
                    d.velocity = -offset * 0.15f;
                }
            }

            if (Projectile.ai[0] == 60)
            {
                Projectile.hide = false;
                Terraria.Audio.SoundEngine.PlaySound(SoundID.Item69 with { Pitch = -0.2f, Volume = 1.5f }, Projectile.Center);
                Terraria.Audio.SoundEngine.PlaySound(SoundID.Item14, Projectile.Center);
                for (int i = 0; i < 50; i++)
                {
                    Dust d = Dust.NewDustDirect(Projectile.Center - new Vector2(Projectile.width / 2, Projectile.height / 2), Projectile.width, Projectile.height, DustID.SolarFlare, Main.rand.NextFloat(-5f, 5f), Main.rand.NextFloat(-5f, 5f), 150, default, Main.rand.NextFloat(1.5f, 2.5f));
                    d.noGravity = true;
                    d.velocity *= 2f;
                }
            }

            if (Projectile.ai[0] >= 60)
            {
                Lighting.AddLight(Projectile.Center, 2f, 1.2f, 0.4f);
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            if (Projectile.ai[0] >= 60)
            {
                Texture2D texture = ModContent.Request<Texture2D>("AncientLegacyMod/Content/Items/Misc/SunStrike").Value;

                float progress = (Projectile.ai[0] - 60) / 40f;
                float opacity = MathHelper.Lerp(1f, 0f, progress);

                float scaleX = MathHelper.Lerp(10f, 0.1f, progress);
                float scaleY = 100f;

                Vector2 drawPos = Projectile.Center - Main.screenPosition;

                Vector2 origin = new Vector2(texture.Width / 2f, texture.Height);

                Color glowColor = Color.Orange * opacity * 0.8f;
                glowColor.A = 0;

                Main.EntitySpriteDraw(texture, drawPos, null, glowColor, 0f, origin,
                    new Vector2(scaleX, scaleY), SpriteEffects.None, 0);

                Main.EntitySpriteDraw(texture, drawPos, null, Color.White * opacity, 0f, origin,
                    new Vector2(scaleX * 0.3f, scaleY), SpriteEffects.None, 0);

                Lighting.AddLight(Projectile.Center, 1.5f, 1f, 0.4f);
            }
            return false;
        }
    }
}