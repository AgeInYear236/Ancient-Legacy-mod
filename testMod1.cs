using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.GameContent.UI;
using Terraria.ModLoader;
using testMod1.Common.Systems;
using testMod1.Content.Items.Armor;
using testMod1.Content.Items.Misc;

namespace testMod1
{
	public class testMod1 : Mod
	{
        public static int goldCurrencyId;
        public static ModKeybind armletKeybind;
        public static ModKeybind ta1Keybind;
        public static ModKeybind ta2Keybind;
        public static ModKeybind bmKeybind;
        public static ModKeybind bkbKeybind;
        public static ModKeybind chronoKeybind;
        public static ModKeybind timelapseKeybind;


        public override void Load()
        {
            goldCurrencyId = CustomCurrencyManager.RegisterCurrency(new GoldCurrencySystem(ModContent.ItemType<GoldBag>(), 999L));
            armletKeybind = KeybindLoader.RegisterKeybind(this, "Toggle Armlet", "L");
            ta1Keybind = KeybindLoader.RegisterKeybind(this, "Activate Refraction", "K");
            ta2Keybind = KeybindLoader.RegisterKeybind(this, "Activate Meld", "J");
            bmKeybind = KeybindLoader.RegisterKeybind(this, "Activate Blade Mail", "P");
            bkbKeybind = KeybindLoader.RegisterKeybind(this, "Activate Black King Bar", "O");
            chronoKeybind = KeybindLoader.RegisterKeybind(this, "Activate Chronosphere", "I");
            timelapseKeybind = KeybindLoader.RegisterKeybind(this, "Use Time Lapse", "U");

        }

        public override void Unload()
        {
            armletKeybind = null;
            ta1Keybind = null;
            ta2Keybind = null;
            bmKeybind = null;
            bkbKeybind = null;
            chronoKeybind = null;
            timelapseKeybind = null;
        }
    }
}
