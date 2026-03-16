using AncientLegacyMod.Common.Rarity;
using AncientLegacyMod.Content.Items.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AncientLegacyMod.Content.Items.Weapons
{
    public class Parasma : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 38;
            Item.DamageType = DamageClass.Magic;
            Item.mana = 10;
            Item.width = 34;
            Item.height = 34;
            Item.useTime = 22;
            Item.useAnimation = 22;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.noMelee = true;
            Item.knockBack = 4;
            Item.value = Item.sellPrice(gold: 7);
            Item.rare = ModContent.GetInstance<MagicRarity3>().Type;
            Item.UseSound = SoundID.Item43;
            Item.autoReuse = true;

            Item.shoot = ModContent.ProjectileType<ParasmaProjectile>();
            Item.shootSpeed = 10f;
        }
    }
}