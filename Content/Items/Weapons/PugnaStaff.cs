using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using testMod1.Common.Rarity;
using testMod1.Content.Items.Projectiles;

namespace testMod1.Content.Items.Weapons
{
    public class PugnaStaff : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 52;
            Item.DamageType = DamageClass.Magic;
            Item.mana = 27;
            Item.width = 40;
            Item.height = 40;
            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.noMelee = true;
            Item.value = Item.buyPrice(gold: 5);
            Item.rare = ModContent.GetInstance<MagicRarity2>().Type;
            Item.UseSound = SoundID.Item20;
            Item.shoot = ModContent.ProjectileType<PugnaBlastProjectile>();
            Item.shootSpeed = 12f;
        }

        public override Vector2? HoldoutOffset() => new Vector2(8, -4);

        public override void HoldItem(Player player)
        {
            if (Main.myPlayer == player.whoAmI)
            {
                player.ChangeDir(Main.MouseWorld.X < player.Center.X ? -1 : 1);
            }

            Vector2 dir = (Main.MouseWorld - player.MountedCenter).SafeNormalize(Vector2.UnitX);

            player.itemRotation = dir.ToRotation() + MathHelper.PiOver4;

            if (player.direction == -1)
            {
                player.itemRotation += MathHelper.PiOver2;
            }
        }

        public override bool AltFunctionUse(Player player) => true;

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                Item.shoot = ModContent.ProjectileType<PugnaLifeDrainProjectile>();
                Item.useTime = 5;
                Item.useAnimation = 5;
                Item.mana = 2;
                Item.autoReuse = true;
            }
            else
            {
                Item.shoot = ModContent.ProjectileType<PugnaBlastProjectile>();
                Item.useTime = 25;
                Item.useAnimation = 25;
                Item.mana = 25;
            }
            return base.CanUseItem(player);
        }
    }
}
