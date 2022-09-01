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
    public partial class QueryInfo : UserControl
    {        //窗体弹出或消失效果
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

        public QueryInfo()
        {
            InitializeComponent();//窗体弹出效果
            AnimateWindow(this.Handle, 10, AW_CENTER);
        }

        //初始化数据库连接
        DBHelper dh = new DBHelper();
        MySqlConnection conn = null;
        DataSet ds = null;
        MySqlDataAdapter da = null;

        //绑定书类别
        private void ShowClass()
        {
            try
            {
                string sql2 = @"select '0' ID,'全部' booktype
                                union all
                                select ID, booktype from booktype";
                da = new MySqlDataAdapter(sql2, conn);
                da.Fill(ds, "图书类别");

                this.cmbBookType.DataSource = ds.Tables["图书类别"];
                this.cmbBookType.DisplayMember = "BookType";
                this.cmbBookType.ValueMember = "ID";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { }
        }


        //给DataGridView中填充数据
        private void ShowData(string sql)
        {
            try
            {
                conn = dh.Connection;
                ds = new DataSet();
                da = new MySqlDataAdapter(sql, conn);
                da.Fill(ds, "查询图书");
                this.dataGridView1.DataSource = ds.Tables["查询图书"];
                lblRowsCount.Text = this.dataGridView1.Rows.Count.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { }
        }

        //刷新数据事件
        private void btnReFrensh_Click(object sender, EventArgs e)
        {
            string sql = @"select * from view_book";
            ShowData(sql); //绑定数据
        }

        private void cmbBookType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sql = "";
            if (this.cmbBookType.Text == "全部")
            {
                sql = string.Format(@"select * from view_book where BookName like '%{0}%'", this.txtBookName.Text);
                ShowData(sql);
            }
            else if (this.txtBookName.Text == "")
            {
                sql = string.Format(@"select * from view_book where BookType='{0}'", this.cmbBookType.Text);
                ShowData(sql);
            }
            else if (this.cmbBookType.Text == "全部" && this.txtBookName.Text == "")
            {
                sql = string.Format(@"select * from view_book");
                ShowData(sql);
            }
            else if (this.cmbBookType.Text != "全部" && this.txtBookName.Text != "")
            {
                sql = string.Format(@"select * from view_book where BookType='{0}' and BookName like '%{1}%'", this.cmbBookType.Text, this.txtBookName.Text);
                ShowData(sql);
            }

        }

        //按照书名搜索
        private void txtBookName_TextChanged(object sender, EventArgs e)
        {
            string sql = "";
            if (this.cmbBookType.Text == "全部")
            {
                sql = string.Format(@"select * from view_book where BookName like '%{0}%'", this.txtBookName.Text);
                ShowData(sql);
            }
            else if (this.txtBookName.Text == "")
            {
                sql = string.Format(@"select * from view_book where BookType='{0}'", this.cmbBookType.Text);
                ShowData(sql);
            }
            else if (this.cmbBookType.Text != "全部" && this.txtBookName.Text != "")
            {
                sql = string.Format(@"select * from view_book where BookType='{0}' and BookName like '%{1}%'", this.cmbBookType.Text, this.txtBookName.Text);
                ShowData(sql);
            }
            else if (this.cmbBookType.Text == "全部" && this.txtBookName.Text == "")
            {
                sql = string.Format(@"select * from view_book");
                ShowData(sql);
            }
        }

        //按照书的ISBN查询
        private void txtBookISBN_TextChanged(object sender, EventArgs e)
        {
            string sql = "";
            if (this.txtBookISBN.Text != "")
            {
                sql = string.Format(@"select * from view_book where BookNo like '%{0}%'", txtBookISBN.Text);
                ShowData(sql);
            }
            else
            {
                sql = string.Format(@"select * from view_book");
                ShowData(sql);
            }

        }

        private void QueryInfo_Load(object sender, EventArgs e)
        {
            try
            {
                conn = dh.Connection;   //获取链接
                ds = new DataSet();     //创建数据临时仓库

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
    }
}