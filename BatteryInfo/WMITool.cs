using System;
using System.Management;

namespace BatteryInfo
{
    class WMITool
    {
        public static object QuerySingle(string scope ,string wmiClass,string propName)
        {
            ManagementObjectSearcher wmiQuery = new ManagementObjectSearcher(scope, $"SELECT * FROM {wmiClass}");

            ManagementObjectCollection objects = wmiQuery.Get();

            foreach (ManagementObject obj in objects)
            {
                if (obj != null)
                {
                    return obj[propName];
                }
            }
            throw new Exception();
        }
    }
}
