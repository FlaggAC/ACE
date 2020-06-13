using System;
using System.Linq;

using ACE.Common.Extensions;
using ACE.DatLoader;
using ACE.Entity.Enum;
using ACE.Entity.Enum.Properties;
using ACE.Server.Entity.Actions;
using ACE.Server.Managers;
using ACE.Server.Network.GameMessages.Messages;

namespace ACE.Server.WorldObjects
{
    partial class Player
    {        
        public bool nobottle = false;

        /// <summary>
        /// A player earns XP through natural progression, ie. kills and quests completed
        /// </summary>
        /// <param name="amount">The amount of XP being added</param>
        /// <param name="xpType">The source of XP being added</param>
        /// <param name="shareable">True if this XP can be shared with Fellowship</param>
        public void EarnXP(long amount, XpType xpType, ShareType shareType = ShareType.All)
        {
            //Console.WriteLine($"{Name}.EarnXP({amount}, {sharable}, {fixedAmount})");

            // apply xp modifier
            var modifier = PropertyManager.GetDouble("xp_modifier").Item;
            var enchantment = EnchantmentManager.GetXPMod();
            long bottlesixthxp = 0;
            long distributionxp = 0;

            // checks inventory for bottles, adds to list. sorts them based on value of bottledxp
            // Counts bottles in inventory if any.
            var items = GetInventoryItemsOfWCID(777777);
            items = items.OrderBy(o => o.BottleXp).ToList();
            int bottlecount = items.Count();

            if (Level > 69)
            {
                // checks if you have a bottle or not
                if (bottlecount < 1)
                {
                    nobottle = false;
                    //Session.Network.EnqueueSend(new GameMessageSystemChat($" [DEBUG] No Bottles Found", ChatMessageType.Broadcast)); // remove broadcast for people without bottle.
                }
                else if (bottlecount > 0)
                {
                    nobottle = true;
                    //Session.Network.EnqueueSend(new GameMessageSystemChat($" [DEBUG] One or more bottles were found.", ChatMessageType.Broadcast)); // Tells player if the bottle is being detected or not.
                }

                // distributes xp to one or all bottles
                if (nobottle && xpType != XpType.Bottle)
                {
                    // Handles gained xp to bottle from enlightened characters
                    if (Enlightenment == 1)
                    {
                        if (modifier == 3)
                            bottlesixthxp = (long)Math.Round((amount * enchantment * (modifier - 1.0) / 6) / bottlecount);
                        else
                            bottlesixthxp = (long)Math.Round((amount * enchantment * (modifier - 0.3) / 6) / bottlecount);
                    }
                    else if (Enlightenment == 2)
                    {
                        if (modifier == 3)
                            bottlesixthxp = (long)Math.Round((amount * enchantment * (modifier - 1.2) / 6) / bottlecount);
                        else
                            bottlesixthxp = (long)Math.Round((amount * enchantment * (modifier - 0.4) / 6) / bottlecount);
                    }
                    else if (Enlightenment == 3)
                    {
                        if (modifier == 3)
                            bottlesixthxp = (long)Math.Round((amount * enchantment * (modifier - 1.4) / 6) / bottlecount);
                        else
                            bottlesixthxp = (long)Math.Round((amount * enchantment * (modifier - 0.5) / 6) / bottlecount);
                    }
                    else if (Enlightenment == 4)
                    {
                        if (modifier == 3)
                            bottlesixthxp = (long)Math.Round((amount * enchantment * (modifier - 1.6) / 6) / bottlecount);
                        else
                            bottlesixthxp = (long)Math.Round((amount * enchantment * (modifier - 0.6) / 6) / bottlecount);
                    }
                    else if (Enlightenment == 5)
                    {
                        if (modifier == 3)
                            bottlesixthxp = (long)Math.Round((amount * enchantment * (modifier - 1.8) / 6) / bottlecount);
                        else
                            bottlesixthxp = (long)Math.Round((amount * enchantment * (modifier - 0.7) / 6) / bottlecount);
                    }
                    else if (Enlightenment == 6)
                    {
                        if (modifier == 3)
                            bottlesixthxp = (long)Math.Round((amount * enchantment * (modifier - 2.0) / 6) / bottlecount);
                        else
                            bottlesixthxp = (long)Math.Round((amount * enchantment * (modifier - 0.8) / 6) / bottlecount);
                    }
                    else if (Enlightenment == 7)
                    {
                        if (modifier == 3)
                            bottlesixthxp = (long)Math.Round((amount * enchantment * (modifier - 2.2) / 6) / bottlecount);
                        else
                            bottlesixthxp = (long)Math.Round((amount * enchantment * (modifier - 0.9) / 6) / bottlecount);
                    }
                    else if (Enlightenment >= 8)
                    {
                        if (modifier == 3)
                            bottlesixthxp = (long)Math.Round((amount * enchantment * (modifier - 2.4) / 6) / bottlecount);
                        else
                            bottlesixthxp = (long)Math.Round((amount * enchantment * (modifier - 1.0) / 6) / bottlecount);
                    }
                    else
                        bottlesixthxp = (long)Math.Round((amount * enchantment * modifier / 6) / bottlecount);

                    // DISTRIBUTION XP DEBUG
                    if (Enlightenment == 1)
                    {
                        if (modifier == 3)
                            distributionxp = (long)Math.Round(amount * enchantment * (modifier - 1.0) / 6);
                        else
                            distributionxp = (long)Math.Round(amount * enchantment * (modifier - 0.3) / 6);
                    }
                    else if (Enlightenment == 2)
                    {
                        if (modifier == 3)
                            distributionxp = (long)Math.Round(amount * enchantment * (modifier - 1.2) / 6);
                        else
                            distributionxp = (long)Math.Round(amount * enchantment * (modifier - 0.4) / 6);
                    }
                    else if (Enlightenment == 3)
                    {
                        if (modifier == 3)
                            distributionxp = (long)Math.Round(amount * enchantment * (modifier - 1.4) / 6);
                        else
                            distributionxp = (long)Math.Round(amount * enchantment * (modifier - 0.5) / 6);
                    }
                    else if (Enlightenment == 4)
                    {
                        if (modifier == 3)
                            distributionxp = (long)Math.Round(amount * enchantment * (modifier - 1.6) / 6);
                        else
                            distributionxp = (long)Math.Round(amount * enchantment * (modifier - 0.6) / 6);
                    }
                    else if (Enlightenment == 5)
                    {
                        if (modifier == 3)
                            distributionxp = (long)Math.Round(amount * enchantment * (modifier - 1.8) / 6);
                        else
                            distributionxp = (long)Math.Round(amount * enchantment * (modifier - 0.7) / 6);
                    }
                    else if (Enlightenment == 6)
                    {
                        if (modifier == 3)
                            distributionxp = (long)Math.Round(amount * enchantment * (modifier - 2.0) / 6);
                        else
                            distributionxp = (long)Math.Round(amount * enchantment * (modifier - 0.8) / 6);
                    }
                    else if (Enlightenment == 7)
                    {
                        if (modifier == 3)
                            distributionxp = (long)Math.Round(amount * enchantment * (modifier - 2.2) / 6);
                        else
                            distributionxp = (long)Math.Round(amount * enchantment * (modifier - 0.9) / 6);
                    }
                    else if (Enlightenment >= 8)
                    {
                        if (modifier == 3)
                            distributionxp = (long)Math.Round(amount * enchantment * (modifier - 2.4) / 6);
                        else
                            distributionxp = (long)Math.Round(amount * enchantment * (modifier - 1.0) / 6);
                    }
                    else
                        distributionxp = (long)Math.Round(amount * enchantment * modifier / 6);


                    //Session.Network.EnqueueSend(new GameMessageSystemChat($" [DEBUG] Distributing {distributionxp:N0} XP to {bottlecount} bottle(s).", ChatMessageType.Broadcast));

                    foreach (var item in items)
                    {
                        if (item.BottleXp >= 15000000000) // if bottle is full doesnt add more xp to bottle.
                        {
                            item.BottleXp = 15000000000;
                            item.LongDesc = $"This bottle has {item.BottleXp:N0}/15,000,000,000 experience points stored.";
                            Session.Network.EnqueueSend(new GameMessageSystemChat($" [Bottle] One of your bottles is full!", ChatMessageType.Broadcast));
                        }
                        else
                        {
                            item.BottleXp = (long)Math.Round((double)item.BottleXp + bottlesixthxp);  // adds to current bottle xp and amount that was divided to total and also divides the xp evenly into bottles per bottle.                        


                            if (item.BottleXp >= 15000000000) // secondary check when added xp goes over 450M after xp is added
                            {
                                item.BottleXp = 15000000000;
                                Session.Network.EnqueueSend(new GameMessageSystemChat($" [Bottle] You fill an XP bottle.", ChatMessageType.Broadcast));
                            }

                            item.LongDesc = $"This bottle has {item.BottleXp:N0}/15,000,000,000 experience points stored.";

                            //if (item.BottleXp < 1000000000 && bottlecount < 2) // only adds debug added xp msg if bottle is still not full.
                           //     Session.Network.EnqueueSend(new GameMessageSystemChat($" [DEBUG] Added {bottlesixthxp:N0} to your Bottle of XP. ", ChatMessageType.Broadcast));
                          //  else
                          //      Session.Network.EnqueueSend(new GameMessageSystemChat($" [DEBUG] Added {bottlesixthxp:N0} to your Bottle of XP. ", ChatMessageType.Broadcast));
                        }
                    }// handles adding xp to bottles and updating the ID info.
                }
            }// handles xp bottles for players level 70 and higher.

            var m_amount = (long)Math.Round(amount * enchantment * modifier);

            if (nobottle)
                m_amount = (long)Math.Round((amount * enchantment * modifier) - (bottlesixthxp * bottlecount)); // reduced earned xp by 1/6th xp because its going into bottle instead of yourself per bottle.

            //TO DO apply reduction to enlightened players as well? OR leave that as it is since they earn less xp anyway.

            // Values are based on retail // 1.0 = 100% -- PotatoAC's normal xp rate is 1.5 which is 150%. DXP for PotatoAC is 3.0 or 300%
            if (Enlightenment == 1)
                {
                if (modifier == 3)
                {
                    m_amount = (long)Math.Round(amount * enchantment * (modifier - 1.0)); // DXP 200%
                }
                else
                {
                    m_amount = (long)Math.Round(amount * enchantment * (modifier - 0.3)); // N 120%
                }
            }// N 120% DXP 200%
            else if (Enlightenment == 2)
            {
                if (modifier == 3)
                {
                    m_amount = (long)Math.Round(amount * enchantment * (modifier - 1.2)); // DXP 180%
                }
                else
                {
                    m_amount = (long)Math.Round(amount * enchantment * (modifier - 0.4)); // N 110%
                }
            }// N 110% DXP 180%
            else if (Enlightenment == 3)
            {
                if (modifier == 3)
                {
                    m_amount = (long)Math.Round(amount * enchantment * (modifier - 1.4)); // DXP 160%
                }
                else
                {
                    m_amount = (long)Math.Round(amount * enchantment * (modifier - 0.5)); // N 100%
                }
            }// N 100% DXP 160%
            else if (Enlightenment == 4)
            {
                if (modifier == 3)
                {
                    m_amount = (long)Math.Round(amount * enchantment * (modifier - 1.6)); // DXP 140%
                }
                else
                {
                    m_amount = (long)Math.Round(amount * enchantment * (modifier - 0.6)); // N 90%
                }
            }// N 90% DXP 140%
            else if (Enlightenment == 5)
            {
                if (modifier == 3)
                {
                    m_amount = (long)Math.Round(amount * enchantment * (modifier - 1.8)); // DXP 120%
                }
                else
                {
                    m_amount = (long)Math.Round(amount * enchantment * (modifier - 0.7)); // N 80%
                }
            }// N 80% DXP 120%
            else if (Enlightenment == 6)
            {
                if (modifier == 3)
                {
                    m_amount = (long)Math.Round(amount * enchantment * (modifier - 2.0)); // DXP 100%
                }
                else
                {
                    m_amount = (long)Math.Round(amount * enchantment * (modifier - 0.8)); // N 70%
                }
            }// N 70% DXP 100%
            else if (Enlightenment == 7)
            {
                if (modifier == 3)
                {
                    m_amount = (long)Math.Round(amount * enchantment * (modifier - 2.2)); // DXP 80%
                }
                else
                {
                    m_amount = (long)Math.Round(amount * enchantment * (modifier - 0.9)); // N 60%
                }
            }// N 60% DXP 80%
            else if (Enlightenment >= 8)
            {
                if (modifier == 3)
                {
                    m_amount = (long)Math.Round(amount * enchantment * (modifier - 2.4)); // DXP 60%
                }
                else
                {
                    m_amount = (long)Math.Round(amount * enchantment * (modifier - 1.0)); // N 50%
                }
            }// N 50% DXP 60%

            if (m_amount < 0)
            {
                log.Warn($"{Name}.EarnXP({amount}, {shareType})");
                log.Warn($"modifier: {modifier}, enchantment: {enchantment}, m_amount: {m_amount}");
                return;
            }

            GrantXP(m_amount, xpType, shareType);
        }

