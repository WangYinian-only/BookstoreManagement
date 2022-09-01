using BookstoreManagement.Controls;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookstoreManagement.Forms
{
    public partial class Form_DashBoard : Form
    {
        //窗体弹出或消失效果
        [DllImport("user32.dll", EntryPoint = "AnimateWindow")]
        private static extern bool AnimateWindow(IntPtr handle, int ms, int flags);
        public const Int32 AW_HOR_POSITIVE = 0x00000001;
        public const Int32 AW_HOR_NEGATIVE = 0x00000002;
        public const Int32 AW_VER_POSITIVE = 0x00000004;
        public const Int32 AW_VER_NEGATIVE = 0x00000008;
        public const Int32 AW_CENTER = 0x00000010;
        public const Int32 AW_HIDE = 0x00010000;
        public const Int32 AW_ACTIVATE = 0x00020000;
        public const Int32 AW_SLIDE = 0x00040000;
        public const Int32 AW_BLEND = 0x00080000;


        int PanelWidth;
        bool isCollapsed;
        private string managerId;       //保存登录用户的编号
        int[] colorFul = new int[3];    //定义一个Int数组，保存RGB颜色
        string Auth;

        public Form_DashBoard()
        {
            InitializeComponent();
        }

        public Form_DashBoard(string Id, string name, string juese)
        {
            InitializeComponent();
            //窗体弹出效果
            //AnimateWindow(this.Handle, 0, AW_CENTER);
            this.managerId = Id;
            GetAuth();    //获取权限
            CheckAuth();    //功能验证
            ChangeColor();
            Color c = Color.FromArgb(colorFul[0], colorFul[1], colorFul[2]);  //创建一个颜色对象并赋予RGB颜色
            ChangeColor(c); //调用系统主题更改方法，传入颜色值 'c'
            this.lblName.Text = name;
            this.lblJuese.Text = juese;
            PanelWidth = panelLeft.Width;
            isCollapsed = false;
            timer2.Start(); //定时器启动，时间开始动态运转
            init(); //初始化窗体
        }
        private BillManage billmanage;
        private BookAdd bookadd;
        private BorrowManage borrmanage;
        private Home home;
        private ManageUser manageuser;
        private QueryInfo queryInfo;
        private ReaderManage readermanage;

        private void init()
        {
            billmanage = new BillManage();
            bookadd = new BookAdd();
            borrmanage = new BorrowManage();
            home = new Home();
            manageuser = new ManageUser();
            queryInfo = new QueryInfo();
            readermanage = new ReaderManage();
        }

        private void ChangeColor()
        {
            DBHelper dh = new DBHelper();
            try
            {
                MySqlConnection conn = dh.Connection;
                dh.OpenConnection();
                string sql = string.Format("select MColorFul from maneger where  MID = '{0}'", managerId);
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                string result = Convert.ToString(cmd.ExecuteScalar());

                //字符串处理转换成int数组
                string[] param = (result.Split(','));
                for (int i = 0; i < param.Length; i++)
                {
                    colorFul[i] = Convert.ToInt32(param[i]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                dh.CloseConnection();
            }
        }

        private void AddControlsToPanel(Control c)
        {
            c.Dock = DockStyle.Fill;
            panelControl.Controls.Clear();
            panelControl.Controls.Add(c);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            //窗体弹出效果
            AnimateWindow(this.Handle, 10, AW_HIDE + AW_CENTER);
            Application.Exit();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            if (isCollapsed)
            {
                panelLeft.Width = panelLeft.Width + 4;
                if (panelLeft.Width >= PanelWidth)
                {
                    timer1.Stop();
                    isCollapsed = false;
                    this.Refresh();
                }
            }
            else
            {
                panelLeft.Width = panelLeft.Width - 4;
                if (panelLeft.Width <= 64)
                {
                    timer1.Stop();
                    isCollapsed = true;
                    this.Refresh();
                }
            }
        }

        private void lblSideMove(Control btn)
        {
            lblSide.Top = btn.Top;
            lblSide.Height = btn.Height;

        }
        private void timer2_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString(format);
        }

        private void Form_DashBoard_Load(object sender, EventArgs e)
        {
            AddControlsToPanel(new Home());
            ReadOut();  //更改时间格式
        }

        private void ReadOut()
        {
            //先将内容读取出来
            string File = Application.StartupPath;
            string path = File.Substring(0, File.Length - 9) + "id_pwd\\TimeConfig.txt";
            FileStream fs = new FileStream(path, FileMode.Open);
            StreamReader sr = new StreamReader(fs, Encoding.Default);
            string content = sr.ReadToEnd();
            sr.Close();
            fs.Close();
            //对内容进行分割处理
            if (content != "" || content != null)
            {
                this.format = content;
            }

        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            lblSideMove(btnHome);
            AddControlsToPanel(home);
        }

        private void btnbookmanage_Click(object sender, EventArgs e)
        {
            if (this.lblJuese.Text != "用户")
            {
                lblSideMove(btnbookmanage);
                AddControlsToPanel(bookadd);
            }

        }

        private void btnreader_Click(object sender, EventArgs e)
        {
            if (this.lblJuese.Text != "用户")
            {
                lblSideMove(btnreader);
                AddControlsToPanel(readermanage);
            }

        }

        private void btnborrow_Click(object sender, EventArgs e)
        {
            if (this.lblJuese.Text != "用户")
            {
                lblSideMove(btnborrow);
                AddControlsToPanel(borrmanage);
            }
        }

        private void btnqueryinfo_Click(object sender, EventArgs e)
        {
            lblSideMove(btnqueryinfo);
            AddControlsToPanel(queryInfo);
        }


        private string format = "HH:mm:ss";
        private void changeTime(string format)
        {
            this.format = format;
        }

        private void btnsystemset_Click(object sender, EventArgs e)
        {
            if (this.lblJuese.Text != "用户" && this.lblJuese.Text != "管理员")
            {
                Form_SystemSet fs = new Form_SystemSet(managerId);
                fs.change += new Form_SystemSet.ChangeMainForm(ChangeColor);
                fs.ChangeTimeFormat += new Form_SystemSet.ChangeMainTime(changeTime);
                fs.ShowDialog();
            }
        }

        //更改窗口主题
        private void ChangeColor(Color c)
        {
            this.panel4.BackColor = c;
            this.panelLeft.BackColor = c;
            this.lblTitle.ForeColor = c;
            this.btnHome.BackColor = c;
            this.btnbookmanage.BackColor = c;
            this.btnreader.BackColor = c;
            this.btnborrow.BackColor = c;
            this.btnqueryinfo.BackColor = c;
            this.btnsystemset.BackColor = c;
            this.btnbillmanage.BackColor = c;
            this.btnusermanage.BackColor = c;
            this.button8.BackColor = c;
        }

        private void btnbillmanage_Click(object sender, EventArgs e)
        {
            if (this.lblJuese.Text != "用户")
            {
                lblSideMove(btnbillmanage);
                AddControlsToPanel(billmanage);
            }
        }

        private void btnusermanage_Click(object sender, EventArgs e)
        {
            if (this.lblJuese.Text != "用户" && this.lblJuese.Text != "管理员")
            {
                lblSideMove(btnusermanage);
                AddControlsToPanel(manageuser);
            }
        }

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        private void panel3_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Clicks == 1)
            {
                //窗体移动
                if (e.Button == MouseButtons.Left)
                {
                    ReleaseCapture(); //释放鼠标捕捉
                    //发送左键点击的消息至该窗体(标题栏)
                    SendMessage(Handle, 0xA1, 0x02, 0);
                }
            }
        }

        //最大化最小化切换
        private void panel3_DoubleClick(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
            }
            else if (this.WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Maximized;
            }
        }

        private void GetAuth()
        {
            DBHelper dh = new DBHelper();
            try
            {
                MySqlConnection conn = dh.Connection;
                dh.OpenConnection();

                string sql = string.Format(@"select MAuthArea from maneger where MID = '{0}'", managerId);
                Auth = Convert.ToString(new MySqlCommand(sql, conn).ExecuteScalar());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                dh.CloseConnection();
            }
        }

        //判断验证
        private void CheckAuth()
        {
            btnHome.Enabled = Auth.Contains("A") ? true : false;
            btnbookmanage.Enabled = Auth.Contains("B") ? true : false;
            btnreader.Enabled = Auth.Contains("C") ? true : false;
            btnborrow.Enabled = Auth.Contains("D") ? true : false;
            btnqueryinfo.Enabled = Auth.Contains("E") ? true : false;
            btnsystemset.Enabled = Auth.Contains("F") ? true : false;
            btnbillmanage.Enabled = Auth.Contains("G") ? true : false;
            btnusermanage.Enabled = Auth.Contains("H") ? true : false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
            }
            else if (this.WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Maximized;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}