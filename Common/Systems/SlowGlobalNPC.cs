using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AncientLegacyMod.Content.Buffs;

namespace AncientLegacyMod.Common.Systems
{
    public class SlowGlobalNPC : GlobalNPC
    {
        public bool isSlowed;

        public override bool InstancePerEntity => true;

        public override void ResetEffects(NPC npc)
        {
            isSlowed = false;
        }

        public override bool PreAI(NPC npc)
        {
            if (npc.HasBuff(ModContent.BuffType<StunBuff>()))
            {
                npc.velocity = npc.velocity * 0.67f;
                return false;
            }
            return true;
        }
    }
}