using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using testMod1.Content.Items.Projectiles;

namespace testMod1.Content.Items.Weapons
{
    public class HuskarSpear : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 66;
            Item.DamageType = DamageClass.Melee;
            Item.width = 40;
            Item.height = 40;
            Item.useTime = 45;
            Item.useAnimation = 45;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 6;
            Item.value = Item.buyPrice(0, 10, 0, 0);
            Item.rare = ItemRarityID.Expert;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;

            Item.noUseGraphic = true;
            Item.noMelee = true;
            Item.shoot = ModContent.ProjectileType<HuskarSpearProjectile>();
            Item.shootSpeed = 15f;
        }

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }

        public override bool? UseItem(Player player)
        {
            
            int maxLife = player.statLifeMax2;
            int currentLife = player.statLife;
            float missingHealthPercent = 0f;

            if (maxLife > 0)
            {
                missingHealthPercent = 1f - (float)currentLife / maxLife;
            }
            else
            {
                missingHealthPercent = 0f;
            }

            missingHealthPercent = MathHelper.Clamp(missingHealthPercent, 0f, 1f);

            Item.damage += (int)(missingHealthPercent * 0.9f);

            if (player.statLife > 50)
            {
                player.statLife -= 8;
            }
            return base.UseItem(player);
        }

        public override void ModifyHitNPC(Player player, NPC target, ref NPC.HitModifiers modifiers)
        {
            target.AddBuff(BuffID.OnFire, 100);
            base.ModifyHitNPC(player, target, ref modifiers);
        }


}
}
