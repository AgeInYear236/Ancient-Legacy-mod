using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent.UI;
using Terraria.ID;
using Terraria.IO;
using Terraria.ModLoader;
using AncientLegacyMod.Content.Items.Materials;


namespace AncientLegacyMod.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    public class AxeBoots : ModItem
    {
        public static readonly int maxHealth = 10;

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.defense = 6;
            Item.rare = ItemRarityID.Red;
        }

        public override void UpdateEquip(Player player)
        {
            player.statLifeMax2 += maxHealth;

        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<RawFury>(), 10);
            recipe.AddIngredient(ItemID.HellstoneBar, 6);
            recipe.AddIngredient(ItemID.TissueSample, 8);
            recipe.AddTile(TileID.Hellforge);
            recipe.Register();
        }

    }
}
