using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using testMod1.Common.Rarity;

namespace testMod1.Content.Items.Materials
{
    public class Madstone : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.maxStack = 999;
            Item.material = true;
            Item.value = 10;
            Item.rare = ModContent.GetInstance<MaterialRarity1>().Type;

        }
    }
}
