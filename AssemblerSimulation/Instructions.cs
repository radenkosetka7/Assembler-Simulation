using System;
using System.Collections.Generic;

namespace AssemblerSimulation
{

    internal class Instructions
    {
        public Flags flags = new Flags();
        private int startingaddress = 0;
        public List<string> instructions = new List<string>();
        public Dictionary<string, long> DictionaryRegisters = new Dictionary<string, long>();
        public Dictionary<string, long> DictrionaryMemory = new Dictionary<string, long>();
        public Instructions()
        {
            instructions.Add("add");
            instructions.Add("sub");
            instructions.Add("and");
            instructions.Add("or");
            instructions.Add("not");
            instructions.Add("load");
            instructions.Add("store");
            instructions.Add("mov");
            instructions.Add("cmp");
            instructions.Add("je");
            instructions.Add("jne");
            instructions.Add("jge");
            instructions.Add("jl");
            DictionaryRegisters.Add("rax", 0);
            DictionaryRegisters.Add("rcx", 0);
            DictionaryRegisters.Add("rsi", 0);
            DictionaryRegisters.Add("rdi", 0);
            for (int i = 0; i < 10; i++)
            {
                var address = "0x" + startingaddress.ToString();
                DictrionaryMemory.Add(address, 0);
                startingaddress++;
            }
        }
        public void AddMemorySpace(string data)
        {
                DictrionaryMemory.Add(data, 0);
        }

