using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ArtanButton1;

namespace Đồ_án_cuối_kì_CTDL
{
    

    public partial class Form1 : Form
    {
        string result = "";
        public Form1()
        {
            InitializeComponent();
        }
        //Nút số 
        private void Num_Click(object sender, EventArgs e)
        {
            ArtanButton button = new ArtanButton();
            button = (ArtanButton)sender;
            TextDisplay2.Text = TextDisplay2.Text + button.Text;
            UpdateCursorPosition();

        }
        //Nút dấu đơn giản
        private void MathOperators(object sender, EventArgs e)
        {
            ArtanButton button = new ArtanButton();
            button = (ArtanButton)sender;
            TextDisplay2.Text = TextDisplay2.Text + button.Text;
            
            UpdateCursorPosition();
        }
        //Nút dấu nâng cao
        private void SpecOperators(object sender, EventArgs e)
        {
            ArtanButton button = new ArtanButton();
            button = (ArtanButton)sender;
            if (button.Text == "1/x")
            {
                TextDisplay2.Text = TextDisplay2.Text + "1/";
            }
            if (button.Text == "√x")
            {
                TextDisplay2.Text = TextDisplay2.Text + "√";
            }
            UpdateCursorPosition();
        }

        //set cái cursor về bên lề phải để số được in ra đúng từ lề
        private void TextDisplay2_TextChanged(object sender, EventArgs e)
        {
            UpdateCursorPosition();
        }
        private void UpdateCursorPosition()
        {
            TextDisplay2.SelectionStart = TextDisplay2.TextLength;
            TextDisplay2.ScrollToCaret();
        }


        //Xóa từ phải sang trái
        private void Backspace_Click(object sender, EventArgs e)
        {
            if (TextDisplay2.TextLength > 0)
            {
                TextDisplay2.Text = TextDisplay2.Text.Substring(0, TextDisplay2.TextLength - 1);
            }
        }






        private void Equal_Click(object sender, EventArgs e)
        {
            result = TextDisplay2.Text;
            TinhToan(result);
        }
        public void TinhToan(string result)
        {
            TextDisplay2.Text = null;
            
        }

        private void ExitBtn_Click(object sender, EventArgs e)   //Nút tắt
        {
            Close();
        }

        private void MiniBtn_Click(object sender, EventArgs e)   //Nút thu nhỏ min
        {
            this.WindowState = FormWindowState.Minimized;
        }

        
    }
}
