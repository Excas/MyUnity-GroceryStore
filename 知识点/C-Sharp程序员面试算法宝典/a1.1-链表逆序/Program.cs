using Common;

namespace a1._1_链表逆序
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            LNode test=NodeHelper.GetTestNode();
            LNode res=Reverse(test);
            NodeHelper.ConsoleNode(res);
        }

        public static LNode Reverse(LNode head)
        {
            LNode pre, cur, next = null;

            cur = head.Next;
            next = cur.Next;
            cur.Next = null;
            pre = cur;
            cur = next;
            while (cur.Next!=null)
            {
                next = cur.Next;
                cur.Next = pre;
                pre = cur;
                cur = next;
            }

            cur.Next = pre;
            head.Next = cur;
            return head;
        }
    }
}