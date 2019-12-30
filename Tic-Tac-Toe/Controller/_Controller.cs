using System;
using System.Collections.Generic;
using System.Text;
using Tic_Tac_Toe.Models;

namespace Tic_Tac_Toe.Controller
{
    class _Controller
    {
        public int currentBoard = -1;
        public int oldCurrentBoard = -1;
        public int move = 0;
        public int Whose_Turn = 1;
        public Node Root;
        public int Level;  // Game Level  : 0 Random , 1 minimax with Pruning   ,  2 minimax Full tree
        public List<Board> grid { get; set; }
        public _Controller()
        {
            grid = new List<Board>();
            for (int i = 0; i < 9; i++)
            {
                grid.Add(new Board());
            }
        }
        public void PlayGame()
        {
            ShowGrid();
            while (Check_Game_Completion())
            {
                // Do this until the player chooses a valid move
                do
                {
                    // This Variable for Save CurrentBoard Before Choose
                    oldCurrentBoard = currentBoard;

                    if (Whose_Turn == 1)
                    {
                        string input;
                        Console.Write(": It's your turn. \n");
                        // Get the move
                        if (currentBoard == -1)
                        {
                            Console.Write("\nEnter number the grid and Which index of it :\n");
                            input = Console.ReadLine();

                            currentBoard = Convert.ToInt32(input);
                        }
                        else
                            Console.Write("\nEnter an index in board " + currentBoard + " :");
                        input = Console.ReadLine();
                        move = Convert.ToInt32(input);
                    }
                    else
                    {
                        // Computer selects board and cell
                        //  Level Random
                        if (Level == 0)
                        {
                            Random rnd = new Random();
                            if (currentBoard == -1)
                            {
                                currentBoard = rnd.Next(0, 9);
                            }
                            move = rnd.Next(0, 9);

                        }
                        //  Level Medium
                        else if (Level == 1)
                        {

                        }
                        //  Level Expert
                        else
                        {
                            if (currentBoard == -1)
                            {
                                currentBoard = GetBestBoard();
                            }
                            move = GetBestMoveAvailable();
                        }
                        Console.Write("\nComputer chooses Board " + currentBoard + " and index " + move + "  >>>>>>\n\n");
                    }
                } while (!CurrentBoardAndmoveIsValid());
                // Change whose turn it is
                char c = 'X';
                switch (Whose_Turn)
                {
                    case (1):
                        grid[currentBoard].board[move] = 'X';
                        Whose_Turn = 2;
                        break;
                    case (2):
                        grid[currentBoard].board[move] = 'O';
                        Whose_Turn = 1;
                        c = 'O';
                        break;
                }
                CheckIfBoardBecomeCompleted();

                if (CheckIfBoardBecomeFilled(currentBoard))
                {
                    FillBoardByChar(currentBoard, c);
                }
                if (grid[move].Status == StatusBoard.Filled)
                {
                    currentBoard = -1;
                    MakeAllBoardsAvailabeOrNot(true);
                }
                else
                {
                    currentBoard = move;
                    MakeAllBoardsAvailabeOrNot(false);
                }
                ShowGrid();
            }
        }
        public void CheckIfBoardBecomeCompleted()
        {
            if(grid[currentBoard].getAllPossibleMoves().Count == 0)
                grid[currentBoard].Status = StatusBoard.Completed;
        }
        public void MakeAllBoardsAvailabeOrNot(Boolean MakeThemAvailable)
        {
            if (MakeThemAvailable)
            {
                for (int i = 0; i < 9; i++)
                {
                    if (grid[i].Status == StatusBoard.NotAvailable)

                        grid[i].Status = StatusBoard.Available;
                }
            }
            else
            {
                for (int i = 0; i < 9; i++)
                {
                    if (grid[i].Status != StatusBoard.Filled)

                        grid[i].Status = StatusBoard.NotAvailable;
                }
                grid[currentBoard].Status = StatusBoard.Available;
            }
        }
        public Boolean CheckIfBoardBecomeFilled(int currentBoard)
        {
            //  first row
            if (grid[currentBoard].board[0] == grid[currentBoard].board[1]
                && grid[currentBoard].board[1] == grid[currentBoard].board[2])
                return true;
            //  second row
            if (grid[currentBoard].board[3] == grid[currentBoard].board[4]
                && grid[currentBoard].board[4] == grid[currentBoard].board[5])
                return true;
            //  third row
            if (grid[currentBoard].board[6] == grid[currentBoard].board[7]
                && grid[currentBoard].board[7] == grid[currentBoard].board[8])
                return true;
            //  first column
            if (grid[currentBoard].board[0] == grid[currentBoard].board[3]
                && grid[currentBoard].board[3] == grid[currentBoard].board[6])
                return true;
            //  second column
            if (grid[currentBoard].board[1] == grid[currentBoard].board[4]
                && grid[currentBoard].board[4] == grid[currentBoard].board[7])
                return true;
            //  third column
            if (grid[currentBoard].board[2] == grid[currentBoard].board[5]
                && grid[currentBoard].board[5] == grid[currentBoard].board[8])
                return true;
            //  main diagonal
            if (grid[currentBoard].board[0] == grid[currentBoard].board[4]
                && grid[currentBoard].board[4] == grid[currentBoard].board[8])
                return true;
            //  secondary diagonal
            if (grid[currentBoard].board[2] == grid[currentBoard].board[4]
                && grid[currentBoard].board[4] == grid[currentBoard].board[6])
                return true;
            return false;
        }
        public Boolean Check_Game_Completion()
        {
            //  Check if a player won
            if (CheckIfPlayerWon())
                return false;
            for (int i = 0; i < 9; i++)
            {
                if (grid[i].Status == StatusBoard.Available)
                    return true;
            }
            Console.Write("\n\nNobody won ..........>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>\n\n");
            return false;
        }
        public Boolean CheckIfPlayerWon()
        {
            // Check for The first row and column and main diagonal
            if ((grid[0].Status == StatusBoard.Filled && grid[1].Status == StatusBoard.Filled && grid[2].Status == StatusBoard.Filled
                && grid[0].board[0] == grid[1].board[0]
                && grid[1].board[0] == grid[2].board[0])
                || (grid[0].Status == StatusBoard.Filled && grid[4].Status == StatusBoard.Filled && grid[8].Status == StatusBoard.Filled
                && grid[0].board[0] == grid[4].board[0]
                && grid[4].board[0] == grid[8].board[0])
                || (grid[0].Status == StatusBoard.Filled && grid[3].Status == StatusBoard.Filled && grid[6].Status == StatusBoard.Filled
                && grid[0].board[0] == grid[3].board[0]
                && grid[3].board[0] == grid[6].board[0]))
            {
                Console.Write("\nThe player '" + grid[0].board[0] + "' is Wonner >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>\n\n");
                return true;
            }
            // Check for The socand row and column and socandary diagonal
            if ((grid[2].Status == StatusBoard.Filled && grid[4].Status == StatusBoard.Filled && grid[6].Status == StatusBoard.Filled
                && grid[2].board[0] == grid[4].board[0]
                && grid[4].board[0] == grid[6].board[0])
               || (grid[1].Status == StatusBoard.Filled && grid[4].Status == StatusBoard.Filled && grid[7].Status == StatusBoard.Filled
                && grid[1].board[0] == grid[4].board[0]
                && grid[4].board[0] == grid[7].board[0])
               || (grid[3].Status == StatusBoard.Filled && grid[4].Status == StatusBoard.Filled && grid[5].Status == StatusBoard.Filled
                && grid[3].board[0] == grid[4].board[0]
                && grid[4].board[0] == grid[5].board[0]))
            {
                Console.Write("\nThe player '" + grid[4].board[0] + "' is Wonner >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>\n\n");
                return true;
            }
            // check for The third row and column
            if ((grid[2].Status == StatusBoard.Filled && grid[5].Status == StatusBoard.Filled && grid[8].Status == StatusBoard.Filled
              && grid[2].board[0] == grid[5].board[0]
              && grid[5].board[0] == grid[8].board[0])
             || (grid[6].Status == StatusBoard.Filled && grid[7].Status == StatusBoard.Filled && grid[8].Status == StatusBoard.Filled
              && grid[6].board[0] == grid[7].board[0]
              && grid[7].board[0] == grid[8].board[0]))
            {
                Console.Write("\nThe player '" + grid[8].board[0] + "' is Wonner >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>\n\n");
                return true;
            }

            return false;
        }
        public Boolean CurrentBoardAndmoveIsValid()
        {
            Boolean check = (move < 9 && move >= 0 && currentBoard >= 0 && currentBoard < 9 &&
                grid[currentBoard].board[move] != 'X' && grid[currentBoard].board[move] != 'O'
                && grid[currentBoard].Status == StatusBoard.Available);
            if (!check)
                currentBoard = oldCurrentBoard;
            return check;
        }
        public void FillBoardByChar(int index, char c)
        {
            for (int i = 0; i < 9; i++)
            {
                grid[index].board[i] = c;
            }
            grid[index].Status = StatusBoard.Filled;
        }
        public int GetBestMoveAvailable()
        {
            KeyValuePair<int, int> bestMove = new KeyValuePair<int, int>(-1, -1); int Bestvalue = -10;

            var listMoves = grid[currentBoard].getAllPossibleMoves();
            for (int i = 0; i < listMoves.Count; i++)
            {
                int cell = listMoves[i];
                    grid[currentBoard].board[cell] = 'O';
                    KeyValuePair<int, int> retrieved_Value = minimax(0, false);

                    if (Bestvalue < retrieved_Value.Key)
                    {
                        if (GoodMove(cell))
                        {
                            Bestvalue = retrieved_Value.Key;
                        }
                        bestMove = new KeyValuePair<int, int>(cell, retrieved_Value.Value);
                    }
                    char value;
                    Char.TryParse(cell.ToString(), out value);
                    grid[currentBoard].board[cell] = value;
            }
            return bestMove.Key;
        }
        public int GetBestBoard()
        {
            int Bestindex = -1;
            for (int i = 0; i < 9; i++)
            {
                if (grid[i].Status != StatusBoard.Filled && grid[i].Status != StatusBoard.Completed)
                {
                    if (GoodMove(i, 'O'))
                        Bestindex = i;
                }
            }
            if (Bestindex == -1)
            {
                for (int i = 0; i < 9; i++)
                {
                    if (grid[i].Status != StatusBoard.Filled && grid[i].Status != StatusBoard.Completed)
                    {
                        if (Bestindex == -1)
                            Bestindex = i;
                        if (!GoodMove(i))
                            Bestindex = i;
                    }
                }
            }
            return Bestindex;
        }
        public Boolean GoodMove(int index, char c = 'X')
        {
            char value;
            var listMoves = grid[index].getAllPossibleMoves();

            for (int i = 0; i < listMoves.Count; i++)
            {
                int cell = listMoves[i];

                grid[index].board[cell] = c;
                if (CheckIfBoardBecomeFilled(index))
                {
                    Char.TryParse(cell.ToString(), out value);
                    grid[index].board[cell] = value;
                    if (c == 'X')
                        return false;
                    else
                        return true;
                }
                Char.TryParse(cell.ToString(), out value);
                grid[index].board[cell] = value;
            }
            return true;
        }
        public KeyValuePair<int, int> minimax(int depth, bool maximizingPlayer)
        {
            int won = grid[currentBoard].Evaluate();

            // If Maximizer Or Minimizer has won the game return his evaluated
            if (won == 1 || won == -1)
                return new KeyValuePair<int, int>(won, depth);

            // If there are no more moves and no winner then it is a equals
            if (grid[currentBoard].getAllPossibleMoves().Count==0)
                return new KeyValuePair<int, int>(0, depth);

            // If this maximizer's move 
            if (maximizingPlayer)
            {
                //  for bestValue and depth
                KeyValuePair<int, int> best = new KeyValuePair<int, int>(0, 0);
                var listMoves = grid[currentBoard].getAllPossibleMoves();

                for (int i = 0; i < listMoves.Count; i++)
                {
                    int cell = listMoves[i];
                    // Make the move 
                    grid[currentBoard].board[cell] = 'O';

                    // Call minimax recursively and choose 
                    // the maximum value and depth
                    var res = minimax(depth + 1, !maximizingPlayer);
                    if (best.Key < res.Key || (best.Key == res.Key && best.Value > res.Value))
                        best = res;

                    // Undo the move 
                    char value;
                    Char.TryParse(cell.ToString(), out value);
                    grid[currentBoard].board[cell] = value;
                }
                return best;
            }
            // If this minimizer's move 
            else
            {
                //  for bestValue and depth
                KeyValuePair<int, int> best = new KeyValuePair<int, int>(0, 0);

                var listMoves = grid[currentBoard].getAllPossibleMoves();

                for (int i = 0; i < listMoves.Count; i++)
                {
                    int cell = listMoves[i];
                    // Make the move 
                    grid[currentBoard].board[cell] = 'X';

                    // Call minimax recursively and choose the minimum value 
                    var res = minimax(depth + 1, !maximizingPlayer);
                    if (best.Key > res.Key || (best.Key == res.Key && best.Value > res.Value))
                        best = res;

                    // Undo the move 
                    char value;
                    Char.TryParse(cell.ToString(), out value);
                    grid[currentBoard].board[cell] = value;
                } 
                return best;
            }
        }
        public void CreateTree(List<int> list)
        {
            // For 9 elements
            if (list.Count == 9)
            {
                // Level 0
                Root = new Node();
                // Level 1
                Root.AddNode();
                Root.AddNode();
                // Level 2
                Root.listNode[0].AddNode();
                Root.listNode[0].AddNode();
                Root.listNode[1].AddNode();
                Root.listNode[1].AddNode();
                // Level 3
                Root.listNode[0].listNode[0].AddNode(list[0], true);
                Root.listNode[0].listNode[0].AddNode(list[1], true);
                Root.listNode[0].listNode[1].AddNode(list[2], true);
                Root.listNode[0].listNode[1].AddNode(list[3], true);
                Root.listNode[1].listNode[0].AddNode(list[4], true);
                Root.listNode[1].listNode[0].AddNode(list[5], true);
                Root.listNode[1].listNode[1].AddNode(list[6], true);
                Root.listNode[1].listNode[1].AddNode(list[7], true);
                Root.listNode[1].listNode[1].AddNode(list[8], true);
            }
            // For 8 elements
            else if (list.Count == 8)
            {
                // Level 0
                Root = new Node();
                // Level 1
                Root.AddNode();
                Root.AddNode();
                // Level 2
                Root.listNode[0].AddNode();
                Root.listNode[0].AddNode();
                Root.listNode[1].AddNode();
                Root.listNode[1].AddNode();
                // Level 3
                Root.listNode[0].listNode[0].AddNode(list[0], true);
                Root.listNode[0].listNode[0].AddNode(list[1], true);
                Root.listNode[0].listNode[1].AddNode(list[2], true);
                Root.listNode[0].listNode[1].AddNode(list[3], true);
                Root.listNode[1].listNode[0].AddNode(list[4], true);
                Root.listNode[1].listNode[0].AddNode(list[5], true);
                Root.listNode[1].listNode[1].AddNode(list[6], true);
                Root.listNode[1].listNode[1].AddNode(list[7], true);
            }
            // For 7 elements
            else if (list.Count == 7)
            {
                // Level 0
                Root = new Node();
                // Level 1
                Root.AddNode();
                Root.AddNode();
                // Level 2
                Root.listNode[0].AddNode();
                Root.listNode[0].AddNode();
                Root.listNode[1].AddNode();
                // Level 3
                Root.listNode[0].listNode[0].AddNode(list[0], true);
                Root.listNode[0].listNode[0].AddNode(list[1], true);
                Root.listNode[0].listNode[1].AddNode(list[2], true);
                Root.listNode[0].listNode[1].AddNode(list[3], true);
                Root.listNode[1].listNode[0].AddNode(list[4], true);
                Root.listNode[1].listNode[0].AddNode(list[5], true);
                Root.listNode[1].listNode[0].AddNode(list[6], true);
            }
            // For 6 elements
            else if (list.Count == 6)
            {
                // Level 0
                Root = new Node();
                // Level 1
                Root.AddNode();
                Root.AddNode();
                // Level 2
                Root.listNode[0].AddNode();
                Root.listNode[0].AddNode();
                Root.listNode[1].AddNode();
                // Level 3
                Root.listNode[0].listNode[0].AddNode(list[0], true);
                Root.listNode[0].listNode[0].AddNode(list[1], true);
                Root.listNode[0].listNode[1].AddNode(list[2], true);
                Root.listNode[0].listNode[1].AddNode(list[3], true);
                Root.listNode[1].listNode[0].AddNode(list[4], true);
                Root.listNode[1].listNode[0].AddNode(list[5], true);
            }
            // For 5 elements
            else if (list.Count == 5)
            {
                // Level 0
                Root = new Node();
                // Level 1
                Root.AddNode();
                Root.AddNode();
                // Level 2
                Root.listNode[0].AddNode(list[0], true);
                Root.listNode[0].AddNode(list[1], true);
                Root.listNode[1].AddNode(list[2], true);
                Root.listNode[1].AddNode(list[3], true);
                Root.listNode[1].AddNode(list[4], true);
            }
            // For 4 elements
            else if (list.Count == 4)
            {
                // Level 0
                Root = new Node();
                // Level 1
                Root.AddNode();
                Root.AddNode();
                // Level 2
                Root.listNode[0].AddNode(list[0], true);
                Root.listNode[0].AddNode(list[1], true);
                Root.listNode[1].AddNode(list[2], true);
                Root.listNode[1].AddNode(list[3], true);
            }
            // For 3 elements
            else if (list.Count == 3)
            {
                // Level 0
                Root = new Node();
                // Level 1
                Root.AddNode();
                Root.AddNode(list[2], true);
                // Level 2
                Root.listNode[0].AddNode(list[0], true);
                Root.listNode[0].AddNode(list[1], true);
            }
            // For 2 elements
            else if (list.Count == 2)
            {
                // Level 0
                Root = new Node();
                // Level 1
                Root.AddNode(list[0], true);
                Root.AddNode(list[1], true);
            }

        }
        public void ShowGrid()
        {
            /// Row 1
            Console.Write(" " + grid[0].board[0] + " | ");
            Console.Write(grid[0].board[1] + " | ");
            Console.Write(grid[0].board[2] + "  |||  ");
            /// Separator
            Console.Write(grid[1].board[0] + " | ");
            Console.Write(grid[1].board[1] + " | ");
            Console.Write(grid[1].board[2] + "  |||  ");
            /// Separator
            Console.Write(grid[2].board[0] + " | ");
            Console.Write(grid[2].board[1] + " | ");
            Console.Write(grid[2].board[2]);
            Console.WriteLine();
            Console.Write("---|---|--- ||| ---|---|--- ||| ---|---|---");
            Console.WriteLine();
            /// Row 2
            Console.Write(" " + grid[0].board[3] + " | ");
            Console.Write(grid[0].board[4] + " | ");
            Console.Write(grid[0].board[5] + "  |||  ");
            /// Separator               
            Console.Write(grid[1].board[3] + " | ");
            Console.Write(grid[1].board[4] + " | ");
            Console.Write(grid[1].board[5] + "  |||  ");
            /// Separator               
            Console.Write(grid[2].board[3] + " | ");
            Console.Write(grid[2].board[4] + " | ");
            Console.Write(grid[2].board[5]);
            Console.WriteLine();
            Console.Write("---|---|--- ||| ---|---|--- ||| ---|---|---");
            Console.WriteLine();
            /// Row 3
            Console.Write(" " + grid[0].board[6] + " | ");
            Console.Write(grid[0].board[7] + " | ");
            Console.Write(grid[0].board[8] + "  |||  ");
            /// Separator               
            Console.Write(grid[1].board[6] + " | ");
            Console.Write(grid[1].board[7] + " | ");
            Console.Write(grid[1].board[8] + "  |||  ");
            /// Separator               
            Console.Write(grid[2].board[6] + " | ");
            Console.Write(grid[2].board[7] + " | ");
            Console.Write(grid[2].board[8]);
            Console.WriteLine();
            Console.WriteLine();
            Console.Write("##########################################");
            Console.WriteLine();
            Console.WriteLine();
            /// Row 4
            Console.Write(" " + grid[3].board[0] + " | ");
            Console.Write(grid[3].board[1] + " | ");
            Console.Write(grid[3].board[2] + "  |||  ");
            /// Separator
            Console.Write(grid[4].board[0] + " | ");
            Console.Write(grid[4].board[1] + " | ");
            Console.Write(grid[4].board[2] + "  |||  ");
            /// Separator
            Console.Write(grid[5].board[0] + " | ");
            Console.Write(grid[5].board[1] + " | ");
            Console.Write(grid[5].board[2]);
            Console.WriteLine();
            Console.Write("---|---|--- ||| ---|---|--- ||| ---|---|---");
            Console.WriteLine();
            /// Row 5
            Console.Write(" " + grid[3].board[3] + " | ");
            Console.Write(grid[3].board[4] + " | ");
            Console.Write(grid[3].board[5] + "  |||  ");
            /// Separator              
            Console.Write(grid[4].board[3] + " | ");
            Console.Write(grid[4].board[4] + " | ");
            Console.Write(grid[4].board[5] + "  |||  ");
            /// Separator              
            Console.Write(grid[5].board[3] + " | ");
            Console.Write(grid[5].board[4] + " | ");
            Console.Write(grid[5].board[5]);
            Console.WriteLine();
            Console.Write("---|---|--- ||| ---|---|--- ||| ---|---|---");
            Console.WriteLine();
            /// Row 6
            Console.Write(" " + grid[3].board[6] + " | ");
            Console.Write(grid[3].board[7] + " | ");
            Console.Write(grid[3].board[8] + "  |||  ");
            /// Separator              
            Console.Write(grid[4].board[6] + " | ");
            Console.Write(grid[4].board[7] + " | ");
            Console.Write(grid[4].board[8] + "  |||  ");
            /// Separator              
            Console.Write(grid[5].board[6] + " | ");
            Console.Write(grid[5].board[7] + " | ");
            Console.Write(grid[5].board[8]);
            Console.WriteLine();
            Console.WriteLine();
            Console.Write("##########################################");
            Console.WriteLine();
            Console.WriteLine();
            /// Row 7
            Console.Write(" " + grid[6].board[0] + " | ");
            Console.Write(grid[6].board[1] + " | ");
            Console.Write(grid[6].board[2] + "  |||  ");
            /// Separator
            Console.Write(grid[7].board[0] + " | ");
            Console.Write(grid[7].board[1] + " | ");
            Console.Write(grid[7].board[2] + "  |||  ");
            /// Separator
            Console.Write(grid[8].board[0] + " | ");
            Console.Write(grid[8].board[1] + " | ");
            Console.Write(grid[8].board[2]);
            Console.WriteLine();
            Console.Write("---|---|--- ||| ---|---|--- ||| ---|---|---");
            Console.WriteLine();
            /// Row 8
            Console.Write(" " + grid[6].board[3] + " | ");
            Console.Write(grid[6].board[4] + " | ");
            Console.Write(grid[6].board[5] + "  |||  ");
            /// Separator             
            Console.Write(grid[7].board[3] + " | ");
            Console.Write(grid[7].board[4] + " | ");
            Console.Write(grid[7].board[5] + "  |||  ");
            /// Separator             
            Console.Write(grid[8].board[3] + " | ");
            Console.Write(grid[8].board[4] + " | ");
            Console.Write(grid[8].board[5]);
            Console.WriteLine();
            Console.Write("---|---|--- ||| ---|---|--- ||| ---|---|---");
            Console.WriteLine();
            /// Row 9
            Console.Write(" " + grid[6].board[6] + " | ");
            Console.Write(grid[6].board[7] + " | ");
            Console.Write(grid[6].board[8] + "  |||  ");
            /// Separator             
            Console.Write(grid[7].board[6] + " | ");
            Console.Write(grid[7].board[7] + " | ");
            Console.Write(grid[7].board[8] + "  |||  ");
            /// Separator             
            Console.Write(grid[8].board[6] + " | ");
            Console.Write(grid[8].board[7] + " | ");
            Console.Write(grid[8].board[8]);
            Console.WriteLine();
            Console.Write("----------------------------------------------------------------------------\n");
            Console.Write("----------------------------------------------------------------------------");
            Console.WriteLine();

        }
    }
}
