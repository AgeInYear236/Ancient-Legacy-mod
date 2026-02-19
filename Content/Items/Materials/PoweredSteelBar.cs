using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AncientLegacyMod.Common.Rarity;

namespace AncientLegacyMod.Content.Items.Materials
{
    public class PoweredSteelBar : ModItem
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
            Recipe recipe = CreateRecipe(2);
            recipe.AddIngredient(ItemID.IronBar, 2);
            recipe.AddIngredient(ItemID.SoulofLight, 1);
            recipe.AddIngredient(ItemID.SoulofNight, 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();

            Recipe altrecipe = CreateRecipe(2);
            altrecipe.AddIngredient(ItemID.LeadBar, 2);
            altrecipe.AddIngredient(ItemID.SoulofLight, 1);
            altrecipe.AddIngredient(ItemID.SoulofNight, 1);
            altrecipe.AddTile(TileID.MythrilAnvil);
            altrecipe.Register();
        }
    }
}
