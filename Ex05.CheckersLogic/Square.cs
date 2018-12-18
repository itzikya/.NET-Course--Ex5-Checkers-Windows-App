using System;
using System.Collections.Generic;
using System.Text;

namespace B18_Ex02
{
     public class Square
     {
          public struct SquarePosition
          {
               private int m_SquareLine;
               private int m_SquareColumm;

               public SquarePosition(int i_SquareLine, int i_SquareColumn)
               {
                    m_SquareLine = i_SquareLine;
                    m_SquareColumm = i_SquareColumn;
               }

               public int x
               {
                    get { return m_SquareColumm; }
                    set { m_SquareColumm = value; }
               }

               public int y
               {
                    get { return m_SquareLine; }
                    set { m_SquareLine = value; }
               }
          }

          public class SquareNeighbours
          {
               private Square m_UpRightSquare;
               private Square m_UpLeftSquare;
               private Square m_DownRightSquare;
               private Square m_DownLeftSquare;

               public Square UpRight
               {
                    get { return m_UpRightSquare; }
                    set { m_UpRightSquare = value; }
               }

               public Square UpLeft
               {
                    get { return m_UpLeftSquare; }
                    set { m_UpLeftSquare = value; }
               }

               public Square DownRight
               {
                    get { return m_DownRightSquare; }
                    set { m_DownRightSquare = value; }
               }

               public Square DownLeft
               {
                    get { return m_DownLeftSquare; }
                    set { m_DownLeftSquare = value; }
               }
          }

          private SquarePosition m_SquarePosition;
          private Man m_CurrentMan;
          private SquareNeighbours m_SquareNeighbours = new SquareNeighbours();
          private eSquareColor m_SquareColor;

          public SquarePosition Position
          {
               get { return m_SquarePosition; }
               set { m_SquarePosition = value; }
          }

          public eSquareColor SquareColor
          {
               get { return m_SquareColor; }
               set { m_SquareColor = value; }
          }

          public Man CurrentMan
          {
               get { return m_CurrentMan; }
               set { m_CurrentMan = value; }
          }

          public SquareNeighbours Neighbours
          {
               get { return m_SquareNeighbours; }
               set { m_SquareNeighbours = value; }
          }

          public Square()
          {
          }

          public Square(int i_SquareLine, int i_SquareColumm)
          {
               m_SquarePosition = new SquarePosition(i_SquareLine, i_SquareColumm);
               if ((m_SquarePosition.x + m_SquarePosition.y) % 2 == 0)
               {
                    m_SquareColor = eSquareColor.Black;
               }
               else
               {
                    m_SquareColor = eSquareColor.White;
               }
          }

          public void AssignNeighbour(Square i_NeighbourSquare, CheckersGame.ePossibleDirections i_SquareDirection)
          {
               if (i_SquareDirection == CheckersGame.ePossibleDirections.UpLeft)
               {
                    m_SquareNeighbours.UpLeft = new Square();
                    m_SquareNeighbours.UpLeft = i_NeighbourSquare;
               }
               else if (i_SquareDirection == CheckersGame.ePossibleDirections.UpRight)
               {
                    m_SquareNeighbours.UpRight = new Square();
                    m_SquareNeighbours.UpRight = i_NeighbourSquare;
               }
               else if (i_SquareDirection == CheckersGame.ePossibleDirections.DownLeft)
               {
                    m_SquareNeighbours.DownLeft = new Square();
                    m_SquareNeighbours.DownLeft = i_NeighbourSquare;
               }
               else
               {
                    m_SquareNeighbours.DownRight = new Square();
                    m_SquareNeighbours.DownRight = i_NeighbourSquare;
               }
          }

          public enum eSquareColor
          {
               Black, White
          }
     }
}
