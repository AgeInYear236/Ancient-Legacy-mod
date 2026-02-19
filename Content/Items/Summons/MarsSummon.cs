using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AncientLegacyMod.Content.Items.Summons
{
    public class MarsSummon : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Type] = 6;
            ProjectileID.Sets.MinionTargettingFeature[Type] = true;
            Main.projPet[Type] = true;
            ProjectileID.Sets.MinionSacrificable[Type] = true;
        }

        public override void SetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 32;
            Projectile.tileCollide = true;
            Projectile.friendly = true;
            Projectile.minion = true;
            Projectile.DamageType = DamageClass.Summon;
            Projectile.minionSlots = 1f;
            Projectile.penetrate = -1;
            Projectile.aiStyle = -1;
        }

        public override bool MinionContactDamage() => true;

        public override void AI()
        {
            Player owner = Main.player[Projectile.owner];

            if (!CheckActive(owner)) return;

            GeneralBehavior(owner, out Vector2 vectorToIdle, out float distanceToIdle);
            SearchForTargets(owner, out bool foundTarget, out float distanceToTarget, out Vector2 targetCenter);
            Movement(foundTarget, distanceToTarget, targetCenter, distanceToIdle, vectorToIdle);
            Visuals();
        }

        private bool CheckActive(Player owner)
        {
            if (owner.dead || !owner.active)
            {
                return false;
            }
            if (owner.HasBuff(ModContent.BuffType<Buffs.MarsSummonBuff>()))
            {
                Projectile.timeLeft = 2;
            }
            return true;
        }

        private void GeneralBehavior(Player owner, out Vector2 vectorToIdle, out float distanceToIdle)
        {
            Vector2 idlePosition = owner.Center;
            float minionPositionOffsetX = (10 + Projectile.minionPos * 40) * -owner.direction;
            idlePosition.X += minionPositionOffsetX;

            vectorToIdle = idlePosition - Projectile.Center;
            distanceToIdle = vectorToIdle.Length();

            if (distanceToIdle > 400f)
            {
                Projectile.position = idlePosition;
                Projectile.velocity *= 0.1f;
                Projectile.netUpdate = true;
            }

            Projectile.velocity.Y += 0.4f;
            if (Projectile.velocity.Y > 10f) Projectile.velocity.Y = 10f;
        }

        private void SearchForTargets(Player owner, out bool foundTarget, out float distanceToTarget, out Vector2 targetCenter)
        {
            distanceToTarget = 700f;
            targetCenter = Projectile.position;
            foundTarget = false;

            if (owner.HasMinionAttackTargetNPC)
            {
                NPC npc = Main.npc[owner.MinionAttackTargetNPC];
                float between = Vector2.Distance(npc.Center, Projectile.Center);
                if (between < 2000f)
                {
                    distanceToTarget = between;
                    targetCenter = npc.Center;
                    foundTarget = true;
                }
            }

            if (!foundTarget)
            {
                foreach (var npc in Main.ActiveNPCs)
                {
                    if (npc.CanBeChasedBy())
                    {
                        float between = Vector2.Distance(npc.Center, Projectile.Center);
                        bool inRange = between < distanceToTarget;
                        bool lineOfSight = Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, npc.position, npc.width, npc.height);

                        if (inRange && lineOfSight)
                        {
                            distanceToTarget = between;
                            targetCenter = npc.Center;
                            foundTarget = true;
                        }
                    }
                }
            }
            Projectile.friendly = foundTarget;
        }

        private void Movement(bool foundTarget, float distanceToTarget, Vector2 targetCenter, float distanceToIdle, Vector2 vectorToIdle)
        {
            float speed = 7f;
            float inertia = 12f;

            if (foundTarget)
            {
                float moveDir = Math.Sign(targetCenter.X - Projectile.Center.X);
                Projectile.velocity.X = (Projectile.velocity.X * (inertia - 1) + moveDir * speed) / inertia;
            }
            else
            {
                if (distanceToIdle > 60f)
                {
                    float moveDir = Math.Sign(vectorToIdle.X);
                    Projectile.velocity.X = (Projectile.velocity.X * (inertia - 1) + moveDir * speed) / inertia;
                }
                else
                {
                    Projectile.velocity.X *= 0.85f;
                }
            }

            bool isGrounded = Projectile.velocity.Y > 0f && Projectile.velocity.Y <= 0.45f;

            if (isGrounded)
            {
                Vector2 scanPos = Projectile.Center + new Vector2((Projectile.width / 2 + 4) * Math.Sign(Projectile.velocity.X), Projectile.height / 4);
                Point tileCoords = scanPos.ToTileCoordinates();

                if (WorldGen.InWorld(tileCoords.X, tileCoords.Y) && Main.tile[tileCoords.X, tileCoords.Y].HasTile && Main.tileSolid[Main.tile[tileCoords.X, tileCoords.Y].TileType])
                {
                    Projectile.velocity.Y = -8f;
                    Projectile.netUpdate = true;
                }

                if (foundTarget && targetCenter.Y < Projectile.Center.Y - 60f && Math.Abs(targetCenter.X - Projectile.Center.X) < 80f)
                {
                    Projectile.velocity.Y = -9.5f;
                    Projectile.netUpdate = true;
                }
            }

            if (Math.Abs(Projectile.velocity.X) < 0.5f && isGrounded && (foundTarget || distanceToIdle > 100f))
            {
                Projectile.velocity.Y = -7f;
            }
        }

        private void Visuals()
        {
            if (Projectile.velocity.X > 0.5f) Projectile.spriteDirection = -1;
            else if (Projectile.velocity.X < -0.5f) Projectile.spriteDirection = 1;

            if (Projectile.velocity.Y < -0.2f || Projectile.velocity.Y > 0.5f)
            {
                Projectile.frame = 5;
                Projectile.frameCounter = 0;
            }
            else if (Math.Abs(Projectile.velocity.X) > 0.5f)
            {
                Projectile.frameCounter++;
                if (Projectile.frameCounter >= 6)
                {
                    Projectile.frameCounter = 0;
                    Projectile.frame++;

                    if (Projectile.frame < 2 || Projectile.frame > 4)
                    {
                        Projectile.frame = 2;
                    }
                }
            }
            else
            {
                Projectile.frameCounter++;
                if (Projectile.frameCounter >= 24)
                {
                    Projectile.frameCounter = 0;
                    Projectile.frame++;

                    if (Projectile.frame > 1)
                    {
                        Projectile.frame = 0;
                    }
                }
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = TextureAssets.Projectile[Type].Value;
            int frameHeight = 32;
            int padding = 3;

            int yPos = Projectile.frame * (frameHeight + padding);
            Rectangle sourceRect = new Rectangle(0, yPos, texture.Width, frameHeight);

            Vector2 origin = sourceRect.Size() / 2f;
            SpriteEffects effects = Projectile.spriteDirection == 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

            Main.EntitySpriteDraw(texture, Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY),
                sourceRect, lightColor, Projectile.rotation, origin, Projectile.scale, effects, 0);

            return false;
        }
    }
}