using AncientLegacyMod.Content.Items.Accessories;
using AncientLegacyMod.Content.Items.Neutrals.Tier1;
using AncientLegacyMod.Content.Items.Neutrals.Tier2;
using AncientLegacyMod.Content.Items.Neutrals.Tier3;
using AncientLegacyMod.Content.Items.Neutrals.Tier4;
using AncientLegacyMod.Content.Items.Neutrals.Tier5;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace AncientLegacyMod.Common.Systems
{
    public class NeutralDropSystem : GlobalNPC
    {
        // --- СОЗДАЕМ НАШИ СОБСТВЕННЫЕ УСЛОВИЯ ДРОПА ---

        // ТИР 1: До Глаза Ктулху (Pre-Hardmode + Не убит Глаз)
        public class Tier1Condition : IItemDropRuleCondition
        {
            public bool CanDrop(DropAttemptInfo info) => !Main.hardMode && !NPC.downedBoss1;
            public bool CanShowItemDropInUI() => true; // ИСПРАВЛЕНО ИМЯ МЕТОДА
            public string GetConditionDescription() => "EyeOfCthulhu";
        }

        // ТИР 2: После Глаза Ктулху, но до Хардмода
        public class Tier2Condition : IItemDropRuleCondition
        {
            public bool CanDrop(DropAttemptInfo info) => !Main.hardMode && NPC.downedBoss1;
            public bool CanShowItemDropInUI() => true; // ИСПРАВЛЕНО ИМЯ МЕТОДА
            public string GetConditionDescription() => "После убийства Глаза Ктулху в до-хардмоде";
        }

        // ТИР 3: Хардмод до Плантеры
        public class Tier3Condition : IItemDropRuleCondition
        {
            public bool CanDrop(DropAttemptInfo info) => Main.hardMode && !NPC.downedPlantBoss;
            public bool CanShowItemDropInUI() => true; // ИСПРАВЛЕНО ИМЯ МЕТОДА
            public string GetConditionDescription() => "В хардмоде до убийства Плантеры";
        }

        // ТИР 4: После Плантеры до Мунлорда
        public class Tier4Condition : IItemDropRuleCondition
        {
            public bool CanDrop(DropAttemptInfo info) => NPC.downedPlantBoss && !NPC.downedMoonlord;
            public bool CanShowItemDropInUI() => true; // ИСПРАВЛЕНО ИМЯ МЕТОДА
            public string GetConditionDescription() => "После Плантеры до убийства Мунлорда";
        }

        // ТИР 5: Пост-Мунлорд
        public class Tier5Condition : IItemDropRuleCondition
        {
            public bool CanDrop(DropAttemptInfo info) => NPC.downedMoonlord;
            public bool CanShowItemDropInUI() => true; // ИСПРАВЛЕНО ИМЯ МЕТОДА
            public string GetConditionDescription() => "После убийства Мунлорда";
        }


        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            // ФИЛЬТР: Только обычные монстры
            if (npc.friendly || npc.SpawnedFromStatue || npc.lifeMax <= 5 || npc.value <= 0f)
            {
                return;
            }

            // Регистрируем правила через LeadingConditionRule.

            npcLoot.Add(new LeadingConditionRule(new Tier1Condition())).OnSuccess(
                ItemDropRule.Common(ModContent.ItemType<WeightedDice>(), 5)
            );

            npcLoot.Add(new LeadingConditionRule(new Tier2Condition())).OnSuccess(
                ItemDropRule.Common(ModContent.ItemType<Vambrace>(), 5)
            );

            npcLoot.Add(new LeadingConditionRule(new Tier3Condition())).OnSuccess(
                ItemDropRule.Common(ModContent.ItemType<Doubloon>(), 5)
            );

            npcLoot.Add(new LeadingConditionRule(new Tier4Condition())).OnSuccess(
                ItemDropRule.Common(ModContent.ItemType<OutworldStaff>(), 5)
            );

            npcLoot.Add(new LeadingConditionRule(new Tier5Condition())).OnSuccess(
                ItemDropRule.Common(ModContent.ItemType<GiantRing>(), 5)
            );
        }
    }
}