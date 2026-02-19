using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AncientLegacyMod.Common.Rarity;

namespace AncientLegacyMod.Content.Items.Weapons
{
    public class SunStrikeTome : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 150;
            Item.DamageType = DamageClass.Magic;
            Item.width = 28;
            Item.height = 30;
            Item.useTime = 40;
            Item.useAnimation = 40;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.noMelee = true;
            Item.knockBack = 6f;
            Item.value = Item.buyPrice(gold: 7);
            Item.rare = ModContent.GetInstance<MagicRarity3>().Type;
            Item.mana = 50;
            Item.UseSound = SoundID.Item88;
            Item.autoReuse = false;
            Item.shoot = ModContent.ProjectileType<Projectiles.SunStrikeProjectile>();

            Item.scale = 0.6f;

        }

        public override Vector2? HoldoutOffset() => new Vector2(-2, 4);


        public override bool CanUseItem(Player player)
        {
            if (player.Center.Y / 16f > Main.worldSurface)
            {
                return false;
            }
            return base.CanUseItem(player);
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            position = Main.MouseWorld;
            velocity = Vector2.Zero;
        }
    }
}