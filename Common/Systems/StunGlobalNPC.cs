using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using testMod1.Content.Buffs;

namespace testMod1.Common.Systems
{
    public class StunGlobalNPC : GlobalNPC
    {
        public bool isStunned;

        public override bool InstancePerEntity => true;

        public override void ResetEffects(NPC npc)
        {
            isStunned = false;
        }

        public override bool PreAI(NPC npc)
        {
            if (npc.HasBuff(ModContent.BuffType<StunBuff>()))
            {
                npc.velocity = Microsoft.Xna.Framework.Vector2.Zero;
                return false;
            }
            return true;
        }
    }
}