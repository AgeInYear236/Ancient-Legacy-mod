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

namespace testMod1.Content.Items.Accessories
{
    public class MoonShard  : ModItem
    {
        public override void SetDefaults()
        {
            Item.material = true;
            Item.width = 32;
            Item.height = 32;
            Item.accessory = true;
            Item.rare = ModContent.GetInstance<AccRarityStats>().Type;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            //player.GetModPlayer<modPlayer1>().hasZoomAccessory = true;
            player.GetAttackSpeed(DamageClass.Melee) += 0.2f;
            player.GetAttackSpeed(DamageClass.Ranged) += 0.2f;
        }
    }
}
