using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AncientLegacyMod.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class AbaHead : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.rare = ItemRarityID.Purple;
            Item.defense = 10;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetCritChance(DamageClass.Magic) += 12;
            player.GetAttackSpeed(DamageClass.Magic) += 0.10f;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return head.type == ModContent.ItemType<AbaHead>() &&
                   body.type == ModContent.ItemType<AbaArmor>() &&
                   legs.type == ModContent.ItemType<AbaLegs>();
        }

        public override void UpdateArmorSet(Player player)
        {
            //player.setBonus = "Запретное Искусство: +100 макс. ХП\n" +
                             // "Расход маны снижен на 80%, но атаки тратят ХП\n" +
                              //"10% шанс выпустить снаряд Аба, восстанавливающий здоровье";
            player.setBonus = Language.GetTextValue("Mods.AncientLegacyMod.Items.ItemSetBonus.AbaHead");


            player.statLifeMax2 += 100;
            player.manaCost -= 0.80f;

            if (player.itemAnimation > 0 && player.HeldItem.DamageType == DamageClass.Magic)
            {
                if (player.itemAnimation == player.itemAnimationMax - 1)
                {

                    int healthCost = 20;
                    player.statLife -= healthCost;
                    if (player.statLife <= 0)
                    {
                        player.KillMe(PlayerDeathReason.ByCustomReason(player.name + Language.GetTextValue("Mods.AncientLegacyMod.Death.Aba")), healthCost, 0);
                        return;
                    }

                    if (Main.myPlayer == player.whoAmI && Main.rand.NextFloat() < 0.10f)
                    {
                        Vector2 velocity = (Main.MouseWorld - player.Center).SafeNormalize(Vector2.UnitX) * 10f;
                        Projectile.NewProjectile(
                            player.GetSource_FromThis(),
                            player.Center,
                            velocity,
                            ModContent.ProjectileType<Projectiles.AbaArmorProjectile>(),
                            100,
                            2f,
                            player.whoAmI
                        );
                    }
                }
            }
        }
    }
}