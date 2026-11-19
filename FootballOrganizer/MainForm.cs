using FootballOrganizer.Data;
using FootballOrganizer.Forms;
using FootballOrganizer.Models;

namespace FootballOrganizer;

public partial class MainForm : Form
{
    private List<Tournament> tournaments = new List<Tournament>();
    private string currentPath = Storage.DefaultPath;

    public MainForm()
    {
        InitializeComponent();
        LoadOnStart();
        RefreshList();
    }

    private void LoadOnStart()
    {
        if (!File.Exists(currentPath))
        {
            // перший запуск - показуємо приклад
            AddTestData();
            return;
        }

        try
        {
            tournaments = Storage.Load(currentPath);
        }
        catch (Exception ex)
        {
            MessageBox.Show("Не вдалося прочитати файл з турнірами:\n" + ex.Message, "Помилка",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    // приклад турнірів для першого запуску
    private void AddTestData()
    {
        League upl = new League("Українська Прем'єр-ліга", true);
        upl.AddTeam(new Team("Динамо", "Київ"));
        upl.AddTeam(new Team("Шахтар", "Донецьк"));
        upl.AddTeam(new Team("Дніпро-1", "Дніпро"));
        upl.AddTeam(new Team("Ворскла", "Полтава"));
        upl.AddTeam(new Team("Зоря", "Луганськ"));
        upl.AddTeam(new Team("Полісся", "Житомир"));
        upl.GenerateSchedule();
        // результати перших 6 турів, щоб було що подивитись в таблиці і статистиці
        Random rnd = new Random(7);
        foreach (Match m in upl.Matches)
        {
            if (m.Round <= 6)
                m.SetResult(rnd.Next(0, 5), rnd.Next(0, 4));
        }
        tournaments.Add(upl);

        Cup cup = new Cup("Кубок України", false);
        cup.AddTeam(new Team("Динамо", "Київ"));
        cup.AddTeam(new Team("Шахтар", "Донецьк"));
        cup.AddTeam(new Team("Зоря", "Луганськ"));
        cup.AddTeam(new Team("Олександрія", "Олександрія"));
        cup.AddTeam(new Team("Кривбас", "Кривий Ріг"));
        cup.AddTeam(new Team("Ворскла", "Полтава"));
        // 6 команд - дві пройдуть в 1/2 без гри
        cup.GenerateSchedule();
        // перший раунд зіграний повністю (один матч по пенальті), і один півфінал,
        // щоб у сітці було видно як команди проходять далі
        bool penaltyShown = false;
        foreach (CupTie tie in cup.Rounds[0])
        {
            if (tie.IsBye)
                continue;
            if (!penaltyShown)
            {
                tie.Matches[0].SetResult(1, 1);
                tie.PenaltyWinner = tie.TeamB;
                penaltyShown = true;
            }
            else
                tie.Matches[0].SetResult(rnd.Next(2, 4), rnd.Next(0, 2));
        }
        cup.AdvanceRound();
        cup.Rounds[1][0].Matches[0].SetResult(2, 1);
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
        else if (listBoxTournaments.SelectedItem is Cup cup)
        {
            CupForm form = new CupForm(cup);
            form.ShowDialog(this);
            ShowInfo(cup);
        }
    }

    private void buttonStats_Click(object sender, EventArgs e)
    {
        if (listBoxTournaments.SelectedItem is Tournament t)
        {
            TeamStatsForm form = new TeamStatsForm(t);
            form.ShowDialog(this);
        }
    }

    // ---------------- меню Файл ----------------

    private bool SaveTo(string path)
    {
        try
        {
            Storage.Save(path, tournaments);
            currentPath = path;
            return true;
        }
        catch (Exception ex)
        {
            MessageBox.Show("Не вдалося зберегти:\n" + ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }
    }

    private void menuOpen_Click(object sender, EventArgs e)
    {
        using OpenFileDialog dialog = new OpenFileDialog();
        dialog.Filter = "Турніри (*.json)|*.json|Всі файли (*.*)|*.*";
        if (dialog.ShowDialog(this) != DialogResult.OK)
            return;

        try
        {
            tournaments = Storage.Load(dialog.FileName);
            currentPath = dialog.FileName;
            RefreshList();
        }
        catch (Exception ex)
        {
            MessageBox.Show("Не вдалося відкрити файл:\n" + ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void menuSave_Click(object sender, EventArgs e)
    {
        if (SaveTo(currentPath))
            MessageBox.Show("Збережено у файл\n" + currentPath, "Збереження", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void menuSaveAs_Click(object sender, EventArgs e)
    {
        using SaveFileDialog dialog = new SaveFileDialog();
        dialog.Filter = "Турніри (*.json)|*.json";
        dialog.FileName = "tournaments.json";
        if (dialog.ShowDialog(this) == DialogResult.OK)
            SaveTo(dialog.FileName);
    }

    private void menuExit_Click(object sender, EventArgs e)
    {
        Close();
    }

    // при закритті програми все зберігається автоматично
    private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        SaveTo(currentPath);
    }

    private void buttonDelete_Click(object sender, EventArgs e)
    {
        if (listBoxTournaments.SelectedItem is Tournament t)
        {
            DialogResult answer = MessageBox.Show("Видалити турнір \"" + t.Name + "\" разом з усіма результатами?",
                "Видалення", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (answer != DialogResult.Yes)
                return;

            tournaments.Remove(t);
            RefreshList();
        }
    }
}
