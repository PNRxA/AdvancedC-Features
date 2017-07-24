﻿using System.Collections;
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
    }
}
