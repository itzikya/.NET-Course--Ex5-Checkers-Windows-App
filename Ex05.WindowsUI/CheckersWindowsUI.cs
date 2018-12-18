using System;
using System.Collections.Generic;
using System.Text;
using B18_Ex02;

namespace Ex05.WindowsUI
{
     public class CheckersWindowsUI
     {
          private CheckersForm m_CheckersForm;

          public void Run()
          {
               m_CheckersForm = new CheckersForm();
               m_CheckersForm.ShowDialog();
          }
     }
}
