using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Management;        // NuGet this if it throws an error
/**************************************************************
 * Copyright (c) 2021
 * Author: Jerron Rhen
 * Filename: Hardwarespec.cs
 * Date Created: 11/16/2021
 * Modifications:
 **************************************************************/
/**************************************************************
 * Overview: Pulls the Hardware information of the current computer (ONLY WORKS FOR WINDOWS MACHINES) and uploads/updates a data base
 *      
 **************************************************************/
namespace Hardwarespecs
{
    class HardwareSpecPuller
    {
        static void Main(string[] args)
        {
            Console.WriteLine(GetPCid());
            Console.WriteLine(GetPCName());
            Console.WriteLine(GetCPUInfo());
            Console.WriteLine(GetGPUInfo());
            Console.WriteLine(GetCpuSpeedInGHz());
            Console.WriteLine(GetRAMsize());
            Console.WriteLine(GetStorageInfo());
            Console.WriteLine(GetRAMspeed());
            Console.WriteLine("Press any key...");
            Console.ReadKey();

            //string connectionString = "server=satou.cset.oit.edu,5433; database=PolyDestopn; UID=PolyCode; password=P0lyC0d3";
            //String INquery = "INSERT INTO PolyDestopn.dbo.desktop Values('" + GetPCid() + "','"
            //  + GetPCName() + "' , '" + GetCPUInfo() + "' , '" + GetCpuSpeedInGHz() + "' , '"
            //  + GetGPUInfo() + "' , '" + GetRAMspeed() + "' , '" + GetRAMsize() + "' , '"
            //  + GetStorageInfo() + "');";
            //try
            //{
            //    using (SqlConnection conn = new SqlConnection(connectionString))
            //    {
            //        conn.Open();
            //        if (conn.State == System.Data.ConnectionState.Open)
            //        {
            //            using (SqlCommand cmd = conn.CreateCommand())
            //            {
            //                cmd.CommandText = INquery;
            //                cmd.ExecuteNonQuery();
            //            }
            //        }
            //    }
            //}
            //catch (Exception eSql)
            //{
            //    Debug.WriteLine("Exception: " + eSql.Message);
            //}

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
                    break;
                }
            }
            return info;
        }

        private static string GetStorageInfo()
        {
            ManagementClass mc = new ManagementClass("win32_LogicalDisk");
            ManagementObjectCollection moc = mc.GetInstances();
            String info = string.Empty;
            UInt64 t = 0;
            foreach (ManagementObject mo in moc)                                                       
            {
                t = (UInt64)mo.Properties["size"].Value;
                t = (t / 1000) / 1000 / 1000 + 1;                                                              // GB conversion: Storage companys cant decide if 1000 or 1024 per unit is needed for the next one, Windows uses 1000 per unit
                info = t.ToString() + "GB";
                break;
            }
            return info;
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
            return info;
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
            return GHz.ToString();
        }

        static string GetCPUInfo()
        {
            ManagementClass mc = new ManagementClass("win32_processor");
            ManagementObjectCollection moc = mc.GetInstances();
            String info = string.Empty;
            foreach (ManagementObject mo in moc)
            {
                info = (string)mo["Name"];
            }
            return info;
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
            return info;
        }
        static string GetRAMsize()
        {
            ManagementClass mc = new ManagementClass("Win32_PhysicalMemory");
            ManagementObjectCollection oCollection = mc.GetInstances();

            long MemSize = 0;
            long mCap = 0;
            String info = String.Empty;
            // In case more than one Memory stick are installed
            foreach (ManagementObject obj in oCollection)
            {
                mCap = Convert.ToInt64(obj["Capacity"]);
                MemSize += mCap;
            }
            MemSize = (MemSize / 1024) / 1024 / 1024;                   // conversion to GB
            return("Ram: " + MemSize.ToString() + "GB");
        }
        static string GetRAMspeed()
        {
            ManagementClass mc = new ManagementClass("Win32_PhysicalMemory");
            ManagementObjectCollection oCollection = mc.GetInstances();
            UInt32 t = 0;
            // In case more than one Memory stick are installed
            foreach (ManagementObject obj in oCollection)
            {
                t = (UInt32)obj["Speed"];
                break;
            }
            return ("RamSpeed: " + t.ToString());
        }
    }
}
