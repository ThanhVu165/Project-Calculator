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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using System.Runtime.InteropServices;
namespace Đồ_án_cuối_kì_CTDL
{
    

    public partial class Form1 : Form
    {
        
        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImport("user32.dll")]
        private static extern bool ReleaseCapture();

        private const int WM_NCLBUTTONDOWN = 0xA1; // Mã sự kiện kéo cửa sổ
        private const int HTCAPTION = 0x2;        // Mã thanh tiêu đề của cửa sổ


        string result = "";
        
        public Form1()
        {
            InitializeComponent();
        }


        //Giúp di chuyển window
        private void TitlePanel_Paint(object sender, PaintEventArgs e)
        {
            TitlePanel.MouseDown += PanelTitleBar_MouseDown;
        }
        private void PanelTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }



        //Nút tắt
        private void ExitBtn_Click(object sender, EventArgs e)   
        {
            Close();
        }
        //Nút thu nhỏ min
        private void MiniBtn_Click(object sender, EventArgs e)   
        {
            this.WindowState = FormWindowState.Minimized;
        }



        //set cái cursor về bên lề phải để số được in ra đúng từ lề
        
        private void TextDisplay2_TextChanged(object sender, EventArgs e)
        {
            UpdateCursorPosition2();
        }
        private void UpdateCursorPosition2()
        {
            TextDisplay2.SelectionStart = TextDisplay2.TextLength;
            TextDisplay2.ScrollToCaret();
        }





        //Nút số 
        private void Num_Click(object sender, EventArgs e)
        {
            ArtanButton button = new ArtanButton();
            button = (ArtanButton)sender;
            if (TextDisplay1.Text.Length > 0)
            {
                TextDisplay2.Text = null;
                TextDisplay1.Text = null;
            }
            TextDisplay2.Text = TextDisplay2.Text + button.Text;
            UpdateCursorPosition2();

        }
        //Nút dấu đơn giản
        private void MathOperators_Click(object sender, EventArgs e)
        {
            ArtanButton button = new ArtanButton();
            button = (ArtanButton)sender;
            if (TextDisplay1.Text.Length > 0)
            {
                TextDisplay1.Text = null;
            }
            TextDisplay2.Text = TextDisplay2.Text + button.Text;
            
            UpdateCursorPosition2();
        }
        //Nút dấu nâng cao
        private void SpecOperators_Click(object sender, EventArgs e)
        {
            ArtanButton button = new ArtanButton();
            button = (ArtanButton)sender;
            if (TextDisplay1.Text.Length > 0)
            {
                TextDisplay2.Text = null;
                TextDisplay1.Text = null;
            }
            if (button.Text == "1/x")
            {
                TextDisplay2.Text = TextDisplay2.Text + "1/";
            }
            if (button.Text == "√x")
            {
                TextDisplay2.Text = TextDisplay2.Text + "√";
            }

            if (button.Text == "(")
            {
                TextDisplay2.Text = TextDisplay2.Text + "(";
            }
            if (button.Text == ")")
            {
                TextDisplay2.Text = TextDisplay2.Text + ")";
            }
            UpdateCursorPosition2();
        }



        //Xóa dữ liệu đầu vào 
        private void ClearEntryBtn_Click(object sender, EventArgs e)
        {
            TextDisplay2.Text = null;
        }
        //Xóa toàn bộ dữ liệu trên màn hình hiển thị
        private void ClearBtn_Click(object sender, EventArgs e)
        {
            TextDisplay1.Text = null;
            TextDisplay2.Text = null;
        }
        //Xóa từ phải sang trái
        private void Backspace_Click(object sender, EventArgs e)
        {
            if (TextDisplay2.TextLength > 0)
            {
                if (TextDisplay2.Text.Substring(TextDisplay2.TextLength - 1, 1) == "s")
                {
                    TextDisplay2.Text = TextDisplay2.Text.Substring(0, TextDisplay2.TextLength - 3);
                }
                else if (TextDisplay2.Text.Substring(TextDisplay2.TextLength - 1, 1) == "/")
                {
                    TextDisplay2.Text = TextDisplay2.Text.Substring(0, TextDisplay2.TextLength - 2);
                }
                else
                    TextDisplay2.Text = TextDisplay2.Text.Substring(0, TextDisplay2.TextLength - 1);
            }
            
        }
       
        //Nút Ans
        private void AnsBtn_Click(object sender, EventArgs e)
        {
            if (TextDisplay1.Text.Length > 0 && TextDisplay2.Text.Length > 0)
            {
                TextDisplay2.Text = null;
                TextDisplay1.Text = null;
            }
            TextDisplay2.Text += "Ans";
        }

        //Lịch sử tính toán
        private void HistoryBtn_Click(object sender, EventArgs e)
        {
            HistoryPanel.Height = (HistoryPanel.Height == 5) ? HistoryPanel.Height = 345 : HistoryPanel.Height = 5; 
        }
        private void ClearHistoryBtn_Click(object sender, EventArgs e)
        {
            HistoryDisplay.Clear();
            HistoryDisplay.AppendText("There is no history yet.");
        }


        public Solve solve = new Solve();
        //Nút bằng
        private void Equal_Click(object sender, EventArgs e)
        {
            if (TextDisplay2.Text != "")
            {
                result = TextDisplay2.Text;
                TextDisplay1.Text = TextDisplay2.Text;
                solve.Input(result);
                TextDisplay2.Text = solve.GetResult();
                if (HistoryDisplay.Text == "There is no history yet." && TextDisplay2.Text != "")
                {
                    HistoryDisplay.Clear();
                }
                HistoryDisplay.AppendText($"{TextDisplay1.Text} = {TextDisplay2.Text} \n");
            }
            


        }

        
    }
}
