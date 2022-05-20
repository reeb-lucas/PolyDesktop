using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Management;
using System.Text;
using System.IO;
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
        //static void Main(string[] args)
        //{
        //    WriteToDB();
        //}
        public static void WriteToDB()
        {
            string PCName = GetPCName();
            string CPUinfo = GetCPUInfo();
            string CPUspeed = GetCpuSpeedInGHz();
            string GPUinfo = GetGPUInfo();
            string RAMsize = GetRAMsize();
            string RAMSpeed = GetRAMspeed();
            string StorageInfo = GetStorageInfo();
            string UID;
            string connectionString = "server=satou.cset.oit.edu,5433; database=PolyDesktop; UID=PolyCode; password=P0lyC0d3";
            String INquery = "INSERT INTO PolyDesktop.dbo.desktop(c_ID, c_name, CPU, CPU_speed, GPU, RAM_speed, RAM_size, drive_size) Values(" + "@UID" + ","
                 + "@PCName" + " , " + "@CPUInfo" + " , " + "@CpuSpeed" + " , " + "@GPUInfo" + " , " + "@RAMspeed" + " , " + "@RAMsize" + " , " + "@StorageInfo" + ");";
            try
            {
                string fPath = "c:\\Program Files\\PolyDesktop\\UID.txt";
                UID = File.ReadAllText(fPath);
                string DELquery = "DELETE FROM PolyDesktop.dbo.desktop WHERE c_ID = " + UID;
                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        if (conn.State == System.Data.ConnectionState.Open)
                        {
                            using (SqlCommand cmd = conn.CreateCommand())
                            {
                                cmd.CommandText = DELquery;
                                cmd.ExecuteNonQuery();

                                cmd.CommandText = INquery;
                                cmd.Parameters.AddWithValue("@UID", UID);
                                cmd.Parameters.AddWithValue("@PCName", PCName);
                                cmd.Parameters.AddWithValue("@CPUInfo", CPUinfo);
                                cmd.Parameters.AddWithValue("@CpuSpeed", CPUspeed);
                                cmd.Parameters.AddWithValue("@GPUInfo", GPUinfo);
                                cmd.Parameters.AddWithValue("@RAMspeed", RAMSpeed);
                                cmd.Parameters.AddWithValue("@RAMsize", RAMsize);
                                cmd.Parameters.AddWithValue("@StorageInfo", StorageInfo);

                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                }
                catch (Exception eSql)
                {
                    Debug.WriteLine("Exception: " + eSql.Message);
                }
            }
            catch
            {
                UID = GetPCid();
                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        if (conn.State == System.Data.ConnectionState.Open)
                        {
                            using (SqlCommand cmd = conn.CreateCommand())
                            {
                                cmd.CommandText = INquery;
                                cmd.Parameters.AddWithValue("@UID", UID);
                                cmd.Parameters.AddWithValue("@PCName", PCName);
                                cmd.Parameters.AddWithValue("@CPUInfo", CPUinfo);
                                cmd.Parameters.AddWithValue("@CpuSpeed", CPUspeed);
                                cmd.Parameters.AddWithValue("@GPUInfo", GPUinfo);
                                cmd.Parameters.AddWithValue("@RAMspeed", RAMSpeed);
                                cmd.Parameters.AddWithValue("@RAMsize", RAMsize);
                                cmd.Parameters.AddWithValue("@StorageInfo", StorageInfo);

                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                }
                catch (Exception eSql)
                {
                    Debug.WriteLine("Exception: " + eSql.Message);
                }

            }
            // CREATE TABLE dbo.pesktop(c_ID int, c_name varchar(MAX), CPU varchar(MAX), CPU_speed float, GPU varchar(MAX), RAM_speed int, RAM_size int, drive_size float)
        }
        private static string GetPCid()
        {
            string connectionString = "server=satou.cset.oit.edu,5433; database=PolyDesktop; UID=PolyCode; password=P0lyC0d3";
            String info = string.Empty;
            String IDQuery = "DECLARE @myid uniqueidentifier = NEWID();  SELECT CONVERT(CHAR(36), @myid) AS 'char'; ";
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    if (conn.State == System.Data.ConnectionState.Open)
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.CommandText = IDQuery;
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    info = reader.GetString(0);                                 ////
                                    int count = 0;
                                    char ch = 'c';
                                    StringBuilder bs = new StringBuilder(info.Length);
                                    foreach (char c in info)
                                    {
                                        if (c < '0') continue;
                                        if (c > '9') continue;
                                        if (bs.Length == 0 && c == '0')
                                        {
                                            bs.Append('1');
                                            count++;
                                        }
                                        else if (c == ch) { }
                                        else
                                        {
                                            bs.Append(c);
                                            count++;
                                        }
                                        ch = bs[count - 1];
                                        if (count == 9)
                                        { break; }
                                    }
                                    info = bs.ToString();
                                    Directory.CreateDirectory("c:\\Program Files\\PolyDesktop\\");
                                    string fPath = "c:\\Program Files\\PolyDesktop\\UID.txt";
                                    File.WriteAllText(fPath, info);
                                    break;
                                }
                            }
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception eSql)
            {
                Debug.WriteLine("Exception: " + eSql.Message);
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
                info = t.ToString();
                break;
            }
            return info;
        }
        static string GetGPUInfo()
        {
            ManagementClass mc = new ManagementClass("win32_VideoController");
            ManagementObjectCollection moc = mc.GetInstances();
            String info = "";

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
            return (MemSize.ToString());
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
            return (t.ToString());
        }
    }
}