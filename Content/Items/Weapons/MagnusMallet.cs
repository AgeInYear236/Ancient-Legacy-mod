using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using testMod1.Content.Buffs;
using testMod1.Content.Items.Projectiles;

namespace testMod1.Content.Items.Weapons
{
    public class MagnusMallet : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 30;
            Item.DamageType = DamageClass.Melee;
            Item.width = 40;
            Item.height = 40;
            Item.useTime = 50;
            Item.useAnimation = 50;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 3;
            Item.value = Item.buyPrice(gold: 1);
            Item.rare = ItemRarityID.Orange;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
        }

        public override bool AltFunctionUse(Player player) => true;

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                if (player.HasBuff(ModContent.BuffType<MagnusCooldown>()))
                {
                    return false;
                }

                Item.useStyle = ItemUseStyleID.HoldUp;
                Item.useTime = 100;
                Item.useAnimation = 100;
                Item.damage = 70;
                Item.shoot = ModContent.ProjectileType<MagnusPullProjectile>();
                player.AddBuff(ModContent.BuffType<MagnusCooldown>(), 1500);
            }
            else
            {
                Item.useStyle = ItemUseStyleID.Swing;
                Item.useTime = 50;
                Item.useAnimation = 50;
                Item.damage = 30;
                Item.shoot = ProjectileID.None;
            }
            return base.CanUseItem(player);
        }

        public override void UpdateInventory(Player player)
        {
            if (player.itemAnimation == 0)
            {
                Item.useStyle = ItemUseStyleID.Swing;
            }
        }
    }
}
