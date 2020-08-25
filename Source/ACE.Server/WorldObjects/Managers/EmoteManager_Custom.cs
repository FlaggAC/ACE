using ACE.DatLoader;
using ACE.Entity.Enum;
using ACE.Entity.Enum.Properties;
using ACE.Entity.Models;
using ACE.Server.Entity;
using ACE.Server.Network.GameMessages.Messages;
using log4net;
using System;
using System.Collections.Generic;
using System.Text;

namespace ACE.Server.WorldObjects.Managers
{
    public partial class EmoteManager
    {
        internal void ExecuteEmoteCustom(PropertiesEmote emoteSet, PropertiesEmoteAction emote, WorldObject targetObject)
        {
            var player = targetObject as Player;
            switch ((EmoteType)emote.Type)
            {
                case EmoteType.PotatoResetResistAugs:
                    if (player == null)
                        return;
                    player.SetProperty(PropertyInt.AugmentationResistanceSlash, 0);
                    player.SetProperty(PropertyInt.AugmentationResistancePierce, 0);
                    player.SetProperty(PropertyInt.AugmentationResistanceBlunt, 0);
                    player.SetProperty(PropertyInt.AugmentationResistanceAcid, 0);
                    player.SetProperty(PropertyInt.AugmentationResistanceFire, 0);
                    player.SetProperty(PropertyInt.AugmentationResistanceFrost, 0);
                    player.SetProperty(PropertyInt.AugmentationResistanceLightning, 0);
                    player.SetProperty(PropertyInt.AugmentationResistanceFamily, 0);
                    var msg = $"Your elemental resistance augmentations have been removed!";
                    player.Session.Network.EnqueueSend(new GameMessageSystemChat(msg, ChatMessageType.Broadcast));
                    msg = $"Note that due to limitations of the game, the displayed augmentations on your character augmentation screen may appear incorrect until a relog.";
                    player.Session.Network.EnqueueSend(new GameMessageSystemChat(msg, ChatMessageType.Broadcast));
                    break;
                case EmoteType.PotatoTeachNoobSpells:
                    var spellTable = DatManager.PortalDat.SpellTable;

                    foreach (var spellID in Player.NoobSpellTable)
                    {
                        if (!spellTable.Spells.ContainsKey(spellID))
                        {
                            continue;
                        }
                        var spell = new Spell(spellID, false);
                        player.LearnSpellWithNetworking(spell.Id, false);
                    }
                    break;
                default:
                    log.Debug($"EmoteManager.Execute - Encountered Unhandled EmoteType {(EmoteType)emote.Type} for {WorldObject.Name} ({WorldObject.WeenieClassId})");
                    break;
            }
        }
    }
}
