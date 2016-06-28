namespace WMS.Client.basic
{
    partial class Fm_SerialNumber
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Fm_SerialNumber));
            this.tsmiEnable = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.cmbType = new System.Windows.Forms.ComboBox();
            this.nudSeqStep = new System.Windows.Forms.NumericUpDown();
            this.txtSeqLength = new System.Windows.Forms.TextBox();
            this.tcbSeqLength = new System.Windows.Forms.TrackBar();
            this.cmbYMD = new System.Windows.Forms.ComboBox();
            this.cmbCompany = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtSuffix = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.rtbDesc = new System.Windows.Forms.RichTextBox();
            this.txtPrefix = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvSerialNumber = new System.Windows.Forms.DataGridView();
            this.CompanyDesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Prefix = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.YYMMDD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SeqLength = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SeqStep = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Suffix = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StatusDesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CreatedBy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CreateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CompanyID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiDisable = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbAdd = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbEdit = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbRefresh = new System.Windows.Forms.ToolStripButton();
            this.tsbQuery = new System.Windows.Forms.ToolStripButton();
            this.tstbQuery = new System.Windows.Forms.ToolStripTextBox();
            this.tsslMsg = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSeqStep)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tcbSeqLength)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSerialNumber)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tsmiEnable
            // 
            this.tsmiEnable.Name = "tsmiEnable";
            this.tsmiEnable.Size = new System.Drawing.Size(100, 22);
            this.tsmiEnable.Text = "启用";
            this.tsmiEnable.Click += new System.EventHandler(this.tsmiEnable_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(97, 6);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 34);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.cmbType);
            this.splitContainer1.Panel1.Controls.Add(this.nudSeqStep);
            this.splitContainer1.Panel1.Controls.Add(this.txtSeqLength);
            this.splitContainer1.Panel1.Controls.Add(this.tcbSeqLength);
            this.splitContainer1.Panel1.Controls.Add(this.cmbYMD);
            this.splitContainer1.Panel1.Controls.Add(this.cmbCompany);
            this.splitContainer1.Panel1.Controls.Add(this.label13);
            this.splitContainer1.Panel1.Controls.Add(this.txtSuffix);
            this.splitContainer1.Panel1.Controls.Add(this.label9);
            this.splitContainer1.Panel1.Controls.Add(this.label7);
            this.splitContainer1.Panel1.Controls.Add(this.label6);
            this.splitContainer1.Panel1.Controls.Add(this.btnSave);
            this.splitContainer1.Panel1.Controls.Add(this.rtbDesc);
            this.splitContainer1.Panel1.Controls.Add(this.txtPrefix);
            this.splitContainer1.Panel1.Controls.Add(this.label10);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgvSerialNumber);
            this.splitContainer1.Size = new System.Drawing.Size(884, 506);
            this.splitContainer1.SplitterDistance = 132;
            this.splitContainer1.TabIndex = 15;
            this.splitContainer1.TabStop = false;
            // 
            // cmbType
            // 
            this.cmbType.FormattingEnabled = true;
            this.cmbType.Items.AddRange(new object[] {
            "ReceivingNO",
            "PackingNO"});
            this.cmbType.Location = new System.Drawing.Point(85, 8);
            this.cmbType.Name = "cmbType";
            this.cmbType.Size = new System.Drawing.Size(163, 20);
            this.cmbType.TabIndex = 0;
            // 
            // nudSeqStep
            // 
            this.nudSeqStep.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.nudSeqStep.Location = new System.Drawing.Point(388, 62);
            this.nudSeqStep.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.nudSeqStep.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudSeqStep.Name = "nudSeqStep";
            this.nudSeqStep.Size = new System.Drawing.Size(60, 21);
            this.nudSeqStep.TabIndex = 6;
            this.nudSeqStep.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // txtSeqLength
            // 
            this.txtSeqLength.Location = new System.Drawing.Point(275, 61);
            this.txtSeqLength.Name = "txtSeqLength";
            this.txtSeqLength.ReadOnly = true;
            this.txtSeqLength.Size = new System.Drawing.Size(24, 21);
            this.txtSeqLength.TabIndex = 39;
            this.txtSeqLength.TabStop = false;
            // 
            // tcbSeqLength
            // 
            this.tcbSeqLength.AutoSize = false;
            this.tcbSeqLength.Location = new System.Drawing.Point(85, 58);
            this.tcbSeqLength.Maximum = 20;
            this.tcbSeqLength.Minimum = 1;
            this.tcbSeqLength.Name = "tcbSeqLength";
            this.tcbSeqLength.Size = new System.Drawing.Size(190, 29);
            this.tcbSeqLength.TabIndex = 5;
            this.tcbSeqLength.Value = 1;
            this.tcbSeqLength.Scroll += new System.EventHandler(this.tcbSeqLength_Scroll);
            this.tcbSeqLength.ValueChanged += new System.EventHandler(this.tcbSeqLength_ValueChanged);
            // 
            // cmbYMD
            // 
            this.cmbYMD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbYMD.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbYMD.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmbYMD.FormattingEnabled = true;
            this.cmbYMD.Items.AddRange(new object[] {
            "",
            "YYMMDD",
            "YYMM",
            "YY"});
            this.cmbYMD.Location = new System.Drawing.Point(210, 35);
            this.cmbYMD.Name = "cmbYMD";
            this.cmbYMD.Size = new System.Drawing.Size(125, 20);
            this.cmbYMD.TabIndex = 3;
            // 
            // cmbCompany
            // 
            this.cmbCompany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCompany.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmbCompany.FormattingEnabled = true;
            this.cmbCompany.Location = new System.Drawing.Point(323, 8);
            this.cmbCompany.Name = "cmbCompany";
            this.cmbCompany.Size = new System.Drawing.Size(125, 20);
            this.cmbCompany.TabIndex = 1;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(308, 64);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(77, 12);
            this.label13.TabIndex = 35;
            this.label13.Text = "流水号步长：";
            // 
            // txtSuffix
            // 
            this.txtSuffix.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txtSuffix.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtSuffix.Location = new System.Drawing.Point(388, 35);
            this.txtSuffix.Name = "txtSuffix";
            this.txtSuffix.Size = new System.Drawing.Size(60, 21);
            this.txtSuffix.TabIndex = 4;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(344, 38);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(41, 12);
            this.label9.TabIndex = 31;
            this.label9.Text = "后缀：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(5, 64);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 12);
            this.label7.TabIndex = 30;
            this.label7.Text = "流水号长度：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(153, 38);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 29;
            this.label6.Text = "年月日：";
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSave.Location = new System.Drawing.Point(465, 62);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(125, 65);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "保存(&S)";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // rtbDesc
            // 
            this.rtbDesc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.rtbDesc.Location = new System.Drawing.Point(85, 89);
            this.rtbDesc.Name = "rtbDesc";
            this.rtbDesc.Size = new System.Drawing.Size(363, 38);
            this.rtbDesc.TabIndex = 7;
            this.rtbDesc.Text = "";
            // 
            // txtPrefix
            // 
            this.txtPrefix.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txtPrefix.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPrefix.Location = new System.Drawing.Point(85, 35);
            this.txtPrefix.Name = "txtPrefix";
            this.txtPrefix.Size = new System.Drawing.Size(60, 21);
            this.txtPrefix.TabIndex = 2;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(41, 92);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 12);
            this.label10.TabIndex = 9;
            this.label10.Text = "描述：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(41, 38);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "前缀：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(254, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "所属公司：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "类型名：";
            // 
            // dgvSerialNumber
            // 
            this.dgvSerialNumber.AllowUserToAddRows = false;
            this.dgvSerialNumber.AllowUserToDeleteRows = false;
            this.dgvSerialNumber.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvSerialNumber.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSerialNumber.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CompanyDesc,
            this.Type,
            this.Prefix,
            this.YYMMDD,
            this.SeqLength,
            this.SeqStep,
            this.Suffix,
            this.Description,
            this.StatusDesc,
            this.CreatedBy,
            this.CreateTime,
            this.ID,
            this.Status,
            this.CompanyID});
            this.dgvSerialNumber.ContextMenuStrip = this.contextMenuStrip1;
            this.dgvSerialNumber.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSerialNumber.Location = new System.Drawing.Point(0, 0);
            this.dgvSerialNumber.MultiSelect = false;
            this.dgvSerialNumber.Name = "dgvSerialNumber";
            this.dgvSerialNumber.ReadOnly = true;
            this.dgvSerialNumber.RowTemplate.Height = 23;
            this.dgvSerialNumber.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSerialNumber.Size = new System.Drawing.Size(884, 370);
            this.dgvSerialNumber.TabIndex = 9;
            this.dgvSerialNumber.TabStop = false;
            this.dgvSerialNumber.DoubleClick += new System.EventHandler(this.tsmiEdit_Click);
            // 
            // CompanyDesc
            // 
            this.CompanyDesc.DataPropertyName = "CompanyDesc";
            this.CompanyDesc.HeaderText = "所属公司";
            this.CompanyDesc.Name = "CompanyDesc";
            this.CompanyDesc.ReadOnly = true;
            this.CompanyDesc.Width = 78;
            // 
            // Type
            // 
            this.Type.DataPropertyName = "Type";
            this.Type.HeaderText = "类型名";
            this.Type.Name = "Type";
            this.Type.ReadOnly = true;
            this.Type.Width = 66;
            // 
            // Prefix
            // 
            this.Prefix.DataPropertyName = "Prefix";
            this.Prefix.HeaderText = "前缀";
            this.Prefix.Name = "Prefix";
            this.Prefix.ReadOnly = true;
            this.Prefix.Width = 54;
            // 
            // YYMMDD
            // 
            this.YYMMDD.DataPropertyName = "YYMMDD";
            this.YYMMDD.HeaderText = "年月日";
            this.YYMMDD.Name = "YYMMDD";
            this.YYMMDD.ReadOnly = true;
            this.YYMMDD.Width = 66;
            // 
            // SeqLength
            // 
            this.SeqLength.DataPropertyName = "SeqLength";
            this.SeqLength.HeaderText = "流水号长度";
            this.SeqLength.Name = "SeqLength";
            this.SeqLength.ReadOnly = true;
            this.SeqLength.Width = 90;
            // 
            // SeqStep
            // 
            this.SeqStep.DataPropertyName = "SeqStep";
            this.SeqStep.HeaderText = "流水号步长";
            this.SeqStep.Name = "SeqStep";
            this.SeqStep.ReadOnly = true;
            this.SeqStep.Width = 90;
            // 
            // Suffix
            // 
            this.Suffix.DataPropertyName = "Suffix";
            this.Suffix.HeaderText = "后缀";
            this.Suffix.Name = "Suffix";
            this.Suffix.ReadOnly = true;
            this.Suffix.Width = 54;
            // 
            // Description
            // 
            this.Description.DataPropertyName = "Description";
            this.Description.HeaderText = "描述";
            this.Description.Name = "Description";
            this.Description.ReadOnly = true;
            this.Description.Width = 54;
            // 
            // StatusDesc
            // 
            this.StatusDesc.DataPropertyName = "StatusDesc";
            this.StatusDesc.HeaderText = "状态";
            this.StatusDesc.Name = "StatusDesc";
            this.StatusDesc.ReadOnly = true;
            this.StatusDesc.Width = 54;
            // 
            // CreatedBy
            // 
            this.CreatedBy.DataPropertyName = "CreatedBy";
            this.CreatedBy.HeaderText = "创建人";
            this.CreatedBy.Name = "CreatedBy";
            this.CreatedBy.ReadOnly = true;
            this.CreatedBy.Width = 66;
            // 
            // CreateTime
            // 
            this.CreateTime.DataPropertyName = "CreateTime";
            this.CreateTime.HeaderText = "创建时间";
            this.CreateTime.Name = "CreateTime";
            this.CreateTime.ReadOnly = true;
            this.CreateTime.Width = 78;
            // 
            // ID
            // 
            this.ID.DataPropertyName = "ID";
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Visible = false;
            this.ID.Width = 42;
            // 
            // Status
            // 
            this.Status.DataPropertyName = "Status";
            this.Status.HeaderText = "Status";
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            this.Status.Visible = false;
            this.Status.Width = 66;
            // 
            // CompanyID
            // 
            this.CompanyID.DataPropertyName = "CompanyID";
            this.CompanyID.HeaderText = "CompanyID";
            this.CompanyID.Name = "CompanyID";
            this.CompanyID.ReadOnly = true;
            this.CompanyID.Visible = false;
            this.CompanyID.Width = 84;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiAdd,
            this.tsmiEdit,
            this.toolStripSeparator4,
            this.tsmiEnable,
            this.tsmiDisable,
            this.toolStripSeparator5,
            this.tsmiRefresh});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(101, 126);
            // 
            // tsmiAdd
            // 
            this.tsmiAdd.Image = global::WMS.Client.Properties.Resources.plus;
            this.tsmiAdd.Name = "tsmiAdd";
            this.tsmiAdd.Size = new System.Drawing.Size(100, 22);
            this.tsmiAdd.Text = "添加";
            this.tsmiAdd.Click += new System.EventHandler(this.tsmiAdd_Click);
            // 
            // tsmiEdit
            // 
            this.tsmiEdit.Image = global::WMS.Client.Properties.Resources.edit;
            this.tsmiEdit.Name = "tsmiEdit";
            this.tsmiEdit.Size = new System.Drawing.Size(100, 22);
            this.tsmiEdit.Text = "编辑";
            this.tsmiEdit.Click += new System.EventHandler(this.tsmiEdit_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(97, 6);
            // 
            // tsmiDisable
            // 
            this.tsmiDisable.Name = "tsmiDisable";
            this.tsmiDisable.Size = new System.Drawing.Size(100, 22);
            this.tsmiDisable.Text = "失效";
            this.tsmiDisable.Click += new System.EventHandler(this.tsmiDisable_Click);
            // 
            // tsmiRefresh
            // 
            this.tsmiRefresh.Image = global::WMS.Client.Properties.Resources.refresh;
            this.tsmiRefresh.Name = "tsmiRefresh";
            this.tsmiRefresh.Size = new System.Drawing.Size(100, 22);
            this.tsmiRefresh.Text = "刷新";
            this.tsmiRefresh.Click += new System.EventHandler(this.tsmiRefresh_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbAdd,
            this.toolStripSeparator1,
            this.tsbEdit,
            this.toolStripSeparator2,
            this.tsbRefresh,
            this.tsbQuery,
            this.tstbQuery});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(884, 34);
            this.toolStrip1.TabIndex = 14;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbAdd
            // 
            this.tsbAdd.AutoSize = false;
            this.tsbAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbAdd.Image = global::WMS.Client.Properties.Resources.plus;
            this.tsbAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAdd.Name = "tsbAdd";
            this.tsbAdd.Size = new System.Drawing.Size(32, 32);
            this.tsbAdd.ToolTipText = "新增";
            this.tsbAdd.Click += new System.EventHandler(this.tsmiAdd_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 34);
            // 
            // tsbEdit
            // 
            this.tsbEdit.AutoSize = false;
            this.tsbEdit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbEdit.Image = global::WMS.Client.Properties.Resources.edit;
            this.tsbEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbEdit.Name = "tsbEdit";
            this.tsbEdit.Size = new System.Drawing.Size(32, 32);
            this.tsbEdit.ToolTipText = "编辑";
            this.tsbEdit.Click += new System.EventHandler(this.tsmiEdit_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 34);
            // 
            // tsbRefresh
            // 
            this.tsbRefresh.AutoSize = false;
            this.tsbRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbRefresh.Image = global::WMS.Client.Properties.Resources.refresh;
            this.tsbRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRefresh.Name = "tsbRefresh";
            this.tsbRefresh.Size = new System.Drawing.Size(32, 32);
            this.tsbRefresh.ToolTipText = "刷新";
            this.tsbRefresh.Click += new System.EventHandler(this.tsmiRefresh_Click);
            // 
            // tsbQuery
            // 
            this.tsbQuery.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbQuery.AutoSize = false;
            this.tsbQuery.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbQuery.Image = global::WMS.Client.Properties.Resources.search;
            this.tsbQuery.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbQuery.Name = "tsbQuery";
            this.tsbQuery.Size = new System.Drawing.Size(32, 32);
            this.tsbQuery.ToolTipText = "查询";
            // 
            // tstbQuery
            // 
            this.tstbQuery.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tstbQuery.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tstbQuery.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tstbQuery.Name = "tstbQuery";
            this.tstbQuery.Size = new System.Drawing.Size(100, 34);
            // 
            // tsslMsg
            // 
            this.tsslMsg.ForeColor = System.Drawing.Color.Red;
            this.tsslMsg.Name = "tsslMsg";
            this.tsslMsg.Size = new System.Drawing.Size(0, 17);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslMsg});
            this.statusStrip1.Location = new System.Drawing.Point(0, 540);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(884, 22);
            this.statusStrip1.TabIndex = 13;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // Fm_SerialNumber
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 562);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Fm_SerialNumber";
            this.Text = "序号规则";
            this.Load += new System.EventHandler(this.Fm_SerialNumber_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudSeqStep)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tcbSeqLength)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSerialNumber)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripMenuItem tsmiEnable;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtSuffix;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.RichTextBox rtbDesc;
        private System.Windows.Forms.TextBox txtPrefix;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvSerialNumber;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmiAdd;
        private System.Windows.Forms.ToolStripMenuItem tsmiEdit;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem tsmiDisable;
        private System.Windows.Forms.ToolStripMenuItem tsmiRefresh;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbAdd;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbEdit;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsbRefresh;
        private System.Windows.Forms.ToolStripButton tsbQuery;
        private System.Windows.Forms.ToolStripTextBox tstbQuery;
        private System.Windows.Forms.ToolStripStatusLabel tsslMsg;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.DataGridViewTextBoxColumn CompanyDesc;
        private System.Windows.Forms.DataGridViewTextBoxColumn Type;
        private System.Windows.Forms.DataGridViewTextBoxColumn Prefix;
        private System.Windows.Forms.DataGridViewTextBoxColumn YYMMDD;
        private System.Windows.Forms.DataGridViewTextBoxColumn SeqLength;
        private System.Windows.Forms.DataGridViewTextBoxColumn SeqStep;
        private System.Windows.Forms.DataGridViewTextBoxColumn Suffix;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private System.Windows.Forms.DataGridViewTextBoxColumn StatusDesc;
        private System.Windows.Forms.DataGridViewTextBoxColumn CreatedBy;
        private System.Windows.Forms.DataGridViewTextBoxColumn CreateTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
        private System.Windows.Forms.DataGridViewTextBoxColumn CompanyID;
        private System.Windows.Forms.ComboBox cmbCompany;
        private System.Windows.Forms.TrackBar tcbSeqLength;
        private System.Windows.Forms.ComboBox cmbYMD;
        private System.Windows.Forms.NumericUpDown nudSeqStep;
        private System.Windows.Forms.TextBox txtSeqLength;
        private System.Windows.Forms.ComboBox cmbType;
    }
}