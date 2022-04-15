using System;
using CustomLinkedListLib;
using NUnit.Framework;

namespace CustomLinkedNUnitTests
{
    public class CustomLinkedListTests
    {
        [Test]
        public void Test_AddFirst_Int()
        {
            // ARRANGE
            CustomLinkedList<int> myList = new();
            for (int i = 0; i < 5; i++)
            {
                myList.AddLast(i);
            }

            // ACT
            myList.AddFirst(100);

            // ASSERT
            Assert.AreEqual(100, myList.First.Value);
        }

        [Test]
        public void Test_AddLast_Int()
        {
            //  ARRANGE
            CustomLinkedList<int> myList = new();
            for (int i = 0; i < 5; i++)
            {
                myList.AddFirst(i);
            }

            // ACT
            myList.AddLast(100);

            // ASSERT
            Assert.AreEqual(100, myList.Last.Value);
        }

        [Test]
        public void Test_AddFirst_String()
        {
            // ARRANGE
            CustomLinkedList<string> myList = new();
            for (int i = 0; i < 5; i++)
            {
                myList.AddLast($"{i}");
            }

            // ACT
            myList.AddFirst("hello");

            // ASSERT
            Assert.AreEqual("hello", myList.First.Value);
        }

        [Test]
        public void Test_AddLast_String()
        {
            //  ARRANGE
            CustomLinkedList<string> myList = new();
            for (int i = 0; i < 5; i++)
            {
                myList.AddFirst($"{i}");
            }

            // ACT
            myList.AddLast("hello");

            // ASSERT
            Assert.AreEqual("hello", myList.Last.Value);
        }

        [Test]
        public void Test_AddAfter()
        {
            //  ARRANGE
            CustomLinkedList<string> myList = new();
            for (int i = 0; i < 5; i++)
            {
                myList.AddLast($"{i}");
            }

            var nodeToAddAfter = myList.First.NextNode;
            var valueOfNodeToAdd = "hello";

            // ACT
            myList.AddAfter(nodeToAddAfter, valueOfNodeToAdd);

            // ASSERT
            Assert.AreEqual(valueOfNodeToAdd, nodeToAddAfter.NextNode.Value);
        }

        [Test]
        public void Test_AddBefore()
        {
            //  ARRANGE
            CustomLinkedList<string> myList = new();
            for (int i = 0; i < 5; i++)
            {
                myList.AddLast($"{i}");
            }

            var nodeToAddAfter = myList.First.NextNode;
            var valueOfNodeToAdd = "hello";

            // ACT
            myList.AddBefore(nodeToAddAfter, valueOfNodeToAdd);

            // ASSERT
            Assert.AreEqual(valueOfNodeToAdd, nodeToAddAfter.PreviousNode.Value);
        }

        [Test]
        public void Test_Remove()
        {
            //  ARRANGE
            CustomLinkedList<string> myList = new();
            for (int i = 0; i < 5; i++)
            {
                myList.AddLast($"{i}");
            }

            var valueOfNodeToRemove = "3";

            // ACT
            bool isRemoved = myList.Remove(valueOfNodeToRemove);

            // ASSERT
            Assert.AreEqual(isRemoved, true);
        }

        [Test]
        public void Test_RemoveFirst()
        {
            //  ARRANGE
            CustomLinkedList<string> myList = new();
            for (int i = 0; i < 5; i++)
            {
                myList.AddLast($"{i}");
            }

            var firstBeforeRemoval = myList.First;

            // ACT
            myList.RemoveFirst();

            // ASSERT
            Assert.AreNotEqual(firstBeforeRemoval, myList.First);
        }

        [Test]
        public void Test_RemoveLast()
        {
            //  ARRANGE
            CustomLinkedList<string> myList = new();
            for (int i = 0; i < 5; i++)
            {
                myList.AddLast($"{i}");
            }

            var lastBeforeRemoval = myList.Last;

            // ACT
            myList.RemoveLast();

            // ASSERT
            Assert.AreNotEqual(lastBeforeRemoval, myList.Last);
        }

        [Test]
        public void Test_Find()
        {
            //  ARRANGE
            CustomLinkedList<string> myList = new();
            for (int i = 0; i < 5; i++)
            {
                myList.AddLast($"{i}");
            }
            myList.AddLast("4");

            var valueOfNodeToFind = "4";

            // ACT
            var foundNode = myList.Find(valueOfNodeToFind);

            // ASSERT
            Assert.AreSame(foundNode, myList.Last.PreviousNode);
        }

        [Test]
        public void Test_FindLast()
        {
            //  ARRANGE
            CustomLinkedList<string> myList = new();
            for (int i = 0; i < 5; i++)
            {
                myList.AddLast($"{i}");
            }
            myList.AddLast("4");

            var valueOfNodeToFind = "4";

            // ACT
            var foundNode = myList.FindLast(valueOfNodeToFind);

            // ASSERT
            Assert.AreSame(foundNode, myList.Last);
        }

        [Test]
        public void Test_Contains()
        {
            //  ARRANGE
            CustomLinkedList<string> myList = new();
            for (int i = 0; i < 5; i++)
            {
                myList.AddLast($"{i}");
            }

            var firstValueToCheck = "4";
            var secondValueToCheck = "100";

            // ACT
            bool containsFirst = myList.Contains(firstValueToCheck);
            bool containsSecond = myList.Contains(secondValueToCheck);

            // ASSERT
            Assert.Multiple(() =>
            {
                Assert.AreEqual(containsFirst, true);
                Assert.AreEqual(containsSecond, false);
            });
        }

        [Test]
        public void Test_ContainsNode()
        {
            //  ARRANGE
            CustomLinkedList<string> myList = new();
            for (int i = 0; i < 5; i++)
            {
                myList.AddLast($"{i}");
            }

            var nodeToCheck = myList.Last.PreviousNode;
       
            // ACT
            bool containsNode = myList.ContainsNode(nodeToCheck);

            // ASSERT
            Assert.Multiple(() =>
            {
                Assert.AreEqual(containsNode, true);
                Assert.Throws<ArgumentNullException>(() => myList.ContainsNode(null),
                message: "ArgumentNullException should be thrown, if the parameter equals to null");
            });
        }

        [Test]
        public void Test_Clear()
        {
            //  ARRANGE
            CustomLinkedList<string> myList = new();
            for (int i = 0; i < 5; i++)
            {
                myList.AddLast($"{i}");
            }

            var countBeforeClear = myList.Count;

            // ACT
            myList.Clear();

            // ASSERT
            Assert.Multiple(() =>
            {
                Assert.AreEqual(myList.Count, 0);
                Assert.AreNotEqual(myList.Count, countBeforeClear);
            });
        }
    }
}
