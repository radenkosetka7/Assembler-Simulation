using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

namespace AssemblerSimulation
{
    internal class Syntax
    {
        public Dictionary<int, string> LineDictionary = new Dictionary<int, string>();
        public Dictionary<string, int> LabelDictionary = new Dictionary<string, int>();
        private int executableLine;
        private string filename;
        public Instructions commands = new Instructions();
        public Syntax()
        {
            executableLine = 1;
        }
        public static bool IsMemoryAddress(string address)
        {
            if (address.Length < 3)
                return false;
            if (address[0] == '0' && address[1] == 'x')
            {
                for (int i = 2; i < address.Length; i++)
                {
                    if (((address[i] >= '0' && address[i] <= '9') || (address[i] >= 'a' && address[i] <= 'f')))
                    {
                        continue;
                    }
                    else
                        return false;
                }
                return true;
            }
            return false;
        }
        public static bool IsLabel(string label)
        {
            if (label.EndsWith(':'))
                return true;
            else
                return false;
        }
        public static bool IsNumber(string text)
        {
            double test;
            return double.TryParse(text, out test);
        }
        private string[] SplitterFunction(string argument)
        {
            List<string> commands = new List<string>();

            char[] delimers = { ' ', ',' };
            string[] words = argument.Split(delimers);
            foreach (var word in words)
            {
                if (word == "")
                {
                    continue;
                }
                commands.Add(word);
            }
            return commands.ToArray();
        }

        private void CheckSyntax(string line)
        {
            string[] arguments = SplitterFunction(line);
            if (arguments.Length > 3)
                throw new Exception("Syntax error");
            else if (arguments.Length == 3)
            {
                if (arguments[0] == "mov" || arguments[0] == "add" || arguments[0] == "sub" || arguments[0] == "and" || arguments[0] == "or" || arguments[0] == "cmp")
                {
                    if (commands.DictionaryRegisters.ContainsKey(arguments[1]) && commands.DictionaryRegisters.ContainsKey(arguments[2]))
                        return;
                    else if (commands.DictionaryRegisters.ContainsKey(arguments[1]) && IsMemoryAddress(arguments[2]))
                    {
                        return;
                    }
                    else if (IsMemoryAddress(arguments[1]) && commands.DictionaryRegisters.ContainsKey(arguments[2]))
                    {
                        return;
                    }
                    else if (IsMemoryAddress(arguments[1]) && IsNumber(arguments[2]))
                    {
                        return;
                    }
                    else if (commands.DictionaryRegisters.ContainsKey(arguments[1]) && IsNumber(arguments[2]))
                    {
                        return;
                    }
                    else if (arguments[0] == "cmp")
                    {
                        if (IsNumber(arguments[1]) && IsMemoryAddress(arguments[2]))
                        {
                            return;
                        }
                        else if (IsNumber(arguments[1]) && commands.DictionaryRegisters.ContainsKey(arguments[2]))
                        {
                            return;
                        }
                    }
                    else
                        throw new Exception("Syntax error");
                }
            }
            else if (arguments.Length == 2)
            {
                if (arguments[0] == "je" || arguments[0] == "jne" || arguments[0] == "jl" || arguments[0] == "jge")
                {
                    FindLabels(filename);
                    if (LabelDictionary.ContainsKey(arguments[1]))
                    {
                        return;
                    }
                    else
                        throw new Exception("Syntax error");
                }
                else if (arguments[0] == "not" && commands.DictionaryRegisters.ContainsKey(arguments[1]))
                {
                    return;
                }
                else
                    throw new Exception("Syntax error.");
            }
            else if (arguments.Length == 1)
            {
                if (arguments[0] == "load" || arguments[0] == "store")
                {
                    return;
                }
                else if (arguments[0] == "in")
                {
                    SysCall(arguments[0]);
                }
                else if (IsLabel(arguments[0]))
                {
                    return;
                }
                else
                    throw new Exception("Syntax error");
            }
            else
                throw new Exception("Syntax error");
        }

