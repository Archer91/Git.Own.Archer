using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Collections;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Windows.Forms.Design;
using System.Drawing.Drawing2D;
using System.ComponentModel.Design.Serialization;

namespace Archer.ExtendControlLib
{
    [Designer(typeof(ControlDesigner))]
    public partial class TreeViewComboBox : System.Windows.Forms.ComboBox
    {
        // Fields
        private bool blnIsChange = false;
        private int m_ImageIndex = -1;
        private ImageList m_ImageList;
        private ListItemCollection m_items;
        private object objSource;
        private Color m_ItemHoverBackColor;
        private Color m_ItemHoverGradientBackColor = Color.Empty;
        private Color m_ItemHoverColor;
        private int m_Indent = 16;
        private bool m_CheckBoxes = false;
        private bool isUpdate = false;
        private ComboBoxWindow wnd = null;
        internal static int nextId = 0;

        #region Properties
        [Category("Archer.ECL"), Description("用于绑定的数据源")]
        public new object DataSource
        {
            get
            {
                return this.objSource;
            }
            set
            {
                this.objSource = value;
                base.DataSource = null;
            }
        }

        [Category("Archer.ECL"),
        Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design", typeof(UITypeEditor)),
        TypeConverter(typeof(ImageIndexConverter)),
        DefaultValue(-1)]
        public int ImageIndex
        {
            get
            {
                return this.m_ImageIndex;
            }
            set
            {
                this.m_ImageIndex = value;
            }
        }

        [Description("组合框中的项的图象所使用的ImageList控件"), 
        Browsable(true), 
        DefaultValue((string)null)]
        public ImageList ImageList
        {
            get
            {
                return this.m_ImageList;
            }
            set
            {
                this.m_ImageList = value;
            }
        }

        [Category("Archer.ECL"), 
        DesignerSerializationVisibility(DesignerSerializationVisibility.Content), 
        Editor(typeof(CollectionEditor), typeof(UITypeEditor)), 
        MergableProperty(false), 
        Description("组合框中的项"), 
        DefaultValue((string)null),
        Localizable(true)]
        public new ListItemCollection Items
        {
            get
            {
                if(m_items == null)
                    m_items = new ListItemCollection(this, this);
                return this.m_items;
            }
        }

        [Browsable(false)]
        public new ListItem SelectedItem
        {
            get
            {
                if (this.SelectedIndex == -1)
                {
                    return null;
                }
                return FindRealListItem(getItem(this.SelectedIndex));
            }
        }

        [Browsable(false)]
        public new string SelectedText
        {
            get
            {
                if (this.SelectedIndex == -1)
                {
                    return null;
                }
                return SelectedItem.Text;
            }
            set
            {
                if ((this.SelectedIndex != -1) && !this.blnIsChange)
                {
                    SelectedItem.Text = value;
                }
            }
        }

        [Browsable(false)]
        public new object SelectedValue
        {
            get
            {
                if (this.SelectedIndex == -1)
                {
                    return null;
                }
                return SelectedItem.Value;
            }
            set
            {
                if (this.SelectedIndex != -1)
                {
                    SelectedItem.Value = value;
                }
            }
        }

        [Browsable(false)]
        public CheckedListItemCollection CheckedItems
        {
            get{
                CheckedListItemCollection checkitems = new CheckedListItemCollection();
                if(m_CheckBoxes)
                    FindCheckedItem(this.m_items, ref checkitems);
                return checkitems;
            }
        }

        [Category("Archer.ECL"),
        Description("获取或设置鼠标移上列表项时的前景景颜色")]
        public Color ItemHoverColor
        {
            get{return m_ItemHoverColor;}
            set{m_ItemHoverColor = value;}
        }

        [Category("Archer.ECL"),
        Description("获取或设置鼠标移上列表项时的背景颜色")]
        public Color ItemHoverBackColor
        {
            get{return m_ItemHoverBackColor;}
            set{m_ItemHoverBackColor = value;}
        }

