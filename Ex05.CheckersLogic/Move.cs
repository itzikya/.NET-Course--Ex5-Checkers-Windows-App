using System;
using System.Collections.Generic;
using System.Text;

namespace B18_Ex02
{
     public class Move
     {
          private Square m_SourceSquare;
          private Square m_CapturedSquare;
          private Square m_DestinationSquare;
          private eMoveOption m_MoveOption;

          public eMoveOption MoveOption
          {
               get { return m_MoveOption; }
               set { m_MoveOption = value; }
          }

          public Move()
          {
               m_SourceSquare = new Square();
               m_DestinationSquare = new Square();
          }

          public Move(Square i_SourceSquare, Square i_DestinationSquare)
          {
               m_SourceSquare = i_SourceSquare;
               m_DestinationSquare = i_DestinationSquare;
               m_MoveOption = eMoveOption.Move;
          }

          public Move(Square i_SourceSquare, Square i_CapturedSquare, Square i_DestinationSquare, eMoveOption i_MoveOption)
          {
               m_SourceSquare = i_SourceSquare;
               m_CapturedSquare = i_CapturedSquare;
               m_DestinationSquare = i_DestinationSquare;
               m_MoveOption = i_MoveOption;
          }

          public Move(eMoveOption i_MoveOption)
          {
               m_MoveOption = i_MoveOption;
          }

          public Square SourceSquare
          {
               get { return m_SourceSquare; }
               set { m_SourceSquare = value; }
          }

          public Square CapturedSquare
          {
               get { return m_CapturedSquare; }
               set { m_CapturedSquare = value; }
          }

          public Square DestinationSquare
          {
               get { return m_DestinationSquare; }
               set { m_DestinationSquare = value; }
          }

          public bool IsLegalMove(ref Move i_UserRequestForMove, Team i_ActiveTeam)
          {
               bool isLegalMove = false;

               isLegalMove = IsAttackMove(i_UserRequestForMove, i_ActiveTeam);
               if (isLegalMove == false && i_ActiveTeam.AttackMoves.Count == 0)
               {
                    isLegalMove = CheckRegularMoves(ref i_UserRequestForMove, i_ActiveTeam);
               }

               return isLegalMove;
          }

          public bool IsAttackMove(Move io_UserRequestForMove, Team i_ActiveTeam)
          {
               bool isLegalAttackMove = false;
               foreach (Move availableMove in i_ActiveTeam.AttackMoves)
               {
                    if (IsMoveMatchToMoveInMovesList(ref io_UserRequestForMove, availableMove))
                    {
                         isLegalAttackMove = true;
                    }
               }

               return isLegalAttackMove;
          }

          public bool CheckRegularMoves(ref Move io_UserRequestForMove, Team i_ActiveTeam)
          {
               bool isLegalRegularMove = false;
               foreach (Move availableMove in i_ActiveTeam.RegularMoves)
               {
                    if (IsMoveMatchToMoveInMovesList(ref io_UserRequestForMove, availableMove))
                    {
                         isLegalRegularMove = true;
                    }
               }

               return isLegalRegularMove;
          }

          public bool IsMoveMatchToMoveInMovesList(ref Move i_UserRequestForMove, Move i_AvailableMove)
          {
               bool isMoveMatchToMoveInMovesList = false;
               if (i_AvailableMove.m_DestinationSquare.Position.x == i_UserRequestForMove.m_DestinationSquare.Position.x &&
                    i_AvailableMove.m_DestinationSquare.Position.y == i_UserRequestForMove.m_DestinationSquare.Position.y &&
                    i_AvailableMove.m_SourceSquare.Position.x == i_UserRequestForMove.m_SourceSquare.Position.x &&
                   i_AvailableMove.m_SourceSquare.Position.y == i_UserRequestForMove.m_SourceSquare.Position.y)
               {
                    i_UserRequestForMove = i_AvailableMove;
                    isMoveMatchToMoveInMovesList = true;
               }

               return isMoveMatchToMoveInMovesList;
          }

          public void ExecuteMove()
          {
               m_DestinationSquare.CurrentMan = m_SourceSquare.CurrentMan;
               m_SourceSquare.CurrentMan.CurrentPosition = m_DestinationSquare;
               m_SourceSquare.CurrentMan = null;
               if (m_MoveOption == eMoveOption.Attack)
               {
                    m_CapturedSquare.CurrentMan.Team.ArmyOfMen.Remove(m_CapturedSquare.CurrentMan);
                    m_CapturedSquare.CurrentMan = null;
               }
          }

          public enum eMoveOption
          {
               Quit,
               Move,
               Attack,
          }
     }
}
