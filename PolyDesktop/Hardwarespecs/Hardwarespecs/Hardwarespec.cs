using System;
using System.Management; // NuGet this if it throws an error
/**************************************************************
 * Copyright (c) 2021
 * Author: Jerron Rhen
 * Filename: Hardwarespec.cs
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
           Console.WriteLine( GetPCid());
            Console.WriteLine(GetPCName());
            Console.WriteLine(GetCPUInfo());
            Console.WriteLine(GetGPUInfo());
            Console.WriteLine(GetCpuSpeedInGHz());
            Console.WriteLine(GetRAMsize());
            Console.WriteLine(GetStorageInfo());
            Console.WriteLine(GetRAMspeed());
        }

        private static string GetPCid()
        {
            ManagementClass mc = new ManagementClass("win32_DiskDrive");
            ManagementObjectCollection moc = mc.GetInstances();
            String info = string.Empty;
            foreach (ManagementObject mo in moc)
            {
               
            
                if ((string)mo["MediaType"] == "Fixed hard disk media")
                {
                     info += (string)mo["SerialNumber"];
                }
            }
            return(info);
        }

        private static string GetStorageInfo()
        {
            ManagementClass mc = new ManagementClass("win32_LogicalDisk");
            ManagementObjectCollection moc = mc.GetInstances();
            String info = string.Empty;
            UInt64 t = 0;
            UInt64 Free = 0;
            foreach (ManagementObject mo in moc)                                          // Goes through all storage drives connected this dose include Thumb drives currently
            {

                t = (UInt64)mo.Properties["size"].Value;
                t = (t / 1000) / 1000 / 1000 + 1;                                                              // GB conversion  
                Free = (UInt64)mo["FreeSpace"];
                Free = (Free / 1000) / 1000 / 1000 + 1;                                                  // GB conversion
                info = (string)mo["Name"] + " has " + Free.ToString() + "GB available of " + t.ToString() + "GB";
                break;
            }
            return (info);
        }

        static string GetGPUInfo()
        {
            ManagementClass mc = new ManagementClass("win32_VideoController");
            ManagementObjectCollection moc = mc.GetInstances();
            String info = string.Empty;

            foreach (ManagementObject mo in moc)
            {
                info += (string)mo["Name"] + " ";
            }
            return(info);
        }
        static string GetCpuSpeedInGHz()
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
            return(GHz.ToString());
        }

        static string GetCPUInfo()
        {
            ManagementClass mc = new ManagementClass("win32_processor");
            ManagementObjectCollection moc = mc.GetInstances();
            String info = string.Empty;
            foreach (ManagementObject mo in moc)
            {    
                info = (string)mo["Name"];
                //name = name.Replace("(TM)", "™").Replace("(tm)", "™").Replace("(R)", "®").Replace("(r)", "®").Replace("(C)", "©").Replace("(c)", "©").Replace("    ", " ").Replace("  ", " ");
            }
            return(info);
        }
        static string GetPCName()
        {
            ManagementClass mc = new ManagementClass("Win32_ComputerSystem");
            ManagementObjectCollection moc = mc.GetInstances();
            String info = String.Empty;
            foreach (ManagementObject mo in moc)
            {
                info = (string)mo["Name"];
            }
            return(info);
        }
        static string GetRAMsize()
        {
            ManagementClass mc = new ManagementClass("Win32_PhysicalMemory");
            ManagementObjectCollection oCollection = mc.GetInstances();

            long MemSize = 0;
            long mCap = 0;
            String info = String.Empty;
            // In case more than one Memory sticks are installed
            foreach (ManagementObject obj in oCollection)
            {
                mCap = Convert.ToInt64(obj["Capacity"]);
                MemSize += mCap;  
            }
            MemSize = (MemSize / 1024) / 1024 / 1024; // conversion to GB
            return("Ram: " + MemSize.ToString() + "GB");
        }
        static string GetRAMspeed()
        {
            ManagementClass mc = new ManagementClass("Win32_PhysicalMemory");
            ManagementObjectCollection oCollection = mc.GetInstances();
            UInt32 t = 0;
            // In case more than one Memory sticks are installed
            foreach (ManagementObject obj in oCollection)
            {
                t = (UInt32)obj["Speed"];
            }
            return ("RamSpeed: " + t.ToString());
        }
    }
}
