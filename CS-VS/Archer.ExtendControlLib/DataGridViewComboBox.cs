using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Archer.ExtendControlLib
{
    public partial class DataGridViewComboBox : ComboBox
    {
        #region 成员变量

        private const int WM_LBUTTONDOWN = 0x201, WM_LBUTTONDBLCLK = 0x203;
        private ToolStripControlHost dataGridViewHost;
        private ToolStripControlHost textBoxHost;
        private ToolStripDropDown dropDown;

        private string m_sKeyWords = "";
        private string m_sDisplayMember = "";
        private string m_sValueMember = "";
        private string m_sDisplayField = "";
        private string m_Separator = "|";
        private string m_NullValue = "";

        private bool m_blDropShow = false;
        private bool m_blPopupAutoSize = false;
        private int m_SelectedIndex = -1;

        public event AfterSelectorEventHandler AfterSelector;

        #endregion 成员变量

        #region 属性

        [Description("空值时的默认值"), Browsable(true), Category("Archer.ECL")]
        public string NullValue
        {
            set { m_NullValue = value; }
            get { return m_NullValue; }
        }
        [Description("查询关键字"), Browsable(true), Category("Archer.ECL")]
        public string sKeyWords
        {
            set { m_sKeyWords = value; }
            get { return m_sKeyWords; }
        }
        [Description("文本框显示字段用逗号分割"), Browsable(true), Category("Archer.ECL")]
        public string sDisplayMember
        {
            set { m_sDisplayMember = value; }
            get { return m_sDisplayMember; }
        }
        [Description("取值字段"), Browsable(true), Category("Archer.ECL")]
        public string sValueMember
        {
            set { m_sValueMember = value; }
            get { return m_sValueMember; }
        }
        [Description("下拉表格显示列，空为显示所有列"), Browsable(true), Category("Archer.ECL")]
        public string sDisplayField
        {
            set { m_sDisplayField = value; }
            get { return m_sDisplayField; }
        }
        [Description("分割符号"), Browsable(true), Category("Archer.ECL")]
        public string SeparatorChar
        {
            set { m_Separator = value; }
            get { return m_Separator; }
        }
        [Description("下拉表格尺寸是否为自动"), Browsable(true), Category("Archer.ECL")]
        public bool PopupGridAutoSize
        {
            set { m_blPopupAutoSize = value; }
            get { return m_blPopupAutoSize; }
        }
        [Description("是否显示条件输入窗口"), Browsable(true), Category("Archer.ECL")]
        public bool RowFilterVisible
        {
            set { dropDown.Items[0].Visible = value; }
            get { return dropDown.Items[0].Visible; }
        }
        public DataView DataView
        {
            get
            {
                DataTable dt = GetDataTableFromDataSource();
                if (dt == null)
                {
                    return null;
                }
                return dt.DefaultView;
            }
        }
        [Description("设置DataGridView属性"), Browsable(true), Category("Archer.ECL")]
        public DataGridView DataGridView
        {
            get
            {
                return dataGridViewHost.Control as DataGridView;
            }
        }
        public TextBox TextBox
        {
            get
            {
                return textBoxHost.Control as TextBox;
            }
        }
        [Description("数据源"), Browsable(true), Category("Archer.ECL")]
        public new Object DataSource
        {   
            set
            {
                if (m_sDisplayField != string.Empty)
                {
                    DataGridView.Columns.Clear();
                    DataGridView.AutoGenerateColumns = false;
                    string[] displayFields = m_sDisplayField.Split(',');
                    foreach (string item in displayFields)
                    {
                        DataGridViewTextBoxColumn dgvCell = new DataGridViewTextBoxColumn();
                        dgvCell.Name = item;
                        dgvCell.DataPropertyName = item;
                        DataGridView.Columns.Add(dgvCell);
                    }
                }
                DataGridView.DataSource = value;
            }
            get { return DataGridView.DataSource; }
        }
        [Browsable(false), Bindable(true), Category("Archer.ECL")]
        public string Value
        {
            set
            {
                int i = 0;
                if (m_sValueMember == string.Empty)
                {
                    Text = value;
                }
                else
                {
                    Text = "";
                    if (DataView != null)
                    {
                        DataView.RowFilter = "";
                        foreach (DataRowView item in DataView)
                        {
                            if (item[m_sValueMember].ToString() == value)
                            {
                                m_SelectedIndex = i;
                                string[] displayList = m_sDisplayMember.Split(',');
                                foreach (string em in displayList)
                                {
                                    if (DataGridView.Columns.Contains(em))
                                    {
                                        object obj = DataView[m_SelectedIndex][em];
                                        Text += obj.ToString() + m_Separator;
                                    }
                                }
                                Text = Text.TrimEnd('|');
                                break;
                            }
                            i++;
                        }
                    }
                }
            }
            get
            {
                if (Text == string.Empty)
                {
                    m_SelectedIndex = -1;
                }
                if (!String.IsNullOrEmpty(m_sValueMember))
                {
                    if (DataView == null)
                    {
                        return Text;
                    }
                    if (m_SelectedIndex > -1)
                    {
                        object obj = DataView[m_SelectedIndex][m_sValueMember];
                        return obj.ToString();
                    }
                    else
                    {
                        return m_NullValue;
                    }
                }
                else
                {
                    return Text;
                }
            }
        }
        
        #endregion 属性
        public DataGridViewComboBox()
        {
            //InitializeComponent();
            DrawDataGridView();
        }
        //绘制DataGridView以及下拉DataGridView
        private void DrawDataGridView()
        {
            DataGridView dataGridView = new DataGridView();
            dataGridView.ScrollBars = ScrollBars.Both;
            dataGridView.AutoSize = true;
            dataGridView.AllowUserToAddRows = false;
            dataGridView.AllowUserToDeleteRows = false;
            dataGridView.AllowUserToResizeRows = false;
            dataGridView.Dock = DockStyle.Fill;

            dataGridView.BackgroundColor = SystemColors.Control;
            dataGridView.BorderStyle = BorderStyle.None;
            dataGridView.ReadOnly = true;
            dataGridView.AllowUserToAddRows = false;
            dataGridView.RowHeadersVisible = false;
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView.DoubleClick += dataGridView_DoubleClick;
            dataGridView.KeyDown += dataGridView_KeyDown;

            //设置DataGridView的数据源
            Form frmDataSource = new Form();
            frmDataSource.Controls.Add(dataGridView);
            frmDataSource.SuspendLayout();
            dataGridViewHost = new ToolStripControlHost(dataGridView);
            dataGridViewHost.AutoSize = m_blPopupAutoSize;

            TextBox textBox = new TextBox();
            textBox.Width = dataGridView.Width;
            textBox.TextChanged += textBox_TextChanged;
            textBox.KeyDown += textBox_KeyDown;
            textBoxHost = new ToolStripControlHost(textBox);
            textBoxHost.AutoSize = false;

            dropDown = new ToolStripDropDown();
            dropDown.Items.Add(textBoxHost);
            dropDown.Items.Add(dataGridViewHost);
        }

        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyData == Keys.Enter)
            {
                PopupGridView(e);
            }
            else if(e.KeyData == Keys.Down || e.KeyData==Keys.Up)
            {
                DataGridView.Focus();
            }
        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            DataView.RowFilter = GetRowFilterString(TextBox.Text);
        }

        private void dataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyData == Keys.Enter)
            {
                PopupGridView(e);
            }
        }

        public void dataGridView_DoubleClick(object sender, EventArgs e)
        {
            PopupGridView(e);
        }
        private void PopupGridView(EventArgs e)
        {
            if (DataGridView.SelectedRows.Count > 0)
            {
                DataGridViewRow dgvRow = DataGridView.CurrentRow;
                m_SelectedIndex = DataGridView.CurrentRow.Index;
                if (m_sDisplayMember != string.Empty)
                {
                    Text = "";
                    string[] displayList = m_sDisplayMember.Split(',');
                    foreach (string item in displayList)
                    {
                        if (DataGridView.Columns.Contains(item))
                        {
                            Text += dgvRow.Cells[item].Value.ToString() + m_Separator;
                        }
                    }
                    Text = Text.TrimEnd('|');
                }
                else
                {
                    Text = dgvRow.Cells[0].Value.ToString();
                }
                RaiseAfterSelector(this, new AfterSelectorEventArgs(-1, -1, dgvRow));
            }
            dropDown.Close();
            m_blDropShow = false;
        }
        private DataTable GetDataTableFromDataSource()
        {
            object dataSource = DataGridView.DataSource;
            return GetDataTableFromDataSource(dataSource);
        }
        //从DataGridView获取数据表
        private DataTable GetDataTableFromDataSource(object pDataSource)
        {
            if(pDataSource is DataTable)
            {
                return (DataTable)pDataSource;
            }
            else if(pDataSource is DataView)
            {
                return ((DataView)pDataSource).Table;
            }
            else if(pDataSource is BindingSource)
            {
                object bind = ((BindingSource)pDataSource).DataSource;
                if(bind is DataTable)
                {
                    return (DataTable)bind;
                }
                else
                {
                    return ((DataView)bind).Table;
                }
            }
            else
            {
                return null;
            }
        }
        private void ShowDropDown()
        {
            if(dropDown != null)
            {
                if(DataView != null)
                {
                    DataView.RowFilter = "";
                    TextBox.Text = "";
                    dropDown.Show(this, 0, this.Height);

                    TextBox.Focus();
                }
            }
        }
        private string GetRowFilterString(string pText)
        {
            string sFilter = "";
            if(null == m_sDisplayMember || m_sDisplayMember==string.Empty)
            {
                m_sDisplayMember = DataView.Table.Columns[0].ColumnName;
            }
            if(m_sKeyWords == string.Empty)
            {
                foreach (DataColumn cl in DataView.Table.Columns)
                {
                    m_sKeyWords += cl.ColumnName + ",";
                }
                m_sKeyWords = m_sKeyWords.Trim().TrimEnd(",".ToCharArray());
            }
            string[] sColumns = m_sKeyWords.Split(',');
            foreach (string item in sColumns)
            {
                sFilter += "Convert(" + item + ",'System.String') like " + "'%" + pText + "%'" + " or ";
            }
            sFilter = sFilter.Trim().TrimEnd("or".ToCharArray());
            return sFilter;
        }

        protected virtual void RaiseAfterSelector(object sender,AfterSelectorEventArgs e)
        {
            OnAfterSelector(sender, e);
        }
        protected void OnAfterSelector(object sender,AfterSelectorEventArgs e)
        {
            if(AfterSelector != null)
            {
                AfterSelector(sender, e);
            }
        }
        public string GetDataProperty(string pColumn)
        {
            string value = "";
            if(DataView != null)
            {
                if(DataGridView.Columns.Contains(pColumn))
                {
                    value = DataView[m_SelectedIndex][pColumn].ToString();
                }
            }
            return value;
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            ShowDropDown();
            if(e.KeyData == Keys.Enter)
            {
                DataView.RowFilter = GetRowFilterString(Text);
                PopupGridView(null);
            }
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            if(char.IsDigit(e.KeyChar) || char.IsLetter(e.KeyChar))
            {
                this.TextBox.Text = e.KeyChar.ToString();
                this.TextBox.SelectionStart = this.TextBox.Text.Length;
                e.Handled = true;
            }
            base.OnKeyPress(e);
        }

        protected override void WndProc(ref Message m)
        {
            if(m.Msg==WM_LBUTTONDBLCLK || m.Msg==WM_LBUTTONDOWN)
            {
                if(m_blDropShow)
                {
                    m_blDropShow = false;
                }
                else
                {
                    m_blDropShow = true;
                }
                if(m_blDropShow)
                {
                    ShowDropDown();
                }
                else
                {
                    dropDown.Close();
                }
                return;
            }
            base.WndProc(ref m);
        }

        protected override void Dispose(bool pDisposing)
        {
            if(pDisposing)
            {
                if(dropDown != null)
                {
                    dropDown.Dispose();
                    dropDown = null;
                }
            }
            base.Dispose(pDisposing);
        }
    }

    public delegate void AfterSelectorEventHandler(object sender,AfterSelectorEventArgs e);
    public class AfterSelectorEventArgs:EventArgs
    {
        public int RowIndex
        {
            get;
            set;
        }

        public int ColumnIndex
        {
            get;
            set;
        }

        public object Value
        {
            get;
            set;
        }

        public AfterSelectorEventArgs(int pRowIndex,int pColumnIndex,object pValue)
            :base()
        {
            RowIndex = pRowIndex;
            ColumnIndex = pColumnIndex;
            Value = pValue;
        }
    }

}
