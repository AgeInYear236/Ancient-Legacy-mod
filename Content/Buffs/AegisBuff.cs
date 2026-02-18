using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace testMod1.Content.Buffs
{
    public class AegisBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = false; 
            Main.debuff[Type] = false;
        }
    }
}
