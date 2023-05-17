using Common;

namespace a0._0_链表基础操作
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            //NodeHelper.GetTestNode();
            
            //AddLinkList();

            DelLinkNode();
        }

       

        #region 单链表添加
        public static void AddLink()
        {
            LNode node = new LNode();
            node.Data = 1;
            node.Next = new LNode(2);
            node.Next.Next = new LNode(3);
            NodeHelper.ConsoleNode(node);
        }
        public static void AddLinkList()
        {
            //--思路
            //--创建一个临时节点和一个当前节点
            //--1.当前节点指向 头结点（head）
            //--2.循环添加，存储临时节点
            //--3.当前节点指向 临时节点
            //--4.当前节点下移 指向下一节点（临时节点）
            int i = 1;
            //t头结点
            LNode head=new LNode(0);
            LNode tmp = null;
            //当前节点==头结点
            LNode cur = head;
            while (i <= 8)
            {
                //新的节点
                tmp = new LNode(i);
                //新的节点  指向--> 当前节点的下一节点
                cur.Next = tmp;
                //当前节点 向下移-->
                cur = tmp;
                i++;
            }
            NodeHelper.ConsoleNode(head,"循环添加节点");
        }
        #endregion

        #region 单链表删除

        public static void DelLinkNode()
        {
            LNode head = NodeHelper.GetTestNode();

            // 1.仅以移除头结点
            //LNode cur = head.Next;
            
            //2.移除中间的节点
            int target = 4;
            LNode cur = head;
            for (; cur.Next != null; cur = cur.Next)
            {
                if (cur.Next.Data==target)
                {
                    LNode node = cur.Next.Next;
                    cur.Next = node;
                }
            }

            
            NodeHelper.ConsoleNode(head);
        }
        

        #endregion
        
    }
}