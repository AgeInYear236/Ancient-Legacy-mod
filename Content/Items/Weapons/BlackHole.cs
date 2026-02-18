using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.ModLoader;
using testMod1.Common.Rarity;
using testMod1.Content.Items.Misc;
using testMod1.Content.Items.Projectiles;

namespace testMod1.Content.Items.Weapons
{
    public class BlackHole : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 300; 
            Item.DamageType = DamageClass.Generic;
            Item.width = 40;
            Item.height = 40;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = ItemUseStyleID.HoldUp; 
            Item.noMelee = true;
            Item.channel = true;
            Item.mana = 100;
            Item.shoot = ModContent.ProjectileType<BlackHoleProjectile>();
        }

        public override void SetStaticDefaults()
        {
            ItemID.Sets.ShimmerTransformToItem[ModContent.ItemType<ShardOfDesolation>()] = ModContent.ItemType<BlackHole>();
        }
    }
}
