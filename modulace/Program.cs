using System;
using System.IO;

namespace modulace
{
    class Program
    {
        /*
        author:     Vaclav Bajtek
        project:    arp2020
        */
        static void Main(string[] args)
        {
            string input, modulated = "";
            Console.Write("input: ");
            input = Console.ReadLine();
            foreach (var bit in input)
            {
                switch (bit)
                {
                    case '0':
                        modulated += "PN";
                        break;
                    case '1':
                        modulated += "PP";
                        break;
                    default:
                        throw new Exception("Invalid I/O (0/1)");
                }
            }
            Console.WriteLine($"out FM: {modulated}");
            Console.ReadKey(true);
            modulated = "";
            bool first = true;
            for (int i = 0; i < input.Length; i++)
            {
                if (first)
                {
                    if (input[i] == '0')
                    {
                        modulated += "PN";
                    }
                    if (input[i] == '1')
                    {
                        modulated += "NP";
                    }
                    first = false;
                }
                else
                {
                    if (input[i] == '1')
                    {
                        modulated += "NP";
                    }
                    else
                    {
                        if (input[i] == '0' && input[i - 1] == '0')
                        {
                            modulated += "PN";
                        }
                        if (input[i] == '0' && input[i - 1] == '1')
                        {
                            modulated += "NN";
                        }
                    }
                }
            }
            Console.WriteLine($"out MFM: {modulated}");
            //rll
            modulated = "";
            while (input.Length > 0)
            {
                if (input.Length == 1)
                {
                    input += "00";
                }
                if (input[0] == '1')
                {
                    switch (input.Substring(0, 2))
                    {
                        case "10":
                            modulated += "NPNN";
                            input = input.Remove(0, 2);
                            break;
                        case "11":
                            modulated += "PNNN";
                            input = input.Remove(0, 2);
                            break;
                        default:
                            throw new Exception("error at 10/11");
                    }
                }
                else if (input[0] == '0')
                {
                    if (input.Substring(0, 3) == "000")
                    {
                        modulated += "NNNPNN";
                        input = input.Remove(0, 3);
                    }
                    else
                    {
                        if (input[1] == '1')
                        {
                            switch (input.Substring(0, 3))
                            {
                                case "010":
                                    modulated += "PNNPNN";
                                    input = input.Remove(0, 3);
                                    break;
                                case "011":
                                    modulated += "NNPNNN";
                                    input = input.Remove(0, 3);
                                    break;
                                default:
                                    throw new Exception("error at 010/011");
                            }
                        }
                        else
                        {
                            switch (input.Substring(0, 4))
                            {
                                case "0010":
                                    modulated += "NNPNNPNN";
                                    input = input.Remove(0, 4);
                                    break;
                                case "0011":
                                    modulated += "NNNNPNNN";
                                    input = input.Remove(0, 4);
                                    break;
                                default:
                                    throw new Exception("error at 0010/0011");
                            }
                        }
                    }
                }
                else
                {
                    throw new Exception("Invalid I/O _ _ _ _");
                }
            }
            Console.WriteLine($"RLL: {modulated}");
            Console.ReadKey(true);
        }
    }
}
