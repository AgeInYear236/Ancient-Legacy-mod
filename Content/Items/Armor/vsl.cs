using AncientLegacyMod.Content.Items.Materials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AncientLegacyMod.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    public class vsl : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.rare = ItemRarityID.Purple;
            Item.defense = 14;
        }

        public override void UpdateEquip(Player player)
        {
            player.moveSpeed += 0.20f;
            player.maxRunSpeed += 2f; 

            player.aggro -= 250;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<PoweredSteelBar>(), 12);
            recipe.AddIngredient(ModContent.ItemType<UnstableComponent>(), 8);
            recipe.AddIngredient(ModContent.ItemType<VoidFragment>(), 10);

            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();

        }
    }
}
