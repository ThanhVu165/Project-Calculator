using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace Đồ_án_cuối_kì_CTDL
{
    public class Solve
    {
        MyStack myStack = new MyStack();
        string Infix;
        double Ans;
        bool OpCheck = true; //Check giá trị số âm hoặc dương ở đầu phương trình ,sau khi mở ngoặc, !,%
        bool ErrorCheck = false;


        /* =∞; Lũy thừa nhưng không phải số nguyên; 2 dấu toán tử đứng cạnh nhau; chỉ có toán tử nhưng không có số
          Trong căn bé hơn 0; thiếu dấu ngoặc */





        //Gán số liệu từ màn hình 
        public void Input(string input)
        {
            Infix = input.Replace(" ", ""); //Xóa dấu cách giữa các phần tử
            Infix = Infix.Replace("Ans", "A"); //Thay Ans bằng A để dễ xử lí
        }

        //Lấy kết quả
        public string GetResult()
        {
            double result = 0;

            List<string> Postfix = ConvertInfix(Infix);
            result = CalPostfix(Postfix);
            if (ErrorCheck) return "Lỗi";


            return Math.Round(result, 8).ToString("0.########");
        }

        //Kiểm tra dấu
        public bool Operators(char value)
        {
            return value == '+' || value == '-' || value == '×' || value == '÷' || value == '^' || value == '√' || value == '/' || value == '%' || value == '!';
        }
        // Độ ưa tiên
        static int Priority(char value)
        {
            switch (value)
            {
                case '+':
                case '-':
                    return 1;
                case '×':
                case '/':
                case '÷':
                    return 2;
                case '^':
                    return 3;
                case '%':
                case '√':
                case '!':
                    return 4;
                default:
                    return 0;
            }
        }

        //Chuyển sang Postfix để tính toán
        public List<string> ConvertInfix(string Infix)
        {

            List<string> Postfix = new List<string>();

            string number = "";



            for (int i = 0; i < Infix.Length; i++)
            {
                char? prev = i > 0 ? Infix[i - 1] : (char?)null;//biến prev giúp chứa giá trị đứng trước


                //Nhận diện số 
                if (char.IsDigit(Infix[i]) || Infix[i] == '.')
                {
                    number += Infix[i];
                    continue;
                }
                //Thực hiện kiểm tra trước và sau Ans để nhân ngầm
                if (Infix[i] == 'A')
                {

                    //Kiểm tra để thêm số vào Postfix khi gặp Ans để tranh bị lỗi 
                    if (number.Length > 0)
                    {
                        Postfix.Add(number);
                        number = "";
                    }

                    Postfix.Add("Ans");//Thêm Ans vào Postfix



                    //Kiếm tra nếu phía trước là số, !, %, Ans, ) thì thêm dấu x vào stack 
                    if (prev.HasValue && (char.IsDigit(prev.Value) || prev == '!' || prev == '%' || prev == 'A' || prev == ')'))
                    {
                        while (!myStack.IsEmpty() && Priority((char)myStack.Peek()) >= Priority('×'))
                            Postfix.Add(myStack.Pop().ToString());
                        myStack.Push('×');
                    }



                    OpCheck = false;//Không cho phép kiểm tra giá trị âm dương sau ANS

                    continue;
                }
                else if (Operators(Infix[i]))
                {
                    //Không cho phép kiểm tra giá trị âm dương sau dấu ! và %
                    if (Infix[i] == '!' || Infix[i] == '%') OpCheck = false;

                    //Kiểm tra giá trị âm dương của một toán hạng
                    if (OpCheck)
                    {
                        if (number == "" && Infix[i] == '-')
                        {
                            number += '-';
                            continue;
                        }
                        if (number == "" && Infix[i] == '+')
                        {
                            number += '+';
                            continue;
                        }


                    }
                    //KIỂM TRA TRƯỜNG HỢP NHÂN NGẦM ĐỐI VỚI PHÍA TRƯỚC DẤU CĂNG
                    if (Infix[i] == '√')
                    {
                        if (prev.HasValue && (char.IsDigit(prev.Value) || prev == '!' || prev == '%' || prev == 'A' || prev == ')'))
                        {
                            while (!myStack.IsEmpty() && Priority((char)myStack.Peek()) >= Priority('×'))
                                Postfix.Add(myStack.Pop().ToString());
                            myStack.Push('×');
                        }
                    }

                    //Thêm toán hạng vào Postfix trước khi tiến hành xét độ ưu tiên toán tử
                    if (number.Length > 0)
                    {
                        Postfix.Add(number);
                        number = "";
                    }

                    //Xét hai trường hợp là dấu ^ và không phải dấu mũ để đảm bảo việc mũ chồng không bị sai
                    while (!myStack.IsEmpty() && (Infix[i] != '^' && Priority((char)myStack.Peek()) >= Priority(Infix[i])
                                                || Infix[i] == '^' && Priority((char)myStack.Peek()) > Priority(Infix[i])))
                    {
                        Postfix.Add(myStack.Pop().ToString());
                    }
                    myStack.Push(Infix[i]);//Push toán tử vào myStack

                }
                else if (Infix[i] == '(')
                {
                    //Thêm toán hạng vào Postfix trước khi thực hiện thêm ( vào stack để đảm bảo không gây ra lỗi
                    if (number.Length > 0)
                    {
                        Postfix.Add(number);
                        number = "";
                    }

                    //Kiểm tra phần tử phía trước để thực hiện nhân ngầm (nếu có)
                    if (prev.HasValue && (char.IsDigit(prev.Value) || prev == '!' || prev == '%' || prev == 'A' || prev == ')'))
                    {
                        while (!myStack.IsEmpty() && Priority((char)myStack.Peek()) >= Priority('×'))
                            Postfix.Add(myStack.Pop().ToString());
                        myStack.Push('×');
                    }

                    //Push ( vào myStack
                    myStack.Push(Infix[i]);

                    OpCheck = true; // Cho phép kiểm tra giá trị âm dương 


                }
                else if (Infix[i] == ')')
                {
                    //Không cho phép kiểm tra giá trị âm dương vì vừa đóng ngoặt 
                    OpCheck = false;

                    //Thêm toán hạng trước khi kết thúc đóng ngoặt
                    if (number.Length > 0)
                    {
                        Postfix.Add(number);
                        number = "";
                    }

                    //Pop mọi toán tử còn lại trong myStack vào Postfix cho đến khi gặp (
                    while (!myStack.IsEmpty() && (char)myStack.Peek() != '(')
                    {
                        Postfix.Add(myStack.Pop().ToString());
                    }
                    //Pop ( ra khỏi myStack
                    if (myStack.Count() > 0 && (char)myStack.Peek() == '(')
                    {

                        myStack.Pop();
                    }
                }
            }

            //Thêm toán hạng còn xót lại cuối dãy Infix vào Postfix 
            if (number.Length > 0)
            {
                Postfix.Add(number);
                number = "";
            }
            //Pop tất cả toán tử còn lại trong myStack ra Postfix
            while (myStack.Count() > 0)
            {
                Postfix.Add(myStack.Pop().ToString());
            }

            return Postfix;
        }

        public double CalPostfix(List<string> Postfix)
        {

            foreach (string value in Postfix)
            {
                //Nếu gặp Ans sẽ Push giá trị Ans đã có vào myStack
                if (value == "Ans")
                {
                    myStack.Push(Ans);
                    continue;
                }

                //Kiểm tra số để Push vào myStack
                if (double.TryParse(value, out double num))
                {
                    myStack.Push(num);
                    continue;
                }

                //TRƯỜNG HỢP CHỈ CÓ 1 SỐ ĐƯỢC LẤY RA KHỎI myStack (Đối với toán tử: !, √, %)
                if (value == "!" || value == "√" || value == "%")
                {
                    double num1 = Convert.ToDouble(myStack.Pop());
                    switch (value)
                    {
                        case "!":
                            if (!(int.TryParse(num1.ToString(), out int number) && number > 0)) ErrorCheck = true;
                            else myStack.Push(Factorial((int)num1));
                            break;
                        case "%":
                            myStack.Push((num1 / 100));
                            break;
                        case "√":
                            if (num1 < 0) ErrorCheck = true;
                            else myStack.Push(Math.Sqrt(num1));
                            break;
                    }
                    continue;
                }

                //TRƯỜNG HỢP CÓ 2 SỐ ĐƯỢC LẤY RA KHỎI myStack (Đối với toán tử: +, -, x, ÷(/), ^)
                if (value == "+" || value == "-" || value == "×" || value == "/" || value == "÷" || value == "^")
                {
                    double num1 = Convert.ToDouble(myStack.Pop());
                    double num2 = Convert.ToDouble(myStack.Pop());
                    switch (value)
                    {
                        case "+":
                            myStack.Push(num2 + num1);
                            break;
                        case "-":
                            myStack.Push(num2 - num1);
                            break;
                        case "×":
                            myStack.Push(num1 * num2);
                            break;
                        case "/":
                        case "÷":
                            if (num1 == 0) ErrorCheck = true;
                            else myStack.Push(num2 / num1);
                            break;
                        case "^":
                            myStack.Push(Math.Pow(num2, num1));
                            break;
                    }
                    continue;
                }
            }
            

            return Ans = Convert.ToDouble(myStack.Pop());
        }
        public int Factorial(double value)
        {
            if (value == 0) return 1;
            else return (int)value * Factorial(value - 1);
        }
    }
}