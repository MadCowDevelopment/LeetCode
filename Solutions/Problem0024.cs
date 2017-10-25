using Xunit;

namespace Solutions
{
    public class Problem0024
    {
        [Theory]
        [InlineData(new int[0], new int[0])]
        [InlineData(new[] { 1 }, new[] { 1})]
        [InlineData(new[] { 1, 2, 3, 4 }, new[] { 2, 1, 4, 3 })]
        [InlineData(new[] { 1, 2, 3, 4, 5 }, new[] { 2, 1, 4, 3, 5 })]
        public void Solution(int[] input, int[] expected)
        {
            var head = ConvertArrayToListNode(input);

            AssertEquality(ConvertArrayToListNode(expected), SwapPairs(head));
        }

        private static ListNode SwapPairs(ListNode head)
        {
            if (head == null) return null;
            if (head.next == null) return head;

            ListNode next = head.next;
            ListNode current = head;
            while (current?.next != null)
            {
                ListNode a1 = current;
                ListNode a2 = current.next;
                ListNode a3 = current.next.next;

                ListNode a4 = current.next.next?.next;
                a1.next = a4 ?? a3;
                a2.next = a1;

                current = a3;
            }

            return next;
        }

        private void AssertEquality(ListNode expected, ListNode actual)
        {
            ListNode currentExpected = expected;
            ListNode currentActual = actual;
            do
            {
                Assert.Equal(currentExpected?.val, currentActual?.val);
                currentExpected = currentExpected?.next;
                currentActual = currentActual?.next;
            } while (currentExpected != null || currentActual != null);
        }

        private ListNode ConvertArrayToListNode(int[] input)
        {
            ListNode head = null;

            if (input.Length > 0)
            {
                head = new ListNode(input[0]);
                ListNode current = head;

                for (int i = 1; i < input.Length; i++)
                {
                    current.next = new ListNode(input[i]);
                    current = current.next;
                }
            }

            return head;
        }

        public class ListNode
        {
            public int val;
            public ListNode next;

            public ListNode(int x)
            {
                val = x;
            }

            public override string ToString()
            {
                return val.ToString();
            }
        }
    }
}