        /// <summary>
        /// Directly grants XP to the player, without the XP modifier
        /// </summary>
        /// <param name="amount">The amount of XP to grant to the player</param>
        /// <param name="xpType">The source of the XP being granted</param>
        /// <param name="shareable">If TRUE, this XP can be shared with fellowship members</param>
        public void GrantXP(long amount, XpType xpType, ShareType shareType = ShareType.All)
        {
            var modifier = PropertyManager.GetDouble("xp_modifier").Item;
            var enchantment = EnchantmentManager.GetXPMod();
            var items = GetInventoryItemsOfWCID(777777);
            items = items.OrderBy(o => o.Value).ToList();

            if (Fellowship != null && Fellowship.ShareXP && shareType.HasFlag(ShareType.Fellowship))
            {
                // this will divy up the XP, and re-call this function
                // with ShareType.Fellowship removed
                Fellowship.SplitXp((ulong)amount, xpType, shareType, this);
                return;
            }

            if (Enlightenment >= 1 && xpType == XpType.Fellowship)
            {
                UpdateXpAndLevel(amount / 3, xpType);
            } // make all xp gained from fellowships divided by 3 while enlightened.
            else
            {

                if (xpType == XpType.Bottle)
                {

                    // reduces xp gained from bottle while enlightened.
                    if (Enlightenment == 1)
                    {
                        amount = (long)Math.Round(amount / enchantment / 1.5);
                    }
                    else if (Enlightenment == 2)
                    {
                        amount = (long)Math.Round(amount / enchantment / 2.5);
                    }
                    else if (Enlightenment == 3)
                    {
                        amount = (long)Math.Round(amount / enchantment / 3.5);
                    }
                    else if (Enlightenment == 4)
                    {
                        amount = (long)Math.Round(amount / enchantment / 4.5);
                    }
                    else if (Enlightenment == 5)
                    {
                        amount = (long)Math.Round(amount / enchantment / 5.5);
                    }
                    else if (Enlightenment == 6)
                    {
                        amount = (long)Math.Round(amount / enchantment / 6.5);
                    }
                    else if (Enlightenment == 7)
                    {
                        amount = (long)Math.Round(amount / enchantment / 7.5);
                    }
                    else if (Enlightenment >= 8)
                    {
                        amount = (long)Math.Round(amount / enchantment / 8.5);
                    }

                    if (PKMode)
                    {
                        amount = 0;
                        Session.Network.EnqueueSend(new GameMessageSystemChat($"Well that was silly, you use your xp bottle but gained nothing because you are in PKMode.", ChatMessageType.Broadcast));
                        return;
                    }
                    else
                        UpdateXpAndLevel(amount, xpType);

                    foreach (var item in items)
                    {

                        if (item.BottleXp == 0)
                        {
                            item.BottleXp = 0;
                            item.LongDesc = $"This bottle has {item.BottleXp:N0}/15,000,000,000 experience points stored.";
                        }

                    }// ensures other bottles are not being reduces to 0 if they have xp.
                    if (Enlightenment > 0)
                        Session.Network.EnqueueSend(new GameMessageSystemChat($" You used your Bottle of Xp and gained {amount:N0} experience points! Enlightenment has reduced the amount of xp gained.", ChatMessageType.Broadcast));
                    else
                        Session.Network.EnqueueSend(new GameMessageSystemChat($" You used your Bottle of Xp and gained {amount:N0} experience points!", ChatMessageType.Broadcast));
                }// handles xp passed through using xp bottle.
                else if (xpType == XpType.PK && PKMode)
                {
                    UpdateXpAndLevel(amount, xpType);                    
                }
                else if (!PKMode)
                    UpdateXpAndLevel(amount, xpType);
            }

            // for passing XP up the allegiance chain,
            // this function is only called at the very beginning, to start the process.
            if (shareType.HasFlag(ShareType.Allegiance))
                UpdateXpAllegiance(amount);

            // only certain types of XP are granted to items
            if (xpType == XpType.Kill || xpType == XpType.Quest)
            {
                if (Enlightenment == 1)
                {
                    if (modifier == 3)
                    {
                        amount = (long)Math.Round(amount * (modifier - 2.0)); // adds missing 100% (300%)
                        GrantItemXP(amount);
                    }
                    else
                    {
                        amount = (long)Math.Round(amount * (modifier - 1.2)); // adds missing 30% (150%)
                        GrantItemXP(amount);
                    }
                }
                else if (Enlightenment == 2)
                {
                    if (modifier == 3)
                    {
                        amount = (long)Math.Round(amount * (modifier - 1.8)); // adds missing 120% (300%)
                        GrantItemXP(amount);
                    }
                    else
                    {
                        amount = (long)Math.Round(amount * (modifier - 1.1)); // adds missing 40% (150%)
                        GrantItemXP(amount);
                    }
                }
                else if (Enlightenment == 3)
                {
                    if (modifier == 3)
                    {
                        amount = (long)Math.Round(amount * (modifier - 1.6)); // adds missing 140% (300%)
                        GrantItemXP(amount);
                    }
                    else
                    {
                        amount = (long)Math.Round(amount * (modifier - 1.0)); // adds missing 50% (150%)
                        GrantItemXP(amount);
                    }
                }
                else if (Enlightenment == 4)
                {
                    if (modifier == 3)
                    {
                        amount = (long)Math.Round(amount * (modifier - 1.4)); // adds missing 160% (300%)
                        GrantItemXP(amount);
                    }
                    else
                    {
                        amount = (long)Math.Round(amount * (modifier - 0.9)); // adds missing 60% (150%)
                        GrantItemXP(amount);
                    }
                }
                else if (Enlightenment == 5)
                {
                    if (modifier == 3)
                    {
                        amount = (long)Math.Round(amount * (modifier - 1.2)); // adds missing 180% (300%)
                        GrantItemXP(amount);
                    }
                    else
                    {
                        amount = (long)Math.Round(amount * (modifier - 0.8)); // adds missing 70% (150%)
                        GrantItemXP(amount);
                    }
                }
                else if (Enlightenment == 6)
                {
                    if (modifier == 3)
                    {
                        amount = (long)Math.Round(amount * (modifier - 1.0)); // adds missing 200% (300%)
                        GrantItemXP(amount);
                    }
                    else
                    {
                        amount = (long)Math.Round(amount * (modifier - 0.7)); // adds missing 80% (150%)
                        GrantItemXP(amount);
                    }
                }
                else if (Enlightenment == 7)
                {
                    if (modifier == 3)
                    {
                        amount = (long)Math.Round(amount * (modifier - 0.8)); // adds missing 220% (300%)
                        GrantItemXP(amount);
                    }
                    else
                    {
                        amount = (long)Math.Round(amount * (modifier - 0.6)); // adds missing 90% (150%)
                        GrantItemXP(amount);
                    }
                }
                else if (Enlightenment >= 8)
                {
                    if (modifier == 3)
                    {
                        amount = (long)Math.Round(amount * (modifier - 0.6)); // adds missing 240% (300%)
                        GrantItemXP(amount);
                    }
                    else
                    {
                        amount = (long)Math.Round(amount * (modifier - 0.5)); // adds missing 100% (150%)
                        GrantItemXP(amount);
                    }
                }
                else
                {
                    GrantItemXP(amount);
                }
            }
        }

