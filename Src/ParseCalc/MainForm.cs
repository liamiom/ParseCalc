namespace ParseCalc;

public partial class MainForm : Form
{
    private readonly List<Label> _labelList = new List<Label>();
    private List<decimal?> _results = new List<decimal?>();

    private dynamic ResultBox => new
    {
        Top = 0,
        Left = CalcTextBox.Width - 220,
        ItemHeight = 18,
        ItemWidth = Width - CalcTextBox.Width
    };

    public MainForm()
    {
        InitializeComponent();
        tabControl1.TabSelected += TabControl1_TabSelected;
    }

    private void RefreshResults()
    {
        _results = Math.Parse(CalcTextBox.Text).ToList();
        Refresh();
    }

    private void ReDrawResults()
    {
        int count = 0;
        decimal result = 0;
        foreach (decimal? item in _results)
        {
            int top = ResultBox.Top + (ResultBox.ItemHeight * count);
            if (item.HasValue)
            {
                DrawString(item.Value.ToShortFormat(), top, ResultBox.Left + item.Value.LeftPadding());
                result += item.Value;
            }

            count++;
        }

        ResultTextBox.Text = _results.Count != 0
            ? result.ToShortFormat()
            : string.Empty;
    }

    public void DrawString(string StringText, float y, float x)
    {
        Graphics formGraphics = CalcTextBox.CreateGraphics();
        Font drawFont = new("Inconsolata", 11);
        SolidBrush drawBrush = new(Color.Black);
        StringFormat drawFormat = new();
        formGraphics.DrawString(StringText, drawFont, drawBrush, x, y, drawFormat);
        drawFont.Dispose();
        drawBrush.Dispose();
        formGraphics.Dispose();
    }

    public void OpenHelp()
    {
        HelpLabel.Dock = DockStyle.Fill;
        HelpLabel.Visible = true;
    }

    public void OpenSaveList()
    {
        SaveFilesListBox.Items.Clear();
        SaveFilesListBox.Items.AddRange(SaveFile.SaveFileList);
        SaveFilesListBox.Dock = DockStyle.Fill;
        SaveFilesListBox.Visible = true;
    }

    public void CloseSaveList()
    {
        SaveFilesListBox.Visible = false;
        HelpLabel.Visible = false;
    }

    public void LoadSaveFile()
    {
        if (SaveFilesListBox.SelectedItem == null)
        {
            return;
        }

        SaveFile.Load((string)SaveFilesListBox.SelectedItem, ref tabControl1);
        SaveFilesListBox.Visible = false;
    }

    private void CalcTextBox_TextChanged(object sender, EventArgs e)
    {
        RefreshResults();
        ReDrawResults();
        tabControl1.SelectedTab.Tag = CalcTextBox.Text;
        SaveFile.ContentChanged = true;
    }

    private void Form1_Load(object sender, EventArgs e) => 
        _ = CalcTextBox.Focus();

    private void Form1_ResizeEnd(object sender, EventArgs e)
    {
        RefreshResults();
        ReDrawResults();
    }

    private void Form1_Paint(object sender, PaintEventArgs e) => 
        ReDrawResults();

    private void Form1_Leave(object sender, EventArgs e) => 
        ReDrawResults();

    private void RefreshTimer_Tick(object sender, EventArgs e) => 
        ReDrawResults();

    private void TabControl1_TabSelected(object sender, EventArgs e)
    {
        CalcTextBox.Text = (string)tabControl1.SelectedTab.Tag;
    }

    private void Form1_FormClosed(object sender, FormClosedEventArgs e) => 
        SaveFile.Save(tabControl1);

    private void CalcTextBox_KeyUp(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.F1)
        {
            OpenHelp();
        }
        if (e.KeyCode == Keys.F2)
        {
            OpenSaveList();
        }
        if (e.KeyCode == Keys.Escape)
        {
            CloseSaveList();
        }
    }

    private void SaveFilesListBox_DoubleClick(object sender, EventArgs e) => 
        LoadSaveFile();
}