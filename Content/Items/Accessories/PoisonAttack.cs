using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AncientLegacyMod.Common.Rarity;
using AncientLegacyMod.Common.Systems;
using AncientLegacyMod.Content.Items.Materials;

namespace AncientLegacyMod.Content.Items.Accessories
{
    public class PoisonAttack : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.accessory = true;
            Item.rare = ModContent.GetInstance<AccRarityPas>().Type;
        }


        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<PeriodicDamagePlayer>().hasPeriodicDamageEffect = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.CursedFlame, 8);
            recipe.AddIngredient(ModContent.ItemType<Madstone>(), 50);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();

        }
    }

    public class PeriodicDamagePlayer : ModPlayer
    {
        public bool hasPeriodicDamageEffect;

        public override void ResetEffects()
        {
            hasPeriodicDamageEffect = false;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (hasPeriodicDamageEffect)
            {
                var globalNPC = target.GetGlobalNPC<PoisonGlobalNPC>();

                if (globalNPC.poisonStacks < 10)
                {
                    globalNPC.poisonStacks++;
                }

                target.AddBuff(ModContent.BuffType<Buffs.PoisonAttackBuff>(), 300);
            }
        }
    }
}
