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
            GetPCid();
            GetPCName();
            GetCPUInfo();
            GetGPUInfo();
            GetCpuSpeedInGHz();
            GetRAMInfo();
            GetStorageInfo();
        }

        private static void GetPCid()
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
            Console.WriteLine(info);
        }

        private static void GetStorageInfo()
        {
            ManagementClass mc = new ManagementClass("win32_LogicalDisk");
            ManagementObjectCollection moc = mc.GetInstances();
            String info = string.Empty;
            UInt64 t = 0;
            UInt64 Free = 0;
            foreach (ManagementObject mo in moc)                                          // Goes through all storage drives connected
            {

                t = (UInt64)mo.Properties["size"].Value;
                t = (t / 1000) / 1000 / 1000 + 1;                                         // GB conversion  
                Free = (UInt64)mo["FreeSpace"];
                Free = (Free / 1000) / 1000 / 1000 + 1;                                   // GB conversion
                info = (string)mo["Name"] + " has " + Free.ToString() + "GB available of " + t.ToString() + "GB";
                Console.WriteLine(info);
            }
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
            Console.WriteLine(GHz.ToString());
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
        static void GetRAMInfo()
        {
            
            ManagementClass mc = new ManagementClass("Win32_PhysicalMemory");
            ManagementObjectCollection oCollection = mc.GetInstances();

            long MemSize = 0;
            long mCap = 0;
            String info = String.Empty;
            UInt32 t = 0;
            // In case more than one Memory sticks are installed
            foreach (ManagementObject obj in oCollection)
            {
                t = (UInt32)obj["Speed"];
                mCap = Convert.ToInt64(obj["Capacity"]);
                MemSize += mCap;  
            }
            MemSize = (MemSize / 1024) / 1024 / 1024; // conversion to GB
            Console.WriteLine("Ram: " + MemSize.ToString() + "GB");
            Console.WriteLine("RamSpeed: "+ t.ToString());

        }
    }
}
