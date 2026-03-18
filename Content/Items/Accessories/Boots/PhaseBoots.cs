using AncientLegacyMod.Common.Rarity;
using AncientLegacyMod.Content.Items.Materials;
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
    public class PhaseBoots : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.value = Item.sellPrice(silver: 75);
            Item.rare = ModContent.GetInstance<MaterialRarity2>().Type;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statDefense += 4;
            player.moveSpeed += 0.3f;
            player.jumpSpeedBoost += 0.3f;
            
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<LeatherBoots>(), 1);
            recipe.AddIngredient(ModContent.ItemType<Chainmail>(), 2);
            recipe.AddIngredient(ModContent.ItemType<VoidFragment>(), 3);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();

        }
    }
}
