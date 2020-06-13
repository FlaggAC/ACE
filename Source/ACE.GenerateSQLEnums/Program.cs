using ACE.Entity.Enum;
using ACE.Server.Entity;
using MySqlX.XDevAPI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ACE.GenerateSQLEnums
{
    class Program
    {
        static string SolutionPath;


        static void Initialize()
        {
            //Get solution Path
            string assemblyname = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
            string path = "";
            using (var stream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(assemblyname + ".solutionpath.txt"))
            {
                using (var sr = new StreamReader(stream))
                {
                    path = sr.ReadToEnd().Trim();
                }
            }
            SolutionPath = path;
        }

        const string SCHEMA_NAME = "ace_enums";

        static void Main(string[] args)
        {
            Initialize();
            var files = Directory.GetFiles(@$"{SolutionPath}\ACE.Entity\Enum");

            var typesToExclude = new List<Type> {
                    typeof(WeenieClassName),
                    typeof(SpellId)
                };

            var enumtypes = Assembly.GetAssembly(typeof(WeenieType)).GetTypes()
                .Where(t => t.IsEnum && t.Namespace.Contains("ACE.Entity.Enum"))
                .Where(t => !(typesToExclude.Contains(t)))
                .ToList();
            StringBuilder sqlString = new StringBuilder();
            foreach (var enumtype in enumtypes)
            {
                var names = Enum.GetNames(enumtype);
                var vals = Enum.GetValues(enumtype)
                    .Cast<object>()
                    .Select(x => long.Parse(Enum.Format(enumtype, x, "d")))
                    .ToArray();
                var map = new Dictionary<long, string>();
                for(int i = 0; i < names.Length; i++)
                {
                    var intval = vals[i];
                    if (map.ContainsKey(intval))
                    {
                        map[intval] += "|" + names[i];
                    }
                    else
                    {
                        map[intval] = names[i];
                    }
                }

                sqlString.AppendLine(@$"DROP TABLE IF EXISTS {SCHEMA_NAME}.{enumtype.Name};");
                sqlString.AppendLine(@$"CREATE TABLE {SCHEMA_NAME}.{enumtype.Name} (id bigint NOT NULL, name varchar(255) NOT NULL, PRIMARY KEY (id)) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_as_cs;");

                sqlString.AppendLine($"INSERT INTO {SCHEMA_NAME}.{enumtype.Name} (id, name) VALUES ");
                foreach (var intval in map.Keys.Take(map.Keys.Count - 1))
                {
                    sqlString.AppendLine($"({intval}, '{map[intval]}'),");
                }
                var intvallast = map.Keys.Last();
                sqlString.AppendLine($"({intvallast}, '{map[intvallast]}');");

                sqlString.AppendLine($@"CREATE UNIQUE INDEX idx_{enumtype.Name} ON ace_enums.{enumtype.Name}(name);");
                sqlString.AppendLine();
            }

            File.WriteAllText($@"{SolutionPath}\LookupOutput.sql", sqlString.ToString());
        }
    }
}
