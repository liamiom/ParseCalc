namespace Gui;

public partial class TabControl : UserControl
{
    public event EventHandler TabSelected;
    public TabButtonControl SelectedTab;
    private List<TabButtonControl> _buttonList = [];

    public List<TabButtonControl> Items => 
        _buttonList;

    public TabControl()
    {
        InitializeComponent();
        Initialize();
        SelectedTab = _buttonList[0];
    }

    public void LoadSaveFile(List<string> SaveFileData)
    {
        bool first = true;
        foreach (string item in SaveFileData)
        {
            if (!first)
            {
                AddNewButton();
            }
            else
            {
                first = false;
            }
            
            _buttonList[_buttonList.Count - 1].Tag = item;
            TabButton_Click(_buttonList[_buttonList.Count - 1], EventArgs.Empty);
            NewButton_TabSelected(_buttonList[_buttonList.Count - 1], EventArgs.Empty);
        }
    }
    
    private void Initialize()
    {
        AddNewButton();
        SelectButton(_buttonList[0]);
    }

    private void AddNewButton()
    {
        TabButtonControl newButton = new TabButtonControl()
        {
            BackColor = Color.DimGray,
            Location = new Point(3, 3),
            Name = "tabButtonControl",
            Size = new Size(50, 21),
            TabName = "Tab " + (_buttonList.Count + 1).ToString()
        };
        
        newButton.Click += TabButton_Click;
        newButton.TabSelected += NewButton_TabSelected;
        _buttonList.Add(newButton);
        RedrawButtons();
    }

    private void RedrawButtons()
    {
        flowLayoutPanel1.SuspendLayout();
        SuspendLayout();

        int i = 0;
        foreach (TabButtonControl item in _buttonList)
        {
            item.Location = new Point(3 + (i * item.Size.Width), 3);
            item.Visible = true;

            i++;
        }

        flowLayoutPanel1.Controls.AddRange(_buttonList.ToArray());

        ResumeLayout(true);
        if (_buttonList.Count == 1)
        {
            _buttonList[0].Visible = false;
        }
    }

    private void SelectButton(object sender)
    {
        SelectedTab = (TabButtonControl)sender;
        foreach (TabButtonControl item in _buttonList)
        {
            item.BackColor = item == SelectedTab
                ? Color.Gray
                : Color.DimGray;
        }
    }

    private void TabButton_Click(object sender, EventArgs e)
    {
        foreach (TabButtonControl item in _buttonList)
        {
            item.Selected = false;
        }

        ((TabButtonControl)sender).Selected = true;
    }

    private void NewButton_TabSelected(object sender, EventArgs e)
    {
        SelectButton(sender);
        TabSelected?.Invoke(sender, e);
    }

    private void flowLayoutPanel1_DoubleClick(object sender, EventArgs e) => 
        AddNewButton();
}
