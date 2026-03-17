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
    [AutoloadEquip(EquipType.Body)]
    public class vsb : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 18; Item.height = 18;
            Item.defense = 18;
            Item.rare = ItemRarityID.Purple;
        }
        public override void UpdateEquip(Player player)
        {
            player.GetAttackSpeed(DamageClass.Ranged) += 0.10f;
            player.shimmerImmune = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<PoweredSteelBar>(), 18);
            recipe.AddIngredient(ModContent.ItemType<UnstableComponent>(), 10);
            recipe.AddIngredient(ModContent.ItemType<VoidFragment>(), 12);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();

        }
    }
}
