using FootballOrganizer.Forms;
using FootballOrganizer.Models;

namespace FootballOrganizer;

public partial class MainForm : Form
{
    private List<Tournament> tournaments = new List<Tournament>();

    public MainForm()
    {
        InitializeComponent();
        AddTestData();
        RefreshList();
    }

    // тимчасово, поки немає збереження у файл
    private void AddTestData()
    {
        League upl = new League("Українська Прем'єр-ліга", true);
        upl.AddTeam(new Team("Динамо", "Київ"));
        upl.AddTeam(new Team("Шахтар", "Донецьк"));
        upl.AddTeam(new Team("Дніпро-1", "Дніпро"));
        upl.AddTeam(new Team("Ворскла", "Полтава"));
        tournaments.Add(upl);

        Cup cup = new Cup("Кубок України", false);
        cup.AddTeam(new Team("Динамо", "Київ"));
        cup.AddTeam(new Team("Шахтар", "Донецьк"));
        cup.AddTeam(new Team("Зоря", "Луганськ"));
        cup.AddTeam(new Team("Олександрія", "Олександрія"));
        tournaments.Add(cup);
    }

    private void RefreshList()
    {
        listBoxTournaments.Items.Clear();
        foreach (Tournament t in tournaments)
            listBoxTournaments.Items.Add(t);

        if (listBoxTournaments.Items.Count > 0)
            listBoxTournaments.SelectedIndex = 0;
        else
            ShowInfo(null);
    }

    private void ShowInfo(Tournament? t)
    {
        listBoxTeams.Items.Clear();
        if (t == null)
        {
            labelInfo.Text = "Турнір не вибрано";
            return;
        }

        int played = t.Matches.Count(m => m.IsPlayed);
        labelInfo.Text = t.Name + "\n" +
            "Тип: " + t.TypeName + "\n" +
            "Матчі у відповідь: " + (t.HasReturnMatches ? "так" : "ні") + "\n" +
            "Команд: " + t.Teams.Count + "\n" +
            "Зіграно матчів: " + played + " з " + t.Matches.Count;

        foreach (Team team in t.Teams)
            listBoxTeams.Items.Add(team);
    }

    private void listBoxTournaments_SelectedIndexChanged(object sender, EventArgs e)
    {
        ShowInfo(listBoxTournaments.SelectedItem as Tournament);
    }

    private void buttonAdd_Click(object sender, EventArgs e)
    {
        TournamentForm form = new TournamentForm();
        if (form.ShowDialog(this) == DialogResult.OK && form.Result != null)
        {
            tournaments.Add(form.Result);
            RefreshList();
            listBoxTournaments.SelectedIndex = tournaments.Count - 1;
        }
    }

    private void buttonEdit_Click(object sender, EventArgs e)
    {
        if (listBoxTournaments.SelectedItem is not Tournament t)
            return;

        int index = listBoxTournaments.SelectedIndex;
        TournamentForm form = new TournamentForm(t);
        if (form.ShowDialog(this) == DialogResult.OK)
        {
            RefreshList();
            listBoxTournaments.SelectedIndex = index;
        }
    }

    private void buttonOpen_Click(object sender, EventArgs e)
    {
        if (listBoxTournaments.SelectedItem is League league)
        {
            LeagueForm form = new LeagueForm(league);
            form.ShowDialog(this);
            ShowInfo(league);
        }
        else if (listBoxTournaments.SelectedItem is Cup)
        {
            // TODO: зробити форму для кубка
            MessageBox.Show("Кубок поки в розробці", "Кубок", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }

    private void buttonDelete_Click(object sender, EventArgs e)
    {
        if (listBoxTournaments.SelectedItem is Tournament t)
        {
            tournaments.Remove(t);
            RefreshList();
        }
    }
}
