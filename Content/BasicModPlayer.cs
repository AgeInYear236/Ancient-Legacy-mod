using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.UI;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.IO;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using AncientLegacyMod.Content.Buffs;
using AncientLegacyMod.Content.Items.Accessories;
using AncientLegacyMod.Content.Items.Armor;
using AncientLegacyMod.Content.Items.Misc;
using AncientLegacyMod.Content.Items.Projectiles;
using static System.Net.Mime.MediaTypeNames;

namespace AncientLegacyMod.Content
{
    public class BasicModPlayer : ModPlayer   
    {
        public bool extraSlotUnlocked = false;

        public bool hasArmlet, hasTAShield, hasBladeMail, hasBKB, hasChronosphere, hasTimelapse;

        public bool armletActive = false;
        public int armletLifeDrainTimer = 0;
        public int shieldCharges = 0;
        public bool isAmbushActive = false;
        public int ambushTimer = 0;
        private const int MaxAmbushTimer = 600;

        // History Timelapse
        private struct PlayerState { public Vector2 Position; public int Health, Mana; }
        private Queue<PlayerState> history = new Queue<PlayerState>();

        public override void ResetEffects()
        {
            hasArmlet = hasTAShield = hasBladeMail = hasBKB = hasChronosphere = hasTimelapse = false;
        }
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
            // ARMLET
            if (AncientLegacyMod.armletKeybind.JustPressed && hasArmlet)
            {
                armletActive = !armletActive;
                Terraria.Audio.SoundEngine.PlaySound(SoundID.Item44, Player.Center);
            }

            // TA SHIELD 1 (Refraction)
            if (AncientLegacyMod.ta1Keybind.JustPressed && hasTAShield && !Player.HasBuff(ModContent.BuffType<TA1CooldownBuff>()))
            {
                shieldCharges = 5;
                Player.AddBuff(ModContent.BuffType<TA1Buff>(), 30 * 60);
                Player.AddBuff(ModContent.BuffType<TA1CooldownBuff>(), 60 * 60);
                Terraria.Audio.SoundEngine.PlaySound(SoundID.Item37, Player.Center);
            }

            // TA SHIELD 2 (Meld)
            if (AncientLegacyMod.ta2Keybind.JustPressed && hasTAShield && !Player.HasBuff(ModContent.BuffType<TA2Buff>()) && !Player.HasBuff(ModContent.BuffType<TA2CooldownBuff>()))
            {
                isAmbushActive = true;
                ambushTimer = 0;
                Player.AddBuff(ModContent.BuffType<TA2Buff>(), 11 * 60);
            }

            // BLADE MAIL
            if (AncientLegacyMod.bmKeybind.JustPressed && hasBladeMail && !Player.HasBuff(ModContent.BuffType<BMCooldownBuff>()))
            {
                Player.AddBuff(ModContent.BuffType<BMBuff>(), 8 * 60);
                Player.AddBuff(ModContent.BuffType<BMCooldownBuff>(), 20 * 60);
            }


            // BKB
            if (AncientLegacyMod.bkbKeybind.JustPressed && hasBKB && !Player.HasBuff(ModContent.BuffType<BlackKingBarCooldownBuff>()))
            {
                Player.AddBuff(ModContent.BuffType<BlackKingBarBuff>(), 7 * 60);
                Player.AddBuff(ModContent.BuffType<BlackKingBarCooldownBuff>(), 90 * 60);
                Terraria.Audio.SoundEngine.PlaySound(SoundID.Item4, Player.Center);
                SpawnBKBVisuals();
            }

            // CHRONOSPHERE
            if (AncientLegacyMod.chronoKeybind.JustPressed && hasChronosphere && !Player.HasBuff(ModContent.BuffType<ChronosphereCooldownBuff>()))
            {
                Player.AddBuff(ModContent.BuffType<ChronosphereBuff>(), 10 * 60);
                Player.AddBuff(ModContent.BuffType<ChronosphereCooldownBuff>(), 120 * 60);
                if (Player.whoAmI == Main.myPlayer)
                    Projectile.NewProjectile(Player.GetSource_Accessory(Player.HeldItem), Player.Center, Vector2.Zero, ModContent.ProjectileType<ChronosphereProjectile>(), 0, 0f, Player.whoAmI);
                Terraria.Audio.SoundEngine.PlaySound(SoundID.Item117, Player.Center);
            }

