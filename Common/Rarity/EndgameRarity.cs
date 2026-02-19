using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace AncientLegacyMod.Common.Rarity
{
    public   class EndgameRarity : ModRarity
    {
        public override Color RarityColor
        {
            get
            {
                List<Color> colors = new List<Color> {
                    new Color(255, 182, 193), 
                    new Color(255, 218, 185), 
                    new Color(255, 250, 205), 
                    new Color(177, 255, 186), 
                    new Color(173, 216, 230), 
                    new Color(200, 190, 255), 
                    new Color(255, 190, 255)  
                };

                float speed = 1.5f;
                float time = Main.GlobalTimeWrappedHourly * speed;

                int currentIndex = (int)time % colors.Count;
                int nextIndex = (currentIndex + 1) % colors.Count;

                float progress = time % 1f;

                return Color.Lerp(colors[currentIndex], colors[nextIndex], progress);
            }
        }
    }
}
