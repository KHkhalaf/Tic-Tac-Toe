using System;
using Tic_Tac_Toe.Controller;
using Tic_Tac_Toe.Models;

namespace Tic_Tac_Toe
{
    class Program
    {
        static void Main(string[] args)
        {
            
            //while (true)
            //{
            //    int n;double res;
            //    n = Convert.ToInt32(Console.ReadLine());
            //    res = (n == 1) ? 0 : 1 + Math.Log2(n / 2);
            //    Console.Write("\nlob(n)="+res+"\n");
            //}
            _Controller controller = new _Controller();
            Console.Write("-number of a grid or index of it inside the range [0,1,2,3,4,5,6,7,8,9]\n\n");
            controller.Level = 2; // Difficult
            controller.PlayGame();
            controller.ShowGrid();
        }
    }
}
