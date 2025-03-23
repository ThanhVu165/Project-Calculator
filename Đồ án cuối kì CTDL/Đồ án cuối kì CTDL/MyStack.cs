using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Đồ_án_cuối_kì_CTDL
{
    //Các phương thức của Stack: IsEmpty, Push, Pop, Peek, Reverse, Sort, Clear, Contains, Remove, Count
    public class Node
    {
        public Node next; //Trỏ đến phần tử kế tiếp trong Stack
        public object data; //Dữ liệu của Node
    }
    public class MyStack
    {
        Node top; //Biến trỏ đến phần tử trên cùng của Stack

        public int count = 0; //tạo biến đếm

        public bool IsEmpty()
        {
            return top == null; //Kiểm tra xem Stack có rỗng không
        }
        public void Push(object data)
        {
            Node newNode = new Node(); //Tạo một Node mới
            newNode.data = data; //Gán giá trị cho Node
            newNode.next = top; //Trỏ Node mới tới Node trên cùng của Stack
            top = newNode;  //Đặt Node mới làm top
            count++; //Tăng biến đếm
        }
        public object Pop()
        {
            if (IsEmpty())
            {
                return null;//Kiểm tra xem Stack có rỗng không
            }
            object data = top.data; //Lấy dữ liệu của Node trên cùng
            top = top.next; //Chuyển top sang Node tiếp theo
            if (count >= 0) //Giảm biến đếm
                count--;
            return data; //Trả về dữ liệu của Node trên cùng
        }
        public object Peek()
        {
            return top.data; //Lấy dữ liệu của Node trên cùng
        }

        public void Reverse()
        {
            List<object> list = new List<object>(); // Tạo danh sách tạm để lưu các phần tử trong Stack
            while (!IsEmpty()) // Lặp cho đến khi Stack rỗng
                list.Add(Pop()); // Lấy từng phần tử khỏi Stack và lưu vào danh sách

            for (int i = 0; i < list.Count; i++) // Duyệt lại danh sách đã lưu
                Push(list[i]); // Đưa các phần tử trở lại Stack
        }

        public void Sort()
        {

            List<object> list = new List<object>(); // Tạo danh sách tạm để lưu các phần tử trong Stack
            while (!IsEmpty()) // Lặp cho đến khi Stack rỗng
                list.Add(Pop()); // Lấy từng phần tử khỏi Stack và lưu vào danh sách

            list.Sort(); // Sắp xếp danh sách

            for (int i = 0; i < list.Count; i++) // Duyệt lại danh sách đã lưu
                Push(list[i]); // Đưa các phần tử trở lại Stack
        }
        public void Clear()
        {
            while (!IsEmpty()) // Lặp cho đến khi Stack rỗng
                Pop();
        }
        public bool Contains(object value)
        {

            List<object> list = new List<object>(); // Tạo danh sách tạm để lưu các phần tử trong Stack

            while (!IsEmpty()) // Lặp cho đến khi Stack rỗng
                list.Add(Pop()); // Lấy từng phần tử khỏi Stack và lưu vào danh sách

            for (int i = 0; i < list.Count; i++) // Duyệt lại danh sách đã lưu
                Push(list[i]); // Đưa các phần tử trở lại Stack

            return list.Contains(value);
        }
        public void Remove(object value)
        {
            List<object> list = new List<object>(); // Tạo danh sách tạm để lưu các phần tử trong Stack
            while (!IsEmpty()) // Lặp cho đến khi Stack rỗng
                list.Add(Pop()); // Lấy từng phần tử khỏi Stack và lưu vào danh sách

            if (list.Contains(value)) // Kiểm tra xem danh sách có chứa giá trị cần xóa không
            {
                list.Remove(value); // Xóa giá trị cần xóa
            }

            for (int i = 0; i < list.Count; i++) // Duyệt lại danh sách đã lưu
                Push(list[i]); // Đưa các phần tử trở lại Stack
        }
        public int Count()
        {
            return count;
        }
    }
}
