using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace testMod1.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    public class AbaLegs : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(0, 4, 0, 0);
            Item.rare = ItemRarityID.Purple;
            Item.defense = 14;
        }

        public override void UpdateEquip(Player player)
        {
            player.moveSpeed += 0.15f; 
            player.GetDamage(DamageClass.Magic) += 0.10f;

            player.onHitDodge = true; 

        }
    }
}