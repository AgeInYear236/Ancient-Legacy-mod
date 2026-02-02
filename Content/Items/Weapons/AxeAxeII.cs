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
    public class AxeAxeII : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 41;
            Item.DamageType = DamageClass.Melee;
            Item.width = 55;
            Item.height = 55;
            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 6;
            Item.value = 10000;
            Item.rare = ModContent.GetInstance<MeleeRarity2>().Type;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;

            Item.axe = 40;
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                Item.useStyle = ItemUseStyleID.Swing;
                Item.useTime = 10;
                Item.useAnimation = 10;
                Item.damage = 1;
                Item.axe = 0;
            }
            else
            {
                Item.height = 100;
                Item.width = 100;
                Item.useStyle = ItemUseStyleID.Swing;
                Item.useTime = 100;
                Item.useAnimation = 100;
                Item.damage = 40;
                Item.UseSound = SoundID.Item1;
            }
            return base.CanUseItem(player);
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (player.altFunctionUse == 2)
            {
                int twentyPercentHealth = (int)(target.lifeMax * 0.35f);

                if (target.boss == false && target.life <= twentyPercentHealth)
                {
                    NPC.HitInfo killHit = new NPC.HitInfo();
                    killHit.Damage = 99999;
                    target.StrikeNPC(killHit);
                }
                else
                {
                    player.Hurt(PlayerDeathReason.ByCustomReason($"{player.name} severely cut himself."), 500, 0);
                }
            }
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<AxeAxe>(), 1)
                .AddIngredient(ItemID.AdamantiteBar, 20)
                .AddIngredient(ItemID.SoulofMight, 5)
                .AddIngredient(ItemID.SoulofFright, 5)
                .AddTile(TileID.DemonAltar)
                .Register();
        }
    }
}
