using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AncientLegacyMod.Content.Tiles
{
    public class PatrolBannerTile : ModTile
    {
        public override string Texture => "AncientLegacyMod/Content/Tiles/PatrolBannerTile";

        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true; 
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = true; 
            TileID.Sets.DisableSmartCursor[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style1xX);
            TileObjectData.newTile.Width = 1;
            TileObjectData.newTile.Height = 3;

            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16, 12 };
            TileObjectData.newTile.CoordinateWidth = 16;
            TileObjectData.newTile.CoordinatePadding = 2;

            TileObjectData.newTile.Origin = new Point16(0, 1);
            TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.Platform, TileObjectData.newTile.Width, 0);

            TileObjectData.addTile(Type);

            AddMapEntry(new Color(255, 142, 111), Language.GetText("Mods.AncientLegacyMod.Tiles.PatrolBannerTile.MapEntry")); 

            DustType = DustID.WoodFurniture;
        }

        public override bool PreDraw(int i, int j, SpriteBatch spriteBatch) // Sway by wind
        {
            Tile tile = Main.tile[i, j];
            Texture2D texture = ModContent.Request<Texture2D>(Texture).Value;

            float windSway = (float)Main.windSpeedCurrent * (float)Math.Sin(Main.GlobalTimeWrappedHourly * 2.0 + (i * 0.2));

            Vector2 offset = new Vector2(windSway * 1f, 4f);

            Rectangle frame = new Rectangle(tile.TileFrameX, tile.TileFrameY, 16, 16);
            Vector2 zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
            if (Main.drawToScreen)
            {
                zero = Vector2.Zero;
            }

            spriteBatch.Draw(
                texture,
                new Vector2(i * 16 - (int)Main.screenPosition.X, j * 16 - (int)Main.screenPosition.Y) + zero + offset,
                frame,
                Lighting.GetColor(i, j),
                0f,
                Vector2.Zero,
                1f,
                SpriteEffects.None,
                0f
            );

            return false;
        }
    }
}
