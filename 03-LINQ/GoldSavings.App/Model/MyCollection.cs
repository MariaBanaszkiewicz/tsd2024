using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace GoldSavings.App.Model
{
    public class MyCollection<T> : ICollection<T>
    {

        private Random rnd = new Random();

        private List<T> list;

        public MyCollection(List<T> l) { 
            list = l;
        }


        public int Count => list.Count;

        public bool IsReadOnly => false;

        public bool IsEmpty => list.Count == 0;

        public void Add(T item)
        {
            int i = rnd.Next(0, 1);
            if (i % 2 == 0)
            {
                list.Add(item);
            } else
            {
                list.Insert(0, item);
            }
        }

        public T Get(int index)
        {
            int i = rnd.Next(0, index);

            return list[i];
        }

        public void Clear()
        {
            list.Clear();
        }

        public bool Contains(T item)
        {
            return list.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            list.CopyTo(array, arrayIndex);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        public bool Remove(T item)
        {
            return list.Remove(item);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return list.GetEnumerator();
        }
    }
}
