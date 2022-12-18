using System;
namespace AssemblerSimulation
{

    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Syntax syntax = new Syntax();
                Console.WriteLine("Enter the name of the script");
                string filename = Console.ReadLine();
                if (filename.Contains('.'))
                {
                    if (!filename.EndsWith("txt"))
                    {
                        throw new Exception("Invalid extension");
                    }
                }
                else if (!filename.Contains('.'))
                {
                    filename = filename + ".txt";
                }
                Console.WriteLine("Debugging Mode/Normal Mode [0|1]");
                var choice = Console.ReadLine();
                Console.Clear();
                if (choice == "1")
                {
                    syntax.SingleStepMode(filename);
                }
                else if (choice == "0")
                {
                    syntax.DebuggingMode(filename);
                }
            }
            catch(Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
            Console.ReadLine();
        }
    }
}