        [Category("Archer.ECL"),
        DefaultValue((string)null),
        Description("获取或设置鼠标移上列表项时的背景颜色")]
        public Color ItemHoverGradientBackColor
        {
            get{return m_ItemHoverGradientBackColor;}
            set{m_ItemHoverGradientBackColor = value;}
        }
        
        [DefaultValue(16),
        Description("子节点的缩进宽度")]
        public int Indent
        {
            get{return m_Indent;}
            set{m_Indent = value;}
        }

        [DefaultValue(false),
        Description("是否显示复选框")]
        public bool CheckBoxes
        {
            get{return m_CheckBoxes;}
            set{m_CheckBoxes = value;}
        }

        #endregion

        #region Methods
        public TreeViewComboBox()
        {
            m_ItemHoverBackColor = SystemColors.Highlight;
            m_ItemHoverColor = SystemColors.HighlightText;
            base.Sorted = false;
        }

        /// <summary>
        /// 绑定数据。
        /// </summary>
        public void DataBind()
        {
            if ((this.DataSource != null) && (base.DisplayMember != null))
            {
                switch (this.DataSource.GetType().Name)
                {
                    case "DataTable":
                    {
                        DataTable dtblTmp = (DataTable)this.DataSource;
                        foreach (DataRow drowTmp in dtblTmp.Rows)
                        {
                            string strText = drowTmp[base.DisplayMember].ToString();
                            string strValue = (base.ValueMember == null) ? null : drowTmp[base.ValueMember].ToString();
                            ListItem item = new ListItem(strText, strValue, this.ImageIndex);
                            this.m_items.Add(item);
                        }
                        break;
                    }
                }
            }
        }

        //锁定更新
        public new void BeginUpdate()
        {
            base.BeginUpdate();
            isUpdate = true;
        }

        //解除更新
        public new void EndUpdate()
        {
            InsertItem(m_items);
            base.EndUpdate();
            isUpdate = false;
        }

        //绘制项
        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            if(e.Index == -1)return;
            Rectangle trect = new Rectangle(0, 0, 0, 0), crect = new Rectangle(0, 0, 0, 0);
            ListItem item = getItem(e.Index);
            if(item == null)return;

            bool isEdit = (e.State & DrawItemState.ComboBoxEdit) == DrawItemState.ComboBoxEdit;

            if(m_CheckBoxes) //复选框的区域
            {
                crect = new Rectangle(m_Indent * item.level, e.Bounds.Top + ((e.Bounds.Height - 16) / 2), 16, 16);
                if(isEdit)
                    crect.Offset(2, 0);
            }
            else
            {
                crect.X = m_Indent * item.level + 2;
            }
            //if(isEdit)
                DrawBackground(e, e.Bounds, item);
            //else
            //    DrawBackground(e, new Rectangle(crect.Left - 1, e.Bounds.Top, e.Bounds.Width - crect.Left + 1, e.Bounds.Height), item);
            Rectangle irect = new Rectangle(crect.Right, 0, 0, 0);
            if(ImageList != null && item.ImageIndex != -1)//画图象
            {
                Image sIcon = ImageList.Images[item.ImageIndex];
                irect = new Rectangle(crect.Right, e.Bounds.Top + ((e.Bounds.Height - sIcon.Height) / 2), sIcon.Width, sIcon.Height);
                //图象太大
                if(irect.Height > e.Bounds.Height)
                {
                    irect = new Rectangle(crect.Right, e.Bounds.Top, e.Bounds.Height, e.Bounds.Height);
                }
                e.Graphics.DrawImage(sIcon, irect); 
            }
            trect = new Rectangle(irect.Right, e.Bounds.Top, e.Bounds.Width - irect.Width, e.Bounds.Height);
            if(m_CheckBoxes) //画复选框
            {
                if(item.checkboxLeft == -1)
                    item.checkboxLeft= crect.Left;
                ControlPaint.DrawCheckBox(e.Graphics, crect , item.Checked ? ButtonState.Checked | ButtonState.Flat : ButtonState.Flat);
            }
            DrawString(e, item, trect); //输出文本

