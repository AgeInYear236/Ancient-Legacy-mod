using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace AncientLegacyMod.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Back)]
    public class AbaCape : ModItem
    {

        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 24;
            Item.accessory = true;
            Item.vanity = true;
            Item.rare = ItemRarityID.Purple;
            Item.value = Item.sellPrice(0, 2, 0, 0);
        }

        public override void UpdateVanity(Player player)
        {
            player.back = (sbyte)Item.backSlot;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!hideVisual)
            {
                player.back = (sbyte)Item.backSlot;
            }
        }
    }
}