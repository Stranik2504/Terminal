using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;

namespace Terminal.Classes
{
    public class DevisesManager
    {
        private List<string> _disks = new();
        private bool _isActive;
        private const int Delay = 1000;
        
        public event Action<string> AddDisk;
        public event Action<string> RemoveDisk;

        public void StartLisining()
        {
            _isActive = true;
            new Thread(Update).Start();
        }

        public void StopLisining()
        {
            _isActive = false;
        }

        private void Update()
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
