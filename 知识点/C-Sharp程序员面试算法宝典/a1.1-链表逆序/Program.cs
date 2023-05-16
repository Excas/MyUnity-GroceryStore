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

        public static LNode Reverse(LNode test)
        {
            LNode preNode = null;
            LNode curNode = test;
            LNode nextNode = null;
            for (; curNode.Next!=null; nextNode=curNode.Next)
            {
                nextNode = curNode.Next;
                curNode.Next = null;
                curNode = nextNode;
            }

            return curNode;
        }
    }
}