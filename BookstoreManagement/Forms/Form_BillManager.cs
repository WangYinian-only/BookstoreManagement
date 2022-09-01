using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookstoreManagement.Forms
{
    public partial class Form_BillManager : Form
    {
        public Form_BillManager()
        {
            InitializeComponent();
        }

        //初始化数据库连接
        DBHelper dh = new DBHelper();
        MySqlConnection conn = null;
        MySqlCommand cmd = null;
        string id;

        public Form_BillManager(object[] param)
        {
            InitializeComponent();
            initEdit(param);
        }

        //非空验证
        private bool Check()
        {
            if (this.txtReaderID.Text == "")
            {
                MessageBox.Show("请输入读者编号！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.txtReaderID.Focus();
                return false;
            }
            else if (this.txtBookId.Text == "")
            {
                MessageBox.Show("请输入图书编号！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.txtBookId.Focus();
                return false;
            }
            else if (this.txtBookName.Text == "")
            {
                MessageBox.Show("请输入图书名称！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.txtBookName.Focus();
                return false;
            }
            else if (this.dtBorrow.Value == null)
            {
                MessageBox.Show("请补充借阅日期！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.dtBorrow.Focus();
                return false;
            }
            else if (this.dtYingBack.Value == null)
            {
                MessageBox.Show("请补充应还日期！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.dtYingBack.Focus();
                return false;
            }
            else if (this.txtBookId.Text == "")
            {
                MessageBox.Show("请输入读者编号！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.txtBookId.Focus();
                return false;
            }
            else
            {
                return true;
            }
        }

        private void btnCanel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void EditData()
        {
            string readerId = this.txtReaderID.Text;
            string bookId = this.txtBookId.Text;
            string bookName = this.txtBookName.Text;
            string bookWriter = this.txtBookWriter.Text;
            DateTime dtBorrow = this.dtBorrow.Value;
            DateTime dtYingBack = this.dtYingBack.Value;
            decimal fineMoney = this.nudMoney.Value;
            string state = this.cmbState.Text;
            string manageId = this.txtManageId.Text;
            DateTime dtBack = this.dtBack.Value;
            string sql;
            if (this.dtBack.Enabled)
            {
                sql = string.Format(@"update borrow set BookID='{0}',BookName='{1}',BookWriter='{2}',Outdate='{3}',YHdate='{4}',Indate='{5}',Fine={6},CLState='{7}',MID='{8}'
                                        where ReaID = {9}", bookId, bookName, bookWriter, dtBorrow, dtYingBack, dtBack, fineMoney, state, manageId, id);
            }
            else
            {
                sql = string.Format(@"update borrow set BookID='{0}',BookName='{1}',BookWriter='{2}',Outdate='{3}',YHdate='{4}',Fine={5},CLState='{6}',MID='{7}'
                                        where ReaID = {8}", bookId, bookName, bookWriter, dtBorrow, dtYingBack, fineMoney, state, manageId, id);
            }
            cmd = new MySqlCommand(sql, conn);
            int result = cmd.ExecuteNonQuery();
            if (result == 1)
            {
                MessageBox.Show("修改成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("修改失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private Point mousePoint = new Point();
        private void panel6_MouseDown(object sender, MouseEventArgs e)
        {
            this.mousePoint.X = e.X;
            this.mousePoint.Y = e.Y;
        }

        private void panel6_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Top = Control.MousePosition.Y - mousePoint.Y;
                this.Left = Control.MousePosition.X - mousePoint.X;
            }
        }


        //初始化文本信息
        private void initEdit(object[] param)
        {
            id = Convert.ToString(param[0]);
            this.txtReaderID.Text = Convert.ToString(param[0]);
            this.txtBookId.Text = Convert.ToString(param[1]);
            this.txtBookName.Text = Convert.ToString(param[2]);
            this.txtBookWriter.Text = Convert.ToString(param[3]);
            this.dtBorrow.Value = Convert.ToDateTime(param[4]);
            this.dtYingBack.Value = Convert.ToDateTime(param[5]);
            //this.dtBack.Value = Convert.ToDateTime(param[6]);
            this.nudMoney.Value = Convert.ToDecimal(param[7]);
            this.cmbState.Text = Convert.ToString(param[8]);
            this.txtManageId.Text = Convert.ToString(param[9]);
        }

        private void btnBaoCun_Click(object sender, EventArgs e)
        {
            if (Check())
            {
                try
                {
                    conn = dh.Connection;
                    dh.OpenConnection();
                    //调用修改数据
                    EditData();
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

        private void cbEnable_CheckedChanged(object sender, EventArgs e)
        {
            this.dtBack.Enabled = cbEnable.Checked ? true : false;
        }
    }
}