namespace FootballOrganizer.Forms;

partial class TournamentForm
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
        labelName = new Label();
        textBoxName = new TextBox();
        labelType = new Label();
        comboBoxType = new ComboBox();
        checkBoxReturn = new CheckBox();
        groupBoxTeams = new GroupBox();
        labelCount = new Label();
        buttonRemoveTeam = new Button();
        listBoxTeams = new ListBox();
        buttonAddTeam = new Button();
        textBoxCity = new TextBox();
        labelCity = new Label();
        textBoxTeam = new TextBox();
        labelTeam = new Label();
        buttonOk = new Button();
        buttonCancel = new Button();
        groupBoxTeams.SuspendLayout();
        SuspendLayout();
        //
        // labelName
        //
        labelName.AutoSize = true;
        labelName.Location = new Point(12, 15);
        labelName.Name = "labelName";
        labelName.Size = new Size(39, 15);
        labelName.TabIndex = 0;
        labelName.Text = "Назва:";
        //
        // textBoxName
        //
        textBoxName.Location = new Point(110, 12);
        textBoxName.Name = "textBoxName";
        textBoxName.Size = new Size(302, 23);
        textBoxName.TabIndex = 1;
        //
        // labelType
        //
        labelType.AutoSize = true;
        labelType.Location = new Point(12, 47);
        labelType.Name = "labelType";
        labelType.Size = new Size(77, 15);
        labelType.TabIndex = 2;
        labelType.Text = "Тип турніру:";
        //
        // comboBoxType
        //
        comboBoxType.DropDownStyle = ComboBoxStyle.DropDownList;
        comboBoxType.FormattingEnabled = true;
        comboBoxType.Items.AddRange(new object[] { "Чемпіонат (кругова система)", "Кубок (олімпійська система)" });
        comboBoxType.Location = new Point(110, 44);
        comboBoxType.Name = "comboBoxType";
        comboBoxType.Size = new Size(302, 23);
        comboBoxType.TabIndex = 3;
        //
        // checkBoxReturn
        //
        checkBoxReturn.AutoSize = true;
        checkBoxReturn.Location = new Point(110, 76);
        checkBoxReturn.Name = "checkBoxReturn";
        checkBoxReturn.Size = new Size(145, 19);
        checkBoxReturn.TabIndex = 4;
        checkBoxReturn.Text = "Матчі у відповідь";
        checkBoxReturn.UseVisualStyleBackColor = true;
        //
        // groupBoxTeams
        //
        groupBoxTeams.Controls.Add(labelCount);
        groupBoxTeams.Controls.Add(buttonRemoveTeam);
        groupBoxTeams.Controls.Add(listBoxTeams);
        groupBoxTeams.Controls.Add(buttonAddTeam);
        groupBoxTeams.Controls.Add(textBoxCity);
        groupBoxTeams.Controls.Add(labelCity);
        groupBoxTeams.Controls.Add(textBoxTeam);
        groupBoxTeams.Controls.Add(labelTeam);
        groupBoxTeams.Location = new Point(12, 106);
        groupBoxTeams.Name = "groupBoxTeams";
        groupBoxTeams.Size = new Size(400, 300);
        groupBoxTeams.TabIndex = 5;
        groupBoxTeams.TabStop = false;
        groupBoxTeams.Text = "Команди";
        //
        // labelTeam
        //
        labelTeam.AutoSize = true;
        labelTeam.Location = new Point(10, 25);
        labelTeam.Name = "labelTeam";
        labelTeam.Size = new Size(54, 15);
        labelTeam.TabIndex = 0;
        labelTeam.Text = "Команда:";
        //
        // textBoxTeam
        //
        textBoxTeam.Location = new Point(75, 22);
        textBoxTeam.Name = "textBoxTeam";
        textBoxTeam.Size = new Size(150, 23);
        textBoxTeam.TabIndex = 1;
        //
        // labelCity
        //
        labelCity.AutoSize = true;
        labelCity.Location = new Point(10, 54);
        labelCity.Name = "labelCity";
        labelCity.Size = new Size(41, 15);
        labelCity.TabIndex = 2;
        labelCity.Text = "Місто:";
        //
        // textBoxCity
        //
        textBoxCity.Location = new Point(75, 51);
        textBoxCity.Name = "textBoxCity";
        textBoxCity.Size = new Size(150, 23);
        textBoxCity.TabIndex = 3;
        //
        // buttonAddTeam
        //
        buttonAddTeam.Location = new Point(235, 21);
        buttonAddTeam.Name = "buttonAddTeam";
        buttonAddTeam.Size = new Size(155, 53);
        buttonAddTeam.TabIndex = 4;
        buttonAddTeam.Text = "Додати команду";
        buttonAddTeam.UseVisualStyleBackColor = true;
        buttonAddTeam.Click += buttonAddTeam_Click;
        //
        // listBoxTeams
        //
        listBoxTeams.FormattingEnabled = true;
        listBoxTeams.Location = new Point(10, 85);
        listBoxTeams.Name = "listBoxTeams";
        listBoxTeams.Size = new Size(380, 169);
        listBoxTeams.TabIndex = 5;
        //
        // buttonRemoveTeam
        //
        buttonRemoveTeam.Location = new Point(255, 262);
        buttonRemoveTeam.Name = "buttonRemoveTeam";
        buttonRemoveTeam.Size = new Size(135, 27);
        buttonRemoveTeam.TabIndex = 6;
        buttonRemoveTeam.Text = "Видалити команду";
        buttonRemoveTeam.UseVisualStyleBackColor = true;
        buttonRemoveTeam.Click += buttonRemoveTeam_Click;
        //
        // labelCount
        //
        labelCount.AutoSize = true;
        labelCount.Location = new Point(10, 268);
        labelCount.Name = "labelCount";
        labelCount.Size = new Size(61, 15);
        labelCount.TabIndex = 7;
        labelCount.Text = "Команд: 0";
        //
        // buttonOk
        //
        buttonOk.Location = new Point(216, 416);
        buttonOk.Name = "buttonOk";
        buttonOk.Size = new Size(95, 30);
        buttonOk.TabIndex = 6;
        buttonOk.Text = "ОК";
        buttonOk.UseVisualStyleBackColor = true;
        buttonOk.Click += buttonOk_Click;
        //
        // buttonCancel
        //
        buttonCancel.DialogResult = DialogResult.Cancel;
        buttonCancel.Location = new Point(317, 416);
        buttonCancel.Name = "buttonCancel";
        buttonCancel.Size = new Size(95, 30);
        buttonCancel.TabIndex = 7;
        buttonCancel.Text = "Скасувати";
        buttonCancel.UseVisualStyleBackColor = true;
        //
        // TournamentForm
        //
        AcceptButton = buttonOk;
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        CancelButton = buttonCancel;
        ClientSize = new Size(424, 458);
        Controls.Add(buttonCancel);
        Controls.Add(buttonOk);
        Controls.Add(groupBoxTeams);
        Controls.Add(checkBoxReturn);
        Controls.Add(comboBoxType);
        Controls.Add(labelType);
        Controls.Add(textBoxName);
        Controls.Add(labelName);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "TournamentForm";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Турнір";
        groupBoxTeams.ResumeLayout(false);
        groupBoxTeams.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Label labelName;
    private TextBox textBoxName;
    private Label labelType;
    private ComboBox comboBoxType;
    private CheckBox checkBoxReturn;
    private GroupBox groupBoxTeams;
    private Label labelTeam;
    private TextBox textBoxTeam;
    private Label labelCity;
    private TextBox textBoxCity;
    private Button buttonAddTeam;
    private ListBox listBoxTeams;
    private Button buttonRemoveTeam;
    private Label labelCount;
    private Button buttonOk;
    private Button buttonCancel;
}
