using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using FESScript2.Graphics.UserControls;

namespace FESScript2.CodeWorks.BlockCreation
{
    public static class Loader
    {

        private const char splitChar = '|';
        private const char dataPackSplitChar = '&';

        /// <summary>
        /// Loads all of the block design from block directory
        /// </summary>

        public static void Load() 
        {
            //Settings.Settings.LoadSettings();
            if (!Directory.Exists(Directories.Blocks)) 
            {
                Directory.CreateDirectory(Directories.Blocks);
            }
            foreach (string file in Directory.GetFiles(Directories.Blocks, "*.FESBlock", SearchOption.AllDirectories)) 
            {
                //try 
                //{ 
                LoadBlock(Directory.GetParent(file).FullName, Path.GetFileName(file));
                //}
                //catch(Exception e) 
                //{
                //    continue;
                //}
            }
            Transpiler.GenerateFullCpp.CompileFile();
        }

        /// <summary>
        /// Loads one block.
        /// </summary>
        /// <param name="path">Path of the file.</param>
        /// <param name="name">Name of the file.</param>

        private static void LoadBlock(string path, string name) 
        {
            bool designEnd = false;
            string currentFile = Path.Combine(path, name);
            StreamReader reader = new StreamReader(currentFile);
            BlockType block = new BlockType(0,"", "", Graphics.UserControls.SubUserControls.Type.Error);
            block.Name = Path.GetFileNameWithoutExtension(currentFile);
            int exitCode = -1;
            while (!reader.EndOfStream && !designEnd) 
            {
                try
                { 
                    LoadLine(reader.ReadLine(), block, ref designEnd, ref exitCode);
                }
                catch (Exception)
                {
                    continue;
                }
            }
            block.Category = new DirectoryInfo(Path.GetDirectoryName(currentFile)).Name;
            Category.AddOrCreate(block.Category, ref block);
            string UserCode = "";
            block.SingleActionOutput = !block.HasMoreThanOneActionOutputDot();
            while (!reader.EndOfStream)
            {
                UserCode += reader.ReadLine();
            }
            if (exitCode == 0) 
            { 
                Transpiler.GenerateFullCpp.GenerateFullFromFileCpp(UserCode, block);
            }
            else if (exitCode == 1 || exitCode == 2) 
            { 
                block.FakeString = UserCode;
            }
            reader.Close();
        }

        /// <summary>
        /// Takes care of one line of the code.
        /// </summary>
        /// <param name="line">String to analyze.</param>
        /// <param name="block">BlockType in which data will be loaded.</param>

