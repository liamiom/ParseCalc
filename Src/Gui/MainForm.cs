using ParseCalc;

namespace Gui;

public partial class MainForm : Form
{
    private List<decimal?> _results = [];

    private (int Top, int Left, int ItemHeight, int ItemWidth) ResultBox => 
    (
        Top: 0,
        Left: CalcTextBox.Width - 220,
        ItemHeight: 18,
        ItemWidth: Width - CalcTextBox.Width
    );

    private (int Top, int Left, int ItemHeight, int ItemWidth) GetResultBox(int index, int leftPadding) =>
    (
        Top: 18 * index,
        Left: CalcTextBox.Width - 220 + leftPadding,
        ItemHeight: 18,
        ItemWidth: Width - CalcTextBox.Width
    );

    public MainForm()
    {
        InitializeComponent();
        tabControl1.TabSelected += TabControl1_TabSelected;
    }

    private void RefreshResults()
    {
        _results = ParseCalc.Math.Parse(CalcTextBox.Text).ToList();
        Refresh();
    }

    private void ReDrawResults()
    {
        for (int i = 0; i < _results.Count; i++)
        {
            if (_results[i].HasValue)
            {
                decimal value = _results[i] ?? 0;
                var box = GetResultBox(index: i, leftPadding: value.LeftPadding());
                DrawString(value.ToShortFormat(), box.Top, box.Left);
            }
        }

        int longestDecimals = _results.Any()
            ? _results.Select(i => (i ?? 0).ToDecimals().Length).Max()
            : 0;
        string resultFormat = $"n{longestDecimals}";

        decimal result = _results.Sum(i => i ?? 0);
        ResultTextBox.Text = _results.Count != 0
            ? result.ToString(resultFormat)
            : string.Empty;
    }

    public void DrawString(string StringText, float y, float x)
    {
        Graphics formGraphics = CalcTextBox.CreateGraphics();
        Font drawFont = new("Courier New", 13);
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
        else if (e.KeyCode == Keys.F2)
        {
            OpenSaveList();
        }
        else if (e.KeyCode == Keys.Escape)
        {
            CloseSaveList();
        }
    }

    private void SaveFilesListBox_DoubleClick(object sender, EventArgs e) => 
        LoadSaveFile();
}