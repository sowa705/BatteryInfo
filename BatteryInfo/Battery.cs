using System;

namespace BatteryInfo
{
    public class Battery
    {
        public float NominalCapacity;  //Measured in Watt hours
        public float RealCapacity;
        public float CurrentCapacity;

        public float ChargePercent;

        public float CurrentVoltage;

        public TimeSpan TimeToDischarge;

        public BatteryStatus Status;

        public float ChargeRate;
        public void Update()
        {
            if (!(bool)WMITool.QuerySingle("root/WMI", "BatteryStatus", "Active"))
            {
                Status = BatteryStatus.Unavailable;
                return;
            }
            Status = BatteryStatus.Discharging;
            if ((bool)WMITool.QuerySingle("root/WMI", "BatteryStatus", "Charging"))
                Status = BatteryStatus.Charging;

            NominalCapacity = ((uint) WMITool.QuerySingle("root/WMI","BatteryStaticData","DesignedCapacity"))/1000f;
            RealCapacity = ((uint)WMITool.QuerySingle("root/WMI", "BatteryFullChargedCapacity", "FullChargedCapacity")) / 1000f;
            CurrentCapacity = ((uint)WMITool.QuerySingle("root/WMI", "BatteryStatus", "RemainingCapacity")) / 1000f;
            ChargePercent = CurrentCapacity / RealCapacity * 100f;

            CurrentVoltage= ((uint)WMITool.QuerySingle("root/WMI", "BatteryStatus", "Voltage")) / 1000f;
            TimeToDischarge = TimeSpan.FromSeconds((uint)WMITool.QuerySingle("root/WMI", "BatteryRuntime", "EstimatedRuntime"));
            if (Status==BatteryStatus.Discharging)
            {
                ChargeRate = -((int)WMITool.QuerySingle("root/WMI", "BatteryStatus", "DischargeRate")) / 1000f;
            }
            else
            {
                ChargeRate = ((int)WMITool.QuerySingle("root/WMI", "BatteryStatus", "ChargeRate")) / 1000f;
            }
        }
    }
}
