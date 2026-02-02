using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using testMod1.Common.Rarity;
using testMod1.Content.Buffs;

namespace testMod1.Content.Items.Accessories
{
    public class BlackKingBar : ModItem
    {
        public static bool isActive = false;
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.accessory = true;
            Item.rare = ModContent.GetInstance<MiscRarity>().Type;
            Item.value = 10000;
            Item.expert = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (isActive && !player.HasBuff(ModContent.BuffType<BlackKingBarCooldownBuff>()))
            {
                player.AddBuff(ModContent.BuffType<BlackKingBarBuff>(), 7 * 60);
            }
        }
    }

    public class BKBPlayer : ModPlayer
    {
        public override void PreUpdate()
        {
            if (Player.HasBuff(ModContent.BuffType<BlackKingBarBuff>()))
            {
                for (int i = 0; i < Player.MaxBuffs; i++)
                {
                    int currentBuffType = Player.buffType[i];

                    if (currentBuffType > 0 && Main.debuff[currentBuffType])
                    {
                        if (currentBuffType != ModContent.BuffType<BlackKingBarCooldownBuff>() &&
                            !BuffID.Sets.NurseCannotRemoveDebuff[currentBuffType])
                        {
                            Player.DelBuff(i);
                            i--;
                        }
                    }
                }

                if (Player.buffTime[Player.FindBuffIndex(ModContent.BuffType<BlackKingBarBuff>())] == 420)
                {

                    if (!Player.HasBuff(ModContent.BuffType<BlackKingBarCooldownBuff>()))
                    {
                        Player.AddBuff(ModContent.BuffType<BlackKingBarCooldownBuff>(), 90 * 60);
                    }

                    Terraria.Audio.SoundEngine.PlaySound(SoundID.Item4, Player.Center);
                    for (int i = 0; i < 30; i++)
                    {
                        Dust d = Dust.NewDustDirect(Player.position, Player.width, Player.height, DustID.GoldCoin, 0f, 0f, 100, default, 2f);
                        d.noGravity = true;
                        d.velocity *= 3f;
                    }
                }
            }
        }
    }
}
