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
    public partial class BillManage : UserControl
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

        public BillManage()
        {
            InitializeComponent();
            //窗体弹出效果
            AnimateWindow(this.Handle, 10, AW_CENTER);
        }

        //初始化数据库连接
        DBHelper dh = new DBHelper();
        MySqlConnection conn = null;
        MySqlCommand cmd = null;
        MySqlDataAdapter da = null;
        DataSet ds = null;


        //绑定数据
        private void ShowData(string sql)
        {
            try
            {
                conn = dh.Connection;
                da = new MySqlDataAdapter(sql, conn);
                ds = new DataSet();
                da.Fill(ds, "罚款账单");
                this.dataGridView1.DataSource = ds.Tables["罚款账单"];
                this.lblRowsCount.Text = this.dataGridView1.Rows.Count.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string a = "D:" + "\\罚款账单管理.xls";
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

        private void BillManage_Load(object sender, EventArgs e)
        {
            ComputeBill();
            string sql = @"select * from borrow where (TIMESTAMPDIFF(DAY,YHdate,(SELECT now()))) > 0";
            ShowData(sql);
            this.dataGridView1.ClearSelection();
            UpdateFine();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string sql = @"select * from borrow where (TIMESTAMPDIFF(DAY,YHdate,(SELECT now()))) > 0";
            ShowData(sql);
            UpdateFine();
        }

        //搜索功能
        private void txtReaderId_TextChanged(object sender, EventArgs e)
        {
            string sql;
            if (this.txtReaderId.Text == "")
            {
                sql = @"select * from borrow where (TIMESTAMPDIFF(DAY,YHdate,(SELECT now()))) > 0";
                ShowData(sql);
            }
            else
            {
                sql = string.Format(@"select * from borrow where (TIMESTAMPDIFF(DAY,YHdate,(SELECT now()))) > 0 AND ReaID = {0}", Convert.ToInt32(this.txtReaderId.Text));
                ShowData(sql);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //删除前验证
            if (this.dataGridView1.Rows.Count > 0)
            {
                if (this.dataGridView1.SelectedRows.Count > 0)
                {
                    if (DialogResult.Yes == MessageBox.Show("确定要删除吗？这将无法恢复！", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                    {
                        delete();
                    }
                }
            }
        }

        //删除账单操作
        private void delete()
        {
            try
            {
                conn = dh.Connection;
                dh.OpenConnection();
                //获取ID
                int id = Convert.ToInt32(this.dataGridView1.SelectedRows[0].Cells[0].Value);
                string sql = string.Format("delete from borrow where ReaID = {0}", id);
                cmd = new MySqlCommand(sql, conn);
                int result = cmd.ExecuteNonQuery();
                if (result == 1)
                {
                    MessageBox.Show("删除成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        //计算账单
        private void ComputeBill()
        {
            try
            {
                conn = dh.Connection;
                dh.OpenConnection();
                string sql;

                //计算图书馆总读者
                sql = "select COUNT(1) from reader";
                cmd = new MySqlCommand(sql, conn);
                this.txtTotalReader.Text = Convert.ToString(cmd.ExecuteScalar());

                //计算扣费图书馆总账单
                sql = @"select(select(select COUNT(*) from reader) * 50) + (select sum(Fine) from borrow)";
                cmd = new MySqlCommand(sql, conn);
                this.txtTotalMoney.Text = Convert.ToString(cmd.ExecuteScalar());

                //计算扣费总账单
                sql = @"select sum(Fine) from borrow";
                cmd = new MySqlCommand(sql, conn);
                this.txtReaderFine.Text = Convert.ToString(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                dh.CloseConnection();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (ObjectRowContext() != null)
            {
                new Form_BillManager(ObjectRowContext()).ShowDialog();
            }

        }

        //数据装箱
        private object[] ObjectRowContext()
        {
            if (this.dataGridView1.Rows.Count != 0)
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
            else
            {
                return null;
            }
        }

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