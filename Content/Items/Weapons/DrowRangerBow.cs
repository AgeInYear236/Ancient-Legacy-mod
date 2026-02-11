using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using testMod1.Common.Rarity;

namespace testMod1.Content.Items.Weapons
{
    public class DrowRangerBow : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 33;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 20;
            Item.height = 40;
            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 2f;
            Item.value = Item.buyPrice(gold: 3);
            Item.rare = ModContent.GetInstance<RangedRarity2>().Type;
            Item.UseSound = SoundID.Item5;
            Item.autoReuse = true;
            Item.shoot = ProjectileID.PurificationPowder;
            Item.shootSpeed = 10f;
            Item.useAmmo = AmmoID.Arrow;
            Item.scale = 1.6f;
        }

        public override Vector2? HoldoutOffset() => new Vector2(-8, 0);

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (type == ProjectileID.WoodenArrowFriendly)
            {
                type = ProjectileID.FrostburnArrow;
            }
        }
    }

    public class SlowProjectileLogic : GlobalProjectile
    {
        public override bool InstancePerEntity => true;
        public bool causesSlow = false;

        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            if (source is EntitySource_ItemUse itemSource && itemSource.Item.type == ModContent.ItemType<DrowRangerBow>())
            {
                causesSlow = true;
            }
        }

        public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (causesSlow)
            {
                target.AddBuff(BuffID.Slow, 240);

                for (int i = 0; i < 10; i++)
                {
                    Dust d = Dust.NewDustDirect(target.position, target.width, target.height, DustID.IceTorch);
                    d.noGravity = true;
                    d.velocity *= 1.5f;
                }
            }
        }
    }
}