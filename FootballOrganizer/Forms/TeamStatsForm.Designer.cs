namespace FootballOrganizer.Forms;

partial class TeamStatsForm
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
        labelTeam = new Label();
        comboBoxTeam = new ComboBox();
        labelSummary = new Label();
        labelMatches = new Label();
        listBoxMatches = new ListBox();
        groupBoxChart = new GroupBox();
        panelChart = new Panel();
        groupBoxChart.SuspendLayout();
        SuspendLayout();
        //
        // labelTeam
        //
        labelTeam.AutoSize = true;
        labelTeam.Location = new Point(12, 15);
        labelTeam.Name = "labelTeam";
        labelTeam.Size = new Size(57, 15);
        labelTeam.TabIndex = 0;
        labelTeam.Text = "Команда:";
        //
        // comboBoxTeam
        //
        comboBoxTeam.DropDownStyle = ComboBoxStyle.DropDownList;
        comboBoxTeam.FormattingEnabled = true;
        comboBoxTeam.Location = new Point(80, 12);
        comboBoxTeam.Name = "comboBoxTeam";
        comboBoxTeam.Size = new Size(262, 23);
        comboBoxTeam.TabIndex = 1;
        comboBoxTeam.SelectedIndexChanged += comboBoxTeam_SelectedIndexChanged;
        //
        // labelSummary
        //
        labelSummary.Location = new Point(12, 48);
        labelSummary.Name = "labelSummary";
        labelSummary.Size = new Size(330, 110);
        labelSummary.TabIndex = 2;
        //
        // labelMatches
        //
        labelMatches.AutoSize = true;
        labelMatches.Location = new Point(12, 165);
        labelMatches.Name = "labelMatches";
        labelMatches.Size = new Size(98, 15);
        labelMatches.TabIndex = 3;
        labelMatches.Text = "Зіграні матчі:";
        //
        // listBoxMatches
        //
        listBoxMatches.FormattingEnabled = true;
        listBoxMatches.HorizontalScrollbar = true;
        listBoxMatches.Location = new Point(12, 185);
        listBoxMatches.Name = "listBoxMatches";
        listBoxMatches.Size = new Size(330, 274);
        listBoxMatches.TabIndex = 4;
        //
        // groupBoxChart
        //
        groupBoxChart.Controls.Add(panelChart);
        groupBoxChart.Location = new Point(356, 8);
        groupBoxChart.Name = "groupBoxChart";
        groupBoxChart.Size = new Size(380, 455);
        groupBoxChart.TabIndex = 5;
        groupBoxChart.TabStop = false;
        groupBoxChart.Text = "Місце в таблиці після кожного туру";
        //
        // panelChart
        //
        panelChart.BackColor = Color.White;
        panelChart.Dock = DockStyle.Fill;
        panelChart.Location = new Point(3, 19);
        panelChart.Name = "panelChart";
        panelChart.Size = new Size(374, 433);
        panelChart.TabIndex = 0;
        //
        // TeamStatsForm
        //
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(748, 475);
        Controls.Add(groupBoxChart);
        Controls.Add(listBoxMatches);
        Controls.Add(labelMatches);
        Controls.Add(labelSummary);
        Controls.Add(comboBoxTeam);
        Controls.Add(labelTeam);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        Name = "TeamStatsForm";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Статистика команди";
        groupBoxChart.ResumeLayout(false);
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Label labelTeam;
    private ComboBox comboBoxTeam;
    private Label labelSummary;
    private Label labelMatches;
    private ListBox listBoxMatches;
    private GroupBox groupBoxChart;
    private Panel panelChart;
}