        /// <summary>
        /// Adds XP to a player's total XP, handles triggers (vitae, level up)
        /// </summary>
        private void UpdateXpAndLevel(long amount, XpType xpType)
        {

            long delevelAmount = 0;     // if deleveled, the amount DelevelXp has been reduced by

            if (DelevelXp != null)
            {
                // delevelAmount = the smaller of 2 numbers
                delevelAmount = Math.Min(amount, (long)DelevelXp);

                DelevelXp -= amount;

                // check if player has earned enough xp to exit deleveled mode
                // if so, send message to player?
                if (DelevelXp <= 0)
                    DelevelXp = null;
            }

            // until we are max level we must make sure that we send
            var xpTable = DatManager.PortalDat.XpTable;

            var maxLevel = GetMaxLevel();
            var maxLevelXp = xpTable.CharacterLevelXPList.Last();

            if (Level != maxLevel)
            {
                var addAmount = amount;

                var amountLeftToEnd = (long)maxLevelXp - TotalExperience ?? 0;
                if (amount > amountLeftToEnd)
                    addAmount = amountLeftToEnd;

                AvailableExperience += addAmount - delevelAmount;
                TotalExperience += addAmount;

                var xpTotalUpdate = new GameMessagePrivateUpdatePropertyInt64(this, PropertyInt64.TotalExperience, TotalExperience ?? 0);
                var xpAvailUpdate = new GameMessagePrivateUpdatePropertyInt64(this, PropertyInt64.AvailableExperience, AvailableExperience ?? 0);
                Session.Network.EnqueueSend(xpTotalUpdate, xpAvailUpdate);

                CheckForLevelup(addAmount, delevelAmount);
            }

            if (xpType == XpType.Kill && DelevelXp >= 0 && XpShow && amount >= 100 || xpType == XpType.Fellowship && DelevelXp >= 0 && XpShow && amount >= 100 || xpType == XpType.Allegiance && DelevelXp >= 0 && XpShow && amount >= 100
                 || xpType == XpType.Admin && DelevelXp >= 0 && XpShow && amount >= 100 || DelevelXp == null && XpShow && amount >= 100 || xpType == XpType.Quest)
            {
                if (DelevelXp == null)
                {
                    Session.Network.EnqueueSend(new GameMessageSystemChat($" Gained {amount:N0} XP.", ChatMessageType.Broadcast));
                }
                else
                {
                    Session.Network.EnqueueSend(new GameMessageSystemChat($" {DelevelXp:N0}(-{amount:N0}) Delevel XP remaining", ChatMessageType.Broadcast));

                }
            }

            if (HasVitae && xpType != XpType.Allegiance)
                UpdateXpVitae(amount);
        }

