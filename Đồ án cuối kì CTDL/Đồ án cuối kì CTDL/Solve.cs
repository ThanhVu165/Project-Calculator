using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Đồ_án_cuối_kì_CTDL
{
    public class Solve
    {
        
        private string Infix;

        public void Input(string input)
        {
            Infix = input;
        }

        public double GetResult()
        {
            double result = 0;
            string Postfix = ConvertInfix(Infix);
            result = TinhPostfix(Postfix);

            return result;
        }
        public bool Operators(char value)
        {
            return value == '＋' || value == '－' || value == '×' || value == '÷' || value == '^' || value == '√' || value == '/';
        }

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
                    return 3;
                default:
                    return 0;
            }
        }
        public string ConvertInfix(string Infix)
        {
            MyStack myStack = new MyStack();
            string Postfix = "";

            foreach (char value in Infix)
            {
                if (char.IsDigit(value))
                {
                    Postfix += value;
                }
                else if (Operators(value))
                {
                    while (!myStack.IsEmpty() && Priority((char)myStack.Peek()) >= Priority(value))
                    {
                        Postfix += myStack.Pop();
                    }
                    myStack.Push(value);
                }
                else if (value == '(')
                {
                    myStack.Push(value);
                }
                else if (value == ')')
                {
                    while (!myStack.IsEmpty() && (char)myStack.Peek() != '(')
                    {
                        Postfix += myStack.Pop();
                    }
                    if (myStack.Count() > 0 && (char)myStack.Peek() == '(')
                    {
                        myStack.Pop();
                    }
                }
            }
            while (myStack.Count() > 0)
            {
                Postfix += myStack.Pop();
            }
            return Postfix;
        }



        public double TinhPostfix(string Postfix)
        {
            MyStack myStack = new MyStack();

            foreach (char value in Postfix)
            {

                if (char.IsDigit(value))
                {
                    myStack.Push((double)(value - '0'));
                }
                else
                {
                    double Num2 = Convert.ToDouble(myStack.Pop());
                    double Num1 = Convert.ToDouble(myStack.Pop());

                    switch (value)
                    {
                        case '＋':
                            myStack.Push((double)Num1 + Num2);
                            break;
                        case '－':
                            myStack.Push((double)Num1 - Num2);
                            break;
                        case '×':
                            myStack.Push((double)Num1 * Num2);
                            break;
                        case '÷':
                        case '/':
                            myStack.Push((double)Num1 / Num2);
                            break;
                        case '^':
                            myStack.Push((double)Math.Pow(Num1, Num2));
                            break;
                    }
                }
            }

            return Convert.ToDouble(myStack.Pop());
        }
    }
}
