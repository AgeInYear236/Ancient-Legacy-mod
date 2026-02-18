using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using testMod1.Content.Buffs;

namespace testMod1.Content.Items.Misc
{
    public class Aegis : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 32;
            Item.maxStack = 1;
            Item.rare = ItemRarityID.Red;
            Item.value = Item.sellPrice(gold: 6, silver: 66, copper: 66);
        }

        public override bool CanPickup(Player player)
        {
            return !player.HasBuff(ModContent.BuffType<AegisCooldownBuff>());
        }

        public override void UpdateInventory(Player player)
        {
            if (player.HasBuff(ModContent.BuffType<AegisCooldownBuff>()))
            {
                Item.TurnToAir();
                //Main.NewText("AAAAAAA");
                return;
            }

            player.AddBuff(ModContent.BuffType<AegisBuff>(), 2);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.DemonHeart, 1)
                .AddIngredient(ItemID.LifeCrystal, 5)
                .AddCondition(Condition.NearLava)
                .Register();
        }
    }
}
