using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using testMod1.Content.Items.Projectiles;

namespace testMod1.Content.Items.Misc
{
    public class Fireball : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 14;
            Item.height = 14;
            Item.maxStack = 9999; 
            Item.consumable = true; 
            Item.knockBack = 2f;
            Item.value = Item.sellPrice(copper: 10);
            Item.rare = ItemRarityID.Orange;

            Item.shoot = ModContent.ProjectileType<FireballProjectile>();
            Item.shootSpeed = 3f;
            Item.ammo = ModContent.ItemType<Fireball>();
        }

        public override void AddRecipes()
        {
            CreateRecipe(50)
                .AddIngredient(ItemID.MusketBall, 50)
                .AddCondition(Condition.NearLava)
                .Register();
        }
    }
}
