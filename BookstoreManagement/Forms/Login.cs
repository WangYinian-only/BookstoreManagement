using BookstoreManagement.Forms;
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

namespace BookstoreManagement
{
    public partial class Login : Form
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
        public Login()
        {
            InitializeComponent();
            //窗体弹出效果
            AnimateWindow(this.Handle, 100, AW_CENTER);
            ReadOut();
        }



        DBHelper dh = new DBHelper();

        private void button2_Click(object sender, EventArgs e)
        {
            //窗体弹出效果
            AnimateWindow(this.Handle, 10, AW_HIDE + AW_CENTER);
            this.Close();
        }

        private bool Check()
        {
            if (txtName.Text.Trim().Equals("") || txtName.Text == null)
            {
                MessageBox.Show("请输入用户名！\t", "登录提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return false;
            }
            else if (txtPassword.Text.Trim().Equals("") || txtPassword.Text == null)
            {
                MessageBox.Show("请输入密码！\t", "登录提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return false;
            }
            else
            {
                return true;
            }
        }
        Form_DashBoard fdb = null;
        private void button1_Click_1(object sender, EventArgs e)
        {
            if (Check())
            {
                //更改提示状态
                this.label3.Text = "正在登录...";
                this.label3.ForeColor = Color.Blue;
                MySqlConnection conn = dh.Connection;
                try
                {
                    dh.OpenConnection();
                    string Name = txtName.Text.Trim();
                    string Password = txtPassword.Text.Trim();
                    //获取是否登录成功
                    string sql1 = string.Format("select COUNT(1) from maneger where MID = '{0}' and Mpwd = '{1}'", Name, Password);
                    MySqlCommand cmd1 = new MySqlCommand(sql1, conn);
                    int result1 = Convert.ToInt32(cmd1.ExecuteScalar());

                    if (this.cb.Checked)
                    {
                        WriteIn();
                    }
                    if (result1 > 0)
                    {
                        //查询账户名称
                        string sql2 = string.Format(@"select M.MName from maneger M
                                                    inner join managertype T
                                                    on(M.MAuth = T.ID)
                                                    where MID = '{0}'and Mpwd = '{1}'", Name, Password);
                        MySqlCommand cmd2 = new MySqlCommand(sql2, conn);
                        string result2 = Convert.ToString(cmd2.ExecuteScalar());

                        //查询账号权限
                        string sql3 = string.Format(@"select T.Jurisdiction from maneger M
                                                    inner join managertype T
                                                    on(M.MAuth = T.ID)
                                                    where MID = '{0}'and Mpwd = '{1}'", Name, Password);
                        MySqlCommand cmd3 = new MySqlCommand(sql3, conn);
                        string result3 = Convert.ToString(cmd3.ExecuteScalar());
                        //初始化主窗口
                        fdb = new Form_DashBoard(Name, result2, result3);
                        this.progressBar1.Visible = true;
                        this.timer1.Enabled = true;
                        this.timer1.Start();  
                    }
                    else
                    {
                        //更改提示状态
                        this.label3.Text = "登录失败";
                        this.label3.ForeColor = Color.Red;
                        MessageBox.Show("账号或密码错误！请检查后登录", "登录提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    dh.CloseConnection();
                }
            }

        }

        //打开超链接与管理员会话
        private void label5_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://wpa.qq.com/msgrd?v=3&uin=3096948550&site=qq&menu=yes");
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (Check())
            {
                if (this.cb.Checked) //如果选中了则执行记录账号和密码操作
                {
                    WriteIn();
                }
                else
                {
                    ClearConfig();
                }
            }
        }

        //写入文档
        private void WriteIn()
        {
            string File = Application.StartupPath;
            string path = File.Substring(0, File.Length - 9) + "id_pwd\\PasswordConfig.txt";
            string content = this.txtName.Text.Trim() + "," + this.txtPassword.Text.Trim() + ",1";
            FileStream fs = new FileStream(path, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs, Encoding.Default);
            sw.Write(content);  //写入文档
            sw.Close();
            fs.Close();
        }

        //读取文档
        private void ReadOut()
        {
            //先将内容读取出来
            string File = Application.StartupPath;
            string path = File.Substring(0, File.Length - 9) + "id_pwd\\PasswordConfig.txt";
            FileStream fs = new FileStream(path, FileMode.Open);
            StreamReader sr = new StreamReader(fs, Encoding.Default);
            string content = sr.ReadToEnd();
            sr.Close();
            fs.Close();
            //对内容进行分割处理
            if (content != "")
            {
                string[] pwd_id = content.Split(',');
                this.txtName.Text = pwd_id[0];  //将内容写进文本框中
                this.txtPassword.Text = pwd_id[1];
                if (pwd_id[2] == "1")
                {
                    this.cb.Checked = true;
                }
            }
        }

        //清空文档
        private void ClearConfig()
        {
            string File = Application.StartupPath;
            string path = File.Substring(0, File.Length - 9) + "id_pwd\\PasswordConfig.txt";
            string content = "";
            FileStream fs = new FileStream(path, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs, Encoding.Default);
            sw.Write(content);  //写入文档
            sw.Close();
            fs.Close();
        }

        private Point mousePoint = new Point();

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            this.mousePoint.X = e.X;
            this.mousePoint.Y = e.Y;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Top = Control.MousePosition.Y - mousePoint.Y;
                this.Left = Control.MousePosition.X - mousePoint.X;
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.cnblogs.com/ITRonion");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this.progressBar1.Value != 100)
            {
                this.progressBar1.PerformStep();
            }
            else
            {
                this.timer1.Stop();
                fdb.Show();
                this.Hide();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
            }
            else if(this .WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Maximized;
            }
        }
    }
}