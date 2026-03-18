using rail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using AncientLegacyMod.Content.Items.Accessories;
using AncientLegacyMod.Content.Items.Materials;
using AncientLegacyMod.Content.Items.Misc;
using AncientLegacyMod.Content.Items.Weapons;
using AncientLegacyMod.Content.Items.Neutrals.Tier1;
using AncientLegacyMod.Content.Items.Neutrals.Tier2;
using AncientLegacyMod.Content.Items.Neutrals.Tier3;
using AncientLegacyMod.Content.Items.Neutrals.Tier4;
using AncientLegacyMod.Content.Items.Neutrals.Tier5;

namespace AncientLegacyMod.Common.Systems
{
    public class NeutralDropSystem : GlobalNPC
    {
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            if (!Main.hardMode && !NPC.downedBoss1)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<WeightedDice>(), 500));
            }
            if (!Main.hardMode && NPC.downedBoss1)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Vambrace>(), 2500));
            }
            if (Main.hardMode && !NPC.downedPlantBoss)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Doubloon>(), 2500));
            }
            if (NPC.downedPlantBoss && !NPC.downedMoonlord)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<OutworldStaff>(), 2500));
            }
            if (NPC.downedMoonlord)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<GiantRing>(), 2500));
            }
        }
    }
}
