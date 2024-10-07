namespace Gui;

public partial class TabButtonControl : UserControl
{
    public event EventHandler TabSelected;
    public bool Selected { get; set; }

    private string? _startingName = null;
    public string TabName
    {
        get
        {
            return NameLabel.Text;
        }
        set
        {
            _startingName ??= value;
            NameLabel.Text = value;
        }
    }

    public TabButtonControl()
    {
        InitializeComponent();
        NameLabel.Dock = DockStyle.Fill;
        NameTextBox.Dock = DockStyle.Fill;
    }
    
    public void SetName()
    {
        NameTextBox.Visible = false;
        NameLabel.Text = string.IsNullOrWhiteSpace(NameTextBox.Text) 
            ? _startingName 
            : NameTextBox.Text;
    }

    private void NameLabel_DoubleClick(object sender, EventArgs e)
    {
        if (NameLabel.Text != _startingName)
        {
            NameTextBox.Text = NameLabel.Text;
        }

        NameTextBox.Visible = true;
    }

    private void NameLabel_Click(object sender, EventArgs e) => 
        TabSelected?.Invoke(this, new EventArgs());

    private void NameTextBox_Leave(object sender, EventArgs e) => 
        SetName();

    private void NameTextBox_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)13)
        {
            SetName();
        }
    }
}
