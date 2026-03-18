using AncientLegacyMod.Common.Rarity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AncientLegacyMod.Content.Items.Accessories.Boots
{
    public class LeatherBoots : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.material = true;
            Item.value = Item.sellPrice(silver: 50);
            Item.rare = ModContent.GetInstance<MaterialRarity1>().Type;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.moveSpeed += 0.1f;
            player.jumpSpeedBoost += 0.1f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Leather, 15);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();

        }
    }
}
