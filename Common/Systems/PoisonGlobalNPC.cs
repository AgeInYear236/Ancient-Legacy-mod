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

namespace AncientLegacyMod.Common.Systems
{
    public class PoisonGlobalNPC : GlobalNPC
    {
        public override bool InstancePerEntity => true;

        public int poisonStacks = 0;

        public override void ResetEffects(NPC npc)
        {
            if (!npc.HasBuff(ModContent.BuffType<PoisonAttackBuff>()))
            {
                poisonStacks = 0;
            }
        }

        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            if (poisonStacks > 0)
            {
                if (npc.lifeRegen > 0) npc.lifeRegen = 0;

                npc.lifeRegen -= 10 * poisonStacks;

                if (damage < poisonStacks) damage = poisonStacks;
            }
        }

        public override void PostAI(NPC npc)
        {
            if (poisonStacks > 0)
            {
                float speedMultiplier = 1f - (poisonStacks * 0.05f);
                npc.velocity.X *= speedMultiplier;
            }
        }

        public override void DrawEffects(NPC npc, ref Color drawColor)
        {
            if (poisonStacks > 0)
            {
                float intensity = poisonStacks / 10f;
                drawColor = Color.Lerp(drawColor, Color.GreenYellow, intensity);
            }
        }
    }
}
