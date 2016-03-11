using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Archer.SSO
{
    public partial class Frm_Customer : Form
    {
        public Frm_Customer()
        {
            InitializeComponent();
        }

        private void Frm_Customer_Load(object sender, EventArgs e)
        {
            dataGridViewComboBox1.sDisplayField = "学号,姓名,性别,专业,入学年份";
            dataGridViewComboBox1.sDisplayMember = "姓名";
            dataGridViewComboBox1.sKeyWords = "学号,姓名";
            dataGridViewComboBox1.sValueMember = "学号";
            dataGridViewComboBox1.DataSource = createStudentTable();
            dataGridViewComboBox1.RowFilterVisible = true;
            dataGridViewComboBox1.AfterSelector += dataGridViewComboBox1_AfterSelector;

           
        }

        void dataGridViewComboBox1_AfterSelector(object sender, ExtendControlLib.AfterSelectorEventArgs e)
        {
            DataGridViewRow row = e.Value as DataGridViewRow;
            DataRowView dr = row.DataBoundItem as DataRowView;

            //MessageBox.Show("学号:" + dr["学号"].ToString() + " 姓名:" + dr["姓名"].ToString() + " 性别:" + dr["性别"].ToString() + " 专业:" + dr["专业"].ToString() + " 入学年份:" + dr["入学年份"].ToString());
            ExtendControlLib.DataGridViewComboBox dgvc = sender as ExtendControlLib.DataGridViewComboBox;

            MessageBox.Show(dgvc.Value);
        }

        private DataTable createStudentTable()
        {
            DataTable dt = new DataTable();
            DataColumn dc = new DataColumn("学号");
            dt.Columns.Add(dc);
            dc = new DataColumn("姓名");
            dt.Columns.Add(dc);
            dc = new DataColumn("性别");
            dt.Columns.Add(dc);
            dc = new DataColumn("专业");
            dt.Columns.Add(dc);
            dc = new DataColumn("入学年份");
            dt.Columns.Add(dc);

            DataRow dr = dt.NewRow();
            dr["学号"] = "20010101";
            dr["姓名"] = "刘德华";
            dr["性别"] = "男";
            dr["专业"] = "影视表演";
            dr["入学年份"] = "2001";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["学号"] = "20010702";
            dr["姓名"] = "张学友";
            dr["性别"] = "男";
            dr["专业"] = "计算机科学技术";
            dr["入学年份"] = "2001";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["学号"] = "20010403";
            dr["姓名"] = "郭富城";
            dr["性别"] = "男";
            dr["专业"] = "哲学系";
            dr["入学年份"] = "2001";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["学号"] = "20010204";
            dr["姓名"] = "柳岩";
            dr["性别"] = "女";
            dr["专业"] = "模特专业（裸模方向）";
            dr["入学年份"] = "2001";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["学号"] = "20010305";
            dr["姓名"] = "郭德纲";
            dr["性别"] = "男";
            dr["专业"] = "中文系";
            dr["入学年份"] = "2001";
            dt.Rows.Add(dr);

            return dt;
        }
    
        private DataTable createTreeViewTable()
        {
            return new DataTable();
        }
    
    }
}
