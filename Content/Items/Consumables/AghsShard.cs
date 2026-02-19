using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AncientLegacyMod.Content.Items.Consumables
{
    public class AghsShard : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 10;
            Item.height = 10;
            Item.scale = 0.5f;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.rare = ItemRarityID.Expert;
            Item.consumable = true;
            Item.expert = true;
        }

        public override bool CanUseItem(Player player)
        {
            return !player.GetModPlayer<BasicModPlayer>().extraSlotUnlocked;
        }

        public override bool? UseItem(Player player)
        {
            player.GetModPlayer<BasicModPlayer>().extraSlotUnlocked = true;
            Terraria.Audio.SoundEngine.PlaySound(SoundID.Item4, player.Center);
            return true;
        }
    }
}
