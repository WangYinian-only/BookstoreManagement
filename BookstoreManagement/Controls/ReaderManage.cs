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
using BookstoreManagement.Forms;
using System.Runtime.InteropServices;

namespace BookstoreManagement.Controls
{
    public partial class ReaderManage : UserControl
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

        public ReaderManage()
        {
            InitializeComponent(); //窗体弹出效果
            AnimateWindow(this.Handle, 10, AW_CENTER);
        }

        //初始化数据库链接
        DBHelper dh = new DBHelper();
        MySqlConnection conn = null;
        MySqlCommand cmd = null;
        DataSet ds = null;
        MySqlDataAdapter da = null;

        private void ReaderManage_Load(object sender, EventArgs e)
        {
            try
            {
                conn = dh.Connection;   //获取链接
                ds = new DataSet();     //创建数据临时仓库

                this.cmbReaderSex.Text = "男";
                this.cmbSchoolName.Text = "阜阳工业经济学校(南校区)";
                this.cmbReaderType.Text = "学生";

                //绑定下拉框数据
                ShowClass();

                //绑定数据源
                string sql = @"select * from view_reader";
                ShowData(sql);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                //
            }
        }

        /// <summary>
        /// 清空文本
        /// </summary>
        private void TextClear()
        {
            this.txtGrade.Clear();
            this.txtReaderID.Clear();
            this.txtReaderName.Clear();
            this.txtTel.Clear();
            this.cmbSchoolName.Text = "";
            this.cmbProName.Text = "";
        }


