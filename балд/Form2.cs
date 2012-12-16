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
          private void button2_Click(object sender, EventArgs e)
        { rts = "";
            bool startwinp = false, selectij = true;//Объявление переменных
            //Объявлены тут, а не глобально, потому что их каждый раз надо переприсваивать по новой
            //korm отвечает за наличие введенного слова в списке использованных, plu - словарь,
            //startwinp подтверждает, что слово содержит введенную букву, и буква была вообще введена
            //selectij будет false, если выделение происходило неверно.
            bap(A, ref B);//Сразу присвоим B=A
            for (int i = 0; i < A.Length; i++)//Проверка на использование введенной буквы
                for (int j = 0; j < A[i].Length; j++)
                    if ((A[i][j] != dataGridView1.Rows[i].Cells[j].Value as string) && (dataGridView1.Rows[ii].Cells[jj].Selected == true))
                    {
                        startwinp = true;
                        break;
                    }
            cc = dataGridView1.GetCellCount(DataGridViewElementStates.Selected);//считаем кол-во выделеных букв
            cellselectedcheck(dataGridView1, A, ref selectij, cc);//Проверка на адекватное выделение
            if (selectij)//Если правильно выделено...
            {
                if (startwinp)//Если буква использована
                {
                    if (cc > 0)//Если вообще что-то выделено
                        for (int i = 0; i < cc; i++)//строке str=значение выделеных ячеек
                            str += dataGridView1.SelectedCells[i].Value;
                    for (int i = str.Length - 1; i >= 0; i--)//Переопределяем str в rts
                        rts += str[i];
                    xx = x.Split(' ');//Разбиваем наш массив использованных слов по пробелам
                    maincheck(filename2, xx, ref x, rts);
                    inp = true;//Разрешаем ввод
                    bap(A, ref B);
                    //Вводим очки
                    textBox3.Text = Convert.ToString(q1);
                    textBox4.Text = Convert.ToString(q2);
                    bool winner = false;//Инциализируем переменную для конца игры
                    for (int i = 0; i < A.Length; i++)
                        for (int j = 0; j < A[i].Length; j++)
                            if (A[i][j] == " ")
                            {
                                winner = false;
                                break;
                            }
                    if (winner)//Если пустых мест нет
                    {
                        //Открываем файл с рекордами
                        StreamReader rr = new StreamReader(filename2);
                        if (q1 > q2)//Если у первого игрока больше баллов, чем у второго
                        {
                            MessageBox.Show("Победитель - 1" + label1.Text, "Конец");
                        }
                        else if (q1 < q2)
                        {
                            MessageBox.Show("Победитель - " + label2.Text, "Конец");
                        }
                        else
                        {
                            MessageBox.Show("Ничья", "Конец");
                        }
                        rr.Close();
                    }
                }
                else//если ошибка в использовании буквы
                {
                    MessageBox.Show("Вы пытаетесь ввести слово, не вписав букву, или не использовав ее?", "Ошибка");
                    for (int i = 0; i < A.Length; i++)
                        for (int j = 0; j < A[i].Length; j++)
                            if (A[i][j] != dataGridView1.Rows[i].Cells[j].Value as string)
                                dataGridView1.Rows[i].Cells[j].Value = " ";
                    inp = true;
                }
            }
            else//Если слово выделяли неправильно
            {
                MessageBox.Show("Вы выделяете буквы как попало", "Ошибка");
                for (int i = 0; i < A.Length; i++)
                    for (int j = 0; j < A[i].Length; j++)
                        if (A[i][j] != dataGridView1.Rows[i].Cells[j].Value as string)
                            dataGridView1.Rows[i].Cells[j].Value = " ";
                inp = true;
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            textBox3.Text = Convert.ToString(0);
            textBox4.Text = Convert.ToString(0);
            label4.Visible = true;
            label3.Visible = true;
            button4.Visible = true;
            button5.Visible = true;
            textBox1.Visible = true;
            textBox2.Visible = true;
            for (int i = 0; i < 6; i++)
            {
                A[i] = new string[5];
                B[i] = new string[5];
            }
        }
    }
}
