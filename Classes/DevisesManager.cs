using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;

namespace Terminal.Classes
{
    internal class DevisesManager
    {
        static DriveInfo[] prevDevices = null;
        static DriveInfo[] nowDevices;
        // static int prevAmout = 0;
        //static int nowAmout = 0;
        static Action<string> Connected;
        static void Main(string[] args)
        {
            Thread t = new Thread(Test);
            t.Start();
            Connected += Test2;
        }
        static private void Test2(string text)
        {
            Console.WriteLine(text);
        }
        static void Test()
        {
            prevDevices = DriveInfo.GetDrives();
            while (true)
            {

                nowDevices = DriveInfo.GetDrives();
                //nowAmout = prevDevices.Length;
                Thread.Sleep(1000);
                Console.Clear();
                try
                {
                    foreach (var nowItem in nowDevices)
                    {
                        bool isFouned = false;
                        foreach (var prevItem in prevDevices)
                        {
                            if (prevItem == nowItem)
                            {
                                isFouned = true;
                            }
                        }
                        if (!isFouned)
                        {
                            Connected(nowItem.Name);
                        }
                    }
                }
                catch (IOException e)
                {
                    Console.WriteLine(e.GetType().Name);
                }
                prevDevices = DriveInfo.GetDrives();
            }
        }
    }
}
