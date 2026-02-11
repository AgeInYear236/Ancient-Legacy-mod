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
using testMod1.Content.Items.Materials;


namespace testMod1.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    public class AxeArmor : ModItem
    {

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.defense = 15;
            Item.rare = ItemRarityID.Red;
        }

        public override void UpdateEquip(Player player)
        {
            /*if (!player.HasBuff(BuffID.Thorns))
            {
                player.AddBuff(BuffID.Thorns, 2);
            }*/
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<RawFury>(), 15);
            recipe.AddIngredient(ItemID.HellstoneBar, 10);
            recipe.AddIngredient(ItemID.TissueSample, 12);
            recipe.AddTile(TileID.Hellforge);
            recipe.Register();
        }

    }
}
