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
    public partial class Form_EditReader : Form
    {
        public Form_EditReader()
        {
            InitializeComponent();
        }

        DBHelper dh = new DBHelper();
        MySqlConnection conn = null;
        MySqlCommand cmd = null;
        DataSet ds = null;
        MySqlDataAdapter da = null;

        private int editReaderId;    //保存读者ID
        object[] param = null;
        public Form_EditReader(object[] param)
        {
            InitializeComponent();
            ShowClass();    //绑定下拉框
            this.param = param;
            this.cmbReaderType.Text = Convert.ToString(param[4]);
            BindContent();
        }

        //绑定数据
        private void BindContent()
        {
            editReaderId = Convert.ToInt32(param[0]);
            this.txtReaderName.Text = Convert.ToString(param[1]);
            this.cmbReaderSex.Text = Convert.ToString(param[2]);
            this.txtReaderID.Text = Convert.ToString(param[3]);
            this.cmbSchoolName.Text = Convert.ToString(param[5]);
            this.cmbProName.Text = Convert.ToString(param[6]);
            this.txtGrade.Text = Convert.ToString(param[7]);
            this.dtpDate.Value = Convert.ToDateTime(param[8]);
            this.txtTel.Text = Convert.ToString(param[9]);
            this.nudMoney.Value = Convert.ToDecimal(param[10]);
        }
        private void ShowClass()
        {
            try
            {
                conn = dh.Connection;
                ds = new DataSet();
                string sql = "select LBID,LBName from readertype";
                da = new MySqlDataAdapter(sql, conn);
                da.Fill(ds, "读者类别");

                this.cmbReaderType.DataSource = ds.Tables["读者类别"];
                this.cmbReaderType.DisplayMember = "LBName";
                this.cmbReaderType.ValueMember = "LBID";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { }
        }

        private void Form_EditReader_Load(object sender, EventArgs e)
        {

        }
        private bool Check()
        {
            string selectText = this.cmbReaderType.Text;
            switch (selectText)
            {
                case "学生":
                    if (txtReaderName.Text.Equals("") || txtReaderName.Text == null)
                    {
                        MessageBox.Show("请输入读者姓名！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtReaderName.Focus();
                        return false;
                    }
                    else if (cmbReaderSex.Text.Equals("") || cmbReaderSex.Text == null)
                    {
                        MessageBox.Show("请选择读者性别！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        cmbReaderSex.Focus();
                        return false;
                    }
                    else if (txtReaderID.Text.Equals("") || txtReaderID.Text == null)
                    {
                        MessageBox.Show("请输入读者学号！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtReaderID.Focus();
                        return false;
                    }
                    else if (txtReaderID.Text.Length != 8)
                    {
                        MessageBox.Show("请输入正切读者学号[8位数]！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtReaderID.Focus();
                        return false;
                    }
                    else if (cmbProName.Text.Equals("") || cmbProName.Text == null)
                    {
                        MessageBox.Show("请选择读者所在专业！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        cmbProName.Focus();
                        return false;
                    }
                    else if (txtTel.Text.Equals("") || txtTel.Text == null)
                    {
                        MessageBox.Show("请输入读者联系方式！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtTel.Focus();
                        return false;
                    }
                    else if (nudMoney.Value != 50)
                    {
                        MessageBox.Show("请输入读者押金！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        nudMoney.Focus();
                        return false;
                    }
                    else if (txtGrade.Text.Equals("") || txtGrade.Text == null)
                    {
                        MessageBox.Show("请输入读者年级！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtGrade.Focus();
                        return false;
                    }
                    else if (cmbSchoolName.Text.Equals("") || cmbSchoolName.Text == null)
                    {
                        MessageBox.Show("请选择读者所在院校！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        cmbSchoolName.Focus();
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                case "教师":
                    if (txtReaderName.Text.Equals("") || txtReaderName.Text == null)
                    {
                        MessageBox.Show("请输入读者姓名！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtReaderName.Focus();
                        return false;
                    }
                    else if (cmbReaderSex.Text.Equals("") || cmbReaderSex.Text == null)
                    {
                        MessageBox.Show("请选择读者性别！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        cmbReaderSex.Focus();
                        return false;
                    }
                    else if (cmbProName.Text.Equals("") || cmbProName.Text == null)
                    {
                        MessageBox.Show("请选择读者所在专业！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        cmbProName.Focus();
                        return false;
                    }
                    else if (txtTel.Text.Equals("") || txtTel.Text == null)
                    {
                        MessageBox.Show("请输入读者联系方式！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtTel.Focus();
                        return false;
                    }
                    else if (nudMoney.Value != 50)
                    {
                        MessageBox.Show("请输入读者押金！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        nudMoney.Focus();
                        return false;
                    }
                    else if (cmbSchoolName.Text.Equals("") || cmbSchoolName.Text == null)
                    {
                        MessageBox.Show("请选择读者所在院校！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        cmbSchoolName.Focus();
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                case "校长":
                    if (txtReaderName.Text.Equals("") || txtReaderName.Text == null)
                    {
                        MessageBox.Show("请输入读者姓名！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtReaderName.Focus();
                        return false;
                    }
                    else if (cmbReaderSex.Text.Equals("") || cmbReaderSex.Text == null)
                    {
                        MessageBox.Show("请选择读者性别！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        cmbReaderSex.Focus();
                        return false;
                    }
                    else if (txtTel.Text.Equals("") || txtTel.Text == null)
                    {
                        MessageBox.Show("请输入读者联系方式！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtTel.Focus();
                        return false;
                    }
                    else if (nudMoney.Value != 50)
                    {
                        MessageBox.Show("请输入读者押金！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        nudMoney.Focus();
                        return false;
                    }
                    else if (cmbSchoolName.Text.Equals("") || cmbSchoolName.Text == null)
                    {
                        MessageBox.Show("请选择读者所在院校！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        cmbSchoolName.Focus();
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                case "职工":
                    if (txtReaderName.Text.Equals("") || txtReaderName.Text == null)
                    {
                        MessageBox.Show("请输入读者姓名！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtReaderName.Focus();
                        return false;
                    }
                    else if (cmbReaderSex.Text.Equals("") || cmbReaderSex.Text == null)
                    {
                        MessageBox.Show("请选择读者性别！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        cmbReaderSex.Focus();
                        return false;
                    }
                    else if (txtTel.Text.Equals("") || txtTel.Text == null)
                    {
                        MessageBox.Show("请输入读者联系方式！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtTel.Focus();
                        return false;
                    }
                    else if (nudMoney.Value != 50)
                    {
                        MessageBox.Show("请输入读者押金！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        nudMoney.Focus();
                        return false;
                    }
                    else if (cmbSchoolName.Text.Equals("") || cmbSchoolName.Text == null)
                    {
                        MessageBox.Show("请选择读者所在院校！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        cmbSchoolName.Focus();
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                case "主任":
                    if (txtReaderName.Text.Equals("") || txtReaderName.Text == null)
                    {
                        MessageBox.Show("请输入读者姓名！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtReaderName.Focus();
                        return false;
                    }
                    else if (cmbReaderSex.Text.Equals("") || cmbReaderSex.Text == null)
                    {
                        MessageBox.Show("请选择读者性别！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        cmbReaderSex.Focus();
                        return false;
                    }
                    else if (txtTel.Text.Equals("") || txtTel.Text == null)
                    {
                        MessageBox.Show("请输入读者联系方式！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtTel.Focus();
                        return false;
                    }
                    else if (nudMoney.Value != 50)
                    {
                        MessageBox.Show("请输入读者押金！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        nudMoney.Focus();
                        return false;
                    }
                    else if (cmbSchoolName.Text.Equals("") || cmbSchoolName.Text == null)
                    {
                        MessageBox.Show("请选择读者所在院校！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        cmbSchoolName.Focus();
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                case "外借":
                    if (txtReaderName.Text.Equals("") || txtReaderName.Text == null)
                    {
                        MessageBox.Show("请输入读者姓名！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtReaderName.Focus();
                        return false;
                    }
                    else if (cmbReaderSex.Text.Equals("") || cmbReaderSex.Text == null)
                    {
                        MessageBox.Show("请选择读者性别！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        cmbReaderSex.Focus();
                        return false;
                    }
                    else if (txtTel.Text.Equals("") || txtTel.Text == null)
                    {
                        MessageBox.Show("请输入读者联系方式！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtTel.Focus();
                        return false;
                    }
                    else if (nudMoney.Value != 50)
                    {
                        MessageBox.Show("请输入读者押金！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        nudMoney.Focus();
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                default:
                    return false;
            }
        }

        private void cmbReaderType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectText = this.cmbReaderType.Text;
            if (selectText.Equals("外借"))
            {
                ControlEnable();
                BindContent();
                this.cmbSchoolName.Text = "";
                this.txtGrade.Text = "";
                this.txtReaderID.Text = "";
                this.cmbProName.Text = "";
                this.cmbSchoolName.Enabled = false; //禁用
                this.txtReaderID.Enabled = false;
                this.txtGrade.Enabled = false;
                this.cmbProName.Enabled = false;
            }
            else if (selectText.Equals("教师"))
            {
               
                ControlEnable();
                BindContent();
                this.txtGrade.Text = "";
                this.txtReaderID.Text = "";
                this.txtReaderID.Enabled = false;
                this.txtGrade.Enabled = false;
            }
            else if (selectText.Equals("校长") || selectText.Equals("主任") || selectText.Equals("职工"))
            {
               
                ControlEnable();
                BindContent();
                this.txtGrade.Text = "";
                this.txtReaderID.Text = "";
                this.cmbProName.Text = "";
                this.txtReaderID.Enabled = false;
                this.txtGrade.Enabled = false;
                this.cmbProName.Enabled = false;
            }
            else if (selectText.Equals("学生"))
            {
                BindContent();
                ControlEnable();
            }
        }

        /// <summary>
        /// 启用所有控件
        /// </summary>
        private void ControlEnable()
        {
            this.txtGrade.Enabled = true;
            this.txtReaderID.Enabled = true;
            this.txtReaderName.Enabled = true;
            this.txtTel.Enabled = true;
            this.cmbProName.Enabled = true;
            this.cmbReaderSex.Enabled = true;
            this.cmbReaderType.Enabled = true;
            this.cmbSchoolName.Enabled = true;
            this.nudMoney.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Check())
            {
                string readerName = this.txtReaderName.Text;
                string readerGrade = this.txtGrade.Text;
                string readerId = this.txtReaderID.Text;
                string readerTel = this.txtTel.Text;
                string readerSex = this.cmbReaderSex.Text;
                string readerPro = this.cmbProName.Text;
                string readerSchool = this.cmbSchoolName.Text;
                int readerType = Convert.ToInt32(this.cmbReaderType.SelectedValue);
                DateTime dt = this.dtpDate.Value;
                decimal readerMoney = this.nudMoney.Value;

                try
                {
                    conn = dh.Connection;
                    dh.OpenConnection();

                    string sql1 = string.Format(@"update reader set ReaName='{0}',ReaSex='{1}',ReaNo='{2}',ReaLBID={3},ReaDep='{4}',
                                                  ReaGrade='{5}',ReaPref='{6}',ReaDate='{7}',ReaTel='{8}',ReaMoney={9}  where ReaID ='{10}'",
                                                  readerName, readerSex, readerId, readerType, readerSchool, readerGrade, readerPro, dt, readerTel, readerMoney, editReaderId);
                    cmd = new MySqlCommand(sql1, conn);
                    int result1 = cmd.ExecuteNonQuery();
                    if (result1 == 1)
                    {
                        MessageBox.Show("修改成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();    //清空所有文本
                    }
                    else
                    {
                        MessageBox.Show("修改成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
    }
}