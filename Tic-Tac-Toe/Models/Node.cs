using System;
using System.Collections.Generic;
using System.Text;

namespace Tic_Tac_Toe.Models
{
    public class Node
    {
        public List<Node> listNode = new List<Node>();
        public bool isTerminalNode = false;
        public int value = 0;
        public void AddNode(int v = 0, bool IsterminalNode = false)
        {
            listNode.Add(new Node() {
            value = v,
            isTerminalNode = IsterminalNode
            });
        }
    }
}
