using Microsoft.Build.Tasks;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AncientLegacyMod.Content.Buffs;

namespace AncientLegacyMod.Content
{
    public class GhostScepterPlayer : ModPlayer
    {
        public bool auraActive;
        public const float AuraRange = 150f;

        public override void ResetEffects()
        {
            auraActive = false;
        }

        public override void PostUpdateEquips()
        {
            if (!auraActive) return;

            Player.GetDamage(DamageClass.Melee) *= 0.5f;
            Player.GetDamage(DamageClass.Ranged) *= 0.5f;
            Player.GetDamage(DamageClass.Magic) *= 1.5f;

            if (Player.whoAmI == Main.myPlayer && Main.rand.NextBool(2))
            {

                for (int i = 0; i < 3; i++)
                {
                    float angle = Main.rand.NextFloat(0, MathHelper.TwoPi);
                    Vector2 spawnPosition = Player.Center + new Vector2(AuraRange, 0).RotatedBy(angle);
                    Dust dust = Dust.NewDustPerfect(spawnPosition, DustID.GreenFairy, Vector2.Zero, 100, default, 1.5f);
                    dust.noGravity = true;
                }

                Vector2 spawnPos = Player.Center + Main.rand.NextVector2Circular(AuraRange, AuraRange);
                Dust dust2 = Dust.NewDustDirect(spawnPos, 0, 0, DustID.GreenTorch, 0f, 0f, 100, default, 1.5f);
                dust2.velocity *= 0.5f;
                dust2.noGravity = true;
            }

            if (Main.rand.NextBool(5))
            {
                Vector2 randomInnerPos = Player.Center + Main.rand.NextVector2Circular(AuraRange, AuraRange);
                Dust d = Dust.NewDustPerfect(randomInnerPos, DustID.GreenTorch, Vector2.Zero, 100, default, 0.5f);
                d.noGravity = true;
                d.fadeIn = 1f;
            }

            // Slow enemies
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC target = Main.npc[i];
                if (target.CanBeChasedBy() && target.Distance(Player.position) < AuraRange)
                {
                    target.velocity *= 0.8f;

                    if (Main.netMode == NetmodeID.Server)
                        NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, i);
                }
            }
        }
    }
}
