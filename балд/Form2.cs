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
        
       //Функция добавления в список слов введенного слова, а также подсчет очков.
        static void listadd(ref bool player, ListBox lst1, ListBox lst2, string rts, ref int q1, ref int q2)
        {
            if (player)
            {
                lst1.Items.Add(rts);
                player = false;
                q1 += rts.Length;
            }
            else
            {
                lst2.Items.Add(rts);
                player = true;
                q2 += rts.Length;
            }
        }
        static void pastedm(ref string[][] A, DataGridView dtg)//Присвоение А элементов datagridview
        {
            for (int i = 0; i < A.Length; i++)
                for (int j = 0; j < A[i].Length; j++)
                    A[i][j] = dtg.Rows[i].Cells[j].Value as string;
        }
        //Проверка на правильное выделение
        static void cellselectedcheck(DataGridView dtg, string[][] A, ref bool selectij, int cc)
        {
            //получаем индексы ich и jch первой выделенной буквы (выделение происходит наоборот, следовательно в коде - последней)
            int ich = dtg.SelectedCells[cc - 1].RowIndex;
            int jch = dtg.SelectedCells[cc - 1].ColumnIndex;
            int dscr = dtg.SelectedCells[cc - 1].RowIndex;
            int dscc = dtg.SelectedCells[cc - 1].ColumnIndex;
            for (int i = cc - 2; i >= 0; i--)//Входим в цикл проверки
            {
                dscr = dtg.SelectedCells[i].RowIndex;//Переприсваиваем переменным индексы текущих строк и столбца
                dscc = dtg.SelectedCells[i].ColumnIndex;
                //Условие неправильного выделения
                if ((dscr == ich && (dscc != jch + 1 && dscc != jch - 1)) || (dscc == jch && (dscr != ich + 1 && dscr != ich - 1)) || (dscr != ich && dscc != jch))
                {
                    selectij = false;
                    break;
                }
                //В противном случае, перемещаем индексы для сравнения
                else
                {
                    ich = dscr;
                    jch = dscc;
                }
            }
        }
    }
}
