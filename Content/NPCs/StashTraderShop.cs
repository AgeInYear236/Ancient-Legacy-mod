using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using testMod1.Content.Items.Consumables;
using testMod1.Content.Items.Weapons;

namespace testMod1.Content.NPCs
{
    public class StashTraderShop : GlobalNPC
    {
        public override bool AppliesToEntity(NPC npc, bool lateInstantiation)
        {
            return npc.ModNPC?.GetType() == typeof(StashTrader);
        }

        public override void ModifyShop(NPCShop shop)
        {
            //WEAPONS

            var blink = new Item(ModContent.ItemType<BlinkDagger>());//
            blink.shopCustomPrice = 220;
            blink.shopSpecialCurrency = testMod1.goldCurrencyId;

            var rogue = new Item(ModContent.ItemType<RogueDagger>());//
            rogue.shopCustomPrice = 1500;
            rogue.shopSpecialCurrency = testMod1.goldCurrencyId;

            var tango = new Item(ModContent.ItemType<Tango>());//
            tango.shopCustomPrice = 3;

            var mango = new Item(ModContent.ItemType<Mango>());//
            mango.shopCustomPrice = 3;

            var axe1 = new Item(ModContent.ItemType<AxeAxe>());//
            axe1.shopCustomPrice = 300;
            axe1.shopSpecialCurrency = testMod1.goldCurrencyId;

            var axe2 = new Item(ModContent.ItemType<AxeAxeII>());//
            axe2.shopCustomPrice = 1500;
            axe2.shopSpecialCurrency = testMod1.goldCurrencyId;

            var axe3 = new Item(ModContent.ItemType<AxeTerraAxe>());//
            axe3.shopCustomPrice = 3500;
            axe3.shopSpecialCurrency = testMod1.goldCurrencyId;

            var hook = new Item(ModContent.ItemType<BoneHook>());//
            hook.shopCustomPrice = 3700;
            hook.shopSpecialCurrency = testMod1.goldCurrencyId;

            var lichbook = new Item(ModContent.ItemType<ChainFrostTome>());//
            lichbook.shopCustomPrice = 950;
            lichbook.shopSpecialCurrency = testMod1.goldCurrencyId;

            var ck = new Item(ModContent.ItemType<CKSword>());//
            ck.shopCustomPrice = 3000;
            ck.shopSpecialCurrency = testMod1.goldCurrencyId;

            var keeper = new Item(ModContent.ItemType<KeeperStaff>());//
            keeper.shopCustomPrice = 500;
            keeper.shopSpecialCurrency = testMod1.goldCurrencyId;

            var magnus = new Item(ModContent.ItemType<MagnusMallet>());//
            magnus.shopCustomPrice = 500;
            magnus.shopSpecialCurrency = testMod1.goldCurrencyId;

            var mkb = new Item(ModContent.ItemType<MKBar>());//
            mkb.shopCustomPrice = 500;
            mkb.shopSpecialCurrency = testMod1.goldCurrencyId;

            var pa = new Item(ModContent.ItemType<PADagger>());//
            pa.shopCustomPrice = 1750;
            pa.shopSpecialCurrency = testMod1.goldCurrencyId;

            var pugna = new Item(ModContent.ItemType<PugnaStaff>());//
            pugna.shopCustomPrice = 1500;
            pugna.shopSpecialCurrency = testMod1.goldCurrencyId;

            var sf = new Item(ModContent.ItemType<ShadowrazeBook>());//
            sf.shopCustomPrice = 1500;
            sf.shopSpecialCurrency = testMod1.goldCurrencyId;

            var slardar = new Item(ModContent.ItemType<SlardarTrident>());//
            slardar.shopCustomPrice = 3000;
            slardar.shopSpecialCurrency = testMod1.goldCurrencyId;

            var sniper = new Item(ModContent.ItemType<SniperRifle>());
            sniper.shopCustomPrice = 5000;
            sniper.shopSpecialCurrency = testMod1.goldCurrencyId;

            //No rules
            shop.Add(tango);
            shop.Add(mango);


            //Eye of Cthulhu
            if(NPC.downedBoss1) {
                shop.Add(axe1);
                shop.Add(keeper);
            }

            if (NPC.downedBoss2)
            {
                shop.Add(magnus);
            }

            if (NPC.downedQueenBee)
            {
                shop.Add(mkb);
            }

            if (NPC.downedBoss3)
            {
                shop.Add(blink);
            }

            if (Main.hardMode) {
                shop.Add(rogue);
                shop.Add(lichbook);
            }


            if (NPC.downedQueenSlime)
            {
                shop.Add(sf);
            }

            if (NPC.downedMechBossAny)
            {
                shop.Add(axe2);
                shop.Add(pa);
            }

            if(NPC.downedPlantBoss)
            {
                shop.Add(ck);
                shop.Add(pugna);
            }

            if (NPC.downedFishron)
            {
                shop.Add(hook);
            }

            if(NPC.downedGolemBoss)
            {
                shop.Add(axe3);
                shop.Add(slardar);
            }

            if(NPC.downedTowerVortex)
            {
                shop.Add(sniper);
            }

        }
    }
}