        /// <summary>
        /// Optionally passes XP up the Allegiance tree
        /// </summary>
        private void UpdateXpAllegiance(long amount)
        {
            if (!HasAllegiance) return;

            AllegianceManager.PassXP(AllegianceNode, (ulong)amount, true);
        }

        /// <summary>
        /// Handles updating the vitae penalty through earned XP
        /// </summary>
        /// <param name="amount">The amount of XP to apply to the vitae penalty</param>
        private void UpdateXpVitae(long amount)
        {
            var vitaePenalty = EnchantmentManager.GetVitae().StatModValue;
            var startPenalty = vitaePenalty;

            var maxPool = (int)VitaeCPPoolThreshold(vitaePenalty, DeathLevel.Value);
            var curPool = VitaeCpPool + amount;
            while (curPool >= maxPool)
            {
                curPool -= maxPool;
                vitaePenalty = EnchantmentManager.ReduceVitae();
                if (vitaePenalty == 1.0f)
                    break;
                maxPool = (int)VitaeCPPoolThreshold(vitaePenalty, DeathLevel.Value);
            }
            VitaeCpPool = (int)curPool;

            Session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt(this, PropertyInt.VitaeCpPool, VitaeCpPool.Value));

            if (vitaePenalty != startPenalty)
            {
                Session.Network.EnqueueSend(new GameMessageSystemChat("Your experience has reduced your Vitae penalty!", ChatMessageType.Magic));
                EnchantmentManager.SendUpdateVitae();
            }

