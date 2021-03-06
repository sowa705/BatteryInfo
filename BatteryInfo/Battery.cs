﻿using System;

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
            try
            {
                if (!(bool)WMITool.QuerySingle("root/WMI", "BatteryStatus", "Active"))
                {
                    Status = BatteryStatus.Unavailable;
                    return;
                }
                Status = BatteryStatus.Discharging;
                if ((bool)WMITool.QuerySingle("root/WMI", "BatteryStatus", "Charging"))
                    Status = BatteryStatus.Charging;
            }
            catch (Exception)
            {
                Status = BatteryStatus.Unavailable;
                return;
            }
            try
            {
                NominalCapacity = ((uint)WMITool.QuerySingle("root/WMI", "BatteryStaticData", "DesignedCapacity")) / 1000f;
            }
            catch { }

            try
            {
                RealCapacity = ((uint)WMITool.QuerySingle("root/WMI", "BatteryFullChargedCapacity", "FullChargedCapacity")) / 1000f;
            }
            catch { }
            try
            {
                CurrentCapacity = ((uint)WMITool.QuerySingle("root/WMI", "BatteryStatus", "RemainingCapacity")) / 1000f;
            }
            catch { }
            try
            {
                ChargePercent = CurrentCapacity / RealCapacity * 100f;
            }
            catch { }
            try
            {
                CurrentVoltage = ((uint)WMITool.QuerySingle("root/WMI", "BatteryStatus", "Voltage")) / 1000f;
            }
            catch { }
            try
            {
                TimeToDischarge = TimeSpan.FromSeconds((uint)WMITool.QuerySingle("root/WMI", "BatteryRuntime", "EstimatedRuntime"));
            }
            catch { }
            if (Status == BatteryStatus.Discharging)
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
