using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Đồ_án_cuối_kì_CTDL
{
    public class Solve
    {

        string Infix;
        double Ans;
        bool ErrorCheck = false;
        /* =∞; Lũy thừa nhưng không phải số nguyên; 2 dấu toán tử đứng cạnh nhau; chỉ có toán tử nhưng không có số
          Trong căn bé hơn 0; thiếu dấu ngoặc */





        //Gán số liệu từ màn hình 
        public void Input(string input)
        {
            Infix = input.Replace(" ", ""); //Xóa dấu cách giữa các phần tử
        }

        //Lấy kết quả
        public string GetResult()
        {
            double result = 0; 
            
            List<string> Postfix = ConvertInfix(Infix);
            result = TinhPostfix(Postfix);
            if (ErrorCheck)
            {
                return "Lỗi";
            }

            
            return result.ToString();
        }

        //Kiểm tra dấu
        public bool Operators(char value)
        {
            return value == '＋' || value == '－' || value == '×' || value == '÷' || value == '^' || value == '√' || value == '/' || value == '%' || value == '!';
        }
        // Độ ưa tiên
        static int Priority(char value)
        {
            switch (value)
            {
                case '＋':
                case '－':
                    return 1;
                case '×':
                case '/':
                case '÷':
                    return 2;
                case '^':
                    return 0;
                case '%':
                case '!':
                    return 4;
                default:
                    return 0;
            }
        }

        //Chuyển sang Postfix để tính toán
        public List<string> ConvertInfix(string Infix)
        {
            MyStack myStack = new MyStack();
            List<string> Postfix = new List<string>();
            string number = "";
            

            foreach (char value in Infix)
            {
                if (value == 'A')
                {
                    Postfix.Add(Convert.ToString(Ans));
                }
                if (char.IsDigit(value) || value == '.')
                {
                    number += value;
                }
                else if (Operators(value))
                {
                    if (number.Length > 0)
                    {
                        Postfix.Add(number);
                        number = "";
                    }
                    if (value == '%')
                    {
                        Postfix.Add("100");
                    }
                    if (value == '!')
                    {
                        Postfix[Postfix.Count - 1] = Factorial(double.Parse(Postfix[Postfix.Count - 1])).ToString();
                        continue;
                    }
                    while (!myStack.IsEmpty() && Priority((char)myStack.Peek()) >= Priority(value))
                    {
                        Postfix.Add(myStack.Pop().ToString());
                    }
                    myStack.Push(value);
                }
                else if (value == '(')
                {
                    if (number.Length > 0)
                    {
                        Postfix.Add(number);
                        number = "";
                    }
                    myStack.Push(value);
                    
                }
                else if (value == ')')
                {
                    if (number.Length > 0)
                    {
                        Postfix.Add(number);
                        number = "";
                    }
                    while (!myStack.IsEmpty() && (char)myStack.Peek() != '(')
                    {
                        Postfix.Add(myStack.Pop().ToString());
                    }
                    if (myStack.Count() > 0 && (char)myStack.Peek() == '(')
                    {
                        myStack.Pop();
                    }
                }
            }
            while (myStack.Count() > 0)
            {
                if (number.Length > 0)
                {
                    Postfix.Add(number);
                    number = "";
                }
                Postfix.Add(myStack.Pop().ToString());
            }
            if (number.Length > 0)
            {
                Postfix.Add(number);
                number = "";
            }
            return Postfix;

        }


        //Tính Postfix
        public double TinhPostfix(List<string> Postfix)
        {
            MyStack myStack = new MyStack();
            foreach (string value in Postfix)
            {
                if (double.TryParse(value, out double number))
                {
                    myStack.Push(number);
                }
                else
                {
                    double Num2 = Convert.ToDouble(myStack.Pop());
                    double Num1 = Convert.ToDouble(myStack.Pop());

                    switch (value)
                    {
                        case "＋":
                            myStack.Push((double)Num1 + Num2);
                            break;
                        case "－":
                            myStack.Push((double)Num1 - Num2);
                            break;
                        case "×":
                            myStack.Push((double)Num1 * Num2);
                            break;
                        case "÷":
                        case "/":
                        case "%":
                            myStack.Push((double)Num1 / Num2);
                            break;
                        case "^":
                            myStack.Push((double)Math.Pow(Num1, Num2));
                            break;
                    }
                }
            }
           
            return  Ans = Convert.ToDouble(myStack.Pop());
        }
        public int Factorial(double value)
        {
            if (value == 0) return 1;
            else return (int)value * Factorial(value - 1);
        }
    }
}