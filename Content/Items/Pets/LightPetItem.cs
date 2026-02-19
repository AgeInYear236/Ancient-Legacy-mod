using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AncientLegacyMod.Content.Buffs;

namespace AncientLegacyMod.Content.Items.Pets
{
    public class LightPetItem : ModItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToVanitypet(ModContent.ProjectileType<LightPetProjectile>(), ModContent.BuffType<LightPetBuff>());
            Item.width = 32;
            Item.height = 32;
            Item.rare = ItemRarityID.Yellow;
            Item.value = Item.buyPrice(gold: 1);
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddBuff(ModContent.BuffType<LightPetBuff>(), 2);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.SoulofLight, 5)
                .AddIngredient(ItemID.GoldBar, 1)
                .AddIngredient(ItemID.Gel, 10)
                .AddTile(TileID.WorkBenches);
                //.Register();
        }
    }
}
