using System;
using System.Collections.Generic;

namespace LinkedList_Implementation
{
    class Program
    {
        static void Main(string[] args)
        {
            CustomLinkedList<int> myList = new();
           
            for (int i = 1; i < 6; i++)
            {
                myList.Add(i);
            }

            var check = myList.Remove(3);
            Console.WriteLine(check);
            Console.WriteLine();

            foreach (var item in myList)
            {
                Console.WriteLine(item);
            }           
        }
    }
}
