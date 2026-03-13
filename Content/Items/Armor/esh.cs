using AncientLegacyMod.Content.Items.Materials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.Map;
using Terraria.ModLoader;

namespace AncientLegacyMod.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class esh : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.rare = ItemRarityID.Orange;
            Item.defense = 16;
        }

        public override void UpdateEquip(Player player)
        {
            player.maxMinions += 2;
            player.GetDamage(DamageClass.Summon) += 0.12f;
            player.GetKnockback(DamageClass.Summon) += 0.2f;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<esb>() &&
                   legs.type == ModContent.ItemType<esl>();
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = Language.GetTextValue("Mods.AncientLegacyMod.Items.ItemSetBonus.esh");

            player.GetModPlayer<EarthPlayer>().earthSpiritSet = true;

            if (Main.rand.NextBool(30))
            {
                Dust d = Dust.NewDustDirect(player.position, player.width, player.height, DustID.Stone, 0, 0, 100, default, 0.8f);
                d.velocity.Y += 1f;
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<PoweredSteelBar>(), 12);
            recipe.AddIngredient(ModContent.ItemType<UnstableComponent>(), 8);
            recipe.AddIngredient(ItemID.StoneBlock, 120);
            recipe.AddIngredient(ItemID.DirtBlock, 210);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();

        }
    }

    public class EarthPlayer : ModPlayer
    {
        public bool earthSpiritSet;

        public override void ResetEffects()
        {
            earthSpiritSet = false;
        }

        public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (earthSpiritSet)
            {
                bool isSummonAttack = proj.minion || proj.sentry ||
                                     ProjectileID.Sets.MinionShot[proj.type] ||
                                     proj.DamageType == DamageClass.Summon ||
                                     proj.DamageType == DamageClass.SummonMeleeSpeed;

                if (isSummonAttack)
                {
                    if (Main.rand.NextBool(4))
                    {
                        int healAmount = 10;

                        Player.statLife += healAmount;
                        if (Player.statLife > Player.statLifeMax2) Player.statLife = Player.statLifeMax2;

                        Player.HealEffect(healAmount);

                        for (int i = 0; i < 5; i++)
                        {
                            Dust d = Dust.NewDustDirect(target.position, target.width, target.height, DustID.Dirt, 0, 0, 0, default, 1f);
                            d.velocity = (Player.Center - d.position) * 0.25f;
                            d.noGravity = true;
                        }
                    }
                }
            }
        }
    }
}
