using System;
using System.Management; // NuGet this if it throws an error
/**************************************************************
 * Copyright (c) 2021
 * Author: Jerron Rhen
 * Filename: Program.cs
 * Date Created: 11/16/2021
 * Modifications:
 **************************************************************/
/**************************************************************
 * Overview:
 *      
 **************************************************************/
namespace Hardwarespecs
{
    class HardwareSpecPuller
    {
        static void Main(string[] args)
        {
            GetPCName();
            GetCPUInfo();
            GetGPUInfo();
           // GetCpuSpeedInGHz();
            GetRAMSize();
        }
        static void GetGPUInfo()
        {
            ManagementClass mc = new ManagementClass("win32_VideoController");
            ManagementObjectCollection moc = mc.GetInstances();
            String info = string.Empty;

            foreach (ManagementObject mo in moc)
            {
                info = (string)mo["Name"];
            }
            Console.WriteLine(info);
        }
        static void GetCpuSpeedInGHz()
        {
            double? GHz = null;
            using (ManagementClass mc = new ManagementClass("Win32_Processor"))
            {
                foreach (ManagementObject mo in mc.GetInstances())
                {
                    GHz = 0.001 * (UInt32)mo.Properties["MaxClockSpeed"].Value;
                    break;
                }
            }
            Console.WriteLine(GHz);
        }

        static void GetCPUInfo()
        {
            ManagementClass mc = new ManagementClass("win32_processor");
            ManagementObjectCollection moc = mc.GetInstances();
            String info = string.Empty;
            foreach (ManagementObject mo in moc)
            {
                info = (string)mo["Name"];
                //name = name.Replace("(TM)", "™").Replace("(tm)", "™").Replace("(R)", "®").Replace("(r)", "®").Replace("(C)", "©").Replace("(c)", "©").Replace("    ", " ").Replace("  ", " ");
            }
            Console.WriteLine(info);
        }
        static void GetPCName()
        {
            ManagementClass mc = new ManagementClass("Win32_ComputerSystem");
            ManagementObjectCollection moc = mc.GetInstances();
            String info = String.Empty;
            foreach (ManagementObject mo in moc)
            {
                info = (string)mo["Name"];
               
            }
            Console.WriteLine(info);
        }
        static void GetRAMSize()
        {
            ManagementScope oMs = new ManagementScope();
            ObjectQuery oQuery = new ObjectQuery("SELECT Capacity FROM Win32_PhysicalMemory");
            ManagementObjectSearcher oSearcher = new ManagementObjectSearcher(oMs, oQuery);
            ManagementObjectCollection oCollection = oSearcher.Get();

            long MemSize = 0;
            long mCap = 0;

             // In case more than one Memory sticks are installed
            foreach (ManagementObject obj in oCollection)
            {
                mCap = Convert.ToInt64(obj["Capacity"]);
                MemSize += mCap;
            }
            MemSize = (MemSize / 1024) / 1024 / 1024; // conversion to GB
            Console.WriteLine("Ram: " + MemSize.ToString() + "GB");
           
        }
    }
}