            if (vitaePenalty.EpsilonEquals(1.0f) || vitaePenalty > 1.0f)
            {
                var actionChain = new ActionChain();
                actionChain.AddDelaySeconds(2.0f);
                actionChain.AddAction(this, () =>
                {
                    var vitae = EnchantmentManager.GetVitae();
                    if (vitae != null)
                    {
                        var curPenalty = vitae.StatModValue;
                        if (curPenalty.EpsilonEquals(1.0f) || curPenalty > 1.0f)
                            EnchantmentManager.RemoveVitae();
                    }
                });
                actionChain.EnqueueChain();
            }
        }

        /// <summary>
        /// Returns the maximum possible character level
        /// </summary>
        public static uint GetMaxLevel()
        {
            return (uint)DatManager.PortalDat.XpTable.CharacterLevelXPList.Count - 1;
        }

        public static int GetLevelForXp(ulong xp)
        {
            for (var i = DatManager.PortalDat.XpTable.CharacterLevelXPList.Count - 1; i >= 0; i--)
                if (xp >= DatManager.PortalDat.XpTable.CharacterLevelXPList[i])
                    return i;

            return 0;
        }

        /// <summary>
        /// Returns TRUE if player >= MaxLevel
        /// </summary>
        public bool IsMaxLevel => Level >= GetMaxLevel();

        /// <summary>
        /// Returns the remaining XP required to reach a level
        /// </summary>
        public long? GetRemainingXP(uint level)
        {
            var maxLevel = GetMaxLevel();
            if (level < 1 || level > maxLevel)
                return null;

            var levelTotalXP = DatManager.PortalDat.XpTable.CharacterLevelXPList[(int)level];

            return (long)levelTotalXP - TotalExperience.Value;
        }

