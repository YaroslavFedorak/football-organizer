using FootballOrganizer.Models;

namespace FootballOrganizer.Forms;

// форма для створення нового турніру або редагування існуючого
public partial class TournamentForm : Form
{
    private Tournament? editing;
    private List<Team> teams = new List<Team>();

    // сюди кладемо результат після натискання ОК
    public Tournament? Result { get; private set; }

    public TournamentForm()
    {
        InitializeComponent();
        comboBoxType.SelectedIndex = 0;
        Text = "Новий турнір";
    }

    public TournamentForm(Tournament tournament) : this()
    {
        editing = tournament;
        Text = "Редагування турніру";

        textBoxName.Text = tournament.Name;
        checkBoxReturn.Checked = tournament.HasReturnMatches;
        comboBoxType.SelectedIndex = tournament is Cup ? 1 : 0;
        // тип вже створеного турніру міняти не можна
        comboBoxType.Enabled = false;

        teams.AddRange(tournament.Teams);
        RefreshTeams();
    }

    private void RefreshTeams()
    {
        listBoxTeams.Items.Clear();
        foreach (Team t in teams)
            listBoxTeams.Items.Add(t);
        labelCount.Text = "Команд: " + teams.Count;
    }

    private void buttonAddTeam_Click(object sender, EventArgs e)
    {
        string name = textBoxTeam.Text.Trim();
        if (name == "")
        {
            MessageBox.Show("Введіть назву команди", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        // однакові назви плутають таблицю і статистику
        if (teams.Any(t => string.Equals(t.Name, name, StringComparison.OrdinalIgnoreCase)))
        {
            MessageBox.Show("Команда з такою назвою вже є в списку", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            textBoxTeam.SelectAll();
            textBoxTeam.Focus();
            return;
        }

        teams.Add(new Team(name, textBoxCity.Text.Trim()));
        RefreshTeams();

        textBoxTeam.Clear();
        textBoxCity.Clear();
        textBoxTeam.Focus();
    }

    private void buttonRemoveTeam_Click(object sender, EventArgs e)
    {
        if (listBoxTeams.SelectedItem is Team team)
        {
            teams.Remove(team);
            RefreshTeams();
        }
    }

    private void buttonOk_Click(object sender, EventArgs e)
    {
        string name = textBoxName.Text.Trim();
        if (name == "")
        {
            MessageBox.Show("Введіть назву турніру", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }
        if (teams.Count < 2)
        {
            MessageBox.Show("У турнірі має бути хоча б 2 команди", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        Tournament t;
        if (editing != null)
        {
            // календар складений під старий склад команд - якщо склад змінився, його треба скласти заново
            bool teamsChanged = teams.Count != editing.Teams.Count || teams.Any(team => !editing.Teams.Contains(team));
            bool returnChanged = checkBoxReturn.Checked != editing.HasReturnMatches;
            if (editing.Matches.Count > 0 && (teamsChanged || returnChanged))
            {
                DialogResult answer = MessageBox.Show("Склад команд або формат турніру змінився, тому календар і всі результати буде видалено. Продовжити?",
                    "Зміна турніру", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (answer != DialogResult.Yes)
                    return;
                editing.ClearSchedule();
            }

            t = editing;
            t.Name = name;
            t.HasReturnMatches = checkBoxReturn.Checked;
            t.Teams.Clear();
        }
        else if (comboBoxType.SelectedIndex == 0)
            t = new League(name, checkBoxReturn.Checked);
        else
            t = new Cup(name, checkBoxReturn.Checked);

        foreach (Team team in teams)
            t.AddTeam(team);

        Result = t;
        DialogResult = DialogResult.OK;
        Close();
    }
}
