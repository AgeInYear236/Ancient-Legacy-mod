using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace testMod1.Content.Items.Accessories
{
    public class Desolator2 : ModItem
    {
        public int ar = 3;
        public override void SetDefaults()
        {

            Item.width = 32;
            Item.height = 32;
            Item.accessory = true;
            Item.rare = ItemRarityID.Red;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<DesolatorPlayer>().hasDesolator = true;
            player.GetModPlayer<DesolatorPlayer>().armorReduction = ar;
        }

        public override bool CanEquipAccessory(Player player, int slot, bool modded)
        {
            for (int i = 3; i < 10; i++)
            {
                if (player.armor[i].type == ModContent.ItemType<Desolator>())
                {
                    return false;
                }
            }
            return true;
        }
    }
}
