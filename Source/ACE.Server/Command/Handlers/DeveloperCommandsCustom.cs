using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Numerics;

using log4net;

using ACE.Common;
using ACE.Common.Extensions;
using ACE.Database;
using ACE.Database.Models.World;
using ACE.DatLoader;
using ACE.DatLoader.FileTypes;
using ACE.Entity;
using ACE.Entity.Enum;
using ACE.Entity.Enum.Properties;
using ACE.Entity.Models;
using ACE.Server.Entity;
using ACE.Server.Entity.Actions;
using ACE.Server.Factories;
using ACE.Server.Managers;
using ACE.Server.Network;
using ACE.Server.Network.GameEvent.Events;
using ACE.Server.Network.GameMessages.Messages;
using ACE.Server.Physics.Entity;
using ACE.Server.Physics.Extensions;
using ACE.Server.Physics.Managers;
using ACE.Server.WorldObjects;
using ACE.Server.WorldObjects.Entity;


using Position = ACE.Entity.Position;
using Spell = ACE.Server.Entity.Spell;

namespace ACE.Server.Command.Handlers
{
    public static partial class DeveloperCommands
    {
        [CommandHandler("getpropertyguid", AccessLevel.Developer, CommandHandlerFlag.None, 2, "Gets a property for the object with the given guid", "/getpropertyguid <guid> <property>")]
        public static void HandleGetPropertyGuid(Session session, params string[] parameters)
        {
            if (parameters.Length < 2)
                return;

            var guidstring = parameters[0];
            var prop = parameters[1];

            if (!TryParseGuid(guidstring, out var guid))
            {
                CommandHandlerHelper.WriteOutputInfo(session, $"Malformed guid {guidstring}");
                return;
            }

            WorldObject obj;
            try
            {
                var pobj = ServerObjectManager.GetObjectA(guid);
                obj = pobj.WeenieObj.WorldObject;
            }
            catch
            {
                CommandHandlerHelper.WriteOutputInfo(session, $"Unable to load world object.");
                return;
            }

            var props = prop.Split('.');
            var propType = props[0];
            var propName = props[1];

            Type pType;
            if (propType.Equals("PropertyInt", StringComparison.OrdinalIgnoreCase))
                pType = typeof(PropertyInt);
            else if (propType.Equals("PropertyInt64", StringComparison.OrdinalIgnoreCase))
                pType = typeof(PropertyInt64);
            else if (propType.Equals("PropertyBool", StringComparison.OrdinalIgnoreCase))
                pType = typeof(PropertyBool);
            else if (propType.Equals("PropertyFloat", StringComparison.OrdinalIgnoreCase))
                pType = typeof(PropertyFloat);
            else if (propType.Equals("PropertyString", StringComparison.OrdinalIgnoreCase))
                pType = typeof(PropertyString);
            else if (propType.Equals("PropertyInstanceId", StringComparison.OrdinalIgnoreCase))
                pType = typeof(PropertyInstanceId);
            else if (propType.Equals("PropertyDataId", StringComparison.OrdinalIgnoreCase))
                pType = typeof(PropertyDataId);
            else
            {
                CommandHandlerHelper.WriteOutputInfo(session, $"Unknown property type: {propType}", ChatMessageType.Broadcast);
                return;

            }

            if (!Enum.TryParse(pType, propName, true, out var result))
            {
                CommandHandlerHelper.WriteOutputInfo(session, $"Couldn't find {prop}");
                return;
            }

            var value = "";
            if (propType.Equals("PropertyInt", StringComparison.OrdinalIgnoreCase))
                value = Convert.ToString(obj.GetProperty((PropertyInt)result));
            else if (propType.Equals("PropertyInt64", StringComparison.OrdinalIgnoreCase))
                value = Convert.ToString(obj.GetProperty((PropertyInt64)result));
            else if (propType.Equals("PropertyBool", StringComparison.OrdinalIgnoreCase))
                value = Convert.ToString(obj.GetProperty((PropertyBool)result));
            else if (propType.Equals("PropertyFloat", StringComparison.OrdinalIgnoreCase))
                value = Convert.ToString(obj.GetProperty((PropertyFloat)result));
            else if (propType.Equals("PropertyString", StringComparison.OrdinalIgnoreCase))
                value = Convert.ToString(obj.GetProperty((PropertyString)result));
            else if (propType.Equals("PropertyInstanceId", StringComparison.OrdinalIgnoreCase))
                value = Convert.ToString(obj.GetProperty((PropertyInstanceId)result));
            else if (propType.Equals("PropertyDataId", StringComparison.OrdinalIgnoreCase))
                value = Convert.ToString(obj.GetProperty((PropertyDataId)result));

            CommandHandlerHelper.WriteOutputInfo(session, $"{obj.Name} ({obj.Guid}): {prop} = {value}");
        }

