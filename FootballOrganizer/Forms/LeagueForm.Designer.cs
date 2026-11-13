namespace FootballOrganizer.Forms;

partial class LeagueForm
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Код, створений конструктором форм

    private void InitializeComponent()
    {
        tabControl1 = new TabControl();
        tabPageCalendar = new TabPage();
        groupBoxResult = new GroupBox();
        buttonClear = new Button();
        buttonSave = new Button();
        numericAway = new NumericUpDown();
        labelColon = new Label();
        numericHome = new NumericUpDown();
        labelMatch = new Label();
        listBoxMatches = new ListBox();
        comboBoxRound = new ComboBox();
        labelRound = new Label();
        labelHint = new Label();
        buttonGenerate = new Button();
        tabPageTable = new TabPage();
        dataGridViewTable = new DataGridView();
        colPlace = new DataGridViewTextBoxColumn();
        colTeam = new DataGridViewTextBoxColumn();
        colPlayed = new DataGridViewTextBoxColumn();
        colWon = new DataGridViewTextBoxColumn();
        colDrawn = new DataGridViewTextBoxColumn();
        colLost = new DataGridViewTextBoxColumn();
        colGoals = new DataGridViewTextBoxColumn();
        colDiff = new DataGridViewTextBoxColumn();
        colPoints = new DataGridViewTextBoxColumn();
        tabControl1.SuspendLayout();
        tabPageCalendar.SuspendLayout();
        groupBoxResult.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)numericAway).BeginInit();
        ((System.ComponentModel.ISupportInitialize)numericHome).BeginInit();
        tabPageTable.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dataGridViewTable).BeginInit();
        SuspendLayout();
        //
        // tabControl1
        //
        tabControl1.Controls.Add(tabPageCalendar);
        tabControl1.Controls.Add(tabPageTable);
        tabControl1.Dock = DockStyle.Fill;
        tabControl1.Location = new Point(0, 0);
        tabControl1.Name = "tabControl1";
        tabControl1.SelectedIndex = 0;
        tabControl1.Size = new Size(684, 461);
        tabControl1.TabIndex = 0;
        //
        // tabPageCalendar
        //
        tabPageCalendar.Controls.Add(groupBoxResult);
        tabPageCalendar.Controls.Add(listBoxMatches);
        tabPageCalendar.Controls.Add(comboBoxRound);
        tabPageCalendar.Controls.Add(labelRound);
        tabPageCalendar.Controls.Add(labelHint);
        tabPageCalendar.Controls.Add(buttonGenerate);
        tabPageCalendar.Location = new Point(4, 24);
        tabPageCalendar.Name = "tabPageCalendar";
        tabPageCalendar.Padding = new Padding(3);
        tabPageCalendar.Size = new Size(676, 433);
        tabPageCalendar.TabIndex = 0;
        tabPageCalendar.Text = "Календар";
        tabPageCalendar.UseVisualStyleBackColor = true;
        //
        // buttonGenerate
        //
        buttonGenerate.Location = new Point(12, 12);
        buttonGenerate.Name = "buttonGenerate";
        buttonGenerate.Size = new Size(160, 30);
        buttonGenerate.TabIndex = 0;
        buttonGenerate.Text = "Скласти календар";
        buttonGenerate.UseVisualStyleBackColor = true;
        buttonGenerate.Click += buttonGenerate_Click;
        //
        // labelHint
        //
        labelHint.AutoSize = true;
        labelHint.Location = new Point(185, 20);
        labelHint.Name = "labelHint";
        labelHint.Size = new Size(0, 15);
        labelHint.TabIndex = 1;
        //
        // labelRound
        //
        labelRound.AutoSize = true;
        labelRound.Location = new Point(12, 57);
        labelRound.Name = "labelRound";
        labelRound.Size = new Size(29, 15);
        labelRound.TabIndex = 2;
        labelRound.Text = "Тур:";
        //
        // comboBoxRound
        //
        comboBoxRound.DropDownStyle = ComboBoxStyle.DropDownList;
        comboBoxRound.FormattingEnabled = true;
        comboBoxRound.Location = new Point(52, 54);
        comboBoxRound.Name = "comboBoxRound";
        comboBoxRound.Size = new Size(120, 23);
        comboBoxRound.TabIndex = 3;
        comboBoxRound.SelectedIndexChanged += comboBoxRound_SelectedIndexChanged;
        //
        // listBoxMatches
        //
        listBoxMatches.FormattingEnabled = true;
        listBoxMatches.Location = new Point(12, 88);
        listBoxMatches.Name = "listBoxMatches";
        listBoxMatches.Size = new Size(380, 334);
        listBoxMatches.TabIndex = 4;
        listBoxMatches.SelectedIndexChanged += listBoxMatches_SelectedIndexChanged;
        //
        // groupBoxResult
        //
        groupBoxResult.Controls.Add(buttonClear);
        groupBoxResult.Controls.Add(buttonSave);
        groupBoxResult.Controls.Add(numericAway);
        groupBoxResult.Controls.Add(labelColon);
        groupBoxResult.Controls.Add(numericHome);
        groupBoxResult.Controls.Add(labelMatch);
        groupBoxResult.Location = new Point(408, 82);
        groupBoxResult.Name = "groupBoxResult";
        groupBoxResult.Size = new Size(256, 170);
        groupBoxResult.TabIndex = 5;
        groupBoxResult.TabStop = false;
        groupBoxResult.Text = "Результат матчу";
        //
        // labelMatch
        //
        labelMatch.Location = new Point(12, 24);
        labelMatch.Name = "labelMatch";
        labelMatch.Size = new Size(232, 36);
        labelMatch.TabIndex = 0;
        labelMatch.Text = "Виберіть матч";
        //
        // numericHome
        //
        numericHome.Location = new Point(40, 68);
        numericHome.Maximum = new decimal(new int[] { 99, 0, 0, 0 });
        numericHome.Name = "numericHome";
        numericHome.Size = new Size(70, 23);
        numericHome.TabIndex = 1;
        //
        // labelColon
        //
        labelColon.AutoSize = true;
        labelColon.Location = new Point(121, 70);
        labelColon.Name = "labelColon";
        labelColon.Size = new Size(10, 15);
        labelColon.TabIndex = 2;
        labelColon.Text = ":";
        //
        // numericAway
        //
        numericAway.Location = new Point(140, 68);
        numericAway.Maximum = new decimal(new int[] { 99, 0, 0, 0 });
        numericAway.Name = "numericAway";
        numericAway.Size = new Size(70, 23);
        numericAway.TabIndex = 3;
        //
        // buttonSave
        //
        buttonSave.Location = new Point(12, 112);
        buttonSave.Name = "buttonSave";
        buttonSave.Size = new Size(110, 30);
        buttonSave.TabIndex = 4;
        buttonSave.Text = "Зберегти";
        buttonSave.UseVisualStyleBackColor = true;
        buttonSave.Click += buttonSave_Click;
        //
        // buttonClear
        //
        buttonClear.Location = new Point(134, 112);
        buttonClear.Name = "buttonClear";
        buttonClear.Size = new Size(110, 30);
        buttonClear.TabIndex = 5;
        buttonClear.Text = "Очистити";
        buttonClear.UseVisualStyleBackColor = true;
        buttonClear.Click += buttonClear_Click;
        //
        // tabPageTable
        //
        tabPageTable.Controls.Add(dataGridViewTable);
        tabPageTable.Location = new Point(4, 24);
        tabPageTable.Name = "tabPageTable";
        tabPageTable.Padding = new Padding(3);
        tabPageTable.Size = new Size(676, 433);
        tabPageTable.TabIndex = 1;
        tabPageTable.Text = "Турнірна таблиця";
        tabPageTable.UseVisualStyleBackColor = true;
        //
        // dataGridViewTable
        //
        dataGridViewTable.AllowUserToAddRows = false;
        dataGridViewTable.AllowUserToDeleteRows = false;
        dataGridViewTable.AllowUserToResizeRows = false;
        dataGridViewTable.BackgroundColor = SystemColors.Window;
        dataGridViewTable.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dataGridViewTable.Columns.AddRange(new DataGridViewColumn[] { colPlace, colTeam, colPlayed, colWon, colDrawn, colLost, colGoals, colDiff, colPoints });
        dataGridViewTable.Dock = DockStyle.Fill;
        dataGridViewTable.Location = new Point(3, 3);
        dataGridViewTable.Name = "dataGridViewTable";
        dataGridViewTable.ReadOnly = true;
        dataGridViewTable.RowHeadersVisible = false;
        dataGridViewTable.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dataGridViewTable.Size = new Size(670, 427);
        dataGridViewTable.TabIndex = 0;
        //
        // colPlace
        //
        colPlace.HeaderText = "№";
        colPlace.Name = "colPlace";
        colPlace.ReadOnly = true;
        colPlace.Width = 40;
        //
        // colTeam
        //
        colTeam.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        colTeam.HeaderText = "Команда";
        colTeam.Name = "colTeam";
        colTeam.ReadOnly = true;
        //
        // colPlayed
        //
        colPlayed.HeaderText = "І";
        colPlayed.Name = "colPlayed";
        colPlayed.ReadOnly = true;
        colPlayed.Width = 45;
        //
        // colWon
        //
        colWon.HeaderText = "В";
        colWon.Name = "colWon";
        colWon.ReadOnly = true;
        colWon.Width = 45;
        //
        // colDrawn
        //
        colDrawn.HeaderText = "Н";
        colDrawn.Name = "colDrawn";
        colDrawn.ReadOnly = true;
        colDrawn.Width = 45;
        //
        // colLost
        //
        colLost.HeaderText = "П";
        colLost.Name = "colLost";
        colLost.ReadOnly = true;
        colLost.Width = 45;
        //
        // colGoals
        //
        colGoals.HeaderText = "М'ячі";
        colGoals.Name = "colGoals";
        colGoals.ReadOnly = true;
        colGoals.Width = 70;
        //
        // colDiff
        //
        colDiff.HeaderText = "Р";
        colDiff.Name = "colDiff";
        colDiff.ReadOnly = true;
        colDiff.Width = 50;
        //
        // colPoints
        //
        colPoints.HeaderText = "О";
        colPoints.Name = "colPoints";
        colPoints.ReadOnly = true;
        colPoints.Width = 50;
        //
        // LeagueForm
        //
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(684, 461);
        Controls.Add(tabControl1);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        Name = "LeagueForm";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Чемпіонат";
        tabControl1.ResumeLayout(false);
        tabPageCalendar.ResumeLayout(false);
        tabPageCalendar.PerformLayout();
        groupBoxResult.ResumeLayout(false);
        groupBoxResult.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)numericAway).EndInit();
        ((System.ComponentModel.ISupportInitialize)numericHome).EndInit();
        tabPageTable.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)dataGridViewTable).EndInit();
        ResumeLayout(false);
    }

    #endregion

    private TabControl tabControl1;
    private TabPage tabPageCalendar;
    private TabPage tabPageTable;
    private Button buttonGenerate;
    private Label labelHint;
    private Label labelRound;
    private ComboBox comboBoxRound;
    private ListBox listBoxMatches;
    private GroupBox groupBoxResult;
    private Label labelMatch;
    private NumericUpDown numericHome;
    private Label labelColon;
    private NumericUpDown numericAway;
    private Button buttonSave;
    private Button buttonClear;
    private DataGridView dataGridViewTable;
    private DataGridViewTextBoxColumn colPlace;
    private DataGridViewTextBoxColumn colTeam;
    private DataGridViewTextBoxColumn colPlayed;
    private DataGridViewTextBoxColumn colWon;
    private DataGridViewTextBoxColumn colDrawn;
    private DataGridViewTextBoxColumn colLost;
    private DataGridViewTextBoxColumn colGoals;
    private DataGridViewTextBoxColumn colDiff;
    private DataGridViewTextBoxColumn colPoints;
}