        /// <summary>
        /// 非空验证
        /// </summary>
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
                        MessageBox.Show("请输入正确读者学号[8位数]！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void btnClear_Click(object sender, EventArgs e)
        {
            TextClear();
        }

        /// <summary>
        /// combox绑定数据源
        /// </summary>

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (Check())
            {
                string readerName = this.txtReaderName.Text;        //姓名
                string readerGrade = this.txtGrade.Text;        //年级
                string readerId = this.txtReaderID.Text;        //学号
                string readerTel = this.txtTel.Text;        //电话
                string readerSex = this.cmbReaderSex.Text;  //性别
                string readerPro = this.cmbProName.Text;    //专业
                string readerSchool = this.cmbSchoolName.Text;  //学校
                int readerType = Convert.ToInt32(this.cmbReaderType.SelectedValue); //类别
                DateTime dt = this.dtpDate.Value;
                decimal readerMoney = this.nudMoney.Value;

                try
                {
                    conn = dh.Connection;
                    dh.OpenConnection();
                    //提前判断该用户是否已经注册阅读证
                    string sql = string.Format(@"select COUNT(1) from reader where ReaName='{0}' and ReaTel='{1}'", readerName, readerTel);
                    cmd = new MySqlCommand(sql, conn);
                    int result = Convert.ToInt32(cmd.ExecuteScalar());
                    if (result == 0)    //没进行注册则进行新注册
                    {
                        string sql1 = string.Format(@"insert into reader(ReaName,ReaSex,ReaNo,ReaLBID,ReaDep,ReaGrade,ReaPref,ReaDate,ReaTel,ReaMoney)
                        values('{0}','{1}','{2}',{3},'{4}','{5}','{6}','{7}','{8}',{9})",
                        readerName, readerSex, readerId, readerType, readerSchool, readerGrade, readerPro, dt, readerTel, readerMoney);
                        cmd = new MySqlCommand(sql1, conn);
                        int result1 = cmd.ExecuteNonQuery();
                        if (result1 == 1)
                        {
                            MessageBox.Show("添加成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            TextClear();    //清空所有文本
                            string sqll = @"select * from view_reader";
                            ShowData(sqll); //刷新数据
                        }
                        else
                        {
                            MessageBox.Show("添加失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else    //已经注册则不再进行注册
                    {
                        MessageBox.Show("您已经注册过了", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        TextClear();    //清空所有文本
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "异常提醒", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    dh.CloseConnection();
                }
            }
        }

        /// <summary>
        /// 属性值更改时做判断
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbReaderType_SelectedIndexChanged(object sender, EventArgs e)
        {
            TextClear();
            string selectText = this.cmbReaderType.Text;
            if (selectText.Equals("外借"))
            {
                ControlEnable();
                this.cmbSchoolName.Enabled = false; //禁用
                this.txtReaderID.Enabled = false;
                this.txtGrade.Enabled = false;
                this.cmbProName.Enabled = false;

            }
            else if (selectText.Equals("教师"))
            {
                ControlEnable();
                this.txtReaderID.Enabled = false;
                this.txtGrade.Enabled = false;
            }
            else if (selectText.Equals("校长") || selectText.Equals("主任") || selectText.Equals("职工"))
            {
                ControlEnable();
                this.txtReaderID.Enabled = false;
                this.txtGrade.Enabled = false;
                this.cmbProName.Enabled = false;
            }
            else if (selectText.Equals("学生"))
            {
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

        /// <summary>
        /// 填充数据
        /// </summary>
        /// <param name="sql"></param>
        private void ShowData(string sql)
        {
            try
            {
                conn = dh.Connection;
                ds = new DataSet();
                da = new MySqlDataAdapter(sql, conn);
                da.Fill(ds, "readerQuery");
                this.dataGridView1.DataSource = ds.Tables["readerQuery"];
                this.lblRowCount.Text = this.dataGridView1.Rows.Count.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { }
        }

        //绑定下拉框数据
        private void ShowClass()
        {
            try
            {
                string sql2 = @"select LBID,LBName from readertype";
                da = new MySqlDataAdapter(sql2, conn);
                da.Fill(ds, "readerTypeAdd");

                this.cmbReaderType.DataSource = ds.Tables["readerTypeAdd"];
                this.cmbReaderType.DisplayMember = "LBName";
                this.cmbReaderType.ValueMember = "LBID";

                string sql3 = @"select '0' LBID,'全部' LBName
                                union all 
                                select LBID,LBName from readertype";
                da = new MySqlDataAdapter(sql3, conn);
                da.Fill(ds, "readerType");
                this.cmbReaderClass.DataSource = ds.Tables["readerType"];
                this.cmbReaderClass.DisplayMember = "LBName";
                this.cmbReaderClass.ValueMember = "LBID";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string sqll = @"select * from view_reader";
            ShowData(sqll);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int rowCount = this.dataGridView1.Rows.Count;       //获取当前dgv中的数据总总数
            int selectRowCount = this.dataGridView1.SelectedRows.Count;     //获取当前选中的数据总数
            if (rowCount < 1)
            {
                MessageBox.Show("当前没有数据可选！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (selectRowCount == 0)
            {
                MessageBox.Show("当前没有数据选中！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                //将数据通过构造方法传递给编辑窗口
                object ReaId = this.dataGridView1.SelectedRows[0].Cells[0].Value;
                object ReaName = this.dataGridView1.SelectedRows[0].Cells[1].Value;
                object ReaSex = this.dataGridView1.SelectedRows[0].Cells[2].Value;
                object ReaNo = this.dataGridView1.SelectedRows[0].Cells[3].Value;
                object ReaLBID = this.dataGridView1.SelectedRows[0].Cells[4].Value;
                object ReaDep = this.dataGridView1.SelectedRows[0].Cells[5].Value;
                object ReaPref = this.dataGridView1.SelectedRows[0].Cells[6].Value;
                object ReaGrade = this.dataGridView1.SelectedRows[0].Cells[7].Value;
                object ReaDate = this.dataGridView1.SelectedRows[0].Cells[8].Value;
                object ReaTel = this.dataGridView1.SelectedRows[0].Cells[9].Value;
                object ReaMoney = this.dataGridView1.SelectedRows[0].Cells[10].Value;

                object[] param = { ReaId, ReaName, ReaSex, ReaNo, ReaLBID, ReaDep, ReaPref, ReaGrade, ReaDate, ReaTel, ReaMoney };
                new Form_EditReader(param).ShowDialog();
            }
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            int rowCount = this.dataGridView1.Rows.Count;       //获取当前dgv中的数据总总数
            int selectRowCount = this.dataGridView1.SelectedRows.Count;     //获取当前选中的数据总数

            if (rowCount < 1)
            {
                MessageBox.Show("当前没有数据可选！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (selectRowCount == 0)
            {
                MessageBox.Show("当前没有数据选中！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (DialogResult.Yes == MessageBox.Show("确定要删除吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    try
                    {
                        conn = dh.Connection;
                        dh.OpenConnection();

                        deleteReader(); //删除读者信息     
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

        private void cmbReaderClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sql = "";
            if (this.cmbReaderClass.Text == "全部")
            {
                sql = string.Format(@"select * from view_reader");
                ShowData(sql);
            }
            else if (txtQueryByName.Text == "")
            {
                sql = string.Format(@"select * from view_reader where LBName='{0}'", this.cmbReaderClass.Text);
                ShowData(sql);
            }
            else if (this.cmbReaderClass.Text != "全部" && this.txtQueryByName.Text != "")
            {
                sql = string.Format(@"select * from view_reader where LBName='{0}' and  ReaName like '%{1}%'", this.cmbReaderClass.Text, this.txtQueryByName.Text);
                ShowData(sql);
            }
        }

        private void txtQueryByName_TextChanged(object sender, EventArgs e)
        {
            string sql = "";
            if (this.cmbReaderClass.Text == "全部")
            {
                sql = string.Format(@"select * from view_reader where ReaName like '%{0}%' ", this.txtQueryByName.Text);
                ShowData(sql);  //刷新数据
            }
            else if (this.txtQueryByName.Text == "")
            {
                sql = string.Format(@"select * from view_reader where LBName='{0}'", this.cmbReaderClass.Text);
                ShowData(sql);  //刷新数据
            }
            else if (this.cmbReaderClass.Text != "全部" && this.txtQueryByName.Text != "")
            {
                sql = string.Format(@"select * from view_reader where LBName='{0}' and  ReaName like '%{1}%'", this.cmbReaderClass.Text, this.txtQueryByName.Text);
                ShowData(sql);  //刷新数据
            }
            else if (this.cmbReaderClass.Text == "全部" && this.txtQueryByName.Text == "")
            {
                string sqll = string.Format(@"select * from view_reader");
                ShowData(sql);  //刷新数据
            }
        }

        //删除读者信息
        private void deleteReader()
        {
            //获得ID编号，为删除提供条件
            int ReaId = Convert.ToInt32(this.dataGridView1.SelectedRows[0].Cells[0].Value);
            string sql = string.Format("delete from reader where ReaID ='{0}'", ReaId);
            cmd = new MySqlCommand(sql, conn);
            int result = cmd.ExecuteNonQuery();
            if (result == 1)
            {
                MessageBox.Show("删除成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                string sqll = string.Format(@"select * from view_reader");
                ShowData(sqll); //刷新
            }
            else
            {
                MessageBox.Show("删除失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string a = "D:" + "\\读者信息.xls";
            ExportExcels(a, dataGridView1);
        }

        //导出数据的方法
        private void ExportExcels(string fileName, DataGridView myDGV)
        {
            string saveFileName = "";
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.DefaultExt = "xls";
            saveDialog.Filter = "Excel文件|*.xls";
            saveDialog.FileName = fileName;
            saveDialog.ShowDialog();
            saveFileName = saveDialog.FileName;
            if (saveFileName.IndexOf(":") < 0) return; //被点了取消
            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            if (xlApp == null)
            {
                MessageBox.Show("无法创建Excel对象，可能您的机子未安装Excel");
                return;
            }
            Microsoft.Office.Interop.Excel.Workbooks workbooks = xlApp.Workbooks;
            Microsoft.Office.Interop.Excel.Workbook workbook = workbooks.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet);
            Microsoft.Office.Interop.Excel.Worksheet worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets[1];//取得sheet1
                                                                                                                                  //写入标题
            for (int i = 0; i < myDGV.ColumnCount; i++)
            {
                worksheet.Cells[1, i + 1] = myDGV.Columns[i].HeaderText;
            }
            //写入数值
            for (int r = 0; r < myDGV.Rows.Count; r++)
            {
                for (int i = 0; i < myDGV.ColumnCount; i++)
                {
                    worksheet.Cells[r + 2, i + 1] = myDGV.Rows[r].Cells[i].Value;
                }
                System.Windows.Forms.Application.DoEvents();
            }
            worksheet.Columns.EntireColumn.AutoFit();//列宽自适应
            if (saveFileName != "")
            {
                try
                {
                    workbook.Saved = true;
                    workbook.SaveCopyAs(saveFileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("导出文件时出错,文件可能正被打开！\n" + ex.Message);
                }
            }
            xlApp.Quit();
            GC.Collect();//强行销毁
            MessageBox.Show("文件: " + fileName + ".xls 保存成功", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}