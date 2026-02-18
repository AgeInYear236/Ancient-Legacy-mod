using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace testMod1.Common.Systems
{
    public class AcornAmmoLogic : GlobalItem
    {
        public override void SetDefaults(Item item)
        {
            if (item.type == ItemID.Acorn)
            {
                item.ammo = ItemID.Acorn;
                item.notAmmo = false;
            }
        }
    }
}
