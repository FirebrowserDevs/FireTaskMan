using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace FireTaskMan.Helpers
{
    public class SysInfo
    {
      
            public static async Task<string> GetGpuBrandNamesAsync()
            {
                string brandNames = string.Empty;

                // Create a WMI query to get the GPU information
                string query = "SELECT Name FROM Win32_VideoController";

                // Create a new ManagementObjectSearcher object
                ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);

                // Get the GPU information and append the brand names to a StringBuilder
                foreach (ManagementObject obj in searcher.Get())
                {
                    string brandName = obj["Name"].ToString();
                    if (!string.IsNullOrEmpty(brandName))
                    {
                        brandNames += $"{brandName}\n";
                    }
                }

                return brandNames.TrimEnd(); // Trim any trailing newline characters
            }
        
    }
}
