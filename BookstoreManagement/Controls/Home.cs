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
using System.Windows.Forms.DataVisualization.Charting;
using System.Threading;
using System.Runtime.InteropServices;

namespace BookstoreManagement.Controls
{
    public partial class Home : UserControl
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

        //构造函数
        public Home()
        {
            InitializeComponent();
            //窗体弹出效果
            AnimateWindow(this.Handle, 100, AW_CENTER);
        }

        //初始化数据库连接
        DBHelper dh = new DBHelper();
        MySqlConnection conn = null;
        MySqlCommand cmd = null;


        //设置Chart1
        private void Chart1Set()
        {
            List<int> x = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14 };  //x轴的列书

            List<int> BookNum = QueryWeekBookCount();
            List<int> PeopleNum = QueryWeekPeopleCount();

            //chart1.Series["书本数"][0].Label = BookNum.ToString();
            //chart1.Series["人数"].Label = BookNum.ToString();
            chart1.Series["书本数"].Points.DataBindXY(x, BookNum);
            chart1.Series["人数"].Points.DataBindXY(x, PeopleNum);
        }


        //设置Chart2
        private void Chart2Set()
        {
            List<string> xData = new List<string>() { "学生", "教师", "主任", "校长", "职工", "外借" };
            List<int> yData = ReaderTypeCount();
            chart2.Series[0]["PieLabelStyle"] = "Outside";//将文字移到外侧 
            chart2.Series[0]["PieLineColor"] = "Black";//绘制黑色的连线。 
            chart2.Series[0].Points.DataBindXY(xData, yData);
        }

        //设置Chart3
        private void Chart3Set()
        {
            List<string> xData = new List<string>() { "男", "女" };
            List<int> yData = QueryPeopleCount();   //将返回的集合赋值给yData
            chart3.Series[0]["PieLabelStyle"] = "Outside";//将文字移到外侧 
            chart3.Series[0]["PieLineColor"] = "Black";//绘制黑色的连线。 
            chart3.Series[0].Points.DataBindXY(xData, yData);
        }

        //查询男士和女士人数
        private List<int> QueryPeopleCount()
        {
            List<int> list = new List<int>();
            try
            {
                conn = dh.Connection;
                dh.OpenConnection();
                string sql = "";
                int Count = 0;

                //查询出男生的人数
                sql = @"select COUNT(reader.ReaSex)  from borrow,reader where borrow.ReaID = reader.ReaID and reader.ReaSex = '男'";
                cmd = new MySqlCommand(sql, conn);
                Count = Convert.ToInt32(cmd.ExecuteScalar());
                list.Add(Count);

                //查询出女生的人数
                sql = @"select COUNT(reader.ReaSex)  from borrow,reader where borrow.ReaID = reader.ReaID and reader.ReaSex = '女'";
                cmd = new MySqlCommand(sql, conn);
                Count = Convert.ToInt32(cmd.ExecuteScalar());
                list.Add(Count);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                dh.CloseConnection();
            }
            return list;
        }

        //查询读者类别的个数
        private List<int> ReaderTypeCount()
        {
            List<int> list = new List<int>();
            try
            {
                conn = dh.Connection;
                dh.OpenConnection();
                string sql = "";
                int count = 0;

                //查询学生的个数
                sql = @"select COUNT(reader.ReaLBID)  from borrow,reader where borrow.ReaID = reader.ReaID and reader.ReaLBID = 1";
                cmd = new MySqlCommand(sql, conn);
                count = Convert.ToInt32(cmd.ExecuteScalar());
                list.Add(count);

                //查询教师的个数
                sql = @"select COUNT(reader.ReaLBID)  from borrow,reader where borrow.ReaID = reader.ReaID and reader.ReaLBID = 3";
                cmd = new MySqlCommand(sql, conn);
                count = Convert.ToInt32(cmd.ExecuteScalar());
                list.Add(count);

                //查询主任的个数
                sql = @"select COUNT(reader.ReaLBID)  from borrow,reader where borrow.ReaID = reader.ReaID and reader.ReaLBID = 4";
                cmd = new MySqlCommand(sql, conn);
                count = Convert.ToInt32(cmd.ExecuteScalar());
                list.Add(count);

                //查询校长的个数
                sql = @"select COUNT(reader.ReaLBID)  from borrow,reader where borrow.ReaID = reader.ReaID and reader.ReaLBID = 5";
                cmd = new MySqlCommand(sql, conn);
                count = Convert.ToInt32(cmd.ExecuteScalar());
                list.Add(count);

                //查询职工的个数
                sql = @"select COUNT(reader.ReaLBID)  from borrow,reader where borrow.ReaID = reader.ReaID and reader.ReaLBID = 2";
                cmd = new MySqlCommand(sql, conn);
                count = Convert.ToInt32(cmd.ExecuteScalar());
                list.Add(count);

                //查询外借的个数
                sql = @"select COUNT(reader.ReaLBID)  from borrow,reader where borrow.ReaID = reader.ReaID and reader.ReaLBID = 6";
                cmd = new MySqlCommand(sql, conn);
                count = Convert.ToInt32(cmd.ExecuteScalar());
                list.Add(count);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                dh.CloseConnection();
            }
            return list;
        }

        //查询近14天借阅书本数
        private List<int> QueryWeekBookCount()
        {
            List<int> list = new List<int>();
            try
            {
                conn = dh.Connection;
                dh.OpenConnection();
                string sql = "";
                int Count = 0;
                string today = "";  //当前日期
                string yestoday = ""; //昨天的日期
                //使用for循环读取
                for (int i = 14; i > 0; i--)
                {
                    today = DateTime.Now.AddDays(0 - (i - 1)).ToString("yyyy-MM-dd HH:mm:ss");
                    yestoday = DateTime.Now.AddDays(0 - i).ToString("yyyy-MM-dd HH:mm:ss");

                    sql = string.Format(@"select COUNT(1) from borrow where Outdate BETWEEN '{0}' AND '{1}'", yestoday, today);
                    cmd = new MySqlCommand(sql, conn);
                    Count = Convert.ToInt32(cmd.ExecuteScalar());
                    list.Add(Count);
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
            return list;
        }

        //查询近14天借阅人数
        private List<int> QueryWeekPeopleCount()
        {
            List<int> list = new List<int>();
            try
            {
                conn = dh.Connection;
                dh.OpenConnection();
                string sql = "";
                int Count = 0;
                string today = "";  //当前日期
                string yestoday = ""; //昨天的日期
                //使用for循环读取
                for (int i = 14; i > 0; i--)
                {
                    today = DateTime.Now.AddDays(0 - (i - 1)).ToString("yyyy-MM-dd HH:mm:ss");
                    yestoday = DateTime.Now.AddDays(0 - i).ToString("yyyy-MM-dd HH:mm:ss");

                    sql = string.Format(@"select COUNT(DISTINCT ReaID) from borrow where Outdate BETWEEN '{0}' AND '{1}'", yestoday, today);
                    cmd = new MySqlCommand(sql, conn);
                    Count = Convert.ToInt32(cmd.ExecuteScalar());
                    list.Add(Count);
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
            return list;
        }

        //刷新
        private void button1_Click(object sender, EventArgs e)
        {
            Chart1Set();
            Chart2Set();
            Chart3Set();
        }

        //窗体加载事件
        private void Home_Load(object sender, EventArgs e)
        {
            Chart1Set();
            Chart2Set();
            Chart3Set();
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}