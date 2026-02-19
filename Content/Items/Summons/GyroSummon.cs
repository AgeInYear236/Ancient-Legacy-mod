using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AncientLegacyMod.Content.Items.Summons
{
    public class GyroSummon : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 1;
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
            Main.projPet[Projectile.type] = true;
            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
            ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true;
        }

        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 30;
            Projectile.tileCollide = false;
            Projectile.friendly = true;
            Projectile.minion = true;
            Projectile.DamageType = DamageClass.Summon;
            Projectile.minionSlots = 1f;
            Projectile.penetrate = -1;

            Projectile.aiStyle = -1;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            if (player.dead || !player.active) player.ClearBuff(ModContent.BuffType<Buffs.GyroSummonBuff>());
            if (player.HasBuff(ModContent.BuffType<Buffs.GyroSummonBuff>())) Projectile.timeLeft = 2;

            Vector2 idlePosition = player.Center;
            idlePosition.Y -= 48f;

            float spacing = 30f;
            idlePosition.X += (Projectile.minionPos * spacing) - ((player.ownedProjectileCounts[Projectile.type] - 1) * spacing / 2);

            Vector2 vectorToIdle = idlePosition - Projectile.Center;
            float distanceToIdle = vectorToIdle.Length();

            if (distanceToIdle > 2000f)
            {
                Projectile.Center = player.Center;
            }
            float speed = 8f;
            float inertia = 20f;
            if (distanceToIdle > 40f)
            {
                vectorToIdle.Normalize();
                vectorToIdle *= speed;
                Projectile.velocity = (Projectile.velocity * (inertia - 1) + vectorToIdle) / inertia;
            }

            Projectile.rotation = Projectile.velocity.X * 0.05f;
            Projectile.spriteDirection = Projectile.velocity.X > 0 ? 1 : -1;
            ShootLogic();
        }

        private void ShootLogic()
        {
            Projectile.localAI[0]++;

            if (Projectile.localAI[0] > 45f)
            {
                NPC target = null;
                float maxRange = 700f;

                if (Projectile.OwnerMinionAttackTargetNPC != null && Projectile.OwnerMinionAttackTargetNPC.CanBeChasedBy())
                {
                    target = Projectile.OwnerMinionAttackTargetNPC;
                }
                else
                {
                    foreach (NPC npc in Main.npc)
                    {
                        if (npc.CanBeChasedBy())
                        {
                            float dist = Vector2.Distance(npc.Center, Projectile.Center);
                            if (dist < maxRange)
                            {
                                maxRange = dist;
                                target = npc;
                            }
                        }
                    }
                }

                if (target != null)
                {
                    Vector2 shootVel = (target.Center - Projectile.Center).SafeNormalize(Vector2.Zero) * 14f;
                    if (Main.myPlayer == Projectile.owner)
                    {
                        Terraria.Audio.SoundEngine.PlaySound(SoundID.Item11, Projectile.Center);
                        Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, shootVel, ProjectileID.Bullet, Projectile.damage, Projectile.knockBack, Projectile.owner);
                    }
                    Projectile.localAI[0] = 0;
                }
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity) => false;
    }
}
