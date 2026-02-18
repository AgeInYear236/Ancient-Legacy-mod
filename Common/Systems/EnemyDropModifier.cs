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
using testMod1.Content.Items.Accessories;
using testMod1.Content.Items.Materials;
using testMod1.Content.Items.Misc;
using testMod1.Content.Items.Weapons;

namespace testMod1.Common.Systems
{
    public class EnemyDropModifier : GlobalNPC
    {
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Madstone>(), 50, 1, 3));

            if (npc.type == NPCID.Lavabat || npc.type == NPCID.LavaSlime
                || npc.type == NPCID.Demon || npc.type == NPCID.BoneSerpentHead
                || npc.type == NPCID.RedDevil || npc.type == NPCID.FireImp)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<RawFury>(), 2, 1, 3));
            }

            if (npc.type == NPCID.JungleBat || npc.type == NPCID.JungleCreeper
                || npc.type == NPCID.JungleSlime || npc.type == NPCID.MossHornet
                || npc.type == NPCID.BigMossHornet || npc.type == NPCID.GiantMossHornet 
                || npc.type == NPCID.LittleMossHornet || npc.type == NPCID.TinyMossHornet)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<AncientMoss>(), 3, 1, 3));
            }

            if(npc.type == NPCID.Pixie || npc.type == NPCID.Unicorn
                || npc.type == NPCID.Gastropod)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<MagicEnergy>(), 3, 1, 3));
            }

            if(NPCID.Sets.Skeletons[npc.type])
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Armlet>(), 447));
            }

            if (Main.player[Player.FindClosest(npc.position, npc.width, npc.height)].ZoneSnow)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<IceShell>(), 10));
            }

            if (Main.moonPhase == 0 && !Main.dayTime)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Moonstone>(), 10, 1, 3));
            }

            if (npc.type == NPCID.BigMimicCorruption)
            {
                npcLoot.RemoveWhere(rule => rule is OneFromOptionsDropRule opt &&
                    opt.dropIds.Contains(ItemID.DartRifle));

                int[] lootTable = new int[] {
                ItemID.DartRifle,
                ItemID.WormHook,
                ItemID.ChainGuillotines,
                ItemID.ClingerStaff,
                ItemID.PutridScent,
                ModContent.ItemType<HuskarBlood>()
            };

                npcLoot.Add(ItemDropRule.OneFromOptions(1, lootTable));
            }

            if(npc.type == NPCID.RedDevil && Main.hardMode && Main.expertMode)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<HuskarSpear>(), 100));
            }
        }
    }
}
