# BatteryInfo
Simple C# System battery information library
# Usage
Create new instance of BatteryInfo.Battery class and call Update().
~~~~
BatteryInfo.Battery battery = new BatteryInfo.Battery();
battery.Update();
Console.WriteLine("Charge: \t" + battery.ChargePercent + " %");
~~~~
# Limitations
BatteryInfo uses WML queries to get required data. WML may be unavailable on other operating systems than Windows.