        public void Add(string data1, string data2)
        {
            if (DictionaryRegisters.ContainsKey(data1) && DictionaryRegisters.ContainsKey(data2))
            {
                DictionaryRegisters[data1] += DictionaryRegisters[data2];
            }
            else if (DictionaryRegisters.ContainsKey(data1) && Syntax.IsMemoryAddress(data2))
            {
                if (DictrionaryMemory.ContainsKey(data2))
                {
                    DictionaryRegisters[data1] += DictrionaryMemory[data2];
                }
                else
                {
                    AddMemorySpace(data2);
                    DictionaryRegisters[data1] += DictrionaryMemory[data2];
                }
            }
            else if (Syntax.IsMemoryAddress(data1) && DictionaryRegisters.ContainsKey(data2))
            {
                if (DictrionaryMemory.ContainsKey(data1))
                {
                    DictrionaryMemory[data1] += DictionaryRegisters[data2];
                }
                else
                {
                    AddMemorySpace(data1);
                    DictrionaryMemory[data1] += DictionaryRegisters[data2];
                }
            }
            else if (DictionaryRegisters.ContainsKey(data1) && Syntax.IsNumber(data2))
            {
                DictionaryRegisters[data1] += long.Parse(data2);
            }
            else if (Syntax.IsMemoryAddress(data1) && Syntax.IsNumber(data2))
            {
                if (DictrionaryMemory.ContainsKey(data1))
                {
                    DictrionaryMemory[data1] += long.Parse(data2);
                }
                else
                {
                    AddMemorySpace(data1);
                    DictrionaryMemory[data1] += long.Parse(data2);
                }
            }
        }
        public void Sub(string data1, string data2)
        {
            if (DictionaryRegisters.ContainsKey(data1) && DictionaryRegisters.ContainsKey(data2))
            {
                DictionaryRegisters[data1] -= DictionaryRegisters[data2];
            }
            else if (DictionaryRegisters.ContainsKey(data1) && Syntax.IsMemoryAddress(data2))
            {
                if (DictrionaryMemory.ContainsKey(data2))
                {
                    DictionaryRegisters[data1] -= DictrionaryMemory[data2];
                }
                else
                {
                    AddMemorySpace(data2);
                    DictionaryRegisters[data1] -= DictrionaryMemory[data2];
                }
            }
            else if (Syntax.IsMemoryAddress(data1) && DictionaryRegisters.ContainsKey(data2))
            {
                if (DictrionaryMemory.ContainsKey(data1))
                {
                    DictrionaryMemory[data1] -= DictionaryRegisters[data2];
                }
                else
                {
                    AddMemorySpace(data1);
                    DictrionaryMemory[data1] -= DictionaryRegisters[data2];
                }
            }
            else if (DictionaryRegisters.ContainsKey(data1) && Syntax.IsNumber(data2))
            {
                DictionaryRegisters[data1] -= long.Parse(data2);
            }
            else if (Syntax.IsMemoryAddress(data1) && Syntax.IsNumber(data2))
            {
                if (DictrionaryMemory.ContainsKey(data1))
                {
                    DictrionaryMemory[data1] -= long.Parse(data2);
                }
                else
                {
                    AddMemorySpace(data1);
                    DictrionaryMemory[data1] -= long.Parse(data2);
                }
            }
        }
        public void And(string data1, string data2)
        {
            if (DictionaryRegisters.ContainsKey(data1) && DictionaryRegisters.ContainsKey(data2))
            {
                DictionaryRegisters[data1] &= DictionaryRegisters[data2];
            }
            else if (DictionaryRegisters.ContainsKey(data1) && Syntax.IsMemoryAddress(data2))
            {
                if (DictrionaryMemory.ContainsKey(data2))
                {
                    DictionaryRegisters[data1] &= DictrionaryMemory[data2];
                }
                else
                {
                    AddMemorySpace(data2);
                    DictionaryRegisters[data1] &= DictrionaryMemory[data2];
                }
            }
            else if (Syntax.IsMemoryAddress(data1) && DictionaryRegisters.ContainsKey(data2))
            {
                if (DictrionaryMemory.ContainsKey(data1))
                {
                    DictrionaryMemory[data1] &= DictionaryRegisters[data2];
                }
                else
                {
                    AddMemorySpace(data1);
                    DictrionaryMemory[data1] &= DictionaryRegisters[data2];
                }
            }
            else if (DictionaryRegisters.ContainsKey(data1) && Syntax.IsNumber(data2))
            {
                DictionaryRegisters[data1] &= long.Parse(data2);
            }
            else if (Syntax.IsMemoryAddress(data1) && Syntax.IsNumber(data2))
            {
                if (DictrionaryMemory.ContainsKey(data1))
                {
                    DictrionaryMemory[data1] &= long.Parse(data2);
                }
                else
                {
                    AddMemorySpace(data1);
                    DictrionaryMemory[data1] &= long.Parse(data2);
                }
            }
        }
        public void Mov(string data1, string data2)
        {
            if (DictionaryRegisters.ContainsKey(data1) && DictionaryRegisters.ContainsKey(data2))
            {
                DictionaryRegisters[data1] = DictionaryRegisters[data2];
            }
            else if (DictionaryRegisters.ContainsKey(data1) && Syntax.IsMemoryAddress(data2))
            {
                if (DictrionaryMemory.ContainsKey(data2))
                {
                    DictionaryRegisters[data1] = DictrionaryMemory[data2];
                }
                else
                {
                    AddMemorySpace(data2); 
                    DictionaryRegisters[data1] = DictrionaryMemory[data2];
                }
            }
            else if (Syntax.IsMemoryAddress(data1) && DictionaryRegisters.ContainsKey(data2))
            {
                if (DictrionaryMemory.ContainsKey(data1))
                {
                    DictrionaryMemory[data1] = DictionaryRegisters[data2];
                }
                else
                {
                    AddMemorySpace(data1);
                    DictrionaryMemory[data1] = DictionaryRegisters[data2];
                }
            }
            else if (DictionaryRegisters.ContainsKey(data1) && Syntax.IsNumber(data2))
            {
                DictionaryRegisters[data1] = long.Parse(data2);
            }
            else if (Syntax.IsMemoryAddress(data1) && Syntax.IsNumber(data2))
            {
                if (DictrionaryMemory.ContainsKey(data1))
                {
                    DictrionaryMemory[data1] = long.Parse(data2);
                }
                else
                {
                    AddMemorySpace(data1);
                    DictrionaryMemory[data1] = long.Parse(data2);
                }
            }
        }
        public void Or(string data1, string data2)
        {
            if (DictionaryRegisters.ContainsKey(data1) && DictionaryRegisters.ContainsKey(data2))
            {
                DictionaryRegisters[data1] |= DictionaryRegisters[data2];
            }
            else if (DictionaryRegisters.ContainsKey(data1) && Syntax.IsMemoryAddress(data2))
            {
                if (DictrionaryMemory.ContainsKey(data2))
                {
                    DictionaryRegisters[data1] |= DictrionaryMemory[data2];
                }
                else
                {
                    AddMemorySpace(data2);
                    DictionaryRegisters[data1] |= DictrionaryMemory[data2];
                }
            }
            else if (Syntax.IsMemoryAddress(data1) && DictionaryRegisters.ContainsKey(data2))
            {
                if (DictrionaryMemory.ContainsKey(data1))
                {
                    DictrionaryMemory[data1] |= DictionaryRegisters[data2];
                }
                else
                {
                    AddMemorySpace(data1);
                    DictrionaryMemory[data1] |= DictionaryRegisters[data2];
                }
            }
            else if (DictionaryRegisters.ContainsKey(data1) && Syntax.IsNumber(data2))
            {
                DictionaryRegisters[data1] |= long.Parse(data2);
            }
            else if (Syntax.IsMemoryAddress(data1) && Syntax.IsNumber(data2))
            {
                if (DictrionaryMemory.ContainsKey(data1))
                {
                    DictrionaryMemory[data1] |= long.Parse(data2);
                }
                else
                {
                    AddMemorySpace(data1);
                    DictrionaryMemory[data1] |= long.Parse(data2);
                }
            }
        }
        public void Cmp(string data1, string data2)
        {
            long result=0;
            if(DictionaryRegisters.ContainsKey(data1) && DictionaryRegisters.ContainsKey(data2))
            {
                result = DictionaryRegisters[data1] - DictionaryRegisters[data2];
            }
            else if(DictionaryRegisters.ContainsKey(data1) && Syntax.IsMemoryAddress(data2))
            {
                if (DictrionaryMemory.ContainsKey(data2))
                {
                    result = DictionaryRegisters[data1] - DictrionaryMemory[data2];
                }
                else
                {
                    AddMemorySpace(data2);
                    result = DictionaryRegisters[data1] - DictrionaryMemory[data2];
                }
            }
            else if(Syntax.IsMemoryAddress(data1) && DictionaryRegisters.ContainsKey(data2))
            {
                if (DictrionaryMemory.ContainsKey(data1))
                {
                    result = DictrionaryMemory[data1] - DictionaryRegisters[data2];
                }
                else
                {
                    AddMemorySpace(data1);
                    result = DictrionaryMemory[data1] - DictionaryRegisters[data2];
                }
            }
            else if(DictionaryRegisters.ContainsKey(data1) && Syntax.IsNumber(data2))
            {
                result = DictionaryRegisters[data1] - long.Parse(data2);
            }
            else if(Syntax.IsMemoryAddress(data1) && Syntax.IsNumber(data2))
            {
                if (DictrionaryMemory.ContainsKey(data1))
                {
                    result = DictrionaryMemory[data1] - long.Parse(data2);
                }
                else
                {
                    AddMemorySpace(data1);
                    result = DictrionaryMemory[data1] - long.Parse(data2);
                }
            }
            else if(Syntax.IsNumber(data1) && DictionaryRegisters.ContainsKey(data2))
            {
                result = long.Parse(data1) - DictionaryRegisters[data2];
            }
            else if(Syntax.IsNumber(data1) && Syntax.IsMemoryAddress(data2))
            {
                if (DictrionaryMemory.ContainsKey(data2))
                {
                    result = long.Parse(data1) - DictrionaryMemory[data2];
                }
                else
                {
                    AddMemorySpace(data2);
                    result = long.Parse(data1) - DictrionaryMemory[data2];
                }
            }
            if(result>0)
            {
                flags.zf = 0;
                flags.sf = 0;
                flags.of = 0; //unsigned arithmetics
            }
            else if(result<0)
            {
                flags.zf = 0;
                flags.sf = 1;
                flags.of = 0; //unsigned arithmetics
            }
            else if(result==0)
            {
                flags.zf = 1;
                flags.sf = 0;
                flags.of = 0; //unsigned arithmetics
            }

        }
        public void Not(string data1)
        {
            if(DictionaryRegisters.ContainsKey(data1))
            {
                DictionaryRegisters[data1] = ~DictionaryRegisters[data1];
            }
        }
        public void Load()
        {
            long rang = DictionaryRegisters["rsi"];
            string address = "0x" + rang.ToString();
            if (DictrionaryMemory.ContainsKey(address))
            {
                DictionaryRegisters["rax"] = DictrionaryMemory[address];
                DictionaryRegisters["rsi"]++;
            }
            else
                throw new Exception("Out of Memory");

        }
        public void Store()
        {
            long rang = DictionaryRegisters["rdi"];
            string address = "0x" + rang.ToString();
            if (DictrionaryMemory.ContainsKey(address))
            {
                DictrionaryMemory[address] = DictionaryRegisters["rax"];
                DictionaryRegisters["rdi"]++;
            }
            else
                throw new Exception("Out of Memory");
        }   
    }
}
