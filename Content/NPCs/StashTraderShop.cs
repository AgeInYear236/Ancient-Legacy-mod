using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

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


            var blink = new Item(ModContent.ItemType<Items.Weapons.BlinkDagger>());
            blink.shopCustomPrice = 5;
            blink.shopSpecialCurrency = testMod1.goldCurrencyId;

            var pet = new Item(ModContent.ItemType<Items.Pets.LightPetItem>());
            pet.shopCustomPrice = 3;
            pet.shopSpecialCurrency = testMod1.goldCurrencyId;


            var rogue = new Item(ModContent.ItemType<Items.Weapons.RogueDagger>());
            rogue.shopCustomPrice = 5;
            rogue.shopSpecialCurrency = testMod1.goldCurrencyId;


            //No rules
            shop.Add(pet);

            //Eye of Cthulhu
            if(NPC.downedBoss1) {
                shop.Add(blink);
            }

            //Hard mode
            if (Main.hardMode) {
                shop.Add(rogue);
            }

        }
    }
}