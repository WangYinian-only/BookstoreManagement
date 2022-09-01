using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookstoreManagement.Forms
{
    public partial class Form_SystemSet : Form
    {
        public Form_SystemSet()
        {
            InitializeComponent();
        }

        private string managerId;
        public Form_SystemSet(string managerId)
        {
            InitializeComponent();
            this.managerId = managerId;
        }

        //初始化数据库链接
        DBHelper dh = new DBHelper();
        MySqlConnection conn = null;
        MySqlCommand cmd = null;

        //关闭窗口
        private void button4_Click(object sender, EventArgs e)
        {
            //关闭之前做出判断
            if (DialogResult.Yes == MessageBox.Show("确定保存权限修改吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                SetAuth();
                Save();
                this.Close();
            }
            else
            {
                this.Close();
            }
        }

        public delegate void ChangeMainForm(Color c);
        public event ChangeMainForm change;

        //定义一个字符串保存rgb的值
        private string rgbValue;

        //更改主窗体主题
        private void btnColor1_Click(object sender, EventArgs e)
        {
            //rgbValue = "0, 71, 160";
            rgbValue = "0,100,0";
            UpdateDefaultColor(rgbValue);
            Color c = Color.FromArgb(0, 100, 0);
            change(c);
        }

        private void btnColor2_Click(object sender, EventArgs e)
        {
            rgbValue = "0,100,0";
            UpdateDefaultColor(rgbValue);
            Color c = Color.FromArgb(0,100,0);
            change(c);
        }

        private void btnColor3_Click(object sender, EventArgs e)
        {
            rgbValue = "248, 172, 89";
            UpdateDefaultColor(rgbValue);
            Color c = Color.FromArgb(248, 172, 89);
            change(c);
        }

        private void btnColor4_Click(object sender, EventArgs e)
        {
            rgbValue = "26, 179, 148";
            UpdateDefaultColor(rgbValue);
            Color c = Color.FromArgb(26, 179, 148);
            change(c);
        }

        private void btnColor5_Click(object sender, EventArgs e)
        {
            rgbValue = "116, 40, 148";
            UpdateDefaultColor(rgbValue);
            Color c = Color.FromArgb(116, 40, 148);
            change(c);
        }

        private void btnColor6_Click(object sender, EventArgs e)
        {
            rgbValue = "198, 47, 47";
            UpdateDefaultColor(rgbValue);
            Color c = Color.FromArgb(198, 47, 47);
            change(c);
        }

        //定义修改默认主题的方法
        private void UpdateDefaultColor(string color)
        {
            try
            {
                conn = dh.Connection;
                dh.OpenConnection();
                string sql = string.Format("UPDATE maneger set MColorFul = '{0}' where MID = '{1}'", color, managerId);
                cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
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

        public delegate void ChangeMainTime(string format);
        public event ChangeMainTime ChangeTimeFormat;

        int selectedIndex;
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedIndex = this.cmbTime.SelectedIndex + 1;
            if (selectedIndex == 1)
            {
                ChangeTimeFormat("yyyy/MM/dd dddd");
                WriteIn("yyyy/MM/dd dddd");
            }
            else if (selectedIndex == 2)
            {
                ChangeTimeFormat("yyyy/MM/dd dddd tt hh:mm");
                WriteIn("yyyy/MM/dd dddd tt hh:mm");
            }
            else if (selectedIndex == 3)
            {
                ChangeTimeFormat("yyyy-MM-dd HH:mm:ss");
                WriteIn("yyyy-MM-dd HH:mm:ss");
            }
            else if (selectedIndex == 4)
            {
                ChangeTimeFormat("hh:mm");
                WriteIn("hh:mm");
            }
        }

        private void WriteIn(string content)
        {
            string File = Application.StartupPath;
            string path = File.Substring(0, File.Length - 9) + "id_pwd\\TimeConfig.txt";
            FileStream fs = new FileStream(path, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs, Encoding.Default);
            sw.Write(content);  //写入文档
            sw.Close();
            fs.Close();
        }


        private Point mousePoint = new Point();
        private void panel5_MouseDown(object sender, MouseEventArgs e)
        {
            this.mousePoint.X = e.X;
            this.mousePoint.Y = e.Y;
        }

        private void panel5_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Top = Control.MousePosition.Y - mousePoint.Y;
                this.Left = Control.MousePosition.X - mousePoint.X;
            }
        }

        /// <summary>
        /// 以下事件皆为权限分配处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private string manager ="";
        private string user ="";

        //权限修改
        private void SetAuth()
        {
            manager = a.Checked ? manager += "A" : manager += "";
            manager = b.Checked ? manager += "B" : manager += "";
            manager = c.Checked ? manager += "C" : manager += "";
            manager = d.Checked ? manager += "D" : manager += "";
            manager = e.Checked ? manager += "E" : manager += "";
            manager = f.Checked ? manager += "F" : manager += "";
            manager = g.Checked ? manager += "G" : manager += "";
            manager = h.Checked ? manager += "H" : manager += "";
            user = a1.Checked ? user += "A" : user += "";
            user = b1.Checked ? user += "B" : user += "";
            user = c1.Checked ? user += "C" : user += "";
            user = d1.Checked ? user += "D" : user += "";
            user = e1.Checked ? user += "E" : user += "";
            user = f1.Checked ? user += "F" : user += "";
            user = g1.Checked ? user += "G" : user += "";
            user = h1.Checked ? user += "H" : user += "";
        }

        //保存权限修改
        private void Save()
        {   
            try
            {
                conn = dh.Connection;
                dh.OpenConnection();
                string sql = "";
                int result = 0;

                //修改管理员权限
                sql = string.Format(@"update maneger set MAuthArea='{0}' where MAuth = 2", manager);
                cmd = new MySqlCommand(sql, conn);
                result += cmd.ExecuteNonQuery();

                //修改用户权限
                sql = string.Format(@"update maneger set MAuthArea='{0}' where MAuth = 3", user);
                cmd = new MySqlCommand(sql, conn);
                result += cmd.ExecuteNonQuery();

                if (result > 2)
                {
                    MessageBox.Show("修改权限失败！", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void Form_SystemSet_Load(object sender, EventArgs e)
        {

        }
    }
}