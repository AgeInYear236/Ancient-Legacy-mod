using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AncientLegacyMod.Common.Rarity;
using AncientLegacyMod.Content.Buffs;

namespace AncientLegacyMod.Content.Items.Accessories
{
    public class GhostScepter : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 30;
            Item.accessory = true;
            Item.rare = ModContent.GetInstance<AccRarityAura>().Type;
            Item.value = Item.buyPrice(gold: 5);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddBuff(ModContent.BuffType<GhostScepterBuff>(), 2);

            player.GetModPlayer<GhostScepterPlayer>().auraActive = true;
        }
    }
}
