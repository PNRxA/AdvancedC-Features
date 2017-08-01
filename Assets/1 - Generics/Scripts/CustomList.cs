using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Generics
{
    public class CustomList<T>
    {
        public T[] list;
        public int amount { get; private set; }
        //Default constructor
        public CustomList() { amount = 0; }
        private bool itemRemoved;
        //Override for List[0]
        public T this[int index]
        {
            get
            {
                return list[index];
            }
            set
            {
                list[index] = value;
            }
        }
        //Adds an item to the end of the list
        public void Add(T item)
        {
            //Create new array of amount +1
            T[] cache = new T[amount + 1];
            //Check if list is initialised
            if (list != null)
            {
                //Copy all existing items to new array
                for (int i = 0; i < list.Length; i++)
                {
                    cache[i] = list[i];
                }
            }
            //Plalce new item at end index
            cache[amount] = item;
            //Replace old array with new array
            list = cache;
            //Increment amount
            amount++;
        }

        public bool Contains(T item)
        {
            //For each item compare them
            foreach (var obj in list)
            {
                //If item exists return true
                if (EqualityComparer<T>.Default.Equals(obj, item))
                {
                    return true;
                }
            }
            //Else return false
            return false;
        }

        public void Clear()
        {
            //Empty list
            list = null;
            amount = 0;
        }

        public void Remove(T item)
        {
            // Create a new array of amount - 1
            T[] cache = new T[amount - 1];
            // Set boolean to false
            itemRemoved = false;
            // Check if the list has been initialized
            if (list != null)
            {
                // Copy all existing items to new array except removed item
                for (int i = 0; i < list.Length; i++)
                {
                    if (EqualityComparer<T>.Default.Equals(list[i], item))
                    {
                        itemRemoved = true;
                    }
                    else
                    {
                        cache[i] = itemRemoved ? list[i - 1] : list[i];
                    }
                }
            }
            // Replace old array with new array
            list = cache;
            // decrease amount
            amount--;
        }
    }
}