            // TIMELAPSE
            if (AncientLegacyMod.timelapseKeybind.JustPressed && hasTimelapse && !Player.HasBuff(ModContent.BuffType<TimelapseCooldownBuff>()))
            {
                RewindTime();
            }
        }

        public override void PostUpdate()
        {
            if (Player.HasBuff(ModContent.BuffType<AegisBuff>()))
            {
                if (Main.rand.NextBool(5))
                {
                    Dust d = Dust.NewDustDirect(Player.position, Player.width, Player.height, DustID.GoldCoin, 0f, 0f, 150, default, 1.2f);
                    d.noGravity = true;
                    d.velocity *= 0.5f;
                }
            }
        }

        public override void PostUpdateEquips()
        {
            if (!hasArmlet)
            {
                armletActive = false;
            }

            // Armlet
            if (armletActive)
            {
                Player.AddBuff(ModContent.BuffType<ArmletBuff>(), 2);
                float dmg = (Player.statLife < Player.statLifeMax2 / 2) ? 0.40f : 0.20f;
                int def = (Player.statLife < Player.statLifeMax2 / 2) ? 20 : 5;
                Player.GetDamage(DamageClass.Generic) += dmg;
                Player.statDefense += def;
                if (++armletLifeDrainTimer >= 20)
                {
                    if (Player.statLife > 20) Player.statLife -= 8;
                    armletLifeDrainTimer = 0;
                }
            }

            // BKB
            if (Player.HasBuff(ModContent.BuffType<BlackKingBarBuff>()))
            {
                for (int i = 0; i < Player.MaxBuffs; i++)
                {
                    int type = Player.buffType[i];
                    if (type > 0 && Main.debuff[type] && type != ModContent.BuffType<BlackKingBarCooldownBuff>() && !BuffID.Sets.NurseCannotRemoveDebuff[type])
                    {
                        Player.DelBuff(i--);
                    }
                }
            }

            //Timelapse
            if (hasTimelapse)
            {
                if (Main.GameUpdateCount % 5 == 0)
                {
                    history.Enqueue(new PlayerState { Position = Player.position, Health = Player.statLife, Mana = Player.statMana });
                    if (history.Count > 60) history.Dequeue();
                }
            }
            else history.Clear();
        }

        public override void PreUpdate()
        {
            // Meld (TA Shield 2)
            if (isAmbushActive)
            {
                if (!Player.HasBuff(ModContent.BuffType<TA2Buff>())) { BreakAmbush(); return; }
                if (ambushTimer < MaxAmbushTimer) ambushTimer++;
                if (ambushTimer > 10 && Player.velocity.Length() > 0.5f) BreakAmbush();
            }
        }

        public override void ModifyWeaponDamage(Item item, ref StatModifier damage)
        {
            if (shieldCharges > 0) damage.Flat += 30f;
            if (isAmbushActive && ambushTimer > 0) damage *= (1f + (ambushTimer / (float)MaxAmbushTimer) * 5f);
        }

        public override void ModifyHitByNPC(NPC npc, ref Player.HurtModifiers modifiers) { if (shieldCharges > 0) BlockDamage(ref modifiers); }
        public override void ModifyHitByProjectile(Projectile proj, ref Player.HurtModifiers modifiers) { if (shieldCharges > 0) BlockDamage(ref modifiers); }

        private void BlockDamage(ref Player.HurtModifiers modifiers)
        {
            shieldCharges--;
            modifiers.FinalDamage *= 0f;
            modifiers.Knockback *= 0f;
            Player.immune = true;
            Player.immuneTime = 60;
            Terraria.Audio.SoundEngine.PlaySound(SoundID.Item37, Player.Center);
            if (shieldCharges <= 0) Player.ClearBuff(ModContent.BuffType<TA1Buff>());
        }

