using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using testMod1.Content.Tiles;

namespace testMod1.Content.Items.Misc
{
    public class PatrolBannerItem : ModItem
    {
        public override string Texture => "testMod1/Content/Items/Misc/PatrolBannerItem";

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 99;
        }

        public override void SetDefaults()
        {
            Item.width = 12;
            Item.height = 28;
            Item.maxStack = 9999;
            Item.consumable = true;
            Item.useTime = 10;
            Item.useAnimation = 15;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.value = 0;
            Item.rare = ItemRarityID.Quest;
            Item.createTile = ModContent.TileType<PatrolBannerTile>();
            Item.scale = 0.7f;
        }

    }
}
