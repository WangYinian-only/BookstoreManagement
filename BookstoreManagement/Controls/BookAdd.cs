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
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;
using System.Threading;
using BookstoreManagement.Forms;
using System.Runtime.InteropServices;

namespace BookstoreManagement.Controls
{


    public partial class BookAdd : UserControl
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

        public BookAdd()
        {
            InitializeComponent();
            //窗体弹出效果
            AnimateWindow(this.Handle, 10, AW_CENTER);
        }

        DBHelper dh = new DBHelper();
        MySqlConnection conn = null;
        MySqlCommand cmd = null;
        DataSet ds = null;
        MySqlDataAdapter da = null;

        private void BookAdd_Load(object sender, EventArgs e)
        {
            try

            {
                conn = dh.Connection;   //获取链接
                ds = new DataSet();     //创建数据临时仓库

                ShowRoom();   //绑定书馆号
                ShowClass();  //绑定书的类型
                string sqll = @"select * from view_book";
                ShowData(sqll); //绑定数据源


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

        private bool Check()
        {
            string bianHao = txtBianHao.Text.Trim();
            string isbn = txtISBN.Text.Trim();
            string bookName = txtBookName.Text.Trim();
            string bookWriter = txtWriter.Text.Trim();
            string outName = txtoutName.Text.Trim();
            decimal bookPrice = nudPrice.Value;
            string roomNum = Convert.ToString(cmbRoom.SelectedValue);
            string bookType = Convert.ToString(cbmBookType.SelectedValue);

            if (bianHao.Equals("") || bianHao == null)
            {
                MessageBox.Show("请填写编号!            ", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBianHao.Focus();
                return false;
            }
            else if (isbn.Equals("") || isbn == null)
            {
                MessageBox.Show("请填写索书号            ", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtISBN.Focus();
                return false;
            }
            else if (bookName.Equals("") || bookName == null)
            {
                MessageBox.Show("请填写书名            ", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBookName.Focus();
                return false;
            }
            else if (outName.Equals("") || outName == null)
            {
                MessageBox.Show("请填写出版社单位            ", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtoutName.Focus();
                return false;
            }
            else if (bookPrice.Equals(""))
            {
                MessageBox.Show("请填写编号            ", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                nudPrice.Focus();
                return false;
            }
            else if (roomNum.Equals("") || roomNum == null)
            {
                MessageBox.Show("请选择馆室号            ", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbRoom.Focus();
                return false;
            }
            else if (bookType.Equals("") || bookType == null)
            {
                MessageBox.Show("请选择图书类别            ", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbmBookType.Focus();
                return false;
            }
            else if (!(rbYes.Checked || rbNo.Checked))
            {
                MessageBox.Show("请选则是否可借            ", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBianHao.Focus();
                return false;
            }
            else
            {
                return true;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (Check())
            {
                try
                {
                    conn = dh.Connection;
                    dh.OpenConnection();
                    string bianHao = txtBianHao.Text.Trim();
                    string isbn = txtISBN.Text.Trim();
                    string bookName = txtBookName.Text.Trim();
                    string bookWriter = txtWriter.Text.Trim();
                    string outName = txtoutName.Text.Trim();
                    int bookType = Convert.ToInt32(cbmBookType.SelectedValue);
                    decimal bookPrice = nudPrice.Value;
                    string roomNum = Convert.ToString(cmbRoom.SelectedValue);
                    DateTime date = dateTimePicker1.Value;
                    string bookPrim = txtPrim.Text.Trim();
                    string bookMain = txtMain.Text.Trim();
                    int bookNum = Convert.ToInt32(txtBookNum.Text.Trim());
                    int bt = rbNo.Checked ? 0 : 1;

                    string sql = string.Format(@"insert into book(BookID,BookNo,BookName,BookWriter,BookPublish,BookPrice,BookDate,BookClass,BookMain,BookPrim,BookCopy,BookState,BookRNo)
                                              values('{0}','{1}','{2}','{3}','{4}',{5},'{6}',{7},'{8}','{9}',{10},{11},'{12}')",
                                                bianHao, isbn, bookName, bookWriter, outName, bookPrice, date, bookType, bookMain, bookPrim, bookNum, bt, roomNum);
                    cmd = new MySqlCommand(sql, conn);
                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        MessageBox.Show("添加成功！            ", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearText();    //清空文本框
                        string sqll = @"select * from view_book";
                        ShowData(sqll); //刷新数据
                    }
                    else
                    {
                        MessageBox.Show("添加失败！            ", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    MessageBox.Show(txtBookNum.Text.Trim());
                }
                finally
                {
                    dh.CloseConnection();
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearText();
        }

        private void ClearText()
        {
            txtBianHao.Clear();
            txtISBN.Clear();
            txtBookName.Clear();
            txtWriter.Clear();
            txtoutName.Clear();
            cbmBookType.SelectedIndex = 0;
            nudPrice.Value = 0;
            cmbRoom.SelectedIndex = 0;
            txtPrim.Clear();
            txtMain.Clear();
            txtBookNum.Clear();
            rbNo.Checked = false;
            rbYes.Checked = false;
        }

        private void btnRefensh_Click(object sender, EventArgs e)
        {
            string sql = @"select * from view_book";
            ShowData(sql);
        }

        private void ShowData(string sql)
        {
            try
            {
                conn = dh.Connection;
                ds = new DataSet();
                da = new MySqlDataAdapter(sql, conn);
                da.Fill(ds, "bookQuery");
                this.dataGridView1.DataSource = ds.Tables["bookQuery"];
                this.lblShowRowsCount.Text = this.dataGridView1.Rows.Count.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { }
        }

        private void ShowClass()
        {
            try
            {
                string sql2 = @"select ID, booktype from booktype";
                da = new MySqlDataAdapter(sql2, conn);
                da.Fill(ds, "图书类别Add");

                this.cbmBookType.DataSource = ds.Tables["图书类别Add"];
                this.cbmBookType.DisplayMember = "BookType";
                this.cbmBookType.ValueMember = "ID";

                string sql3 = @"select '0' ID,'全部' booktype
                                union all
                                select ID, booktype from booktype";
                da = new MySqlDataAdapter(sql3, conn);
                da.Fill(ds, "图书类别");
                this.cmbClass.DataSource = ds.Tables["图书类别"];
                this.cmbClass.DisplayMember = "BookType";
                this.cmbClass.ValueMember = "ID";


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { }
        }

        private void ShowRoom()
        {
            string sql1 = "select * from room";
            da = new MySqlDataAdapter(sql1, conn);
            da.Fill(ds, "馆室号");
            this.cmbRoom.DataSource = ds.Tables["馆室号"];
            this.cmbRoom.DisplayMember = "RoomNo";
            this.cmbRoom.ValueMember = "RoomNo";

        }

        private void btnEdit_Click(object sender, EventArgs e)
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
                object bianHao = this.dataGridView1.SelectedRows[0].Cells[0].Value;
                object isbn = this.dataGridView1.SelectedRows[0].Cells[1].Value;
                object bookName = this.dataGridView1.SelectedRows[0].Cells[2].Value;
                object bookWriter = this.dataGridView1.SelectedRows[0].Cells[3].Value;
                object outName = this.dataGridView1.SelectedRows[0].Cells[4].Value;
                object bookPrice = this.dataGridView1.SelectedRows[0].Cells[5].Value;
                object date = this.dataGridView1.SelectedRows[0].Cells[6].Value;
                object bookType = this.dataGridView1.SelectedRows[0].Cells[7].Value;
                object bookMain = this.dataGridView1.SelectedRows[0].Cells[8].Value;
                object bookPrim = this.dataGridView1.SelectedRows[0].Cells[9].Value;
                object bookNum = this.dataGridView1.SelectedRows[0].Cells[10].Value;
                object bt = this.dataGridView1.SelectedRows[0].Cells[11].Value;
                object roomNum = this.dataGridView1.SelectedRows[0].Cells[12].Value;

                object[] param = { bianHao, isbn, bookName, bookWriter, outName, bookPrice, date, bookType, bookMain, bookPrim, bookNum, bt, roomNum };
                new Form_EditBook(param).ShowDialog();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
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
                    string bookId = Convert.ToString(this.dataGridView1.SelectedRows[0].Cells[0].Value);
                    try
                    {
                        dh.OpenConnection();
                        conn = dh.Connection;
                        string sql = "";
                        int result = 0;

                        string bianHao = Convert.ToString(this.dataGridView1.SelectedRows[0].Cells[0].Value);
                        string isbn = Convert.ToString(this.dataGridView1.SelectedRows[0].Cells[1].Value);
                        string bookName = Convert.ToString(this.dataGridView1.SelectedRows[0].Cells[2].Value);
                        string bookWriter = Convert.ToString(this.dataGridView1.SelectedRows[0].Cells[3].Value);
                        string outName = Convert.ToString(this.dataGridView1.SelectedRows[0].Cells[4].Value);
                        decimal bookPrice = Convert.ToDecimal(this.dataGridView1.SelectedRows[0].Cells[5].Value);
                        DateTime date = Convert.ToDateTime(this.dataGridView1.SelectedRows[0].Cells[6].Value);
                        int bookType = Convert.ToInt32(this.dataGridView1.SelectedRows[0].Cells[7].Value);
                        string bookMain = Convert.ToString(this.dataGridView1.SelectedRows[0].Cells[8].Value);
                        string bookPrim = Convert.ToString(this.dataGridView1.SelectedRows[0].Cells[9].Value);
                        int bookNum = Convert.ToInt32(this.dataGridView1.SelectedRows[0].Cells[10].Value);
                        int bt = Convert.ToInt32(this.dataGridView1.SelectedRows[0].Cells[11].Value);
                        string roomNum = Convert.ToString(this.dataGridView1.SelectedRows[0].Cells[12].Value);
                        //备份数据
                        sql = string.Format(@"insert into book_recovery(BookID,BookNo,BookName,BookWriter,BookPublish,BookPrice,BookDate,BookClass,BookMain,BookPrim,BookCopy,BookState,BookRNo)
                                              values('{0}','{1}','{2}','{3}','{4}',{5},'{6}',{7},'{8}','{9}',{10},{11},'{12}')",
                                              bianHao, isbn, bookName, bookWriter, outName, bookPrice, date, bookType, bookMain, bookPrim, bookNum, bt, roomNum);
                        cmd = new MySqlCommand(sql, conn);
                        result += cmd.ExecuteNonQuery();

                        //备份完成后开始删除
                        sql = string.Format("delete from book where BookID='{0}'", bookId);
                        cmd = new MySqlCommand(sql, conn);
                        result += cmd.ExecuteNonQuery();
                        if (result == 2)
                        {
                            MessageBox.Show("删除成功！可在数据恢复中恢复", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            string sqll = @"select * from view_book";
                            ShowData(sqll);
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
        }


        private void cmbClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sql = "";
            if (this.cmbClass.Text == "全部")
            {
                sql = string.Format(@"select * from view_book");
                ShowData(sql);
            }
            else if (this.txtBook.Text == "")
            {
                sql = string.Format(@"select * from view_book where BookType='{0}'", this.cmbClass.Text);
                ShowData(sql);
            }
            else if (this.cmbClass.Text != "全部" && this.txtBook.Text != "")
            {
                sql = string.Format(@"select * from view_book where BookType='{0}' and BookName like '%{1}%'", this.cmbClass.Text, this.txtBook.Text);
                ShowData(sql);
            }
        }

        private void txtBook_TextChanged(object sender, EventArgs e)
        {
            string sql = "";
            if (this.cmbClass.Text == "全部")
            {
                sql = string.Format(@"select * from view_book where BookName like '%{0}%'", this.txtBook.Text);
                ShowData(sql);
            }
            else if (this.txtBook.Text == "")
            {
                sql = string.Format(@"select * from view_book where BookType='{0}'", this.cmbClass.Text);
                ShowData(sql);
            }
            else if (this.cmbClass.Text == "全部" && this.txtBook.Text == "")
            {
                sql = string.Format(@"select * from view_book");
                ShowData(sql);
            }
            else if (this.cmbClass.Text != "全部" && this.txtBook.Text != "")
            {
                sql = string.Format(@"select * from view_book where BookType='{0}' and BookName like '%{1}%'", this.cmbClass.Text, this.txtBook.Text);
                ShowData(sql);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string a = "D:" + "\\图书信息.xls";
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