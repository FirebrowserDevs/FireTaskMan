using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireTaskMan.Helpers
{
    public class ProcessInfo : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double CpuUsage { get; set; }
        public long MemoryUsage { get; set; }
        public ProcessPriorityClass Priority { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public void Kill()
        {
            Process.GetProcessById(Id)?.Kill();
        }

        public void SetPriority(ProcessPriorityClass priority)
        {
           
        }
    }
}
