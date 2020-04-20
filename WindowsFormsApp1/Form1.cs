using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calc
{

    public partial class Form1 : Form
    {
        //Промежуточный результат вычислений
        double result; 
        //Участвующий в вычислениях операнд
        double operand; 
        //Знак арифметической операции
        string operation; 
        bool op_flag = false, eq_flag = false, er_flag = false; // Флаги нажатия арифметических операций, равно и флаг возникновения ошибки
        //Количество знаков в Text Box
        int count;

        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Обработчик события KeyDown ввода цифр с помощью клавиатуры
        /// </summary>
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyValue)
            {
                case (char)Keys.D1:
                    button12.PerformClick();
                    break;
                case (char)Keys.D2:
                    button10.PerformClick();
                    break;
                case (char)Keys.D3:
                    button11.PerformClick();
                    break;
                case (char)Keys.D4:
                    button8.PerformClick();
                    break;
                case (char)Keys.D5:
                    button6.PerformClick();
                    break;
                case (char)Keys.D6:
                    button7.PerformClick();
                    break;
                case (char)Keys.D7:
                    button1.PerformClick();
                    break;
                case (char)Keys.D8:
                    button3.PerformClick();
                    break;
                case (char)Keys.D9:
                    button2.PerformClick();
                    break;
                case (char)Keys.D0:
                    button14.PerformClick();
                    break;
                case (char)Keys.Delete:
                    button16.PerformClick();
                    break;
                case (char)Keys.Multiply:
                    button9.PerformClick();
                    break;
                case (char)Keys.Divide:
                    button13.PerformClick();
                    break;
                case (char)Keys.Add:
                    button4.PerformClick();
                    break;
                case (char)Keys.Subtract:
                    button5.PerformClick();
                    break;
                default:
                    break;
            }

        }

        /// <summary>
        /// Обработчик события нажатия кнопок, отвечающих за цифры 0-9 и разделитель целой и дробной части
        /// </summary>
        private void Botton_Click(object sender, EventArgs e)
        {

            if (count < 30 || op_flag || er_flag || eq_flag)
            {
                string tmp = Result.Text;
                if (er_flag)
                {
                    button16_Click(null, null);
                    Result.Text = "";
                }
                else if (Result.Text == "0" || op_flag)
                {
                    Result.Text = "";
                    op_flag = false;
                    er_flag = false;
                }
                if (eq_flag == false)
                {
                    op_flag = false;
                }
                else
                {
                    button16_Click(null, null);
                    Result.Text = "";
                }
                Button button = (Button)sender;
                if (button.Text == ",")
                {
                    if (!Result.Text.Contains(","))
                    {
                        if (tmp == "0")
                        {
                            Result.Text = "0" + button.Text;
                        }
                        else
                        {
                            Result.Text += button.Text;
                        }
                    }
                }
                else
                {
                    Result.Text += button.Text;
                }
            }
        }

        /// <summary>
        /// Обработчик события нажатия кнопки сброса всех изменений
        /// </summary>
        private void button16_Click(object sender, EventArgs e)
        {
            count = 1;
            result = 0;
            operand = 0;
            Result.Text = "0";
            label.Text = "";
            operation = "";
            op_flag = false;
            eq_flag = false;
            er_flag = false;
        }

        /// <summary>
        /// Обработчик события нажатия кнопок отвечающих за арифметические операции
        /// </summary>
        private void Oper_click(object sender, EventArgs e)
        {
            if (er_flag)
            {
                button16_Click(null, null);
            }
            else
            {
                Button button = (Button)sender;

                if (op_flag)
                {
                    operation = button.Text;
                    label.Text = label.Text.Substring(0, Result.Text.Length - 1) + operation;
                }
                else if (eq_flag)
                {
                    operation = button.Text;
                    operand = result;
                    label.Text = result.ToString() + operation;
                    op_flag = true;
                }
                else
                {
                    if (double.TryParse(label.Text + Result.Text, out double promres))
                    {
                        operand = double.Parse(Result.Text);
                        operation = button.Text;
                        label.Text = promres.ToString() + operation;
                    }
                    else
                    {
                        equal_Click(null, null);
                        operation = button.Text;
                        operand = result;
                        label.Text = result.ToString() + operation;
                    }
                    op_flag = true;
                }
            }
            eq_flag = false;
        }
       
        /// <summary>
        /// Обработчик события нажатие кнопки равно
        /// </summary>
        private void equal_Click(object sender, EventArgs e)
        {
            if (er_flag)
            {
                button16_Click(null, null);
            }

            else
            {
                double tmp;
                switch (operation)
                {
                    case "+":
                        if (eq_flag)
                        {
                            result += double.Parse(label.Text.Substring(label.Text.Length - 2, 1));
                        }
                        else if (double.TryParse(Result.Text, out tmp))
                        {
                            result = operand + tmp;
                            label.Text += tmp + "=";
                        }
                        else
                        {
                            result = operand + operand;
                            label.Text += operand + "=";
                        }
                        Result.Text = result.ToString();
                        eq_flag = true;
                        break;
                    case "-":
                        if (eq_flag)
                        {
                            result -= double.Parse(Result.Text.Substring(Result.Text.Length - 2, 1));
                        }
                        else if (double.TryParse(Result.Text, out tmp))
                        {
                            result = operand - tmp;
                            label.Text += tmp + "=";
                        }
                        else
                        {
                            result = operand - operand;
                            label.Text += operand + "=";
                        }
                        Result.Text = result.ToString();
                        eq_flag = true;
                        break;
                    case "*":
                        if (eq_flag)
                        {
                            result *= double.Parse(Result.Text.Substring(Result.Text.Length - 2, 1));
                        }
                        else if (double.TryParse(Result.Text, out tmp))
                        {
                            result = operand * tmp;
                            label.Text += +tmp + "=";
                        }
                        else
                        {
                            result = operand * operand;
                            label.Text += operand + "=";
                        }
                        Result.Text = result.ToString();
                        eq_flag = true;
                        break;
                    case "/":
                        if (eq_flag)
                        {
                            result /= double.Parse(label.Text.Substring(label.Text.Length - 2, 1));
                        }
                        else if (double.TryParse(Result.Text, out tmp))
                        {
                            if (tmp == 0)
                            {
                                Result.Text = "Ошибка";
                                label.Text = "";
                                er_flag = true;
                                break;
                            }
                            else
                            {
                                result = operand / tmp;
                                label.Text += tmp + "=";
                            }
                        }
                        else
                        {
                            result = operand / operand;
                            label.Text += operand + "=";
                        }
                        Result.Text = result.ToString();
                        eq_flag = true;
                        break;
                    default:
                        result = double.Parse(Result.Text);
                        label.Text = Result.Text + "=";
                        break;
                }
            }
            // Передаем результат вычислений в формате строки в буфер
            Clipboard.SetText(result.ToString());
        }
        /// <summary>
        /// Обработчик события Resize формы
        /// Изменяем размер шрифта в Text Box в зависимости от размера окна
        /// Изменение размера шрифта Buttoms в зависимости от ширина окна
        /// </summary>
        private void Form1_Resize(object sender, EventArgs e)
        {
            if (Width >= 500 && Width <= 708)
            {
                label.Font = new Font(Result.Font.FontFamily, 16, FontStyle.Bold);
                equal.Font = new Font(Result.Font.FontFamily, 16, FontStyle.Bold);
                button1.Font = new Font(Result.Font.FontFamily, 16, FontStyle.Bold);
                button2.Font = new Font(Result.Font.FontFamily, 16, FontStyle.Bold);
                button3.Font = new Font(Result.Font.FontFamily, 16, FontStyle.Bold);
                button4.Font = new Font(Result.Font.FontFamily, 16, FontStyle.Bold);
                button5.Font = new Font(Result.Font.FontFamily, 16, FontStyle.Bold);
                button6.Font = new Font(Result.Font.FontFamily, 16, FontStyle.Bold);
                button7.Font = new Font(Result.Font.FontFamily, 16, FontStyle.Bold);
                button8.Font = new Font(Result.Font.FontFamily, 16, FontStyle.Bold);
                button9.Font = new Font(Result.Font.FontFamily, 16, FontStyle.Bold);
                button10.Font = new Font(Result.Font.FontFamily, 16, FontStyle.Bold);
                button11.Font = new Font(Result.Font.FontFamily, 16, FontStyle.Bold);
                button12.Font = new Font(Result.Font.FontFamily, 16, FontStyle.Bold);
                button13.Font = new Font(Result.Font.FontFamily, 16, FontStyle.Bold);
                button14.Font = new Font(Result.Font.FontFamily, 16, FontStyle.Bold);
                button15.Font = new Font(Result.Font.FontFamily, 16, FontStyle.Bold);
                button16.Font = new Font(Result.Font.FontFamily, 16, FontStyle.Bold);
                if (count < 15)
                {
                    Result.Font = new Font(Result.Font.FontFamily, 24, FontStyle.Bold);
                }
                else
                {
                    Result.Font = new Font(Result.Font.FontFamily, 12, FontStyle.Bold);
                }
            }
            else if (Width >=384  && Width < 500)
            {
                label.Font= new Font(Result.Font.FontFamily, 12, FontStyle.Bold);
                equal.Font = new Font(Result.Font.FontFamily, 12, FontStyle.Bold);
                button1.Font = new Font(Result.Font.FontFamily, 12, FontStyle.Bold);
                button2.Font = new Font(Result.Font.FontFamily, 12, FontStyle.Bold);
                button3.Font = new Font(Result.Font.FontFamily, 12, FontStyle.Bold);
                button4.Font = new Font(Result.Font.FontFamily, 12, FontStyle.Bold);
                button5.Font = new Font(Result.Font.FontFamily, 12, FontStyle.Bold);
                button6.Font = new Font(Result.Font.FontFamily, 12, FontStyle.Bold);
                button7.Font = new Font(Result.Font.FontFamily, 12, FontStyle.Bold);
                button8.Font = new Font(Result.Font.FontFamily, 12, FontStyle.Bold);
                button9.Font = new Font(Result.Font.FontFamily, 12, FontStyle.Bold);
                button10.Font = new Font(Result.Font.FontFamily, 12, FontStyle.Bold);
                button11.Font = new Font(Result.Font.FontFamily, 12, FontStyle.Bold);
                button12.Font = new Font(Result.Font.FontFamily, 12, FontStyle.Bold);
                button13.Font = new Font(Result.Font.FontFamily, 12, FontStyle.Bold);
                button14.Font = new Font(Result.Font.FontFamily, 12, FontStyle.Bold);
                button15.Font = new Font(Result.Font.FontFamily, 12, FontStyle.Bold);
                button16.Font = new Font(Result.Font.FontFamily, 12, FontStyle.Bold);
                if (count < 15)
                {
                    Result.Font = new Font(Result.Font.FontFamily, 16, FontStyle.Bold);

                }
                else
                {
                    Result.Font = new Font(Result.Font.FontFamily, 8, FontStyle.Bold);
                }
            }
        }
        /// <summary>
        /// Обработчик события Text Changed для Text Box
        /// Определяем число символов в Text Box и меняем размер шрифта при необходимости
        /// </summary>
        private void Result_TextChange(object sender, EventArgs e)
        {
            count = Result.Text.Length;
            Form1_Resize(null, null);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}