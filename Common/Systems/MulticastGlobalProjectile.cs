using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using AncientLegacyMod.Content.Items.Accessories;

namespace AncientLegacyMod.Common.Systems
{
    public class EntitySource_Multicast : EntitySource_Parent
    {
        public EntitySource_Multicast(Entity entity, string context = null) : base(entity, context) { }
    }

    public class MulticastGlobalProjectile : GlobalProjectile
    {
        public override bool InstancePerEntity => true;

        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            if (source is EntitySource_Multicast)
            {
                return;
            }

            if (source is EntitySource_ItemUse itemSource)
            {
                Player player = Main.player[projectile.owner];
                var modPlayer = player.GetModPlayer<MulticastPlayer>();

                if (modPlayer.hasMulticast)
                {
                    float roll = Main.rand.NextFloat();
                    int multCount = 1;

                    if (roll < 0.02f) multCount = 4;
                    else if (roll < 0.05f) multCount = 3;
                    else if (roll < 0.10f) multCount = 2;

                    if (multCount > 1)
                    {
                        TriggerMulticast(projectile, player, multCount);
                    }
                }
            }
        }

        private void TriggerMulticast(Projectile projectile, Player player, int count)
        {
            var multicastSource = new EntitySource_Multicast(projectile);

            for (int i = 1; i < count; i++)
            {
                Vector2 perturbedSpeed = projectile.velocity.RotatedByRandom(MathHelper.ToRadians(12));

                Projectile.NewProjectile(multicastSource, projectile.Center, perturbedSpeed, projectile.type, projectile.damage, projectile.knockBack, projectile.owner);
            }

            CombatText.NewText(player.getRect(), Color.Orange, $"x{count} Multicast!", true);

            SoundEngine.PlaySound(SoundID.Item4 with { Pitch = 0.5f, Volume = 0.8f }, player.Center);

            for (int i = 0; i < 15; i++)
            {
                Dust d = Dust.NewDustDirect(player.position, player.width, player.height, DustID.Flare, 0, -2f, 100, default, 1.5f);
                d.noGravity = true;
                d.velocity *= 1.5f;
            }
        }
    }
}
