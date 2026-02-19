using Microsoft.Xna.Framework;
using Stubble.Core.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.Map;
using Terraria.ModLoader;
using AncientLegacyMod.Common.Rarity;

namespace AncientLegacyMod.Content.Items.Accessories
{
    public class HuskarBlood : ModItem
    {
        public override void SetDefaults()
        {

            Item.width = 24;
            Item.height = 28;
            Item.accessory = true;
            Item.rare = ModContent.GetInstance<AccRarityStats>().Type;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            int maxLife = player.statLifeMax2;
            int currentLife = player.statLife;
            float missingHealthPercent = 0f;

            if (maxLife > 0)
            {
                missingHealthPercent = 1f - (float)currentLife / maxLife;
            }
            else
            {
                missingHealthPercent = 0f;
            }

            missingHealthPercent = MathHelper.Clamp(missingHealthPercent, 0f, 1f);

            player.GetAttackSpeed(DamageClass.Generic) += missingHealthPercent * 0.3f;
            player.GetDamage(DamageClass.Generic) += missingHealthPercent * 0.3f;
            
        }
    }
}
