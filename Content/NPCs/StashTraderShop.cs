using Terraria;
using Terraria.Chat;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using testMod1.Content.Items.Consumables;
using testMod1.Content.Items.Weapons;

namespace testMod1.Content.NPCs
{
    public class StashTraderShop : GlobalNPC
    {
        public override bool AppliesToEntity(NPC npc, bool lateInstantiation)
        {
            return npc.ModNPC is StashTrader;
        }

        public override void ModifyShop(NPCShop shop)
        {
            var blink = new Item(ModContent.ItemType<BlinkDagger>()) { shopCustomPrice = 720, shopSpecialCurrency = testMod1.goldCurrencyId };
            var rogue = new Item(ModContent.ItemType<RogueDagger>()) { shopCustomPrice = 1500, shopSpecialCurrency = testMod1.goldCurrencyId };
            var tango = new Item(ModContent.ItemType<Tango>()) { shopCustomPrice = 300 };
            var mango = new Item(ModContent.ItemType<Mango>()) { shopCustomPrice = 300 };
            var axe1 = new Item(ModContent.ItemType<AxeAxe>()) { shopCustomPrice = 300, shopSpecialCurrency = testMod1.goldCurrencyId };
            var axe2 = new Item(ModContent.ItemType<AxeAxeII>()) { shopCustomPrice = 1500, shopSpecialCurrency = testMod1.goldCurrencyId };
            var axe3 = new Item(ModContent.ItemType<AxeTerraAxe>()) { shopCustomPrice = 3500, shopSpecialCurrency = testMod1.goldCurrencyId };
            var hook = new Item(ModContent.ItemType<BoneHook>()) { shopCustomPrice = 3700, shopSpecialCurrency = testMod1.goldCurrencyId };
            var lichbook = new Item(ModContent.ItemType<ChainFrostTome>()) { shopCustomPrice = 950, shopSpecialCurrency = testMod1.goldCurrencyId };
            var ck = new Item(ModContent.ItemType<CKSword>()) { shopCustomPrice = 3000, shopSpecialCurrency = testMod1.goldCurrencyId };
            var keeper = new Item(ModContent.ItemType<KeeperStaff>()) { shopCustomPrice = 500, shopSpecialCurrency = testMod1.goldCurrencyId };
            var magnus = new Item(ModContent.ItemType<MagnusMallet>()) { shopCustomPrice = 500, shopSpecialCurrency = testMod1.goldCurrencyId };
            var mkb = new Item(ModContent.ItemType<MKBar>()) { shopCustomPrice = 500, shopSpecialCurrency = testMod1.goldCurrencyId };
            var pa = new Item(ModContent.ItemType<PADagger>()) { shopCustomPrice = 1750, shopSpecialCurrency = testMod1.goldCurrencyId };
            var pugna = new Item(ModContent.ItemType<PugnaStaff>()) { shopCustomPrice = 1500, shopSpecialCurrency = testMod1.goldCurrencyId };
            var sf = new Item(ModContent.ItemType<ShadowrazeBook>()) { shopCustomPrice = 1500, shopSpecialCurrency = testMod1.goldCurrencyId };
            var slardar = new Item(ModContent.ItemType<SlardarTrident>()) { shopCustomPrice = 3000, shopSpecialCurrency = testMod1.goldCurrencyId };
            var sniper = new Item(ModContent.ItemType<SniperRifle>()) { shopCustomPrice = 5000, shopSpecialCurrency = testMod1.goldCurrencyId };
            var hood = new Item(ModContent.ItemType<HoodwinkLauncher>()) { shopCustomPrice = 550, shopSpecialCurrency = testMod1.goldCurrencyId };
            var drow = new Item(ModContent.ItemType<DrowRangerBow>()) { shopCustomPrice = 2000, shopSpecialCurrency = testMod1.goldCurrencyId };
            var wr = new Item(ModContent.ItemType<WindrangerBow>()) { shopCustomPrice = 5000, shopSpecialCurrency = testMod1.goldCurrencyId };
            var ss = new Item(ModContent.ItemType<SunStrikeTome>()) { shopCustomPrice = 7000, shopSpecialCurrency = testMod1.goldCurrencyId };


            shop.Add(tango);
            shop.Add(mango);

            shop.Add(axe1, Condition.DownedEyeOfCthulhu);
            shop.Add(keeper, Condition.DownedEyeOfCthulhu);
            shop.Add(hood, Condition.DownedEyeOfCthulhu);

            shop.Add(magnus, Condition.DownedEaterOfWorlds);
            shop.Add(magnus, Condition.DownedBrainOfCthulhu);
            shop.Add(mkb, Condition.DownedQueenBee);
            shop.Add(blink, Condition.DownedSkeletron);

            shop.Add(rogue, Condition.Hardmode);
            shop.Add(lichbook, Condition.Hardmode);
            shop.Add(drow, Condition.Hardmode);

            shop.Add(sf, Condition.DownedQueenSlime);

            shop.Add(axe2, Condition.DownedMechBossAny);
            shop.Add(pa, Condition.DownedMechBossAny);

            shop.Add(ck, Condition.DownedPlantera);
            shop.Add(pugna, Condition.DownedPlantera);
            shop.Add(wr, Condition.DownedPlantera);

            shop.Add(hook, Condition.DownedDukeFishron);

            shop.Add(axe3, Condition.DownedGolem);
            shop.Add(slardar, Condition.DownedGolem);
            shop.Add(sniper, Condition.DownedGolem);

            shop.Add(ss, Condition.DownedSolarPillar);

        }
    }
}