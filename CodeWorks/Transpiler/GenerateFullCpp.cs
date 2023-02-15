using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace FESScript2.CodeWorks.Transpiler
{
    public static class GenerateFullCpp
    {
        public static List<string> templateCpp = new List<string>(); 
        public static List<string> templateH = new List<string>();
        public const string fileName = "OutputFile";
        public static string headerHead = @"#include <string>
#include <stdexcept>
#include <iostream>
";

        public static void GenerateFullFromFileCpp(string code, Graphics.UserControls.BlockType blockType) 
        {
            string constructor = "";
            string inputInit = "";
            string IOs = "";
            List<Graphics.UserControls.SubUserControls.DotsType> dotsInp = new List<Graphics.UserControls.SubUserControls.DotsType>();
            List<Graphics.UserControls.SubUserControls.DotsType> dotsOut = new List<Graphics.UserControls.SubUserControls.DotsType>();
            for (int i = 0; i < blockType.dots.Count; i++) 
            {
                if (blockType.dots[i].dotType != Graphics.UserControls.SubUserControls.Type.Action && blockType.dots[i].dotType != Graphics.UserControls.SubUserControls.Type.SubAction) 
                { 
                    if (blockType.dots[i].io == Graphics.UserControls.SubUserControls.IO.Input)
                    {
                        dotsInp.Add(blockType.dots[i]);
                    }
                    else if (blockType.dots[i].io == Graphics.UserControls.SubUserControls.IO.Output) 
                    {
                        dotsOut.Add(blockType.dots[i]);
                    }
                }
            }

            for (int i = 0; i < dotsInp.Count; i++)
            {
                if (dotsInp[i].dotType == Graphics.UserControls.SubUserControls.Type.Numerical)
                {
                    constructor += $"long double {dotsInp[i].Name}" + (((i + 1) == dotsInp.Count) ? "" : ", ");
                    inputInit += $"this->{dotsInp[i].Name} = {dotsInp[i].Name}; \n";
                    IOs += $"long double {dotsInp[i].Name}; \n";
                }
                else if (dotsInp[i].dotType == Graphics.UserControls.SubUserControls.Type.Textual)
                {
                    constructor += $"std::string {dotsInp[i].Name}" + (((i + 1) == dotsInp.Count) ? "" : ", ");
                    inputInit += $"this->{dotsInp[i].Name} = {dotsInp[i].Name}; \n";
                    IOs += $"std::string {dotsInp[i].Name}; \n";
                }
                else if (dotsInp[i].dotType == Graphics.UserControls.SubUserControls.Type.Boolean)
                {
                    constructor += $"bool {dotsInp[i].Name}" + (((i + 1) == dotsInp.Count) ? "" : ", ");
                    inputInit += $"this->{dotsInp[i].Name} = {dotsInp[i].Name}; \n";
                    IOs += $"bool {dotsInp[i].Name}; \n";
                }
            }
            for (int i = 0; i < dotsOut.Count; i++)
            {
                if (dotsOut[i].dotType == Graphics.UserControls.SubUserControls.Type.Numerical)
                {
                    inputInit += $"this->{dotsOut[i].Name} = 0; \n";
                    IOs += $"long double {dotsOut[i].Name}; \n";
                }
                else if (dotsOut[i].dotType == Graphics.UserControls.SubUserControls.Type.Textual)
                {
                    inputInit += $@"this->{dotsOut[i].Name} = "" ""; 
";
                    IOs += $"std::string {dotsOut[i].Name}; \n";
                }
                else if (dotsOut[i].dotType == Graphics.UserControls.SubUserControls.Type.Boolean)
                {
                    inputInit += $@"this->{dotsOut[i].Name} = false; 
";
                    IOs += $"bool {dotsOut[i].Name}; \n";
                }
            }

            for (int i = 0; i < blockType.contents.Count; i++)
            {
                if (!blockType.contents[i].isCompiler)
                {
                    if (blockType.contents[i].type == typeof(Graphics.UserControls.SubUserControls.TextBox) || blockType.contents[i].type == typeof(Graphics.UserControls.SubUserControls.Combobox))
                    {
                        IOs += $"std::string {blockType.contents[i].Name};\n";
                    }
                    else if (blockType.contents[i].type == typeof(Graphics.UserControls.SubUserControls.Checkbox))
                    {
                        IOs += $"bool {blockType.contents[i].Name};\n";
                    }
                }
            }

            string templateCpp =
            @$"
{blockType.Name}::{blockType.Name}({constructor})
{{
    {inputInit}
}}
{(blockType.singleActionOutput ? "void" : "int")} {blockType.Name}::Run()
{{
	try
	{{
		{code}
    }}
	catch (const std::exception& a)
	{{
		throw std::runtime_error(""{blockType.Name} caused an exception!"");
	}}
}}";

            string templateH =@$"
class {blockType.Name}
{{
public:
    {IOs}
    {blockType.Name}({constructor});
    {(blockType.singleActionOutput ? "void" : "int")} Run();
}};";

            GenerateFullCpp.templateCpp.Add(templateCpp);
            GenerateFullCpp.templateH.Add(templateH);
        }
        public static void CompileFile() 
        {
            string cppHead =@$"#include ""{fileName}.h""";
            StreamWriter writerCpp = new StreamWriter(Directories.PrecompiledFiles + @"\" + fileName + ".cpp", false);
            writerCpp.WriteLine(cppHead);
            foreach (string line in templateCpp) 
            { 
                writerCpp.WriteLine(line);
            }

            StreamWriter writerH = new StreamWriter(Directories.PrecompiledFiles + @"\" + fileName + ".h", false);
            writerH.WriteLine(headerHead);
            foreach (string line in templateH)
            {
                writerH.WriteLine(line);
            }
            writerH.Close();
            writerCpp.Close();
            //Transpiler.TranspileLibrary(fileName, Directories.PrecompiledFiles);
        }
        public static bool IsCompiled
        {
            get 
            { 
                if (File.Exists(Path.Combine(Directories.PrecompiledFiles + fileName + ".h"))) 
                {
                    return true;
                }
                else 
                {
                    return false;
                }
            }
        }
    }
}