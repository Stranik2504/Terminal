using System;
using System.IO;
using System.Threading;
using Newtonsoft.Json;

namespace Terminal.Classes
{
    public class Manager<T>
    {
        public event Action? Loaded;
        public event Action? Saved;

        private readonly Mutex _mutex = new();
        private readonly string _path;

        public Manager(string path) => _path = path;

        public T Load()
        {
            _mutex.WaitOne();
            
            using var stream = File.OpenText(_path);
            var obj = JsonConvert.DeserializeObject<T>(stream.ReadToEnd());

            _mutex.ReleaseMutex();

            Loaded?.Invoke();
            return obj;
        }

        public void Save(T obj)
        {
            if (obj == null)
                return;
            
            _mutex.WaitOne();

            using var stream = File.CreateText(_path);
            stream.WriteLine(JsonConvert.SerializeObject(obj));

            _mutex.ReleaseMutex();
            
            Saved?.Invoke();
        }
    }
}