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
    public partial class Form_EditBook : Form
    {
        public Form_EditBook()
        {
            InitializeComponent();
        }

        DBHelper dh = new DBHelper();
        MySqlConnection conn = null;
        MySqlCommand cmd = null;
        DataSet ds = null;
        MySqlDataAdapter da = null;
        string bookId = null;   //保存书籍编号

        public Form_EditBook(object[] param)
        {
            InitializeComponent();

            try
            {
                conn = dh.Connection;       //获取链接
                ds = new DataSet();         //创建数据临时仓库

                ShowRoom();   //绑定书馆号
                ShowClass();  //绑定书的类型
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                //
            }
            bookId = Convert.ToString(param[0]);    //保存书籍编号
            this.txtBianHao.Text = Convert.ToString(param[0]);
            this.txtISBN.Text = Convert.ToString(param[1]);
            this.txtBookName.Text =Convert.ToString(param[2]);
            this.txtWriter.Text=Convert.ToString(param[3]);
            this.txtoutName.Text=Convert.ToString(param[4]);
            this.nudPrice.Value=Convert.ToDecimal(param[5]);
            this.dateTimePicker1.Value=Convert.ToDateTime(param[6]);
            this.cbmBookType.Text =  Convert.ToString(param[7]);
            this.txtMain.Text=Convert.ToString(param[8]);
            this.txtPrim.Text=Convert.ToString(param[9]);
            this.txtBookNum.Text = Convert.ToString(param[10]);
            if (Convert.ToInt32(param[11]) == 1)
            {
                rbYes.Checked = true;
                rbNo.Checked = false;
            }
            else if (Convert.ToInt32(param[11]) == 0)
            {
                rbYes.Checked = false;
                rbNo.Checked = true;
            }
            this.cmbRoom.Text = Convert.ToString(param[12]);
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

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ShowClass()
        {
            try
            {
                string sql2 = "select * from booktype";
                da = new MySqlDataAdapter(sql2, conn);
                da.Fill(ds, "图书类别");
                this.cbmBookType.DataSource = ds.Tables["图书类别"];
                this.cbmBookType.DisplayMember = "BookType";
                this.cbmBookType.ValueMember = "ID";
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
            else if (bookWriter.Equals("") || bookWriter == null)
            {
                MessageBox.Show("请填写作者            ", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtWriter.Focus();
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

                    string sql = string.Format(@"update book set BookID='{0}',BookNo='{1}',BookName='{2}',BookWriter='{3}',BookPublish='{4}',BookPrice={5},
                                                BookDate='{6}',BookClass={7},BookMain='{8}',BookPrim='{9}',BookCopy={10},BookState={11},BookRNo='{12}' where BookID='{13}'",
                                                bianHao, isbn, bookName, bookWriter, outName, bookPrice, date, bookType, bookMain, bookPrim, bookNum, bt, roomNum, bookId);
                    cmd = new MySqlCommand(sql, conn);
                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        MessageBox.Show("修改成功！            ", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("修改失败！            ", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
    }
}