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
using testMod1.Content.Buffs;
using testMod1.Content.Items.Summons;

namespace testMod1.Content.Items.Summons
{
    public class MarsSpear : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 28;
            Item.DamageType = DamageClass.Summon;
            Item.mana = 10;
            Item.width = 32;
            Item.height = 32;
            Item.useTime = 36;
            Item.useAnimation = 36;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.noMelee = true;
            Item.knockBack = 3;
            Item.value = Item.sellPrice(gold: 3);
            Item.rare = ModContent.GetInstance<SummonRarity>().Type;
            Item.UseSound = SoundID.Item44;

            Item.shoot = ModContent.ProjectileType<MarsSummon>();
            Item.buffType = ModContent.BuffType<MarsSummonBuff>();
        }

        public override bool Shoot(Player player, Terraria.DataStructures.EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            player.AddBuff(Item.buffType, 2);
            var projectile = Projectile.NewProjectileDirect(source, Main.MouseWorld, velocity, type, damage, knockback, player.whoAmI);
            projectile.originalDamage = Item.damage;
            return false;
        }


    }
}
