using Microsoft.Xna.Framework;
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

namespace testMod1.Content.Items.Accessories
{
    public class Desolator : ModItem
    {
        public int ar = 1;
        public override void SetDefaults()
        {
            Item.material = true;
            Item.width = 32;
            Item.height = 32;
            Item.accessory = true;
            Item.rare = ModContent.GetInstance<AccRarityStats>().Type;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<DesolatorPlayer>().hasDesolator = true;
            player.GetModPlayer<DesolatorPlayer>().armorReduction = ar;
        }

        public override bool CanEquipAccessory(Player player, int slot, bool modded)
        {
            for (int i = 3; i < 10; i++)
            {
                if (player.armor[i].type == ModContent.ItemType<Desolator2>())
                {
                    return false;
                }
            }
            return true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.SharkToothNecklace, 1);
            recipe.AddIngredient(ModContent.ItemType<RawFury>(), 10);
            recipe.AddIngredient(ModContent.ItemType<Madstone>(), 15);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();

        }
    }
}
