using JobApplicationTracker.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobApplicationTracker.Services
{
    public class DataStore<T> where T : class, IEntity
    {
        private readonly List<T> items = new List<T>();

        public void Add(T item)
        {
            items.Add(item);
        }

        public T GetById(int id)
        {
            foreach (T item in items)
            {
                if (item.Id == id)
                    return item;


            }
            return null;

        }

        public bool Update(T item)
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].Id == item.Id)
                {
                    items[i] = item;
                    return true;
                }
            }
            return false;
        }

        public bool Remove(int id)
        {
            T item = GetById(id);
            if (item != null && items.Remove(item))

                return true;
            else return false;
        }

        public IReadOnlyList<T> GetAll()
        {
            return items.AsReadOnly();
        }

        public int Count()
        {
            return items.Count;
        }

        public int NextId()
        {
            int max = 0;
            foreach (T item in items)
            {
                if (item.Id > max)
                    max = item.Id;
            }
            return max + 1;
        }


    }
}
