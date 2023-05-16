﻿using System;

namespace Common
{
    public class LNode
    {
        public int Data;
        public LNode Next;
    }

    public static class NodeHelper
    {
        public static void ConsoleNode(LNode node)
        {
            Console.WriteLine("Result:");
            LNode head = node;
            head.Next = null;
            LNode tmp = null;
            LNode cur = head;
            for (cur = head.Next; cur != null; cur = cur.Next)
            {
                Console.Write(cur.Data + "  ");
            }
        }

        public static LNode GetTestNode()
        {
            LNode head = new LNode();
            head.Next = null;
            LNode tmp = null;
            LNode cur = head;
            for (int i = 0; i < 8; i++)
            {
                tmp = new LNode();
                tmp.Data = i;
                tmp.Next = null;
                cur.Next = tmp;
                cur = tmp;
            }

            Console.WriteLine("Node:");
            for (cur = head.Next; cur != null; cur = cur.Next)
            {
                Console.Write(cur.Data + "  ");
            }

            return head;
        }
    }
}