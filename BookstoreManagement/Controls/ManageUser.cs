using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Runtime.InteropServices;

namespace BookstoreManagement.Controls
{
    public partial class ManageUser : UserControl
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

        public ManageUser()
        {
            InitializeComponent(); //窗体弹出效果
            AnimateWindow(this.Handle, 10, AW_CENTER);
        }

        //初始化数据库连接
        DBHelper dh = new DBHelper();
        MySqlConnection conn = null;
        MySqlCommand cmd = null;
        DataSet ds = null;
        MySqlDataAdapter da = null;

        //绑定下拉框
        private void BindCmb()
        {
            string sql = @"select * from managertype";
            da = new MySqlDataAdapter(sql, conn);
            da.Fill(ds, "ManageType");
            this.cmbQuanXian.DataSource = ds.Tables["ManageType"];
            this.cmbQuanXian.DisplayMember = "Jurisdiction";
            this.cmbQuanXian.ValueMember = "ID";
        }

        //给DataGridView中填充数据
        private void BindData(string sql)
        {
            try
            {
                conn = dh.Connection;
                ds = new DataSet();
                da = new MySqlDataAdapter(sql, conn);
                da.Fill(ds, "Manage");
                this.dataGridView1.DataSource = ds.Tables["Manage"];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { }
        }

        //窗体加载事件
        private void ManageUser_Load(object sender, EventArgs e)
        {
            string sql = @"select * from view_manager";
            BindData(sql); //绑定数据
            BindCmb();  //绑定下拉框
        }

        //非空验证
        private bool Check()
        {
            if (this.txtBianHao.Text == "")
            {
                MessageBox.Show("请输入编号", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.txtBianHao.Focus();
                return false;
            }
            else if (this.txtName.Text == "")
            {
                MessageBox.Show("请输入姓名", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.txtName.Focus();
                return false;
            }
            else if (this.txtPassWord.Text == "")
            {
                MessageBox.Show("请输入密码", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.txtPassWord.Focus();
                return false;
            }
            else if (this.txtTel.Text == "")
            {
                MessageBox.Show("请输入电话", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.txtTel.Focus();
                return false;
            }
            else if (this.txtAddress.Text == "")
            {
                MessageBox.Show("请输入地址", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.txtAddress.Focus();
                return false;
            }
            else if (this.cmbSex.Text == "")
            {
                MessageBox.Show("请选择性别", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.cmbSex.Focus();
                return false;
            }
            else if (this.cmbQuanXian.Text == "")
            {
                MessageBox.Show("请选择权限", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.cmbQuanXian.Focus();
                return false;
            }
            else
            {
                return true;
            }
        }

        //清空文本框
        private void ClearText()
        {
            this.txtAddress.Clear();
            this.txtBianHao.Clear();
            this.txtName.Clear();
            this.txtPassWord.Clear();
            this.txtTel.Clear();
            this.cmbQuanXian.Text = "";
            this.cmbSex.Text = "";
        }

        //保存修改操作
        private void UpdateUser()
        {
            if (Check())
            {
                if (DialogResult.OK == MessageBox.Show("确定要修改吗？这将无法撤销！", "警告提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
                {
                    try
                    {
                        conn = dh.Connection;
                        dh.OpenConnection();

                        string id = this.txtBianHao.Text;
                        string name = this.txtName.Text;
                        string sex = this.cmbSex.Text;
                        string pwd = this.txtPassWord.Text;
                        int auth = this.cmbQuanXian.SelectedIndex + 1;
                        string tel = this.txtTel.Text;
                        string address = this.txtAddress.Text;

                        string sql = string.Format(@"update maneger set MName='{0}',MSex='{1}',Mpwd='{2}',MAuth={3},MTeleph='{4}',MAddre='{5}'
                                             where MID='{6}'", name, sex, pwd, auth, tel, address, id);
                        cmd = new MySqlCommand(sql, conn);
                        if (cmd.ExecuteNonQuery() == 1)
                        {
                            MessageBox.Show("修改成功！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            string sql1 = string.Format(@"select * from view_manager");
                            BindData(sql1); //绑定数据
                        }
                        else
                        {
                            MessageBox.Show("修改失败！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            }
        }

        //删除操作
        private void Delete()
        {
            if (this.txtBianHao.Text != "")
            {
                if (DialogResult.OK == MessageBox.Show("确定要删除吗？这将无法恢复", "信息警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
                {
                    try
                    {
                        conn = dh.Connection;
                        dh.OpenConnection();

                        string id = this.txtBianHao.Text;

                        string sql = string.Format(@"delete from maneger where MID = '{0}'", id);
                        cmd = new MySqlCommand(sql, conn);
                        if (cmd.ExecuteNonQuery() == 1)
                        {
                            MessageBox.Show("删除成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            string sql1 = string.Format(@"select * from view_manager");
                            BindData(sql1); //绑定数据
                        }
                        else
                        {
                            MessageBox.Show("删除失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);

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
            }
            else
            {
                MessageBox.Show("请输入编号再进行删除！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        //添加数据
        private void UserAdd()
        {
            if (Check())
            {
                try
                {
                    conn = dh.Connection;
                    dh.OpenConnection();

                    string id = this.txtBianHao.Text;
                    string name = this.txtName.Text;
                    string sex = this.cmbSex.Text;
                    string pwd = this.txtPassWord.Text;
                    int auth = this.cmbQuanXian.SelectedIndex + 1;
                    string authArea = "";
                    if (auth == 1)
                    {
                        authArea = "ABCDEFGH";
                    }
                    else if (auth == 2)
                    {
                        authArea = "ABCDEG";
                    }
                    else if (auth == 3)
                    {
                        authArea = "AE";
                    }
                    string tel = this.txtTel.Text;
                    string address = this.txtAddress.Text;

                    string sql = string.Format(@"insert into maneger(MID,MName,MSex,Mpwd,MAuth,MAuthArea,MTeleph,MAddre) 
                                                 values('{0}','{1}','{2}','{3}',{4},'{5}','{6}','{7}')", id, name, sex, pwd, auth, authArea, tel, address);
                    cmd = new MySqlCommand(sql, conn);
                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        MessageBox.Show("添加成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        string sql1 = string.Format(@"select * from view_manager");
                        BindData(sql1); //绑定数据
                    }
                    else
                    {
                        MessageBox.Show("添加失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        }

        //单机单元格，将内容反馈到文本框中
        private void ContextFill()
        {
            if (this.dataGridView1.SelectedRows.Count == 1)
            {
                string bianHao = Convert.ToString(this.dataGridView1.SelectedRows[0].Cells[0].Value);
                string name = Convert.ToString(this.dataGridView1.SelectedRows[0].Cells[1].Value);
                string sex = Convert.ToString(this.dataGridView1.SelectedRows[0].Cells[2].Value);
                string password = Convert.ToString(this.dataGridView1.SelectedRows[0].Cells[3].Value);
                string quanXian = Convert.ToString(this.dataGridView1.SelectedRows[0].Cells[4].Value);
                string telphon = Convert.ToString(this.dataGridView1.SelectedRows[0].Cells[5].Value);
                string address = Convert.ToString(this.dataGridView1.SelectedRows[0].Cells[6].Value);

                this.txtBianHao.Text = bianHao;
                this.txtName.Text = name;
                this.cmbSex.Text = sex;
                this.txtPassWord.Text = password;
                this.cmbQuanXian.Text = quanXian;
                this.txtTel.Text = telphon;
                this.txtAddress.Text = address;
            }
            else
            {
                MessageBox.Show("当前没有选中数据!", "空数据提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        //搜索信息
        private void Sousuo()
        {
            try
            {
                if (this.txtBianHao.Text != "")
                {
                    int id = Convert.ToInt32(this.txtBianHao.Text);
                    string sql = string.Format(@"select * from view_manager where MID={0}", id);
                    BindData(sql); //绑定数据
                }
                else
                {
                    string sql = string.Format(@"select * from view_manager");
                    BindData(sql); //绑定数据
                }
            }
            catch (Exception)
            {
                MessageBox.Show("请输入合法编号 [数字][八位数]", "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.txtBianHao.Clear();
            }
        }


        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearText();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Delete();
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            UserAdd();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateUser();
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            Sousuo();
        }

        private void dataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            ContextFill();
        }
    }
}