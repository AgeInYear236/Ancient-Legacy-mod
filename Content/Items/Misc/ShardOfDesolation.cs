using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using testMod1.Common.Rarity;
using testMod1.Content.Items.Materials;

namespace testMod1.Content.Items.Misc
{
    public class ShardOfDesolation : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.maxStack = 999;
            Item.material = true;
            Item.value = 100;
            Item.rare = ModContent.GetInstance<EndgameRarity>().Type;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<ShardOfEternity>(), 1);
            recipe.AddCondition(Condition.NearShimmer);
            recipe.Register();
        }
    }
}
