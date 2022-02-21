using System;
using System.Collections.Generic;

namespace LinkedList_Implementation
{
    class Program
    {
        static void Main(string[] args)
        {
            CustomLinkedList<int> myList = new();
            #region adding event handlers
            myList.AddedElement += CustomLinkedList_AddedElem;
            myList.RemovedElement += CustomLinkedList_RemovedElem;
            myList.ClearedCollection += CustomLinkedList_WasCleared;
            #endregion

            for (int i = 1; i < 6; i++)
            {
                myList.Add(i);
            }

            foreach(var item in myList)
            {
                Console.WriteLine(item);
            }

            var check = myList.Remove(3);
            myList.RemoveFirst();
            myList.RemoveLast();
            Console.WriteLine(check);
            Console.WriteLine();

            myList.Remove(2); 
            foreach (var item in myList)
            {
                Console.WriteLine(item);
            }
            myList.Clear();
        }

        #region event_handlers
        private static void CustomLinkedList_AddedElem<T>(T value)
        {
            Console.WriteLine($"Added new element to CustomLinkedList with value: {value}.");
        }

        private static void CustomLinkedList_RemovedElem<T>(T value)
        {
            Console.WriteLine($"Removed element from CustomLinkedList with value: {value}.");
        }

        private static void CustomLinkedList_WasCleared()
        {
            Console.WriteLine("CustomLinkedList was cleared!");
        }
        #endregion
    }
}