        private static int LoadLine(string line, BlockType block, ref bool designEnd, ref int exitCode) 
        {
            byte space;
            bool compiler = false;
            switch (line[0]) 
            {
            case '#':
                break; //Poznámka
            case 'I': //Vstupní tečka
                if (line[1] == 'C')  //Je podmínková (v cyklu)
                {
                    string[] args1 = line.Substring(2).Split(splitChar);
                    Graphics.UserControls.SubUserControls.DotsType dot1 = new Graphics.UserControls.SubUserControls.DotsType()
                    {
                        ID = int.Parse(args1[0]),
                        dotType = (Graphics.UserControls.SubUserControls.Type)int.Parse(args1[1]),
                        io = Graphics.UserControls.SubUserControls.IO.Input,
                        isConditional = true
                    };
                    block.Dots.Add(dot1);
                }
                else 
                {
                    string[] args1 = line.Substring(1).Split(splitChar);
                    Graphics.UserControls.SubUserControls.DotsType dot1 = new Graphics.UserControls.SubUserControls.DotsType()
                    {
                        ID = int.Parse(args1[0]),
                        dotType = (Graphics.UserControls.SubUserControls.Type)int.Parse(args1[1]),
                        io = Graphics.UserControls.SubUserControls.IO.Input
                    };
                    block.Dots.Add(dot1);
                }
                break;
            case 'O': //Výstupní podmínka
                string[] args2 = line.Substring(1).Split(splitChar);
                Graphics.UserControls.SubUserControls.DotsType dot2 = new Graphics.UserControls.SubUserControls.DotsType()
                {
                    ID = int.Parse(args2[0]),
                    dotType = (Graphics.UserControls.SubUserControls.Type)int.Parse(args2[1]),
                    io = Graphics.UserControls.SubUserControls.IO.Output
                };
                block.Dots.Add(dot2);
                break;
            case 'T': //Nastavuje typ blocku (barvu)
                block.type = (Graphics.UserControls.SubUserControls.Type)int.Parse(line.Substring(1));
                break;
            case 'U': //Nastavuje unikátní identifikátor
                block.ID = int.Parse(line.Substring(1));
                break;
            case 'L': //Přidává popisek
                string[] args4 = line.Substring(1).Split(splitChar);
                Graphics.UserControls.SubUserControls.ContentsType content3 = new Graphics.UserControls.SubUserControls.ContentsType()
                {
                    ID = int.Parse(args4[0]),
                    collumn = int.Parse(args4[1]),
                    text = args4[2],
                    type = typeof(Graphics.UserControls.SubUserControls.TextLabel),
                        
                };
                block.Contents.Add(content3);
                break;
            case 'B': //Přidává textové pole
                if (line[1] == 'D') //Nastavuje, zda slouží pouze compileru
                {
                    space = 2;
                    compiler = true;
                }
                else
                {
                    space = 1;
                }
                string[] args5 = line.Substring(space).Split(splitChar);
                Graphics.UserControls.SubUserControls.ContentsType content4 = new Graphics.UserControls.SubUserControls.ContentsType()
                {
                    ID = int.Parse(args5[0]),
                    collumn = int.Parse(args5[1]),
                    text = args5[2],
                    type = typeof(Graphics.UserControls.SubUserControls.TextBox),
                    isCompiler = compiler
                };
                block.Contents.Add(content4);
                break;
            case 'C': //Přidává Zaškrtávací pole
                if (line[1] == 'D') //Nastavuje, zda slouží pouze compileru
                {
                    space = 2;
                    compiler = true;
                }
                else 
                {
                    space = 1;
                }
                string[] args6 = line.Substring(space).Split(splitChar);
                Graphics.UserControls.SubUserControls.ContentsType content5 = new Graphics.UserControls.SubUserControls.ContentsType()
                {
                    ID = int.Parse(args6[0]),
                    collumn = int.Parse(args6[1]),
                    text = args6[2],
                    type = typeof(Graphics.UserControls.SubUserControls.Checkbox),
                    isCompiler = compiler
                };
                block.Contents.Add(content5);
                break;
            case 'M': //Přidává Výběr z možností
                if (line[1] == 'D') //Nastavuje, zda slouží pouze compileru
                {
                    space = 2;
                    compiler = true;
                }
                else
                {
                    space = 1;
                }
                string[] dataPacks = line.Substring(space).Split(dataPackSplitChar);
                string[] args7 = dataPacks[0].Split(splitChar);
                Graphics.UserControls.SubUserControls.ContentsType content6 = new Graphics.UserControls.SubUserControls.ContentsType()
                {
                    ID = int.Parse(args7[0]),
                    collumn = int.Parse(args7[1]),
                    text = args7[2],
                    type = typeof(Graphics.UserControls.SubUserControls.Combobox),
                    ContentArgs = new Graphics.UserControls.SubUserControls.ContentArgs.ComboBoxArgs(new List<string>(dataPacks[1].Split(splitChar))),
                    isCompiler = compiler
                };
                block.Contents.Add(content6);
                break;
            case 'H': //Přidává c++ knihovny
                Transpiler.GenerateFullCpp.headerHead += @$"{line.Split(splitChar)[1]}
";    
                break;
            case 'F':
                designEnd = true;
                if (line[1] == 'A' && line[2] == 'K' && line[3] == 'E') //Použije obsah jako název proměnné
                {
                    exitCode = 1;
                    block.IsBodyless = true;
                }
                else if (line[1] == 'U') //Nebude vytvářet funkci
                {
                    exitCode = 2;
                    block.CreateFunction = false;
                }
                break;
            case 'E': //Konec, za kterým následuje kód
                if (line[1] == 'N' && line[2] == 'D') 
                { 
                    exitCode = 0;
                    designEnd = true;
                }
                break;
            }
            return exitCode;
        }
    }
}