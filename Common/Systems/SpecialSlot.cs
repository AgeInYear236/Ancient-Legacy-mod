using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using testMod1.Content;
using testMod1.Content.Items.Accessories;

namespace testMod1.Common.Systems
{
    public class SpecialSlot : ModAccessorySlot
    {
        public override string DyeTexture => null;
        public override string VanityTexture => null;
        public override string FunctionalTexture => "testMod1/Content/Misc/AccessorySlot.png";

        public override bool IsVisibleWhenNotEnabled()
        {
            return false;
        }
        public override void OnMouseHover(AccessorySlotType context)
        {
            Main.hoverItemName = "Aura Accessory"; 

            if (context == AccessorySlotType.FunctionalSlot)
            {
                Main.instance.MouseText(Language.GetTextValue("Mods.testMod1.Content.SpecialSlotContext"));
            }
        }
        public override bool IsEnabled() => Player.GetModPlayer<BasicModPlayer>().extraSlotUnlocked;


        public override bool CanAcceptItem(Item item, AccessorySlotType context)
        {
            if (item.type == ModContent.ItemType<GhostScepter>())
            {
                return true;
            }
            if (item.type == ModContent.ItemType<RadianceAcc>())
            {
                return true;
            }
            if (item.type == ModContent.ItemType<Chronosphere>())
            {
                return true;
            }

            return false; 
        }

        public override Vector2? CustomLocation => null;
    }
}
