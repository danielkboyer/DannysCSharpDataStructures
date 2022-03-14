using DannysCSharpDataStructures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
namespace UnitTests
{
    [TestClass]
    public class MinHeapTests
    {
        [TestMethod]
        public void SmallestOnTop()
        {
            MinHeap<Node> testHeap = new MinHeap<Node>();

            testHeap.Add(new Node("Large", 100));
            testHeap.Add(new Node("removeMe", 13));
            testHeap.RemoveMin();
            testHeap.Add(new Node("Small", 50));
            Assert.AreEqual("Small", testHeap.Peek().Name);
            testHeap.Add(new Node("Smallest", 20));

            Assert.AreEqual("Smallest", testHeap.Peek().Name);
        }
        [TestMethod]
        public void DecreaseKey()
        {
            MinHeap<Node> testHeap = new MinHeap<Node>();

            Node large = new Node("Large", 100);
            testHeap.Add(large);

            testHeap.Add(new Node("Small", 50));
            testHeap.Add(new Node("Smallest", 20));
            testHeap.Add(new Node("removeMe", 13));
            testHeap.RemoveMin();
            testHeap.DecreaseKey(large, new Node("NowSmallest", 19));

            Assert.AreEqual("NowSmallest", testHeap.Peek().Name);

        }

        [TestMethod]
        public void SameValuesInKeys()
        {
            MinHeap<Node> testHeap = new MinHeap<Node>();

            Node large = new Node("Same1", 100);
            testHeap.Add(large);

            testHeap.Add(new Node("Same2", 100));
            testHeap.Add(new Node("Same3", 100));


            Assert.AreEqual(3, testHeap.Count);

        }

        [TestMethod]
        public void TenMillionValues()
        {
            MinHeap<Node> testHeap = new MinHeap<Node>();

            Node large = new Node("Large", 10000001);
            testHeap.Add(large);
            for (int x = 10000000; x >= 0; x--)
            {
                testHeap.Add(new Node(x.ToString(), x));
            }

            Assert.AreEqual(0, testHeap.Peek().Value);
            testHeap.DecreaseKey(large, new Node("Smallest", -5));

            Assert.AreEqual(-5, testHeap.Peek().Value);
        }


        private class Node : IComparable<Node>
        {
            public string Name;
            public int Value;

            public Node(string name, int value)
            {
                Name = name;
                Value = value;
            }
            public int CompareTo(Node other)
            {
                return Value.CompareTo(other.Value);
            }
        }
    }

    
}
