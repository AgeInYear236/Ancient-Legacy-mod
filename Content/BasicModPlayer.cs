using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.UI;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.IO;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using testMod1.Content.Items.Accessories;
using testMod1.Content.Items.Armor;
using testMod1.Content.Items.Projectiles;
using static System.Net.Mime.MediaTypeNames;

namespace testMod1.Content
{
    public class BasicModPlayer : ModPlayer   
    {
        public bool extraSlotUnlocked = false;

        public override void SaveData(TagCompound tag)
        {
            tag["extraSlotUnlocked"] = extraSlotUnlocked;
        }

        public override void LoadData(TagCompound tag)
        {
            extraSlotUnlocked = tag.GetBool("extraSlotUnlocked");
        }

        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if (testMod1.armletKeybind.JustPressed)
            {
                if (Armlet.isActive == false)
                {
                    Armlet.isActive = true;
                }
                else
                {
                    Armlet.isActive = false;
                }
            }

            if (testMod1.ta1Keybind.JustPressed)
            {
                TAShield.isActive1 = true;
            }
            if (testMod1.ta1Keybind.JustReleased)
            {
                TAShield.isActive1 = false;
            }

            // ! ! !
            if (testMod1.ta2Keybind.JustPressed)  {
                TAShield.isActive2 = true;
            }
            if (testMod1.ta2Keybind.JustReleased) {
                TAShield.isActive2 = false;
            }
            if (testMod1.bmKeybind.JustPressed)
            {
                BladeMail.isActive = true;
            }
            if (testMod1.bmKeybind.JustReleased)
            {
                BladeMail.isActive = false;
            }

            if (testMod1.bkbKeybind.JustPressed)
            {
                BlackKingBar.isActive = true;
            }
            if (testMod1.bkbKeybind.JustReleased)
            {
                BlackKingBar.isActive = false;
            }

            if (testMod1.chronoKeybind.JustPressed)
            {
                Chronosphere.isActive = true;
            }
            if (testMod1.chronoKeybind.JustReleased)
            {
                Chronosphere.isActive = false;
            }
        }
    }
}