        /// <summary>
        /// Returns the remaining XP required to the next level
        /// </summary>
        public ulong GetRemainingXP()
        {
            var maxLevel = GetMaxLevel();
            if (Level >= maxLevel)
                return 0;

            var nextLevelTotalXP = DatManager.PortalDat.XpTable.CharacterLevelXPList[Level.Value + 1];
            return nextLevelTotalXP - (ulong)TotalExperience.Value;
        }

        /// <summary>
        /// Returns the total XP required to reach a level
        /// </summary>
        public static ulong GetTotalXP(int level)
        {
            var maxLevel = GetMaxLevel();
            if (level < 0 || level > maxLevel)
                return 0;

            return DatManager.PortalDat.XpTable.CharacterLevelXPList[level];
        }

        /// <summary>
        /// Returns the total amount of XP required for a player reach max level
        /// </summary>
        public static long MaxLevelXP
        {
            get
            {
                var xpTable = DatManager.PortalDat.XpTable.CharacterLevelXPList;

                return (long)xpTable[xpTable.Count - 1];
            }
        }

        /// <summary>
        /// Returns the XP required to go from level A to level B
        /// </summary>
        public ulong GetXPBetweenLevels(int levelA, int levelB)
        {
            // special case for max level
            var maxLevel = (int)GetMaxLevel();

            levelA = Math.Clamp(levelA, 1, maxLevel - 1);
            levelB = Math.Clamp(levelB, 1, maxLevel);

            var levelA_totalXP = DatManager.PortalDat.XpTable.CharacterLevelXPList[levelA];
            var levelB_totalXP = DatManager.PortalDat.XpTable.CharacterLevelXPList[levelB];

            return levelB_totalXP - levelA_totalXP;
        }

        public ulong GetXPToNextLevel(int level)
        {
            return GetXPBetweenLevels(level, level + 1);
        }

        /// <summary>
        /// Determines if the player has advanced a level
        /// </summary>
        private void CheckForLevelup(long addAmount, long delevelAmount)
        {
            var xpTable = DatManager.PortalDat.XpTable;

            var maxLevel = GetMaxLevel();

            if (Level >= maxLevel) return;

            var startingLevel = Level;
            bool creditEarned = false;

            var currentXp = (long)TotalExperience - addAmount;
            var nextLevelStartXp = (long)xpTable.CharacterLevelXPList[(Level ?? 0) + 1];

            // increases until the correct level is found
            while (TotalExperience >= nextLevelStartXp)
            {
                var addXp = nextLevelStartXp - currentXp;
                currentXp += addXp;

                Level++;

                // increase the skill credits if the chart allows this level to grant a credit
                if (xpTable.CharacterLevelSkillCreditList[Level ?? 0] > 0 && delevelAmount == 0 && Enlightenment < 1 && !PKMode)
                {
                    AvailableSkillCredits += (int)xpTable.CharacterLevelSkillCreditList[Level ?? 0];
                    TotalSkillCredits += (int)xpTable.CharacterLevelSkillCreditList[Level ?? 0];
                    creditEarned = true;
                }

                delevelAmount -= Math.Min(delevelAmount, addXp);

                // break if we reach max
                if (Level == maxLevel)
                {
                    PlayParticleEffect(PlayScript.WeddingBliss, Guid);
                    break;
                }
                nextLevelStartXp = (long)xpTable.CharacterLevelXPList[(Level ?? 0) + 1];
            }

            if (Level > startingLevel)
            {
                if (PKMode)
                    UpdateProperty(this, PropertyInt64.AvailableExperience, 0);

                var message = (Level == maxLevel) ? $"You have reached the maximum level of {Level}!" : $"You are now level {Level}!";

                if (PKMode)
                    message = (Level == maxLevel) ? $"You have reached the maximum PKMode level of {Level}!" : $"You are now PKMode level {Level}!";

                if (!PKMode)                    
                    message += (AvailableSkillCredits > 0) ? $"\nYou have {AvailableExperience:#,###0} experience points and {AvailableSkillCredits} skill credits available to raise skills and attributes." : $"\nYou have {AvailableExperience:#,###0} experience points available to raise skills and attributes.";

                var levelUp = new GameMessagePrivateUpdatePropertyInt(this, PropertyInt.Level, Level ?? 1);
                var currentCredits = new GameMessagePrivateUpdatePropertyInt(this, PropertyInt.AvailableSkillCredits, AvailableSkillCredits ?? 0);

                if (Level != maxLevel && !creditEarned)
                {
                    var nextLevelWithCredits = 0;

                    var startLevel = (Level ?? 0) + 1;
                    if (DelevelXp != null)
                    {
                        var undeleveledTotalXp = (ulong)(TotalExperience + DelevelXp);
                        startLevel = GetLevelForXp(undeleveledTotalXp);
                    }

                    for (int i = startLevel; i <= maxLevel; i++)
                    {
                        if (xpTable.CharacterLevelSkillCreditList[i] > 0)
                        {
                            nextLevelWithCredits = i;
                            break;
                        }
                    }
                    message += $"\nYou will earn another skill credit at level {nextLevelWithCredits}.";
                }

                if (Fellowship != null)
                    Fellowship.OnFellowLevelUp(this);

                if (AllegianceNode != null)
                    AllegianceNode.OnLevelUp();

                Session.Network.EnqueueSend(levelUp);

                var ignore_burden_below_character_level = PropertyManager.GetLong("ignore_burden_below_character_level").Item;
                if (startingLevel < ignore_burden_below_character_level && Level >= ignore_burden_below_character_level)
                    Session.Network.EnqueueSend(new GameMessageSystemChat($"WARNING: Your level has reached {Level.Value} and therefore you now suffer from the effects of burden! Due to technical limitations, this effect may not accurately be applied until the next time you log in.", ChatMessageType.Broadcast));

                SetMaxVitals();

                // play level up effect
                if (PKMode)
                    PlayParticleEffect(PlayScript.AetheriaLevelUp, Guid);
                else
                    PlayParticleEffect(PlayScript.LevelUp, Guid);

                // upon leveling push to pktop list
                if (PKMode)
                    PkModeStoredLevel = Level;

                // as a player levels if they have not received nether damage resistances give them at thresholds.
                var netheresist = GetProperty(PropertyInt.DotResistRating);

                if (Level <= 99 && LowNetherResist == 1 && NetherFix)
                {
                    UpdateProperty(this, PropertyFloat.ResistNether, 0.95);
                    SetProperty(PropertyInt.DotResistRating, 5 + (int)netheresist);
                    Session.Network.EnqueueSend(new GameMessageSystemChat($"You now have {DotResistRating} passive DoT resistance rating for your level and receive less damage from direct Nether attacks.", ChatMessageType.Magic));
                    SetProperty(PropertyInt.LowNetherResist, 2);
                }
                if (Level >= 100 && Level <= 149 && MedNetherResist == 1 && NetherFix)
                {
                    UpdateProperty(this, PropertyFloat.ResistNether, 0.95);
                    SetProperty(PropertyInt.DotResistRating, 5 + (int)netheresist);
                    Session.Network.EnqueueSend(new GameMessageSystemChat($"You now have {DotResistRating} passive DoT resistance rating for your level and receive less damage from direct Nether attacks.", ChatMessageType.Magic));
                    SetProperty(PropertyInt.MedNetherResist, 2);
                }
                if (Level >= 150 && Level <= 199 && HighNetherResist == 1 && NetherFix)
                {
                    UpdateProperty(this, PropertyFloat.ResistNether, 0.85);
                    SetProperty(PropertyInt.DotResistRating, 5 + (int)netheresist);
                    Session.Network.EnqueueSend(new GameMessageSystemChat($"You now have {DotResistRating} passive DoT resistance rating for your level and receive less damage from direct Nether attacks.", ChatMessageType.Magic));
                    SetProperty(PropertyInt.HighNetherResist, 2);
                }
                if (Level >= 200 && HigherNetherResist == 1 && NetherFix)
                {
                    UpdateProperty(this, PropertyFloat.ResistNether, 0.85);
                    SetProperty(PropertyInt.DotResistRating, 15 + (int)netheresist);
                    Session.Network.EnqueueSend(new GameMessageSystemChat($"You now have {DotResistRating} passive DoT resistance rating for your level and receive less damage from direct Nether attacks.", ChatMessageType.Magic));
                    SetProperty(PropertyInt.HigherNetherResist, 2);
                }

                Session.Network.EnqueueSend(new GameMessageSystemChat(message, ChatMessageType.Advancement), currentCredits);
            }
        }

