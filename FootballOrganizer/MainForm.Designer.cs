namespace FootballOrganizer;

partial class MainForm
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
        labelTournaments = new Label();
        listBoxTournaments = new ListBox();
        buttonAdd = new Button();
        buttonEdit = new Button();
        buttonDelete = new Button();
        groupBoxInfo = new GroupBox();
        listBoxTeams = new ListBox();
        labelTeams = new Label();
        labelInfo = new Label();
        groupBoxInfo.SuspendLayout();
        SuspendLayout();
        //
        // labelTournaments
        //
        labelTournaments.AutoSize = true;
        labelTournaments.Location = new Point(12, 12);
        labelTournaments.Name = "labelTournaments";
        labelTournaments.Size = new Size(48, 15);
        labelTournaments.TabIndex = 0;
        labelTournaments.Text = "Турніри:";
        //
        // listBoxTournaments
        //
        listBoxTournaments.FormattingEnabled = true;
        listBoxTournaments.Location = new Point(12, 32);
        listBoxTournaments.Name = "listBoxTournaments";
        listBoxTournaments.Size = new Size(320, 349);
        listBoxTournaments.TabIndex = 1;
        listBoxTournaments.SelectedIndexChanged += listBoxTournaments_SelectedIndexChanged;
        //
        // buttonAdd
        //
        buttonAdd.Location = new Point(12, 392);
        buttonAdd.Name = "buttonAdd";
        buttonAdd.Size = new Size(100, 30);
        buttonAdd.TabIndex = 2;
        buttonAdd.Text = "Додати";
        buttonAdd.UseVisualStyleBackColor = true;
        buttonAdd.Click += buttonAdd_Click;
        //
        // buttonEdit
        //
        buttonEdit.Location = new Point(122, 392);
        buttonEdit.Name = "buttonEdit";
        buttonEdit.Size = new Size(100, 30);
        buttonEdit.TabIndex = 3;
        buttonEdit.Text = "Редагувати";
        buttonEdit.UseVisualStyleBackColor = true;
        buttonEdit.Click += buttonEdit_Click;
        //
        // buttonDelete
        //
        buttonDelete.Location = new Point(232, 392);
        buttonDelete.Name = "buttonDelete";
        buttonDelete.Size = new Size(100, 30);
        buttonDelete.TabIndex = 4;
        buttonDelete.Text = "Видалити";
        buttonDelete.UseVisualStyleBackColor = true;
        buttonDelete.Click += buttonDelete_Click;
        //
        // groupBoxInfo
        //
        groupBoxInfo.Controls.Add(listBoxTeams);
        groupBoxInfo.Controls.Add(labelTeams);
        groupBoxInfo.Controls.Add(labelInfo);
        groupBoxInfo.Location = new Point(348, 26);
        groupBoxInfo.Name = "groupBoxInfo";
        groupBoxInfo.Size = new Size(440, 396);
        groupBoxInfo.TabIndex = 5;
        groupBoxInfo.TabStop = false;
        groupBoxInfo.Text = "Інформація про турнір";
        //
        // labelInfo
        //
        labelInfo.Location = new Point(12, 24);
        labelInfo.Name = "labelInfo";
        labelInfo.Size = new Size(416, 80);
        labelInfo.TabIndex = 0;
        labelInfo.Text = "Турнір не вибрано";
        //
        // labelTeams
        //
        labelTeams.AutoSize = true;
        labelTeams.Location = new Point(12, 110);
        labelTeams.Name = "labelTeams";
        labelTeams.Size = new Size(64, 15);
        labelTeams.TabIndex = 1;
        labelTeams.Text = "Учасники:";
        //
        // listBoxTeams
        //
        listBoxTeams.FormattingEnabled = true;
        listBoxTeams.Location = new Point(12, 130);
        listBoxTeams.Name = "listBoxTeams";
        listBoxTeams.Size = new Size(416, 244);
        listBoxTeams.TabIndex = 2;
        //
        // MainForm
        //
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(800, 434);
        Controls.Add(groupBoxInfo);
        Controls.Add(buttonDelete);
        Controls.Add(buttonEdit);
        Controls.Add(buttonAdd);
        Controls.Add(listBoxTournaments);
        Controls.Add(labelTournaments);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        Name = "MainForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Футбольний органайзер";
        groupBoxInfo.ResumeLayout(false);
        groupBoxInfo.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Label labelTournaments;
    private ListBox listBoxTournaments;
    private Button buttonAdd;
    private Button buttonEdit;
    private Button buttonDelete;
    private GroupBox groupBoxInfo;
    private Label labelInfo;
    private Label labelTeams;
    private ListBox listBoxTeams;
}
