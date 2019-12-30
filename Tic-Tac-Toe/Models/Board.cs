using System;
using System.Collections.Generic;
using System.Text;

namespace Tic_Tac_Toe.Models
{
    public class Board
    {
        public char[] board = new char[9];
        public StatusBoard Status = StatusBoard.Available;

        public Board()
        {
            board[0] = '0';
            board[1] = '1';
            board[2] = '2';
            board[3] = '3';
            board[4] = '4';
            board[5] = '5';
            board[6] = '6';
            board[7] = '7';
            board[8] = '8';
        }
        public List<int> getAllPossibleMoves()
        {
            List<int> list = new List<int>();
            for (int i = 0; i < 9; i++)
            {
                if (board[i] != 'X' && board[i] != 'O')
                    list.Add(i);
            }

            return list;
        }

        public int Evaluate()
        {
            //  first row
            if (board[0] == board[1]
                && board[1] == board[2])
            {
                if (board[0] == 'O')
                    return 1;
                if (board[0] == 'X')
                    return -1;
            }
            //  second row
            if (board[3] == board[4]
                && board[4] == board[5])
            {
                if (board[3] == 'O')
                    return 1;
                if (board[3] == 'X')
                    return -1;
            }
            //  third row
            if (board[6] == board[7]
                && board[7] == board[8])
            {
                if (board[6] == 'O')
                    return 1;
                if (board[6] == 'X')
                    return -1;
            }
            //  first column
            if (board[0] == board[3]
                && board[3] == board[6])
            {
                if (board[0] == 'O')
                    return 1;
                if (board[0] == 'X')
                    return -1;
            }
            //  second column
            if (board[1] == board[4]
                && board[4] == board[7])
            {
                if (board[1] == 'O')
                    return 1;
                if (board[1] == 'X')
                    return -1;
            }
            //  third column
            if (board[2] == board[5]
                && board[5] == board[8])
            {
                if (board[2] == 'O')
                    return 1;
                if (board[2] == 'X')
                    return -1;
            }
            //  main diagonal
            if (board[0] == board[4]
                && board[4] == board[8])
            {
                if (board[0] == 'O')
                    return 1;
                if (board[0] == 'X')
                    return -1;
            }
            //  secondary diagonal
            if (board[2] == board[4]
                && board[4] == board[6])
            {
                if (board[2] == 'O')
                    return 1;
                if (board[2] == 'X')
                    return -1;
            }
            return 0;
        }
    }
}
