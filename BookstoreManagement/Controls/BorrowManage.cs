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
    public partial class BorrowManage : UserControl
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

        public BorrowManage()
        {
            InitializeComponent();
            //窗体弹出效果
            AnimateWindow(this.Handle, 10, AW_CENTER);
        }

        private string managerId;
        public BorrowManage(string manageId)
        {
            InitializeComponent();
            this.managerId = manageId;

        }

        //初始化数据库连接
        DBHelper dh = new DBHelper();
        MySqlConnection conn = null;
        MySqlCommand cmd = null;
        DataSet ds = null;
        MySqlDataAdapter da = null;


        //清空文本信息
        private void ClearText()
        {
            this.txtBookId.Clear();
            this.txtBookName.Clear();
            this.txtReaderID.Clear();
            this.txtBookWriter.Clear();
            this.cmbState.SelectedIndex = 0;
            this.dtBorrow.Value = DateTime.Now;
            this.dtYingBack.Value = DateTime.Now;
        }

        //清空文本事件
        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearText();
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

        //绑定DataGridView数据
        private void BindDvgDate(string sql)
        {
            try
            {
                conn = dh.Connection;
                ds = new DataSet();
                da = new MySqlDataAdapter(sql, conn);
                da.Fill(ds, "borrow");
                this.dataGridView1.DataSource = ds.Tables["borrow"];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { }
        }

        //窗体加载事件
        private void BorrowManage_Load(object sender, EventArgs e)
        {
            try
            {
                this.cmbState2.SelectedIndex = 0;   //默认第一行选中
                this.cmbState.SelectedIndex = 0;    //默认第一行选中
                this.txtManageId.Text = this.managerId;     //将管理员编号赋值给文本框
                conn = dh.Connection;   //获取链接
                ds = new DataSet();     //创建数据临时仓库

                string sql = @"select ReaID,BookID,BookName,BookWriter,Outdate,YHdate,Indate,Fine,CLState,MID from borrow";
                BindDvgDate(sql);       //绑定数据源
                this.label25.Text = this.dataGridView1.Rows.Count.ToString();
                UpdateFine();   //罚款更新
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

        //添加事件
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (Check())
            {
                try
                {
                    conn = dh.Connection;
                    dh.OpenConnection();
                    if (BorrowCheck())  //借阅限定验证
                    {
                        addBorrow(); //添加借阅
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

        //添加借阅
        private void addBorrow()
        {
            string bookId = this.txtBookId.Text;
            string bookName = this.txtBookName.Text;
            string bookWriter = this.txtBookWriter.Text;
            string manageId = this.txtManageId.Text;
            string readerId = this.txtReaderID.Text;
            string state = this.cmbState.Text;
            DateTime dtBack = this.dtYingBack.Value;    //使归还时间为应还时间
            DateTime dtBorrow = this.dtBorrow.Value;
            DateTime dtYingBack = this.dtYingBack.Value;
            decimal fine = this.nudMoney.Value;

            string sql = string.Format(@"insert into borrow(ReaID,BookID,BookName,BookWriter,Outdate,YHdate,Fine,CLState,MID)
                                         values({0},'{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}')",
                                         readerId, bookId, bookName, bookWriter, dtBorrow, dtYingBack, fine, state, manageId);
            cmd = new MySqlCommand(sql, conn);
            int result = cmd.ExecuteNonQuery();
            if (result == 1)
            {
                MessageBox.Show("借阅成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearText();    //清空文本框
                string sql1 = @"select ReaID,BookID,BookName,BookWriter,Outdate,YHdate,Indate,Fine,CLState,MID from borrow";
                BindDvgDate(sql1);   //刷新数据
            }
            else
            {
                MessageBox.Show("借阅失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //判断，如果借阅次数已经超过了限定次数，则不能再进行借阅 
        private bool BorrowCheck()
        {
            string sql = "";
            int readeId = Convert.ToInt32(this.txtReaderID.Text);

            sql = string.Format(@"select COUNT(1) from reader where ReaID={0}", readeId);
            cmd = new MySqlCommand(sql, conn);
            int result = Convert.ToInt32(cmd.ExecuteScalar());
            if (result == 1)
            {
                sql = string.Format(@"select COUNT(1) from borrow where ReaID={0}", readeId);
                cmd = new MySqlCommand(sql, conn);
                int count = Convert.ToInt32(cmd.ExecuteScalar());

                sql = string.Format("select * from view_borrowmaxcount");
                cmd = new MySqlCommand(sql, conn);
                int MaxCount = Convert.ToInt32(cmd.ExecuteScalar());

                if (count < MaxCount)
                {
                    return true;
                }
                else
                {
                    MessageBox.Show("您的借阅次数已达上限，请先归还其他书本！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            else
            {
                MessageBox.Show("没有注册该读者！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }

        //文本框回车事件，自动补充图书名和作者
        private void txtBookId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (queryBookInfo()[0] != "")
                {
                    this.txtBookName.Text = queryBookInfo()[0];
                    this.txtBookWriter.Text = queryBookInfo()[1];
                }
                else
                {
                    MessageBox.Show("根据书的编号没有找到这本书\n请到 [图书管理] 查看！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        //根据图书编号查询图书名和作者
        private string[] queryBookInfo()
        {
            string[] param = new string[2];
            try
            {
                conn = dh.Connection;
                dh.OpenConnection();
                string bookId = this.txtBookId.Text;
                string sql1 = string.Format(@"select BookName from book where BookID = '{0}'", bookId);
                cmd = new MySqlCommand(sql1, conn);
                param[0] = Convert.ToString(cmd.ExecuteScalar());
                string sq2 = string.Format(@"select BookWriter from book where BookID = '{0}'", bookId);
                cmd = new MySqlCommand(sq2, conn);
                param[1] = Convert.ToString(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                dh.CloseConnection();
            }
            return param;
        }

        //刷新DataGridView
        private void button7_Click(object sender, EventArgs e)
        {
            string sql = @"select ReaID,BookID,BookName,BookWriter,Outdate,YHdate,Indate,Fine,CLState,MID from borrow";
            BindDvgDate(sql);       //绑定数据源
        }

        int rowTotalCount;
        int selectRowCount;
        //选中验证
        private bool selectCheck()
        {
            rowTotalCount = this.dataGridView1.Rows.Count;          //获取DataGridView中的数据总记录
            selectRowCount = this.dataGridView1.SelectedRows.Count; //获取DataGridView中选中的数据总记录
            if (rowTotalCount == 0)
            {
                MessageBox.Show("当前没有数据！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false; //结束当前方法
            }
            else if (selectRowCount == 0)
            {
                MessageBox.Show("您还没有选中数据！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false; //结束当前方法
            }
            else
            {
                return true;
            }
        }

        //打开编辑窗口
        private void button6_Click(object sender, EventArgs e)
        {
            if (selectCheck())
            {
                new Form_BorrowEdit(ObjectRowContext()).ShowDialog();
            }
        }

        //将选中数据行进行包装
        private object[] ObjectRowContext()
        {
            object readerId = this.dataGridView1.SelectedRows[0].Cells[0].Value;
            object bookId = this.dataGridView1.SelectedRows[0].Cells[1].Value;
            object bookName = this.dataGridView1.SelectedRows[0].Cells[2].Value;
            object bookWriter = this.dataGridView1.SelectedRows[0].Cells[3].Value;
            object dtBorrow = this.dataGridView1.SelectedRows[0].Cells[4].Value;
            object dtYingHuan = this.dataGridView1.SelectedRows[0].Cells[5].Value;
            object dtBack = this.dataGridView1.SelectedRows[0].Cells[6].Value;
            object fineMoney = this.dataGridView1.SelectedRows[0].Cells[7].Value;
            object state = this.dataGridView1.SelectedRows[0].Cells[8].Value;
            object manageId = this.dataGridView1.SelectedRows[0].Cells[9].Value;

            object[] param = { readerId, bookId, bookName, bookWriter, dtBorrow, dtYingHuan, dtBack, fineMoney, state, manageId };
            return param;
        }


        //删除事件
        private void button5_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == MessageBox.Show("确定要删除吗？这将无法恢复！", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
            {
                try
                {
                    conn = dh.Connection;
                    dh.OpenConnection();
                    //删除
                    deleteBorrow();
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

        //删除数据
        private void deleteBorrow()
        {
            int readerId = Convert.ToInt32(this.dataGridView1.SelectedRows[0].Cells[0].Value);
            string bookId = Convert.ToString(this.dataGridView1.SelectedRows[0].Cells[1].Value);
            string sql = string.Format("delete from borrow where ReaID={0} and BookID= '{1}'", readerId, bookId);
            cmd = new MySqlCommand(sql, conn);
            int result = cmd.ExecuteNonQuery();
            if (result == 1)
            {
                MessageBox.Show("删除成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearText();    //清空文本框
            }
            else
            {
                MessageBox.Show("删除失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        string selectText;
        string id;
        //分类改变事件
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectText = this.cmbState2.Text;
            id = this.txtID.Text;
            string sql;
            if (selectText == "全部")
            {
                sql = string.Format(@"select ReaID,BookID,BookName,BookWriter,Outdate,YHdate,Indate,Fine,CLState,MID from borrow");
                BindDvgDate(sql);
            }
            else if (id == "")
            {
                sql = string.Format(@"select ReaID,BookID,BookName,BookWriter,Outdate,YHdate,Indate,Fine,CLState,MID from borrow where CLState='{0}'", selectText);
                BindDvgDate(sql);
            }
            else
            {
                sql = string.Format(@"select ReaID,BookID,BookName,BookWriter,Outdate,YHdate,Indate,Fine,CLState,MID from borrow where ReaID={0} and CLState='{1}'", id, selectText);
                BindDvgDate(sql);
            }
        }


        //书ID改变事件，实时更新数据
        private void txtID_TextChanged(object sender, EventArgs e)
        {
            id = this.txtID.Text;
            selectText = this.cmbState2.Text;
            if (id == "")
            {
                string sql = string.Format(@"select ReaID,BookID,BookName,BookWriter,Outdate,YHdate,Indate,Fine,CLState,MID from borrow");
                BindDvgDate(sql);
            }
            else if (selectText == "全部")
            {
                string sql = string.Format(@"select ReaID,BookID,BookName,BookWriter,Outdate,YHdate,Indate,Fine,CLState,MID from borrow where ReaID={0}", id);
                BindDvgDate(sql);
            }

            else
            {
                string sql = string.Format(@"select ReaID,BookID,BookName,BookWriter,Outdate,YHdate,Indate,Fine,CLState,MID from borrow where ReaID={0} and CLState='{1}'", id, selectText);
                BindDvgDate(sql);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string a = "D:" + "\\图书借阅信息.xls";
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

        //更新罚款
        private void UpdateFine()
        {
            try
            {
                conn = dh.Connection;
                dh.OpenConnection();
                string sql = "";
                int result = 0;
                for (int i = 0; i < this.dataGridView1.Rows.Count; i++)
                {
                    int id = Convert.ToInt32(this.dataGridView1.Rows[i].Cells[0].Value);    //获取ID值
                    sql = string.Format(@"SELECT TIMESTAMPDIFF(DAY,borrow.YHdate,(SELECT now())) from borrow where borrow.ReaID = {0}", id);
                    cmd = new MySqlCommand(sql, conn);   //执行查询
                    result = Convert.ToInt32(cmd.ExecuteScalar());
                    if (result > 0)
                    {
                        sql = string.Format(@"update borrow set Fine = {0} where ReaID = {1} and CLState = '借阅中'", result, id);
                        cmd = new MySqlCommand(sql, conn);
                        result = cmd.ExecuteNonQuery(); //执行查询
                    }
                    else
                    {
                        continue;
                    }
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