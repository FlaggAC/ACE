using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;

using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

using ACE.Adapter.GDLE;
using ACE.Adapter.Lifestoned;
using ACE.Common.Extensions;
using ACE.Database;
using ACE.Database.Models.World;
using ACE.Database.SQLFormatters.World;
using ACE.Entity;
using ACE.Entity.Enum;
using ACE.Entity.Enum.Properties;
using ACE.Server.Entity;
using ACE.Server.Factories;
using ACE.Server.Managers;
using ACE.Server.Network;
using ACE.Server.Network.GameMessages.Messages;
using ACE.Server.Physics.Extensions;
using ACE.Server.WorldObjects;

namespace ACE.Server.Command.Handlers.Processors
{
    public partial class DeveloperContentCommands
    {
        public static void ImportSQLWeenieWrapped(Session session, string param, string param2)
        {
            DirectoryInfo di = VerifyContentFolder(session);
            if (!di.Exists) return;

            var sep = Path.DirectorySeparatorChar;

            var prefix = param + " ";

            var sql_folder = $"{di.FullName}{sep}sql{sep}weenies{sep}";

            if (param.Equals("folder", StringComparison.OrdinalIgnoreCase) && !string.IsNullOrWhiteSpace(param2))
            {
                if (param2.Contains(".."))
                {
                    CommandHandlerHelper.WriteOutputInfo(session, $"Path may not contain the sequence '..'");
                    return;
                }
                sql_folder = $"{sql_folder}{param2}{sep}";
                prefix = "";
            }
            else if (param.Equals("all", StringComparison.OrdinalIgnoreCase))
            {
                prefix = "";
            }

            di = new DirectoryInfo(sql_folder);
            if (!di.Exists)
            {
                CommandHandlerHelper.WriteOutputInfo(session, $"Couldn't find folder: {di.FullName}");
                return;
            }

            var files = di.Exists ? di.GetFiles($"{prefix}*.sql", SearchOption.AllDirectories) : null;

            if (files == null || files.Length == 0)
            {
                CommandHandlerHelper.WriteOutputInfo(session, $"Couldn't find {sql_folder}{prefix}*.sql");
                return;
            }

            foreach (var file in files)
                ImportSQLWeenie(session, file.FullName, file.Name);
        }
    }
}
