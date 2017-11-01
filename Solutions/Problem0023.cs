using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Solutions
{
    public class Problem0023
    {
        public class ListNode
        {
            public int val;
            public ListNode next;
            public ListNode(int x) { val = x; }
        }

        [Fact]
        public void NullNodesReturnsEmptyNode()
        {
            var result = Merge((ListNode[])null);

            Assert.Null(result);
        }

        [Fact]
        public void EmptyNodesReturnsEmptyNode()
        {
            var result = Merge(new ListNode[0]);

            Assert.Null(result);
        }

        [Theory]
        [InlineData(new int[] { 1 }, new[] { 1 })]
        [InlineData(new int[] { 1 }, new int[0], new[] { 1 })]
        [InlineData(new int[] { 0, 2, 5 }, new[] { 0, 2, 5 })]
        public void TestCases(params int[][] inputArrays)
        {
            var result = Merge(inputArrays);

            VerifyResult(inputArrays[0], result);
        }

        private void VerifyResult(int[] expected, ListNode actual)
        {
            int i = 0;
            var current = actual;
            bool hasNext;
            do
            {
                Assert.Equal(expected[i], current.val);
                if (current.next != null)
                {
                    current = current.next;
                    i++;
                    hasNext = true;
                }
                else
                {
                    hasNext = false;
                }
            } while (hasNext);
        }

        private ListNode Merge(params int[][] arrays)
        {
            var nodes = new List<ListNode>();

            for (int i = 1; i < arrays.Length; i++)
            {
                var array = arrays[i];
                if (array.Length == 0) continue;
                ListNode parent = new ListNode(array[0]);
                nodes.Add(parent);

                for (int j = 1; j < array.Length; j++)
                {
                    var node = new ListNode(array[j]);
                    parent.next = node;
                    parent = node;
                }
            }

            return Merge(nodes.ToArray());
        }

        private ListNode Merge(ListNode[] listNodes)
        {
            return new Solution().MergeKLists(listNodes);
        }

        public class Solution
        {
            public ListNode MergeKLists(ListNode[] lists)
            {
                Comparison<ListNode> comparer = (node1, node2) =>
                {
                    if (node1 == null && node2 == null) return 0;
                    if (node1 == null) return 1;
                    if (node2 == null) return -1;
                    return node1.val.CompareTo(node2.val);
                };

                if (lists == null || lists.Length == 0) return null;

                var heads = new List<ListNode>(lists);

                heads.Sort(comparer);
                var currentSmallest = heads.FirstOrDefault();
                if (currentSmallest == null) return null;
                heads.Remove(currentSmallest);
                if (currentSmallest.next != null) heads.Add(currentSmallest.next);
                heads.Sort(comparer);

                ListNode first = new ListNode(currentSmallest.val);
                ListNode current = first;
                while (true)
                {
                    currentSmallest = heads.FirstOrDefault();
                    if (currentSmallest == null) break;
                    current.next = currentSmallest;
                    current = currentSmallest;

                    heads.Remove(currentSmallest);
                    if (currentSmallest.next != null)
                    {
                        heads.Add(currentSmallest.next);
                        heads.Sort(comparer);
                    }
                }

                return first;
            }
        }
    }
}