        private void SysCall(string data)
        {
            Console.WriteLine("Insert commands");
            var str = Console.ReadLine();
            var array = SplitterFunction(str);
            string item=null;
            int counter=0;
            if (array.Length > 3)
            {
                if (array[0].ToLower() == "mov" || array[0].ToLower() == "add" || array[0].ToLower() == "sub" ||
                   array[0].ToLower() == "and" || array[0].ToLower() == "or" || array[0].ToLower() == "cmp")
                {
                    counter = 3;
                }
                else if (array[0].ToLower() == "not")
                {
                    counter = 2;
                }
                else if (array[0].ToLower() == "load" || array[0].ToLower() == "store")
                {
                    counter = 1;
                }
                for (int i = 0; i < counter; i++)
                {
                    item += array[i];
                    item += " ";
                }
                item = item.Trim();
                Console.WriteLine(item);
                CheckSyntax(item);
                Execute(item);
            }
            else if (array.Length == 0)
            {
                return;
            }
            else if (array.Length == 1 || array.Length == 2)
            {
                CheckSyntax(str);
                Execute(str);
            }
            else if (array.Length == 3)
            {
                CheckSyntax(str);
                Execute(str);
            }
            else
                throw new Exception("Invalid command");
        }
        private void Execute(string line)
        {
            string[] arguments = SplitterFunction(line);
            if (arguments[0].ToLower() == "add")
            {
                commands.Add(arguments[1], arguments[2]);
            }
            else if (arguments[0].ToLower() == "sub")
            {
                commands.Sub(arguments[1], arguments[2]);
            }
            else if (arguments[0].ToLower() == "and")
            {
                commands.And(arguments[1], arguments[2]);
            }
            else if (arguments[0].ToLower() == "or")
            {
                commands.Or(arguments[1], arguments[2]);
            }
            else if (arguments[0].ToLower() == "mov")
            {
                commands.Mov(arguments[1], arguments[2]);
            }
            else if (arguments[0].ToLower() == "cmp")
            {
                commands.Cmp(arguments[1], arguments[2]);
            }
            else if (arguments[0].ToLower() == "not")
            {
                commands.Not(arguments[1]);
            }
            else if (arguments[0].ToLower() == "load")
            {
                commands.Load();
            }
            else if (arguments[0].ToLower() == "store")
            {
                commands.Store();
            }
            else if (arguments[0].ToLower() == "je")
            {
                Je(arguments[1]);
            }
            else if (arguments[0].ToLower() == "jne")
            {
                Jne(arguments[1]);
            }
            else if (arguments[0].ToLower() == "jl")
            {
                Jl(arguments[1]);
            }
            else if(arguments[0].ToLower()=="jge")
            {
                Jge(arguments[1]);
            }
        }
        private void PrintScript(string filename)
        {
            int counter = 0;
            string line;
            try
            {
                StreamReader stream = new StreamReader(filename);
                while (!stream.EndOfStream)
                {
                    line = stream.ReadLine();
                    if (line.Length == 0)
                    {
                        continue;
                    }
                    else
                    {
                        Console.Write(++counter + ":" + "\t");
                        Console.WriteLine(line);
                    }
                }
            }
            catch (FileNotFoundException exception)
            {
                Console.WriteLine(exception.Message);
            }
        }
        private void WriteRegisters()
        {
            foreach (var regs in commands.DictionaryRegisters)
            {
                Console.WriteLine(regs.Key + "\t" + "\t" + regs.Value + "\t" + "\t" + sizeof(long));

            }
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Flags");
            Console.ResetColor();
            Console.WriteLine("zf" + "=" + commands.flags.zf);
            Console.WriteLine("sf" + "=" + commands.flags.sf);
            Console.WriteLine("of" + "=" + commands.flags.of);
        }
        private void WriteMemory()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Address\t\tData\t\tSize");
            Console.ResetColor();
            foreach (var mem in commands.DictrionaryMemory)
            {
                Console.WriteLine(mem.Key + "\t" + "\t" + mem.Value + "\t" + "\t" + sizeof(long));
            }
            WriteRegisters();
        }
        public void FindMemory(string address)
        {
            Console.WriteLine("Address\t\tData\t\tSize");

            foreach (var items in commands.DictrionaryMemory)
            {
                if (address == items.Key)
                {
                    Console.WriteLine(items.Key + "\t" + "\t" + items.Value + "\t" + "\t" + sizeof(long));
                }
            }
            WriteRegisters();
        }
        private void ReadLines(string file)
        {
            string line;
            int counter = 0;
            try
            {
                StreamReader stream = new StreamReader(file);
                while(!stream.EndOfStream)
                {
                     line = stream.ReadLine().ToLower();
                     LineDictionary.Add(++counter, line);
                }
            }
            catch(FileNotFoundException exception)
            {
                Console.WriteLine(exception.Message);
            }
        }
        private void FindLabels(string file)
        {
            Dictionary<string, int> item = new Dictionary<string, int>();
            try
            {
                int counter = 0;
                string line;
                StreamReader stream = new StreamReader(file);
                while (!stream.EndOfStream)
                {
                    line = stream.ReadLine().ToLower();
                    counter++;
                        if (line.Length > 0)
                        {
                            if (IsLabel(line))
                            {
                                if (!LabelDictionary.ContainsKey(line))
                                {
                                    line = line.Split(':').First();
                                    item.Add(line, counter);
                                }

                            }
                        }
                 
                }
                LabelDictionary = item;

            }
            catch (FileNotFoundException exception)
            {
                Console.WriteLine(exception.Message);
            }
        }
        public void Jne(string label)
        {
            if (commands.flags.zf == 0)
            {
                executableLine = LabelDictionary[label];
            }
        }
        public void Je(string label)
        {
            if (commands.flags.zf == 1)
            {
                executableLine = LabelDictionary[label];
            }
        }
        public void Jl(string label)
        {
            if (commands.flags.sf != commands.flags.of)
            {
                executableLine = LabelDictionary[label];
            }
        }
        public void Jge(string label)
        {
            if (commands.flags.sf == commands.flags.of)
            {
                executableLine = LabelDictionary[label];
            }
        }
        public void SingleStepMode(string file)
        {
            filename = file;
            string line;
            try
            {
                ReadLines(file);
                if(!(LineDictionary.First().Value == "start"))
                {
                    throw new Exception("Can not execute script");
                }
                Console.WriteLine("Do you want to print the script? Yes=1");
                var choice = Console.ReadLine();
                Console.Clear();
                if (choice == "1")
                    PrintScript(file);
                Console.WriteLine();
                while (executableLine < LineDictionary.Count)
                {
                    executableLine++;
                    line = LineDictionary[executableLine];
                    if (line.Length == 0)
                    {
                        continue;
                    }
                    try
                    {
                        CheckSyntax(line);
                        if (!(line == "in"))
                        {
                            Execute(line);
                        }
                        Console.WriteLine($"{executableLine}: line");
                        WriteSpecificAddress();
                        Console.ReadLine();
                        Console.Clear();
                    }
                    catch (SyntaxErrorException exception)
                    {
                        Console.WriteLine(exception.Message);
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }
        public void DebuggingMode(string file)
        {
            filename = file;
            try
            {
                ReadLines(file);
                if (!(LineDictionary.First().Value == "start"))
                {
                    throw new Exception("Can not execute script");
                }
                Console.WriteLine("Do you want to print the script? Yes=1");
                var choice = Console.ReadLine();
                Console.Clear();
                if (choice == "1")
                    PrintScript(file);
                Console.WriteLine();
                string line;
                int parsernumber;
                List<int> point = new List<int>();
                Console.WriteLine("Insert breakpoint(s) line(s)");
                string breakpoints = Console.ReadLine();
                breakpoints = breakpoints.Trim();
                string[] bp = breakpoints.Split(' ');
                for (int i = 0; i < bp.Length; i++)
                {
                    parsernumber = int.Parse(bp[i]);
                    if (parsernumber > LineDictionary.Count || parsernumber<1)
                    {
                        continue;
                    }
                    else
                    {
                        if (!point.Contains(parsernumber))
                        {
                            point.Add(parsernumber);
                        }
                    }
                }
                point.Sort();
                    while (executableLine < LineDictionary.Count)
                    {
                    executableLine++;
                        line = LineDictionary[executableLine];
                    if (line.Length == 0 && point.Contains(executableLine))
                    {
                        Console.WriteLine($"{executableLine}: line");
                        WriteSpecificAddress();
                        Console.ReadLine();
                    }
                    else if(line.Length==0)
                    {
                        continue;
                    }
                        try
                        {
                            if (point.Contains(executableLine))
                            {
                                Console.WriteLine($"{executableLine}: line");
                                WriteSpecificAddress();
                                Console.ReadLine();
                            }
                            CheckSyntax(line);
                            if (!(line == "in"))
                            {
                            Execute(line);
                            }
                        }
                        catch (SyntaxErrorException exception)
                        {
                            Console.WriteLine(exception.Message);
                        }
                    }
             }
            catch (FileLoadException exception)
            {
                Console.WriteLine(exception.Message);
            }
        }
        private void WriteSpecificAddress()
        {
            Console.WriteLine("Specific memory address|All memory addresses [0|1]");
            var item = Console.ReadLine();
            if (item == "0")
            {
                Console.WriteLine("Insert specific memory address (0x..)");
                var address = Console.ReadLine();
                Console.Clear();
                FindMemory(address);
            }
            else if (item != "0")
            {
                WriteMemory();
            }
        }
    }
}