            e.Graphics.Dispose();
        }

        //重设项大小
        protected override void OnMeasureItem(MeasureItemEventArgs e)
        {
            base.OnMeasureItem(e);
            if (this.ImageList != null)
            {
                e.ItemHeight = this.ImageList.ImageSize.Height;
            }
        }

        protected override void WndProc(ref Message m)
        {
            if(m.Msg == 0x134)//WM_CTLCOLORLISTBOX
            {
                if(m_CheckBoxes && wnd == null)
                {
                    wnd = new ComboBoxWindow(base.Items, Handle); //钩子
                    wnd.AssignHandle(m.LParam);
                }
            }
            else if(wnd != null && m.Msg == 0x2)//WM_DESTROY
            {
                wnd.ReleaseHandle();
            }
            else if(m.Msg == 0x400 + 0x105) //自定消息 设置checked
            {
                int index = m.WParam.ToInt32();
                ListItem item = FindRealListItem(getItem(index));
                if(item != null)
                {
                    item.Checked = !item.Checked;
                }
            }
            base.WndProc (ref m);
        }

        //画背景
        private void DrawBackground(DrawItemEventArgs e, Rectangle rect, ListItem item)
        {
            if((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                if(m_ItemHoverGradientBackColor != Color.Empty)
                {
                    //渐变
                    Rectangle r = new Rectangle(rect.Left, rect.Top, rect.Width, rect.Height / 2);
                    e.Graphics.FillRectangle(new LinearGradientBrush(r, m_ItemHoverBackColor, m_ItemHoverGradientBackColor, 90f), r);
                    r = new Rectangle(rect.Left, rect.Top + rect.Height / 2, rect.Width, rect.Height / 2);
                    if(r.Height % 2 == 0){r.Y -= 2; r.Height += 2;}
                    e.Graphics.FillRectangle(new LinearGradientBrush(r, m_ItemHoverBackColor, m_ItemHoverGradientBackColor, 270f), r);
                    ControlPaint.DrawBorder(e.Graphics, rect, m_ItemHoverGradientBackColor, ButtonBorderStyle.Solid);
                }
                else
                    e.Graphics.FillRectangle(new SolidBrush(m_ItemHoverBackColor), rect);
            }
            else
            {
                e.Graphics.FillRectangle(new SolidBrush(item.BackColor), rect);
            }
        }

        //画文本
        private void DrawString(DrawItemEventArgs e, ListItem item, Rectangle rect)
        {
            StringFormat sf = new StringFormat();
            sf.LineAlignment = StringAlignment.Center;
            rect.Offset(0, 1);
            
            SolidBrush sb = null;
            if(!item.ForeColor.Equals(SystemColors.WindowText) || m_ItemHoverGradientBackColor != Color.Empty)
            {
                sb = new SolidBrush(item.ForeColor);
            }
            else if((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                sb = new SolidBrush(m_ItemHoverColor);
            }
            else
            {
                sb = new  SolidBrush(SystemColors.WindowText);
            }

            e.Graphics.DrawString(item.Text, item.Font, sb, rect, sf);
            sb.Dispose();
        }

        //插入整个根集合
        private void InsertItem(ListItemCollection items)
        {
            foreach(ListItem item in items)
            {
                ListItem lit = item.CloneData();
                lit.Items = null;
                lit.level = item.level;
                if(item.Parent.GetType() == typeof(ListItem))
                {
                    ListItem parent = (ListItem)item.Parent;
                    lit.Parent = parent;
                }
                base.Items.Add(lit);
                InsertItem(item.Items);
            }
        }

        //插入新项
        internal int InsertItem(int index, ListItem item, int method)
        {
            if(isUpdate)return 0;
            ListItem lit = item.CloneData();
            lit.Items = null;
            lit.level = item.level;
            int count = 0;
            if(item.Parent.GetType() == typeof(ListItem))
            {
                ListItem parent = (ListItem)item.Parent;
                foreach(ListItem lt1 in parent.Items)
                {
                    GetItemLocation(lt1.Items, lit, ref count);
                }
                lit.Parent = parent;
                int i = 0;
                for(; i < base.Items.Count; i++)
                {
                    if(getItem(i).id == parent.id)
                    {
                        break;
                    }
                }
                base.Items.Insert(i + 1 + index + count, lit);
            }
            else
            {
                foreach(ListItem lt1 in ((ComboBox)item.Parent).Items)
                {
                    GetItemLocation(lt1.Items, lit, ref count);
                }
                base.Items.Insert(index + count, lit);
            }
            return base.Items.Count - 1;
        }

        private void GetItemLocation(ListItemCollection items, ListItem owner, ref int count)
        {
            foreach(ListItem lt in items)
            {
                if(owner.id != lt.id && CheckIsInItems(lt.id))
                {
                    count ++;
                    GetItemLocation(lt.Items, owner, ref count);
                }
            }
        }

        private bool CheckIsInItems(int id)
        {
            foreach(object o in base.Items)
            {
                if(((ListItem)o).id == id)
                {
                    return true;
                }
            }
            return false;
        }

        internal void RemoveItem(ListItem item)
        {
            if(isUpdate)return;
            int index = FindItem(item.id);
            if(index != -1)
                base.Items.RemoveAt(index);
        }

        internal void ClearItem()
        {
            base.Items.Clear();
        }

        private int FindItem(int id)
        {
            if(this.DesignMode)return -1;
            for(int i = 0; i < base.Items.Count; i++)
            {
                if(getItem(i).id == id)
                    return i;
            }
            return -1;
        }

        private void FindItem(int id, ListItem item, ref ListItem find)
        {
            foreach(object lt in item.Items)
            {
                ListItem lt1 = (ListItem)lt;
                if(lt1.id == id)
                {
                    find = lt1;
                    break;
                }
                FindItem(id, lt1, ref find);
            }
        }

        private void FindCheckedItem(ListItemCollection items, ref CheckedListItemCollection checkeditems)
        {
            foreach(object lt in items)
            {
                ListItem lt1 = (ListItem)lt;
                if(lt1.Checked)checkeditems.Add(lt1);
                FindCheckedItem(lt1.Items, ref checkeditems);
            }
        }

        private ListItem FindRealListItem(ListItem item)
        {
            if(item.Parent == null) //为根节点
            {
                foreach(ListItem lt in this.m_items)
                {
                    if(lt.id == item.id)
                        return lt;
                }
                return null;
            }
            else
            {
                ListItem temp = item;
                while(true) // 找到根级
                {
                    if(temp.Parent == null)break;
                    if(temp.Parent.GetType() == typeof(TreeViewComboBox))break;
                    temp = (temp.Parent as ListItem);
                }
                ListItem find = null;
                //找子项里
                FindItem(item.id, temp, ref find);
                return find;
            }
        }

        internal void setText(int id, string text)
        {
            int index = FindItem(id);
            if(index != -1)
            {
                getItem(index).Text = text;
            }
        }

        internal void setImageIndex(int id, int imageIndex)
        {
            int index = FindItem(id);
            if(index != -1)
                getItem(index).ImageIndex = imageIndex;
        }

        internal void setForeColor(int id, Color foreColor)
        {
            int index = FindItem(id);
            if(index != -1)
                getItem(index).ForeColor = foreColor;
        }

        internal void setBackColor(int id, Color backColor)
        {
            int index = FindItem(id);
            if(index != -1)
                getItem(index).BackColor = backColor;
        }

        internal void setFont(int id, Font font)
        {
            int index = FindItem(id);
            if(index != -1)
                getItem(index).Font = font;
        }

        internal void setChecked(int id, bool check)
        {
            int index = FindItem(id);
            if(index != -1)
                getItem(index).Checked = check;
        }

        internal void setItem(int id, ListItem item)
        {
            int index = FindItem(id);
            if(index != -1)
                base.Items[index] = item.CloneData();
        }

        private ListItem getItem(int index)
        {
            if(index < 0 || index > base.Items.Count - 1)
                throw new ArgumentNullException("索引值超出集合的范围");
            return (ListItem)base.Items[index];
        }

        #endregion

        #region ListItemCollection
        public class ListItemCollection : IList, ICollection, IEnumerable
        {
            private TreeViewComboBox owner;
            private object parent;
            private ArrayList items;
            private bool inserted = true;
            internal byte level = 0;

            // Methods
            internal ListItemCollection(TreeViewComboBox comboBox, object parent)
            {
                items = new ArrayList();
                this.owner = comboBox;
                this.parent = parent;
            }

            public ListItemCollection()
            {
                items = new ArrayList();
            }

            internal ListItemCollection(bool inserted)
            {
                items = new ArrayList();
                this.inserted = inserted;
            }

            public ListItem this[int index]
            {
                get{
                    if(index < 0 || index > items.Count - 1)
                        throw new ArgumentNullException("索引值超出集合的范围");
                    return items[index] as ListItem;
                }
                set
                {
                    if(index < 0 || index > items.Count - 1)
                        throw new ArgumentNullException("索引值超出集合的范围");
                    ListItem item = (ListItem)items[index];
                    value.id = item.id;
                    value.level = item.level;
                    items[index] = value;
                    if(owner != null)owner.setItem(value.id, value);
                }
            }

            public int Count
            {
                get{return items.Count;}
            }

            public int Add(string Text)
            {
                return Insert(-1, new ListItem(Text));
            }

            public int Add(string Text, object Value)
            {
                return Insert(-1, new ListItem(Text, Value));
            }

            public int Add(string Text, int ImageIndex)
            {
                return Insert(-1, new ListItem(Text, ImageIndex));
            }

            public int Add(string Text, object Value, int ImageIndex)
            {
                return Insert(-1, new ListItem(Text, Value, ImageIndex));
            }

            public int Add(ListItem item)
            {
                return Insert(-1, item);
            }

            public void AddRange(ListItem[] items)
            {
                for(IEnumerator e = items.GetEnumerator(); e.MoveNext();)
                    Insert(-1, (ListItem)e.Current);
            }

            public int Insert(int index, ListItem item)
            {
                int ret = 0;
                if(inserted)
                    item.level = level;
                int m = 1;
                if(index == -1)
                {
                    index = this.Count;
                    m = 0; //为添加
                }
                item.Host(owner);
                item.Parent = parent;
                item.Text = (item.Text.Length == 0) ? item.GetType().Name : item.Text;
                items.Insert(index, item);
                if(inserted)
                {
                    item.id = TreeViewComboBox.nextId ++;
                    if(owner != null)
                    {
                        ret = item.Index = owner.InsertItem(index, item, m);
                        AddItem(item, item.Items);
                    }
                }
                return ret;
            }

            internal void Host(TreeViewComboBox comboBox, object parent)
            {
                this.owner = comboBox;
                this.parent = parent;
            }

            private void AddItem(ListItem parent, ListItemCollection items)
            {
                int i = 0;
                foreach (ListItem item in items)
                {
                    item.Host(owner);
                    item.Parent = parent;
                    item.level = items.level;
                    item.Index = owner.InsertItem(i, item, 0);
                    i++;
                }
            }

            public void Remove(ListItem item)
            {
                if(owner != null)owner.RemoveItem(item);
                items.Remove(item);
            }

            public void RemoveAt(int index)
            {
                if(owner != null)owner.RemoveItem(this[index]);
                items.RemoveAt(index);
            }

            public int IndexOf(ListItem item)
            {
                return items.IndexOf(item);
            }

            public bool Contains(ListItem item)
            {
                return items.Contains(item);
            }

            public void Clear()
            {
                items.Clear();
                owner.ClearItem();
            }

            public IEnumerator GetEnumerator() 
            {
                return items.GetEnumerator();
            }

            #region ICollection Members
            void ICollection.CopyTo(Array array, int index) 
            {
                for (IEnumerator e = this.GetEnumerator(); e.MoveNext();)
                    array.SetValue(e.Current, index++);
            }
        
            bool ICollection.IsSynchronized 
            {
                get { return false; }
            }

            object ICollection.SyncRoot 
            {
                get { return this; }
            }
            #endregion

            #region IList Members
            object IList.this[int index] 
            {
                get { return this[index]; }
                set { this[index] = (ListItem)value; }
            }

            bool IList.Contains(object item)
            {
                return Contains((ListItem)item);
            }

            int IList.Add(object item)
            {
                return Add((ListItem)item);
            }

            bool IList.IsFixedSize 
            {
                get {return false;}
            }

            int IList.IndexOf(object item)
            {
                return IndexOf((ListItem)item);
            }

            void IList.Insert(int index, object item)
            {
                Insert(index, (ListItem)item);
            }

            void IList.Remove(object item)
            {
                Remove((ListItem)item);
            }

            void IList.RemoveAt(int index)
            {
                RemoveAt(index);
            }

            void IList.Clear()
            {
                Clear();
            }

            bool IList.IsReadOnly
            {
                get{return false;}
            }
            #endregion
        }

        #endregion

        #region CheckedListItemCollection
        public class CheckedListItemCollection : CollectionBase
        {
            public void Add(ListItem item)
            {
                List.Add(item);
            }

            public void Remove(ListItem item)
            {    
                List.Remove(item);
            }

            public ListItem this[int index]
            {
                get{
                    if(index < 0 || index > List.Count - 1)
                        throw new ArgumentNullException("索引值超出集合的范围");
                    return List[index] as ListItem;
                }
            }
        }
        #endregion
        
        #region ComboBoxWindow
        internal class ComboBoxWindow : NativeWindow
        {
            private System.Windows.Forms.ComboBox.ObjectCollection items;
            private IntPtr handle = IntPtr.Zero;

            internal ComboBoxWindow(System.Windows.Forms.ComboBox.ObjectCollection items, IntPtr handle)
            {
                this.items = items;
                this.handle = handle;
            }

            protected override void WndProc(ref Message m)
            {
                if(m.Msg == 0x201)//WM_LBUTTONDOWN
                {
                    Win32.RECT rect = new Win32.RECT();
                    Win32.GetClientRect(m.HWnd, ref rect);
                    Win32.POINTAPI pt = new Win32.POINTAPI();
                    pt.x = Win32.LOWORD(m.LParam.ToInt32());
                    pt.y = Win32.HIWORD(m.LParam.ToInt32());
                    //如果在区域内
                    if(new Rectangle(rect.Left, rect.Top,rect.Left + rect.Right, rect.Top + rect.Bottom).Contains(new Point(pt.x, pt.y)))
                    {
                        int nItemHeight = Win32.SendMessage(m.HWnd, 0x1A1, 0, 0);//LB_GETITEMHEIGHT
                        //获得顶部项的索引
                        int nTopIndex = Win32.SendMessage(m.HWnd, 0x18E, 0, 0); //LB_GETTOPINDEX
                        int nIndex = nTopIndex + pt.y / nItemHeight;
                        if (items.Count == 0)
                        {
                            base.WndProc (ref m);
                            return;
                        }
                        //判断是否在复选框处
                        if(pt.x > ((ListItem)items[nIndex]).checkboxLeft && pt.x < ((ListItem)items[nIndex]).checkboxLeft + 16)
                        {
                            Win32.RECT re = new Win32.RECT();
                            //取位置
                            Win32.SendMessage(m.HWnd, 0x198, nIndex, ref re); //LB_GETITEMRECT
                            //重画
                            Win32.InvalidateRect(m.HWnd, re, 0);
                            //发送自定消息勾选复选框
                            Win32.SendMessage(handle, 0x400 + 0x105, nIndex, 0);
                            return;
                        }
                    }
                }
                base.WndProc (ref m);
            }
        }
        #endregion

    }

    [Serializable,
    DesignTimeVisible(false),
    DefaultProperty("Text"),
    TypeConverter(typeof(ListItemConverter))]
    public class ListItem : Component
    {
        // Fields
        private int m_ImageIndex = -1;
        private int m_Index;
        private object m_Tag;
        private string m_Text = string.Empty;
        private object m_Value;
        private bool m_Checked = false;
        private Font m_Font;
        private TreeViewComboBox comboBox;
        private TreeViewComboBox.ListItemCollection m_items = new TreeViewComboBox.ListItemCollection();
        private Color m_ForeColor, m_BackColor;
        private object parent;
        internal int id = -1;
        internal int checkboxLeft = -1;
        internal byte level = 0;

        // Methods
        public ListItem()
        {
            this.m_ImageIndex = -1;
        }

        public ListItem(string Text)
            : this(Text, Text, -1)
        {
        }

        public ListItem(string Text, int ImageIndex)
            : this(Text, Text, ImageIndex)
        {
        }

        public ListItem(string Text, object Value)
            : this(Text, Value, -1)
        {
        }

        public ListItem(string Text, object Value, int ImageIndex)
        {
            this.m_Text = Text;
            this.m_Value = Value;
            this.m_ImageIndex = ImageIndex;
        }

        internal ListItem CloneData()
        {
            ListItem lt = new ListItem();
            lt.Text = this.Text;
            lt.ImageIndex = this.ImageIndex;
            lt.Font = this.Font;
            lt.ForeColor = this.ForeColor;
            lt.BackColor = this.BackColor;
            lt.Checked = this.Checked;
            lt.id = id;
            return lt;
        }

        public override string ToString()
        {
            return this.Text;
        }

        internal void Host(TreeViewComboBox comboBox)
        {
            this.comboBox = comboBox;
            m_items.Host(comboBox, this);
            m_items.level = (byte)(level + 1);
        }

        #region Properties
        [Category("Archer.ECL"),
        Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design", typeof(UITypeEditor)),
        TypeConverter(typeof(ImageIndexConverter)),
        DefaultValue(-1)]
        public int ImageIndex
        {
            get
            {
                return this.m_ImageIndex;
            }
            set
            {
                this.m_ImageIndex = value;
                if (comboBox != null && id != -1)
                    comboBox.setImageIndex(id, value);
            }
        }

        [Browsable(false),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int Index
        {
            get
            {
                return this.m_Index;
            }
            set
            {
                this.m_Index = value;
            }
        }

        [Browsable(true),
        TypeConverter(typeof(StringConverter)),
        DefaultValue((string)null),
        Description("获取或设置包含数据的对象以与该项关联")]
        public object Tag
        {
            get
            {
                return this.m_Tag;
            }
            set
            {
                this.m_Tag = value;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible),
        Localizable(true),
        Description("获取或设置该项显示的文本")]
        public string Text
        {
            get
            {
                return this.m_Text;
            }
            set
            {
                this.m_Text = value;
                if (comboBox != null && id != -1)
                    comboBox.setText(id, value);
            }
        }

        [Description("获取或设置该项显示的值"),
        TypeConverter(typeof(StringConverter)),
        DefaultValue((string)null),
        Browsable(true)]
        public object Value
        {
            get
            {
                return this.m_Value;
            }
            set
            {
                this.m_Value = value;
            }
        }

        [Browsable(false)]
        public ImageList ImageList
        {
            get
            {
                if (this.comboBox != null)
                {
                    return this.comboBox.ImageList;
                }
                return null;
            }
        }

        [Category("Archer.ECL")]
        public Font Font
        {
            get
            {
                if (m_Font == null)
                {
                    if (comboBox != null)
                    {
                        return comboBox.Font;
                    }
                    return Control.DefaultFont;
                }
                return m_Font;
            }
            set { m_Font = value; }
        }

        [Category("Archer.ECL")]
        public Color ForeColor
        {
            get
            {
                if (m_ForeColor == Color.Empty)
                {
                    if (comboBox != null)
                    {
                        return comboBox.ForeColor;
                    }
                    return Control.DefaultForeColor;
                }
                return m_ForeColor;
            }
            set
            {
                m_ForeColor = value;
                if (comboBox != null && id != -1)
                    comboBox.setForeColor(id, value);
            }
        }

        [Category("Archer.ECL")]
        public Color BackColor
        {
            get
            {
                if (m_BackColor == Color.Empty)
                {
                    if (comboBox != null)
                    {
                        return comboBox.BackColor;
                    }
                    return Control.DefaultBackColor;
                }
                return m_BackColor;
            }
            set
            {
                m_BackColor = value;
                if (comboBox != null && id != -1)
                    comboBox.setBackColor(id, value);
            }
        }

        [Category("Archer.ECL"),
        DefaultValue(false)]
        public bool Checked
        {
            get { return m_Checked; }
            set
            {
                m_Checked = value;
                if (comboBox != null && id != -1)
                    comboBox.setChecked(id, value);
            }
        }

        [Category("Archer.ECL"),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
        Editor(typeof(CollectionEditor), typeof(UITypeEditor)),
        MergableProperty(false),
        Description("组合框中的项"),
        DefaultValue((string)null),
        Browsable(false),
        Localizable(true)]
        public TreeViewComboBox.ListItemCollection Items
        {
            get
            {
                return this.m_items;
            }
            set
            {
                m_items = value;
            }
        }

        internal object Parent
        {
            get { return parent; }
            set { parent = value; }
        }

        #endregion Properties
    }

    internal sealed class ListItemConverter : ExpandableObjectConverter
    {
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(System.ComponentModel.Design.Serialization.InstanceDescriptor))
            {
                return true;
            }
            else
            {
                return base.CanConvertTo(context, destinationType);
            }
        }

        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(System.ComponentModel.Design.Serialization.InstanceDescriptor))
            {
                Type valueType = value.GetType();
                ConstructorInfo ci = valueType.GetConstructor(System.Type.EmptyTypes);
                ListItem item = (ListItem)value;
                return new InstanceDescriptor(ci, null, false);
            }
            else
            {
                return base.ConvertTo(context, culture, value, destinationType);
            }
        }
    }

    internal class Win32
    {
        [DllImport("user32", EntryPoint = "GetClientRect")]
        public static extern int GetClientRect(
            IntPtr hwnd,
            ref RECT lpRect
            );

        [DllImport("user32", EntryPoint = "GetWindowRect")]
        public static extern int GetWindowRect(
            IntPtr hwnd,
            ref RECT lpRect
            );

        [DllImport("user32", EntryPoint = "PtInRect")]
        public static extern int PtInRect(
            ref RECT lpRect,
            ref POINTAPI pt
            );

        [DllImport("user32", EntryPoint = "SendMessage")]
        public static extern int SendMessage(
            IntPtr hwnd,
            int wMsg,
            int wParam,
            int lParam
            );

        [DllImport("user32", EntryPoint = "SendMessage")]
        public static extern int SendMessage(
            IntPtr hwnd,
            int wMsg,
            int wParam,
            ref RECT rect
            );

        [DllImport("user32", EntryPoint = "InvalidateRect")]
        public static extern int InvalidateRect(
            IntPtr hwnd,
            RECT lpRect,
            int bErase
            );

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct POINTAPI
        {
            public int x;
            public int y;
        }

        public static int HIWORD(int lparam)
        {
            return ((lparam >> 16) & 0xffff);
        }

        public static int LOWORD(int lparam)
        {
            return (lparam & 0xffff);
        }

        public static int MakeLParam(int LoWord, int HiWord)
        {
            return (HiWord << 16) | (LoWord & 0xffff);
        }

    }
}
