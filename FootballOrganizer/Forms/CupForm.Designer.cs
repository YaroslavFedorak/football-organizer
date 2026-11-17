namespace FootballOrganizer.Forms;

partial class CupForm
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
        tabPageRounds = new TabPage();
        groupBoxResult = new GroupBox();
        buttonPenalty = new Button();
        comboBoxPenalty = new ComboBox();
        labelPenalty = new Label();
        buttonClear = new Button();
        buttonSave = new Button();
        numericAway = new NumericUpDown();
        labelColon = new Label();
        numericHome = new NumericUpDown();
        listBoxMatches = new ListBox();
        labelMatches = new Label();
        labelTie = new Label();
        listBoxTies = new ListBox();
        buttonNext = new Button();
        comboBoxRound = new ComboBox();
        labelRound = new Label();
        labelStatus = new Label();
        buttonDraw = new Button();
        tabPageBracket = new TabPage();
        panelBracket = new Panel();
        panelCanvas = new Panel();
        tabControl1.SuspendLayout();
        tabPageRounds.SuspendLayout();
        groupBoxResult.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)numericAway).BeginInit();
        ((System.ComponentModel.ISupportInitialize)numericHome).BeginInit();
        tabPageBracket.SuspendLayout();
        panelBracket.SuspendLayout();
        SuspendLayout();
        //
        // tabControl1
        //
        tabControl1.Controls.Add(tabPageRounds);
        tabControl1.Controls.Add(tabPageBracket);
        tabControl1.Dock = DockStyle.Fill;
        tabControl1.Location = new Point(0, 0);
        tabControl1.Name = "tabControl1";
        tabControl1.SelectedIndex = 0;
        tabControl1.Size = new Size(764, 471);
        tabControl1.TabIndex = 0;
        //
        // tabPageRounds
        //
        tabPageRounds.Controls.Add(groupBoxResult);
        tabPageRounds.Controls.Add(listBoxTies);
        tabPageRounds.Controls.Add(buttonNext);
        tabPageRounds.Controls.Add(comboBoxRound);
        tabPageRounds.Controls.Add(labelRound);
        tabPageRounds.Controls.Add(labelStatus);
        tabPageRounds.Controls.Add(buttonDraw);
        tabPageRounds.Location = new Point(4, 24);
        tabPageRounds.Name = "tabPageRounds";
        tabPageRounds.Padding = new Padding(3);
        tabPageRounds.Size = new Size(756, 443);
        tabPageRounds.TabIndex = 0;
        tabPageRounds.Text = "Раунди";
        tabPageRounds.UseVisualStyleBackColor = true;
        //
        // buttonDraw
        //
        buttonDraw.Location = new Point(12, 12);
        buttonDraw.Name = "buttonDraw";
        buttonDraw.Size = new Size(150, 30);
        buttonDraw.TabIndex = 0;
        buttonDraw.Text = "Жеребкування";
        buttonDraw.UseVisualStyleBackColor = true;
        buttonDraw.Click += buttonDraw_Click;
        //
        // labelStatus
        //
        labelStatus.AutoSize = true;
        labelStatus.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        labelStatus.Location = new Point(175, 20);
        labelStatus.Name = "labelStatus";
        labelStatus.Size = new Size(0, 15);
        labelStatus.TabIndex = 1;
        //
        // labelRound
        //
        labelRound.AutoSize = true;
        labelRound.Location = new Point(12, 57);
        labelRound.Name = "labelRound";
        labelRound.Size = new Size(43, 15);
        labelRound.TabIndex = 2;
        labelRound.Text = "Раунд:";
        //
        // comboBoxRound
        //
        comboBoxRound.DropDownStyle = ComboBoxStyle.DropDownList;
        comboBoxRound.FormattingEnabled = true;
        comboBoxRound.Location = new Point(64, 54);
        comboBoxRound.Name = "comboBoxRound";
        comboBoxRound.Size = new Size(160, 23);
        comboBoxRound.TabIndex = 3;
        comboBoxRound.SelectedIndexChanged += comboBoxRound_SelectedIndexChanged;
        //
        // buttonNext
        //
        buttonNext.Location = new Point(282, 51);
        buttonNext.Name = "buttonNext";
        buttonNext.Size = new Size(150, 29);
        buttonNext.TabIndex = 4;
        buttonNext.Text = "Наступний раунд";
        buttonNext.UseVisualStyleBackColor = true;
        buttonNext.Click += buttonNext_Click;
        //
        // listBoxTies
        //
        listBoxTies.FormattingEnabled = true;
        listBoxTies.HorizontalScrollbar = true;
        listBoxTies.Location = new Point(12, 88);
        listBoxTies.Name = "listBoxTies";
        listBoxTies.Size = new Size(420, 334);
        listBoxTies.TabIndex = 5;
        listBoxTies.SelectedIndexChanged += listBoxTies_SelectedIndexChanged;
        //
        // groupBoxResult
        //
        groupBoxResult.Controls.Add(buttonPenalty);
        groupBoxResult.Controls.Add(comboBoxPenalty);
        groupBoxResult.Controls.Add(labelPenalty);
        groupBoxResult.Controls.Add(buttonClear);
        groupBoxResult.Controls.Add(buttonSave);
        groupBoxResult.Controls.Add(numericAway);
        groupBoxResult.Controls.Add(labelColon);
        groupBoxResult.Controls.Add(numericHome);
        groupBoxResult.Controls.Add(listBoxMatches);
        groupBoxResult.Controls.Add(labelMatches);
        groupBoxResult.Controls.Add(labelTie);
        groupBoxResult.Location = new Point(448, 82);
        groupBoxResult.Name = "groupBoxResult";
        groupBoxResult.Size = new Size(296, 340);
        groupBoxResult.TabIndex = 6;
        groupBoxResult.TabStop = false;
        groupBoxResult.Text = "Пара";
        //
        // labelTie
        //
        labelTie.Location = new Point(12, 22);
        labelTie.Name = "labelTie";
        labelTie.Size = new Size(272, 38);
        labelTie.TabIndex = 0;
        labelTie.Text = "Виберіть пару";
        //
        // labelMatches
        //
        labelMatches.AutoSize = true;
        labelMatches.Location = new Point(12, 62);
        labelMatches.Name = "labelMatches";
        labelMatches.Size = new Size(41, 15);
        labelMatches.TabIndex = 1;
        labelMatches.Text = "Матчі:";
        //
        // listBoxMatches
        //
        listBoxMatches.FormattingEnabled = true;
        listBoxMatches.Location = new Point(12, 80);
        listBoxMatches.Name = "listBoxMatches";
        listBoxMatches.Size = new Size(272, 49);
        listBoxMatches.TabIndex = 2;
        listBoxMatches.SelectedIndexChanged += listBoxMatches_SelectedIndexChanged;
        //
        // numericHome
        //
        numericHome.Location = new Point(48, 142);
        numericHome.Maximum = new decimal(new int[] { 99, 0, 0, 0 });
        numericHome.Name = "numericHome";
        numericHome.Size = new Size(70, 23);
        numericHome.TabIndex = 3;
        //
        // labelColon
        //
        labelColon.AutoSize = true;
        labelColon.Location = new Point(135, 144);
        labelColon.Name = "labelColon";
        labelColon.Size = new Size(10, 15);
        labelColon.TabIndex = 4;
        labelColon.Text = ":";
        //
        // numericAway
        //
        numericAway.Location = new Point(162, 142);
        numericAway.Maximum = new decimal(new int[] { 99, 0, 0, 0 });
        numericAway.Name = "numericAway";
        numericAway.Size = new Size(70, 23);
        numericAway.TabIndex = 5;
        //
        // buttonSave
        //
        buttonSave.Location = new Point(12, 178);
        buttonSave.Name = "buttonSave";
        buttonSave.Size = new Size(130, 30);
        buttonSave.TabIndex = 6;
        buttonSave.Text = "Зберегти";
        buttonSave.UseVisualStyleBackColor = true;
        buttonSave.Click += buttonSave_Click;
        //
        // buttonClear
        //
        buttonClear.Location = new Point(154, 178);
        buttonClear.Name = "buttonClear";
        buttonClear.Size = new Size(130, 30);
        buttonClear.TabIndex = 7;
        buttonClear.Text = "Очистити";
        buttonClear.UseVisualStyleBackColor = true;
        buttonClear.Click += buttonClear_Click;
        //
        // labelPenalty
        //
        labelPenalty.AutoSize = true;
        labelPenalty.Location = new Point(12, 228);
        labelPenalty.Name = "labelPenalty";
        labelPenalty.Size = new Size(152, 15);
        labelPenalty.TabIndex = 8;
        labelPenalty.Text = "Переможець по пенальті:";
        //
        // comboBoxPenalty
        //
        comboBoxPenalty.DropDownStyle = ComboBoxStyle.DropDownList;
        comboBoxPenalty.Enabled = false;
        comboBoxPenalty.FormattingEnabled = true;
        comboBoxPenalty.Location = new Point(12, 248);
        comboBoxPenalty.Name = "comboBoxPenalty";
        comboBoxPenalty.Size = new Size(272, 23);
        comboBoxPenalty.TabIndex = 9;
        //
        // buttonPenalty
        //
        buttonPenalty.Enabled = false;
        buttonPenalty.Location = new Point(12, 282);
        buttonPenalty.Name = "buttonPenalty";
        buttonPenalty.Size = new Size(272, 30);
        buttonPenalty.TabIndex = 10;
        buttonPenalty.Text = "Зберегти пенальті";
        buttonPenalty.UseVisualStyleBackColor = true;
        buttonPenalty.Click += buttonPenalty_Click;
        //
        // tabPageBracket
        //
        tabPageBracket.Controls.Add(panelBracket);
        tabPageBracket.Location = new Point(4, 24);
        tabPageBracket.Name = "tabPageBracket";
        tabPageBracket.Padding = new Padding(3);
        tabPageBracket.Size = new Size(756, 443);
        tabPageBracket.TabIndex = 1;
        tabPageBracket.Text = "Сітка";
        tabPageBracket.UseVisualStyleBackColor = true;
        //
        // panelBracket
        //
        panelBracket.AutoScroll = true;
        panelBracket.BackColor = Color.White;
        panelBracket.Controls.Add(panelCanvas);
        panelBracket.Dock = DockStyle.Fill;
        panelBracket.Location = new Point(3, 3);
        panelBracket.Name = "panelBracket";
        panelBracket.Size = new Size(750, 437);
        panelBracket.TabIndex = 0;
        //
        // panelCanvas
        //
        panelCanvas.Location = new Point(0, 0);
        panelCanvas.Name = "panelCanvas";
        panelCanvas.Size = new Size(740, 420);
        panelCanvas.TabIndex = 0;
        panelCanvas.Paint += panelCanvas_Paint;
        //
        // CupForm
        //
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(764, 471);
        Controls.Add(tabControl1);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        Name = "CupForm";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Кубок";
        tabControl1.ResumeLayout(false);
        tabPageRounds.ResumeLayout(false);
        tabPageRounds.PerformLayout();
        groupBoxResult.ResumeLayout(false);
        groupBoxResult.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)numericAway).EndInit();
        ((System.ComponentModel.ISupportInitialize)numericHome).EndInit();
        tabPageBracket.ResumeLayout(false);
        panelBracket.ResumeLayout(false);
        ResumeLayout(false);
    }

    #endregion

    private TabControl tabControl1;
    private TabPage tabPageRounds;
    private TabPage tabPageBracket;
    private Button buttonDraw;
    private Label labelStatus;
    private Label labelRound;
    private ComboBox comboBoxRound;
    private Button buttonNext;
    private ListBox listBoxTies;
    private GroupBox groupBoxResult;
    private Label labelTie;
    private Label labelMatches;
    private ListBox listBoxMatches;
    private NumericUpDown numericHome;
    private Label labelColon;
    private NumericUpDown numericAway;
    private Button buttonSave;
    private Button buttonClear;
    private Label labelPenalty;
    private ComboBox comboBoxPenalty;
    private Button buttonPenalty;
    private Panel panelBracket;
    private Panel panelCanvas;
}