        /// <summary>
        /// Gets a property for the last appraised object
        /// </summary>
        [CommandHandler("getproperty", AccessLevel.Developer, CommandHandlerFlag.RequiresWorld, 1, "Gets a property for the last appraised object", "/getproperty <property>")]
        public static void HandleGetProperty(Session session, params string[] parameters)
        {
            var obj = CommandHandlerHelper.GetLastAppraisedObject(session);
            if (obj == null) return;

            if (parameters.Length < 1)
                return;

            var prop = parameters[0];

            var props = prop.Split('.');
            if (props.Length != 2)
            {
                session.Network.EnqueueSend(new GameMessageSystemChat($"Unknown {prop}", ChatMessageType.Broadcast));
                return;
            }

            var paramnew = new string[]
            {
                obj.Guid.Full.ToString(),
                prop
            };

            HandleGetPropertyGuid(session, paramnew);
        }

        [CommandHandler("setpropertyguid", AccessLevel.Developer, CommandHandlerFlag.None, 3, "Sets a property for the given guid", "/setpropertyguid <guid> <property> <value>")]
        public static void HandleSetPropertyGuid(Session session, params string[] parameters)
        {
            if (parameters.Length < 3)
                return;

            var guidstring = parameters[0];
            var prop = parameters[1];
            var value = parameters[2].Trim();

            if (!TryParseGuid(guidstring, out var guid))
            {
                CommandHandlerHelper.WriteOutputInfo(session, $"Malformed guid {guidstring}");
                return;
            }

            WorldObject obj;
            try
            {
                var pobj = ServerObjectManager.GetObjectA(guid);
                obj = pobj.WeenieObj.WorldObject;
            }
            catch
            {
                CommandHandlerHelper.WriteOutputInfo(session, $"Unable to load world object.");
                return;
            }

            var props = prop.Split('.');
            if (props.Length != 2)
            {
                CommandHandlerHelper.WriteOutputInfo(session, $"Unknown {prop}");
                return;
            }

            var propType = props[0];
            var propName = props[1];

            Type pType;
            if (propType.Equals("PropertyInt", StringComparison.OrdinalIgnoreCase))
                pType = typeof(PropertyInt);
            else if (propType.Equals("PropertyInt64", StringComparison.OrdinalIgnoreCase))
                pType = typeof(PropertyInt64);
            else if (propType.Equals("PropertyBool", StringComparison.OrdinalIgnoreCase))
                pType = typeof(PropertyBool);
            else if (propType.Equals("PropertyFloat", StringComparison.OrdinalIgnoreCase))
                pType = typeof(PropertyFloat);
            else if (propType.Equals("PropertyString", StringComparison.OrdinalIgnoreCase))
                pType = typeof(PropertyString);
            else if (propType.Equals("PropertyInstanceId", StringComparison.OrdinalIgnoreCase))
                pType = typeof(PropertyInstanceId);
            else if (propType.Equals("PropertyDataId", StringComparison.OrdinalIgnoreCase))
                pType = typeof(PropertyDataId);
            else
            {
                CommandHandlerHelper.WriteOutputInfo(session, $"Unknown property type: {propType}");
                return;
            }

            if (!Enum.TryParse(pType, propName, true, out var result))
            {
                CommandHandlerHelper.WriteOutputInfo(session, $"Couldn't find {prop}");
                return;
            }

            if (value == "null")
            {
                if (propType.Equals("PropertyInt", StringComparison.OrdinalIgnoreCase))
                    obj.RemoveProperty((PropertyInt)result);
                else if (propType.Equals("PropertyInt64", StringComparison.OrdinalIgnoreCase))
                    obj.RemoveProperty((PropertyInt64)result);
                else if (propType.Equals("PropertyBool", StringComparison.OrdinalIgnoreCase))
                    obj.RemoveProperty((PropertyBool)result);
                else if (propType.Equals("PropertyFloat", StringComparison.OrdinalIgnoreCase))
                    obj.RemoveProperty((PropertyFloat)result);
                else if (propType.Equals("PropertyString", StringComparison.OrdinalIgnoreCase))
                    obj.RemoveProperty((PropertyString)result);
                else if (propType.Equals("PropertyInstanceId", StringComparison.OrdinalIgnoreCase))
                    obj.RemoveProperty((PropertyInstanceId)result);
                else if (propType.Equals("PropertyDataId", StringComparison.OrdinalIgnoreCase))
                    obj.RemoveProperty((PropertyDataId)result);
            }
            else
            {
                try
                {
                    if (propType.Equals("PropertyInt", StringComparison.OrdinalIgnoreCase))
                    {
                        obj.SetProperty((PropertyInt)result, Convert.ToInt32(value));
                        obj.EnqueueBroadcast(new GameMessagePublicUpdatePropertyInt(obj, (PropertyInt)result, Convert.ToInt32(value)));
                    }
                    else if (propType.Equals("PropertyInt64", StringComparison.OrdinalIgnoreCase))
                    {
                        obj.SetProperty((PropertyInt64)result, Convert.ToInt64(value));
                        obj.EnqueueBroadcast(new GameMessagePublicUpdatePropertyInt64(obj, (PropertyInt64)result, Convert.ToInt64(value)));
                    }
                    else if (propType.Equals("PropertyBool", StringComparison.OrdinalIgnoreCase))
                    {
                        obj.SetProperty((PropertyBool)result, Convert.ToBoolean(value));
                        obj.EnqueueBroadcast(new GameMessagePublicUpdatePropertyBool(obj, (PropertyBool)result, Convert.ToBoolean(value)));
                    }
                    else if (propType.Equals("PropertyFloat", StringComparison.OrdinalIgnoreCase))
                    {
                        obj.SetProperty((PropertyFloat)result, Convert.ToDouble(value));
                        obj.EnqueueBroadcast(new GameMessagePublicUpdatePropertyFloat(obj, (PropertyFloat)result, Convert.ToDouble(value)));
                    }
                    else if (propType.Equals("PropertyString", StringComparison.OrdinalIgnoreCase))
                    {
                        obj.SetProperty((PropertyString)result, value);
                        obj.EnqueueBroadcast(new GameMessagePublicUpdatePropertyString(obj, (PropertyString)result, value));
                    }
                    else if (propType.Equals("PropertyInstanceId", StringComparison.OrdinalIgnoreCase))
                    {
                        obj.SetProperty((PropertyInstanceId)result, Convert.ToUInt32(value));
                        obj.EnqueueBroadcast(new GameMessagePublicUpdateInstanceID(obj, (PropertyInstanceId)result, new ObjectGuid(Convert.ToUInt32(value))));
                    }
                    else if (propType.Equals("PropertyDataId", StringComparison.OrdinalIgnoreCase))
                    {
                        obj.SetProperty((PropertyDataId)result, Convert.ToUInt32(value));
                        obj.EnqueueBroadcast(new GameMessagePublicUpdatePropertyDataID(obj, (PropertyDataId)result, Convert.ToUInt32(value)));
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return;
                }
            }
            CommandHandlerHelper.WriteOutputInfo(session, $"{obj.Name} ({obj.Guid}): {prop} = {value}");
            PlayerManager.BroadcastToAuditChannel(session?.Player, $"{session?.Player?.Name ?? "*Console Admin*"} changed a property for {obj.Name} ({obj.Guid}): {prop} = {value}");

            // hack for easier testing
            if (pType == typeof(PropertyInt) && (PropertyInt)result == PropertyInt.Faction1Bits && obj is Creature creature && creature.RetaliateTargets == null)
                creature.RetaliateTargets = new HashSet<uint>();
        }

        private static bool TryParseGuid(string guidstring, out uint guid)
        {
            //Base 10 or 16?
            NumberStyles numberStyle;
            if (guidstring.StartsWith("0x")) 
            {
                guidstring = guidstring.Substring(2);
                numberStyle = NumberStyles.HexNumber;
            }
            else if (guidstring.Length > 8)
            {
                numberStyle = NumberStyles.Integer;
            }
            else
            {
                numberStyle = NumberStyles.HexNumber;
            }
            return uint.TryParse(guidstring, numberStyle, null, out guid);
        }

        /// <summary>
        /// Sets a property for the last appraised object
        /// </summary>
        [CommandHandler("setproperty", AccessLevel.Developer, CommandHandlerFlag.RequiresWorld, 2, "Sets a property for the last appraised object", "/setproperty <property> <value>")]
        public static void HandleSetProperty(Session session, params string[] parameters)
        {
            var obj = CommandHandlerHelper.GetLastAppraisedObject(session);
            if (obj == null) return;

            if (parameters.Length < 2)
                return;

            var prop = parameters[0];
            var value = parameters[1];

            var props = prop.Split('.');
            if (props.Length != 2)
            {
                session.Network.EnqueueSend(new GameMessageSystemChat($"Unknown {prop}", ChatMessageType.Broadcast));
                return;
            }

            var paramnew = new string[]
            {
                obj.Guid.Full.ToString(),
                prop,
                value
            };

            HandleSetPropertyGuid(session, paramnew);
        }
    }
}
