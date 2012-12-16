using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Windows.Forms;
namespace балд
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
         bool player = true, inp = true;//Логические переменные для смены языка, игрока и регулировки ввода
        string filename2 = "dicru.txt";//Имена файлов со словарями и рекордами
        string str = "", rts = "";//str=строка, считываемая с датагрид, rts - строка str задом наперед (потому что считывает задом наперед)
        int cc = 0, q1 = 0, q2 = 0, ii = 0, jj = 0;//сс - длина выделенного слова, q1,q2 - очки, ii,jj - индексы введенной буквы (для проверки)
        string x = "";//Массив, в который заносятся использованные слова
        string[][] A = new string[5][];//Матрица, дополняющаяся, когда ход защитан
        string[][] B = new string[5][];//Матрица для проверок
        string[] xx;
            
        static void bap(string[][] A, ref string[][] B)//Функция, когда B присваивается A
        {
            for (int i = 0; i < A.Length; i++)
                for (int j = 0; j < A[i].Length; j++)
                    B[i][j] = A[i][j];
        }
       
    }
}
