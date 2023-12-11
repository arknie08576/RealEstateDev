using Newtonsoft.Json;
using RealEstateDev.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RealEstateDev.Repositories
{
    public class FileRepository<T> : IRepository<T> where T : class, IEntity, new()
    {

        protected readonly List<T> _items = new();

        private string fileName;

        public event EventHandler<T>? ItemAdded;
        public event EventHandler<T>? ItemRemoved;

        public FileRepository() {
            fileName = "file.json";
            Load();
        }

        public void Add(T item)
        {

            item.Id = _items.Count + 1;
            _items.Add(item);
            ItemAdded.Invoke(this, item);
        }
        public void Remove(T item)
        {
            _items.Remove(item);
            ItemRemoved.Invoke(this, item);
        }

        public void Save()
        {
            JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
            //var json = JsonSerializer.Serialize<IEnumerable<T>>(_items);
            var json = JsonConvert.SerializeObject(_items, settings);
            File.WriteAllText(fileName, json);
        }
        public void Load()
        {
            if (File.Exists(fileName))
            {
                JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
                var json = File.ReadAllText(fileName);
                //var items = JsonSerializer.Deserialize<IEnumerable<T>>(json);
                var items = JsonConvert.DeserializeObject<List<T>>(json, settings);

                if (items != null)
                {
                    _items.Clear();
                    _items.AddRange(items);
                }
            }
        }
        public T GetById(int id)
        {
            return _items.Single(item => item.Id == id);
        }

        public IEnumerable<T> GetAll()
        {
            return _items.ToList();
        }
    }
}
