using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using testMod1.Common.Rarity;
using testMod1.Content.Buffs;
using testMod1.Content.Items.Materials;

namespace testMod1.Content.Items.Accessories
{
    public class BlackKingBar : ModItem
    {
        public static bool isActive = false;
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.accessory = true;
            Item.rare = ModContent.GetInstance<MiscRarity>().Type;
            Item.value = Item.sellPrice(gold: 2);
            Item.expert = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<BasicModPlayer>().hasBKB = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<PoweredSteelBar>(), 18);
            recipe.AddIngredient(ModContent.ItemType<Madstone>(), 50);
            recipe.AddIngredient(ItemID.AncientBattleArmorMaterial, 2);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();

        }
    }
}
