namespace B18_Ex02
{
     public class Board
     {
          private readonly Square[,] r_ActualBoard;
          private int m_BoardSize;

          public int BoardSize
          {
               get { return m_BoardSize; }
               set { m_BoardSize = value; }
          }

          public char GetSquareContent(int i_BoardLine, int i_BoardColumn)
          {
               char squareContent;
               if (r_ActualBoard[i_BoardLine, i_BoardColumn].CurrentMan == null)
               {
                    squareContent = ' ';
               }
               else
               {
                    squareContent = r_ActualBoard[i_BoardLine, i_BoardColumn].CurrentMan.Sign;
               }

               return squareContent;
          }

          public Square GetSquare(int i_BoardLine, int i_BoardColumn)
          {
               return r_ActualBoard[i_BoardLine, i_BoardColumn];
          }

          public Board(int i_BoardSize)
          {
               m_BoardSize = i_BoardSize;
               r_ActualBoard = new Square[m_BoardSize, m_BoardSize];
               for (int i = 0; i < m_BoardSize; i++)
               {
                    for (int j = 0; j < m_BoardSize; j++)
                    {
                         r_ActualBoard[i, j] = new Square(i, j);
                    }
               }

               neighboursAssignation();
          }

          public void ClearBoard()
          {
               for (int i = 0; i < m_BoardSize; i++)
               {
                    for (int j = 0; j < m_BoardSize; j++)
                    {
                         r_ActualBoard[i, j].CurrentMan = null;
                    }
               }
          }

          private void neighboursAssignation()
          {
               for (int i = 0; i < m_BoardSize; i++)
               {
                    for (int j = 0; j < m_BoardSize; j++)
                    {
                         if (IsSquarePositionInBoardRange(i - 1, j - 1))
                         {
                              r_ActualBoard[i, j].AssignNeighbour(r_ActualBoard[i - 1, j - 1], CheckersGame.ePossibleDirections.UpLeft);
                         }

                         if (IsSquarePositionInBoardRange(i - 1, j + 1))
                         {
                              r_ActualBoard[i, j].AssignNeighbour(r_ActualBoard[i - 1, j + 1], CheckersGame.ePossibleDirections.UpRight);
                         }

                         if (IsSquarePositionInBoardRange(i + 1, j - 1))
                         {
                              r_ActualBoard[i, j].AssignNeighbour(r_ActualBoard[i + 1, j - 1], CheckersGame.ePossibleDirections.DownLeft);
                         }

                         if (IsSquarePositionInBoardRange(i + 1, j + 1))
                         {
                              r_ActualBoard[i, j].AssignNeighbour(r_ActualBoard[i + 1, j + 1], CheckersGame.ePossibleDirections.DownRight);
                         }
                    }
               }
          }

          public bool IsSquarePositionInBoardRange(int squareLine, int squareColumn)
          {
               return (squareLine >= 0 && squareLine < m_BoardSize && squareColumn >= 0 && squareColumn < m_BoardSize) ? true : false;
          }
     }
}
