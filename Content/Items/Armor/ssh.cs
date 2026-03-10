using AncientLegacyMod.Common.Rarity;
using AncientLegacyMod.Content.Items.Materials;
using AncientLegacyMod.Content.Items.Weapons;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.Map;
using Terraria.ModLoader;

namespace AncientLegacyMod.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class ssh : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(0, 2, 0, 0);
            Item.rare = ModContent.GetInstance<MagicRarity2>().Type;
            Item.defense = 8;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Magic) += 0.10f;
            player.GetCritChance(DamageClass.Magic) += 5;
        }


        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<ssb>() && legs.type == ModContent.ItemType<ssl>();
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = Language.GetTextValue("Mods.AncientLegacyMod.Items.ItemSetBonus.ssh");
            player.GetModPlayer<StormPlayer>().hasOverloadSet = true;

            player.manaRegenBonus += 20;
            player.moveSpeed += 0.20f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<PoweredSteelBar>(), 12);
            recipe.AddIngredient(ModContent.ItemType<UnstableComponent>(), 8);
            recipe.AddIngredient(ItemID.Star, 6);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();

        }
    }
}