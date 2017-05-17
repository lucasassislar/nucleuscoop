using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypescriptSyntaxPaste;

namespace Nucleus.Gaming.Build
{
    public class Settings : ISettingStore
    {
        public bool AddIPrefixInterfaceDeclaration { get; set; }
        public bool IsConvertListToArray { get; set; }
        public bool IsConvertMemberToCamelCase { get; set; }
        public bool IsConvertToInterface { get; set; }
        public bool IsInterfaceOptionalProperties { get; set; }
        public TypeNameReplacementData[] ReplacedTypeNameArray { get; set; }
    }

    public class Program
    {
        static int Main(string[] args)
        {
            return 0;

            BuildProgram build = new BuildProgram();
            DirectoryInfo root = build.GetStartLocation();
            DirectoryInfo solution = build.GetSolutionRoot();

            FileInfo genericGameInfo = solution.GetFile("GenericContext.cs", SearchOption.AllDirectories);

            CSharpToTypescriptConverter converter = new CSharpToTypescriptConverter();

            Settings settings = new Settings();
            //settings.IsConvertToInterface = true;

            string typeScript = converter.ConvertToTypescript(File.ReadAllText(genericGameInfo.FullName), settings);

            if (string.IsNullOrEmpty(typeScript))
            {
                return 1;
            }
            else
            {
                string dest = Path.Combine(root.FullName, "index.d.ts");
                if (File.Exists(dest))
                {
                    File.Delete(dest);
                }
                File.WriteAllText(dest, typeScript);
            }

            return 0;
        }
    }
}
