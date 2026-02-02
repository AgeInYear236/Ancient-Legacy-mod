using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using testMod1.Common.Rarity;

namespace testMod1.Content.Items.Accessories
{
    public class WispBottle : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 24;
            Item.accessory = true;
            Item.rare = ModContent.GetInstance<MiscRarity>().Type;
            Item.value = Item.sellPrice(0, 15, 0, 0);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            bool isEquipped = false;

            for (int i = 3; i < 10; i++)
            {
                if (player.armor[i].type == ModContent.ItemType<WispBottle>())
                {
                    isEquipped = true;
                    break;
                }
            }

            if (isEquipped)
            {
                if (player.whoAmI == Main.myPlayer && player.ownedProjectileCounts[ModContent.ProjectileType<Projectiles.WispProjectile>()] <= 0)
                {
                    Projectile.NewProjectile(player.GetSource_Accessory(Item), player.Center, Vector2.Zero, ModContent.ProjectileType<Projectiles.WispProjectile>(), 0, 0f, player.whoAmI);
                }
            }
        }
    }
}