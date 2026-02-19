using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AncientLegacyMod.Common.Rarity;
using AncientLegacyMod.Content.Buffs;
using AncientLegacyMod.Content.Items.Materials;

namespace AncientLegacyMod.Content.Items.Accessories
{

    public class Timelapse : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.accessory = true;
            Item.rare = ModContent.GetInstance<EndgameRarity>().Type;
            Item.value = Item.sellPrice(gold: 5);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<BasicModPlayer>().hasTimelapse = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<EnchantedPrism>(), 5);
            recipe.AddIngredient(ModContent.ItemType<Madstone>(), 99);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}
