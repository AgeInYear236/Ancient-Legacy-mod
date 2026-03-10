using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AncientLegacyMod.Content.Buffs
{
    public class ClownBloodBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true; 
            Main.debuff[Type] = true;
            Main.buffNoTimeDisplay[Type] = false;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            if (npc.lifeRegen > 0) npc.lifeRegen = 0;
            npc.lifeRegen -= 10;

            if (Main.rand.NextBool(4))
            {
                Dust d = Dust.NewDustDirect(npc.position, npc.width, npc.height, DustID.Blood, 0, 0, 100, default, 0.7f);
                d.noGravity = true;
                d.velocity *= 0.5f;
            }
        }
    }
}
