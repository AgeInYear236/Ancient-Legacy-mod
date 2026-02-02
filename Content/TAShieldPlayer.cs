using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace testMod1.Content
{
    public class TAShieldPlayer : ModPlayer
    {
        public int shieldCharges = 0;

        public override void ModifyHitByNPC(NPC npc, ref Player.HurtModifiers modifiers)
        {
            if (shieldCharges > 0)
            {
                BlockDamage(ref modifiers);
            }
        }

        public override void ModifyHitByProjectile(Projectile proj, ref Player.HurtModifiers modifiers)
        {
            if (shieldCharges > 0)
            {
                BlockDamage(ref modifiers);
            }
        }

        private void BlockDamage(ref Player.HurtModifiers modifiers)
        {
            shieldCharges--;

            modifiers.FinalDamage *= 0f;
            modifiers.Knockback *= 0f;

            Player.immune = true;
            Player.immuneTime = 60;

            Terraria.Audio.SoundEngine.PlaySound(SoundID.Item37, Player.Center);
            for (int i = 0; i < 10; i++)
            {
                Dust.NewDust(Player.position, Player.width, Player.height, DustID.Pixie, 0, 0, 0, new Color(255, 153, 204), 2f);
            }

            if (shieldCharges <= 0)
            {
                Player.ClearBuff(ModContent.BuffType<Buffs.TA1Buff>());
                Main.NewText("Break!");
            }
        }

        public override void ModifyWeaponDamage(Item item, ref StatModifier damage)
        {
            if (shieldCharges > 0)
            {
                damage.Flat += 30f;
            }
        }

        public override void DrawEffects(PlayerDrawSet drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
        {
            if (shieldCharges <= 0 || Player.dead) return;

            int dustCount = shieldCharges * 2;

            for (int i = 0; i < dustCount; i++)
            {
                float angle = Main.rand.NextFloat(0, MathHelper.TwoPi);

                float radius = 45f;

                Vector2 offset = new Vector2(radius, 0).RotatedBy(angle);
                Vector2 spawnPos = Player.Center + offset;

                Dust d = Dust.NewDustPerfect(spawnPos, DustID.Pixie, Vector2.Zero, 150, new Color(255, 0, 127), 1.2f);
                d.noGravity = true;

                d.velocity = Player.velocity * 0.5f;
            }
        }
    }
}
