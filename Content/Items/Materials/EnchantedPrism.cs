using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using testMod1.Common.Rarity;

namespace testMod1.Content.Items.Materials
{
    public class EnchantedPrism : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.maxStack = 999;
            Item.material = true;
            Item.value = 100;
            Item.rare = ModContent.GetInstance<CoolStuffRarity>().Type;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.LunarOre, 5);
            recipe.AddIngredient(ItemID.Glass, 5);
            recipe.AddIngredient(ItemID.FragmentNebula, 1);
            recipe.AddIngredient(ItemID.FragmentSolar, 1);
            recipe.AddIngredient(ItemID.FragmentStardust, 1);
            recipe.AddIngredient(ItemID.FragmentVortex, 1);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();

        }
    }
}
