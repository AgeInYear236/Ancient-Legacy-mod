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
    public class emsb : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(gold: 3);
            Item.rare = ItemRarityID.Pink;
            Item.defense = 12;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetAttackSpeed(DamageClass.Melee) += 0.25f;
            player.GetDamage(DamageClass.Melee) += 0.10f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<PoweredSteelBar>(), 18);
            recipe.AddIngredient(ModContent.ItemType<UnstableComponent>(), 10);
            recipe.AddIngredient(ItemID.LivingFireBlock, 100);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}
