using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.ItemDropRules;
using AncientLegacyMod.Content.Items.Weapons;
using AncientLegacyMod.Content.Items.Accessories;
using AncientLegacyMod.Content.Items.Misc;
using AncientLegacyMod.Content.Items.Consumables;
using AncientLegacyMod.Content.Items.Summons;

namespace AncientLegacyMod.Common.Systems
{
    public class BossDropModifier : GlobalNPC
    {
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            if (npc.type == NPCID.QueenBee)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<MKBar>(), 5));
            }

            if (npc.type == NPCID.WallofFlesh)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<AghsShard>(), 1));
            }

            if (npc.type == NPCID.Plantera)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<WindrangerBow>(), 4));
            }

            if (npc.type == NPCID.Deerclops)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<GyroStaff>(), 2));
            }

            if(npc.type == NPCID.DukeFishron)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<BoneHook>(), 4));
            }

            if (npc.type == NPCID.HallowBoss)
            {
                if (Main.dayTime)
                {
                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<ShardOfEternity>());
                }
            }

            if (npc.type == NPCID.MoonLordCore)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Chronosphere>(), 8));
            }

            if (npc.boss)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<GoldBag>(), 1, 10, 150));
            }
        }
    }
}