        private void BreakAmbush() { isAmbushActive = false; ambushTimer = 0; Player.ClearBuff(ModContent.BuffType<TA2Buff>()); Player.AddBuff(ModContent.BuffType<TA2CooldownBuff>(), 30 * 60); }

        public override void OnHitNPCWithItem(Item item, NPC target, NPC.HitInfo hit, int damageDone) { if (isAmbushActive) BreakAmbush(); }
        public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone) { if (isAmbushActive) BreakAmbush(); }

        private void RewindTime()
        {
            if (history.Count > 0)
            {
                var past = history.Peek();
                Player.position = past.Position; Player.statLife = past.Health; Player.statMana = past.Mana;
                history.Clear();
                Player.AddBuff(ModContent.BuffType<TimelapseCooldownBuff>(), 120 * 60);
                Terraria.Audio.SoundEngine.PlaySound(SoundID.Item4, Player.Center);
            }
        }

        private void SpawnBKBVisuals()
        {
            for (int i = 0; i < 30; i++)
            {
                Dust d = Dust.NewDustDirect(Player.position, Player.width, Player.height, DustID.GoldCoin, 0f, 0f, 100, default, 2f);
                d.noGravity = true; d.velocity *= 3f;
            }
        }

        public override void DrawEffects(PlayerDrawSet drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
        {
            if (shieldCharges > 0 && !Player.dead)
            {
                for (int i = 0; i < shieldCharges * 2; i++)
                {
                    Dust d = Dust.NewDustPerfect(Player.Center + new Vector2(45, 0).RotatedBy(Main.rand.NextFloat(0, MathHelper.TwoPi)), DustID.Pixie, Vector2.Zero, 150, new Color(255, 0, 127), 1.2f);
                    d.noGravity = true; d.velocity = Player.velocity * 0.5f;
                }
            }
        }

        public override void OnHitByNPC(NPC npc, Player.HurtInfo hurtInfo)
        {
            if (Player.dead || hurtInfo.Damage <= 0) return;

            if (hasBladeMail)
            {
                int passiveThorns = (int)(hurtInfo.Damage * 0.3f);
                passiveThorns = Math.Max(passiveThorns, 1);

                npc.SimpleStrikeNPC(passiveThorns, 0);
            }

            if (hasBladeMail && Player.HasBuff(ModContent.BuffType<BMBuff>()))
            {
                int activeThorns = (int)(hurtInfo.Damage * 0.9f);

                int pureCompensation = (int)(npc.defense * 0.5f);

                int finalReturn = Math.Max(activeThorns + pureCompensation, 1);

                npc.SimpleStrikeNPC(finalReturn, 0);

                for (int i = 0; i < 5; i++)
                {
                    Dust.NewDust(npc.position, npc.width, npc.height, DustID.Blood, 0, 0, 100, default, 1f);
                }
            }
        }

        public override void OnHitByProjectile(Projectile proj, Player.HurtInfo hurtInfo)
        {
            if (proj.owner >= 0)
            {
                OnHitByNPC(Main.npc[proj.owner], hurtInfo);
            }
        }

        public override bool PreKill(double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            if (Player.HasBuff(ModContent.BuffType<AegisBuff>()))
            {

                Player.statLife = Player.statLifeMax2;
                Player.HealEffect(Player.statLifeMax2);

                Player.AddBuff(ModContent.BuffType<AegisCooldownBuff>(), 12 * 60 * 60);

                Player.ClearBuff(ModContent.BuffType<AegisBuff>());

                SoundEngine.PlaySound(SoundID.Item29, Player.Center);
                for (int i = 0; i < 50; i++)
                {
                    Dust d = Dust.NewDustDirect(Player.position, Player.width, Player.height, DustID.GoldCoin, 0f, 0f, 100, default, 2.5f);
                    d.noGravity = true;
                    d.velocity *= 5f;
                }

                return false;
            }
            return true;
        }
    }
}
