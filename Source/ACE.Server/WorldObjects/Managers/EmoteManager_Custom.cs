using ACE.Entity.Enum;
using ACE.Entity.Enum.Properties;
using ACE.Entity.Models;
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
            switch ((EmoteType)emote.Type)
            {
                case EmoteType.PotatoResetResistAugs:
                    var player = targetObject as Player;
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
                default:
                    log.Debug($"EmoteManager.Execute - Encountered Unhandled EmoteType {(EmoteType)emote.Type} for {WorldObject.Name} ({WorldObject.WeenieClassId})");
                    break;
            }
        }
    }
}
