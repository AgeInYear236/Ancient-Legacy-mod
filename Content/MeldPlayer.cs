using Microsoft.Xna.Framework;
using System.Security.AccessControl;
using Terraria;
using Terraria.ModLoader;
using testMod1.Content.Buffs;
using testMod1.Content.Items.Accessories;

namespace testMod1.Content
{
    public class MeldPlayer : ModPlayer
    {
        public bool isAmbushActive = false;
        public int ambushTimer = 0;
        private const int MaxTimer = 600;


        public override void PreUpdate()
        {
            if (isAmbushActive && !Player.HasBuff(ModContent.BuffType<TA2Buff>()))
            {
                BreakAmbush(false);
                return;
            }

            if (isAmbushActive)
            {
                if (ambushTimer < MaxTimer)
                {
                    ambushTimer++;
                }

                if (ambushTimer > 10 && Player.velocity.Length() > 0.5f)
                {
                    BreakAmbush(true);
                }
            }
        }

        public override void ModifyWeaponDamage(Item item, ref StatModifier damage)
        {
            if (isAmbushActive && ambushTimer > 0)
            {
                float bonusMult = (ambushTimer / (float)MaxTimer) * 5f;
                damage *= (1f + bonusMult);
            }
        }
        public override bool PreItemCheck()
        {
            if (isAmbushActive && Player.itemAnimation > 0)
            {
                BreakAmbush(false);
            }
            return base.PreItemCheck();
        }

        public override void OnHitNPCWithItem(Item item, NPC target, NPC.HitInfo hit, int damageDone) => BreakAmbush(false);
        public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone) => BreakAmbush(false);

        public void BreakAmbush(bool moved)
        {
            if (!isAmbushActive) return;

            isAmbushActive = false;
            ambushTimer = 0;
            Player.ClearBuff(ModContent.BuffType<TA2Buff>());

            Player.AddBuff(ModContent.BuffType<TA2CooldownBuff>(), 30 * 60);

        }
    }
}