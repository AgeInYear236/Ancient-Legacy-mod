using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using AncientLegacyMod.Common.Rarity;

namespace AncientLegacyMod.Content.Items.Accessories
{
    public class Tarrasque : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.accessory = true;
            Item.rare = ModContent.GetInstance<AccRarityStats>().Type;
            Item.value = Item.sellPrice(gold: 2);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statLifeMax2 += 200;
            player.statDefense += 20;

            player.GetDamage(DamageClass.Generic) -= 0.25f;
            player.GetCritChance(DamageClass.Generic) -= 15;

            player.aggro += 500;

            int maxLife = player.statLifeMax2;
            int currentLife = player.statLife;
            float missingHealthPercent = 0f;

            if (maxLife > 0)
            {
                missingHealthPercent = 1f - (float)currentLife / maxLife;
                player.lifeRegen = (int)(100 * missingHealthPercent);
                Main.NewText(player.lifeRegen);
            }
            else
            {
                missingHealthPercent = 0f;
            }
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.LifeCrystal, 10)
                .AddIngredient(ItemID.LifeFruit, 10)
                .AddIngredient(ModContent.ItemType<HuskarBlood>(), 1)
                .AddTile(TileID.AdamantiteForge)
                .Register();
        }
    }
}