        /// <summary>
        /// Spends the amount of XP specified, deducting it from available experience
        /// </summary>
        public bool SpendXP(long amount, bool sendNetworkUpdate = true)
        {
            if (amount > AvailableExperience)
                return false;

            AvailableExperience -= amount;

            if (sendNetworkUpdate)
                Session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt64(this, PropertyInt64.AvailableExperience, AvailableExperience ?? 0));

            return true;
        }

        /// <summary>
        /// Tries to spend all of the players Xp into Attributes, Vitals and Skills
        /// </summary>
        public void SpendAllXp(bool sendNetworkUpdate = true)
        {
            SpendAllAvailableAttributeXp(Strength, sendNetworkUpdate);
            SpendAllAvailableAttributeXp(Endurance, sendNetworkUpdate);
            SpendAllAvailableAttributeXp(Coordination, sendNetworkUpdate);
            SpendAllAvailableAttributeXp(Quickness, sendNetworkUpdate);
            SpendAllAvailableAttributeXp(Focus, sendNetworkUpdate);
            SpendAllAvailableAttributeXp(Self, sendNetworkUpdate);

            SpendAllAvailableVitalXp(Health, sendNetworkUpdate);
            SpendAllAvailableVitalXp(Stamina, sendNetworkUpdate);
            SpendAllAvailableVitalXp(Mana, sendNetworkUpdate);

            foreach (var skill in Skills)
            {
                if (skill.Value.AdvancementClass >= SkillAdvancementClass.Trained)
                    SpendAllAvailableSkillXp(skill.Value, sendNetworkUpdate);
            }
        }

        /// <summary>
        /// Gives available XP of the amount specified, without increasing total XP
        /// </summary>
        public void RefundXP(long amount)
        {
            AvailableExperience += amount;

            var xpUpdate = new GameMessagePrivateUpdatePropertyInt64(this, PropertyInt64.AvailableExperience, AvailableExperience ?? 0);
            Session.Network.EnqueueSend(xpUpdate);
        }

        /*public void HandleMissingXp()
        {
            var verifyXp = GetProperty(PropertyInt64.VerifyXp) ?? 0;
            if (verifyXp == 0) return;

            var actionChain = new ActionChain();
            actionChain.AddDelaySeconds(5.0f);
            actionChain.AddAction(this, () =>
            {
                var xpType = verifyXp > 0 ? "unassigned experience" : "experience points";

                var msg = $"This character was missing some {xpType} --\nYou have gained an additional {Math.Abs(verifyXp).ToString("N0")} {xpType}!";

                Session.Network.EnqueueSend(new GameMessageSystemChat(msg, ChatMessageType.Broadcast));

                if (verifyXp < 0)
                {
                    // add to character's total XP
                    TotalExperience -= verifyXp;

                    CheckForLevelup(addAmount);
                }

                RemoveProperty(PropertyInt64.VerifyXp);
            });

            actionChain.EnqueueChain();
        }*/

        /// <summary>
        /// Returns the total amount of XP required to go from vitae to vitae + 0.01
        /// </summary>
        /// <param name="vitae">The current player life force, ie. 0.95f vitae = 5% penalty</param>
        /// <param name="level">The player DeathLevel, their level on last death</param>
        private double VitaeCPPoolThreshold(float vitae, int level)
        {
            return (Math.Pow(level, 2.5) * 2.5 + 20.0) * Math.Pow(vitae, 5.0) + 0.5;
        }

        /// <summary>
        /// Raise the available XP by a percentage of the current level XP or a maximum
        /// </summary>
        public void GrantLevelProportionalXp(double percent, long min, long max, bool shareable = false)
        {
            var nextLevelXP = GetXPBetweenLevels(Level.Value, Level.Value + 1);

            var scaledXP = (long)Math.Round(nextLevelXP * percent);

            if (max > 0)
                scaledXP = Math.Min(scaledXP, max);

            if (min > 0)
                scaledXP = Math.Max(scaledXP, min);

            var shareType = shareable ? ShareType.All : ShareType.None;

            // a simple condition to grant pk xp versus normal quest xp. 
            if (PKMode)
            {
                GrantXP(scaledXP, XpType.PK, shareType);
                AvailableExperience = 0;
            }
            else
                GrantXP(scaledXP, XpType.Quest, shareType);
        }

        // we use GrantLevelProportionalXp as a base and then factor in the lost xp and total xp assosicated with that level
        public void LoseLevelProportionalXp(double percent, long min, long max)
        {

            var nextLevelXP = GetXPBetweenLevels(Level.Value, Level.Value + 1);

            var scaledXP = (long)Math.Round(nextLevelXP * percent);

            if (max > 0)
                scaledXP = Math.Min(scaledXP, max);

            if (min > 0)
                scaledXP = Math.Max(scaledXP, min);

            var deductedTotal = TotalExperience -= scaledXP;

            var actuallevel = GetLevelForXp((ulong)deductedTotal); // gets the level for the total xp that the player currently has

            // if the total xp calculates to a level that the player is not, then reduce the level by 1 and announce the changes to player
            if (actuallevel < Level)
            {
                UpdateProperty(this, PropertyInt.DeathLevel, Level - 1);
                UpdateProperty(this, PropertyInt.Level, Level - 1);
                UpdateProperty(this, PropertyInt.PkModeStoredLevel, Level); // if downleveled push new level to pk top list
                Session.Network.EnqueueSend(new GameMessageSystemChat($"You lost {scaledXP:N0} XP and have also lost a level, you are now {Level}.", ChatMessageType.Magic));
            }

            // this simply just updates a players total xp to reflect the % loss from dying -- small condition for when a player is level 1
            if (deductedTotal <= 0)
                UpdateProperty(this, PropertyInt64.TotalExperience, 0);
            else
                UpdateProperty(this, PropertyInt64.TotalExperience, deductedTotal);
        }

        /// <summary>
        /// The player earns XP for items that can be leveled up
        /// by killing creatures and completing quests,
        /// while those items are equipped.
        /// </summary>
        public void GrantItemXP(long amount)
        {
            foreach (var item in EquippedObjects.Values.Where(i => i.HasItemLevel))
                GrantItemXP(item, amount);
        }

        public void GrantItemXP(WorldObject item, long amount)
        {
            var prevItemLevel = item.ItemLevel.Value;
            var addItemXP = item.AddItemXP(amount);

            if (addItemXP > 0)
                Session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt64(item, PropertyInt64.ItemTotalXp, item.ItemTotalXp.Value));

            // handle item leveling up
            var newItemLevel = item.ItemLevel.Value;
            if (newItemLevel > prevItemLevel)
            {
                OnItemLevelUp(item, prevItemLevel);

                var actionChain = new ActionChain();
                actionChain.AddAction(this, () =>
                {
                    var msg = $"Your {item.Name} has increased in power to level {newItemLevel}!";
                    Session.Network.EnqueueSend(new GameMessageSystemChat(msg, ChatMessageType.Broadcast));

                    EnqueueBroadcast(new GameMessageScript(Guid, PlayScript.AetheriaLevelUp));
                });
                actionChain.EnqueueChain();
            }
        }
        public long? DelevelXp
        {
            get => GetProperty(PropertyInt64.DelevelXp);
            set { if (!value.HasValue) RemoveProperty(PropertyInt64.DelevelXp); else SetProperty(PropertyInt64.DelevelXp, value.Value); }
        }
    }
}
