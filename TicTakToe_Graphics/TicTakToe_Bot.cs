using System;

namespace TicTakToe_Graphics
{
    internal class TicTakToe_Bot
    {
        public TicTakToe_Bot() { }
        private char playerX = 'X';
        private char playerO = 'O';
        private char emptyCell = ' ';

        public char[] GetBestNextMove(char[] board, char bot)
        {
            int bestMove = -1;
            int bestValue = int.MinValue;

            for (int i = 0; i < board.Length; i++)
            {
                if (board[i] == emptyCell)
                {
                    board[i] = bot;
                    int moveValue = Minimax(board, 0, false, bot);
                    board[i] = emptyCell;

                    if (moveValue > bestValue)
                    {
                        bestMove = i;
                        bestValue = moveValue;
                    }
                }
            }

            if (bestMove != -1)
            {
                board[bestMove] = bot;
            }

            return board;
        }
        //Minimax algoritm, returns the shortest way to a win for the bot
        private int Minimax(char[] board, int depth, bool isMax, char bot)
        {
            char realPlayer = playerX;
            if(bot == playerX)
            {
                realPlayer = playerO;
            }

            int score = CheckForWinning(board, bot);
            if (score == 10) 
                return score - depth;
            if (score == -10) 
                return score + depth;
            if (!IsTheBoardFull(board)) 
                return 0;

            if (isMax)
            {
                int best = int.MinValue;
                for (int i = 0; i < board.Length; i++)
                {
                    if (board[i] == emptyCell)
                    {
                        board[i] = bot;
                        best = Math.Max(best, Minimax(board, depth + 1, !isMax, bot));
                        board[i] = emptyCell;
                    }
                }
                return best;
            }
            else
            {
                int best = int.MaxValue;
                for (int i = 0; i < board.Length; i++)
                {
                    if (board[i] == emptyCell)
                    {
                        board[i] = realPlayer;
                        best = Math.Min(best, Minimax(board, depth + 1, !isMax, bot));
                        board[i] = emptyCell;
                    }
                }
                return best;
            }
        }

        private int CheckForWinning(char[] board, char bot)
        {
            char realPlayer = playerX;
            if (bot == playerX)
            {
                realPlayer = playerO;
            }

            // Checking for rows 
            for (int row = 0; row < 3; row++)
            {
                if (board[row * 3] == board[row * 3 + 1] && board[row * 3 + 1] == board[row * 3 + 2])
                {
                    if (board[row * 3] == bot)
                        return 10;
                    else if (board[row * 3] == realPlayer) 
                        return -10;
                }
            }

            // Checking for columns 
            for (int col = 0; col < 3; col++)
            {
                if (board[col] == board[col + 3] && board[col + 3] == board[col + 6])
                {
                    if (board[col] == bot) 
                        return 10;
                    else if (board[col] == realPlayer) 
                        return -10;
                }
            }

            // Checking for diagonals 
            if (board[0] == board[4] && board[4] == board[8])
            {
                if (board[0] == bot) 
                    return 10;
                else if (board[0] == realPlayer) 
                    return -10;
            }
            if (board[2] == board[4] && board[4] == board[6])
            {
                if (board[2] == bot) 
                    return 10;
                else if (board[2] == realPlayer) 
                    return -10;
            }

            return 0;
        }

        private bool IsTheBoardFull(char[] board)
        {
            for (int i = 0; i < board.Length; i++)
            {
                if (board[i] == emptyCell)
                    return true;
            }
            return false;
        }
    }
}
