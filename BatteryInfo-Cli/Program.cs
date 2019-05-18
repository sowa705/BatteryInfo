using System;
using System.Threading.Tasks;
using BatteryInfo;
namespace BatteryInfo_Cli
{
    class Program
    {
        static async Task Main(string[] args)
        {
            if (args.Length==1)
            {
                if (args[0].Equals("-r",StringComparison.OrdinalIgnoreCase))
                {
                    while (!Console.KeyAvailable)
                    {
                        WriteBatteryInfo();
                        await Task.Delay(2000);
                    }
                    return;
                }
                if (args[0].Equals("-h", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("BatteryInfo-Cli");
                    Console.WriteLine("https://github.com/sowa705/BatteryInfo");
                    Console.WriteLine("Arguments:");
                    Console.WriteLine("-r\tRepeats battery measurements until key is pressed");
                    Console.WriteLine("-h\tDisplays help");
                    Console.WriteLine("no args\tDisplays battery measurement");
                }
                else
                {
                    Console.WriteLine("Unknown argument: "+args[0]);
                    WriteBatteryInfo();
                }
            }
            else
            {
                WriteBatteryInfo();
            }
        }
        static void WriteBatteryInfo()
        {
            Battery b = new Battery();
            b.Update();
            Console.WriteLine("Battery Information:");
            Console.WriteLine("Status: \t"+b.Status);
            if (b.Status==BatteryStatus.Unavailable)
            {
                return;
            }
            Console.WriteLine("Charge: \t" + b.ChargePercent + " %");
            Console.WriteLine("Voltage:\t"+b.CurrentVoltage + " V");
            Console.WriteLine("Charge rate:\t" + b.ChargeRate + " W");
            Console.WriteLine("Time remaining:\t" + b.TimeToDischarge);
            Console.WriteLine();
        }
    }
}
