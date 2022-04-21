using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;

namespace Terminal.Classes
{
    public static class DevicesManager
    {
        public static event Action<string> AddDisk;
        public static event Action<string> RemoveDisk;

        private static List<string> _disks = new();

        private static bool _isActive;

        private const int Delay = 1000;
        
        public static void StartLisining()
        {
            _isActive = true;
            new Thread(Update).Start();
        }

        public static void StopLisining()
        {
            _isActive = false;
        }

        private static void Update()
        {
            while (_isActive)
            {
                var drives = DriveInfo.GetDrives();
                var disks = new List<string>();

                foreach (var disk in drives)
                {
                    disks.Add(disk.Name);
                        
                    if (!_disks.Contains(disk.Name))
                    {
                        AddDisk?.Invoke(disk.Name);
                        _disks.Add(disk.Name);
                    }
                }

                for (int i = 0; i < _disks.Count; i++)
                {
                    if (!disks.Contains(_disks[i]))
                    {
                        RemoveDisk?.Invoke(_disks[i]);
                        _disks.RemoveAt(i);
                        i--;
                    }
                }

                Thread.Sleep(Delay);
            }
        }
    }
}
