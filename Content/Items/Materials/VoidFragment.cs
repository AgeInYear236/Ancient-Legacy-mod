using AncientLegacyMod.Common.Rarity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AncientLegacyMod.Content.Items.Materials
{
    public class VoidFragment : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.maxStack = 999;
            Item.material = true;
            Item.value = 1000;
            Item.rare = ModContent.GetInstance<MaterialRarity2>().Type;

        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(ModContent.ItemType<Madstone>(), 5);
            recipe.AddIngredient(ItemID.Bone, 5);
            recipe.AddIngredient(ItemID.JungleSpores, 5);
            recipe.AddIngredient(ItemID.ShadowScale, 5);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();

            Recipe recipe2 = CreateRecipe(1);
            recipe2.AddIngredient(ModContent.ItemType<Madstone>(), 5);
            recipe2.AddIngredient(ItemID.Bone, 5);
            recipe2.AddIngredient(ItemID.JungleSpores, 5);
            recipe2.AddIngredient(ItemID.TissueSample, 5);
            recipe2.AddTile(TileID.DemonAltar);
            recipe2.Register();
        }
    }
}
