namespace ParseCalc;

partial class MainForm
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        this.components = new System.ComponentModel.Container();
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
        this.CalcTextBox = new System.Windows.Forms.TextBox();
        this.ResultTextBox = new System.Windows.Forms.TextBox();
        this.RefreshTimer = new System.Windows.Forms.Timer(this.components);
        this.SaveFilesListBox = new System.Windows.Forms.ListBox();
        this.tabControl1 = new ParseCalc.TabControl();
        this.HelpLabel = new System.Windows.Forms.Label();
        this.SuspendLayout();
        // 
        // CalcTextBox
        // 
        this.CalcTextBox.AcceptsReturn = true;
        this.CalcTextBox.AcceptsTab = true;
        this.CalcTextBox.AllowDrop = true;
        this.CalcTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
        | System.Windows.Forms.AnchorStyles.Left)
        | System.Windows.Forms.AnchorStyles.Right)));
        this.CalcTextBox.BackColor = System.Drawing.Color.DimGray;
        this.CalcTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
        this.CalcTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.CalcTextBox.ForeColor = System.Drawing.Color.WhiteSmoke;
        this.CalcTextBox.Location = new System.Drawing.Point(12, 30);
        this.CalcTextBox.Multiline = true;
        this.CalcTextBox.Name = "CalcTextBox";
        this.CalcTextBox.Size = new System.Drawing.Size(528, 461);
        this.CalcTextBox.TabIndex = 0;
        this.CalcTextBox.TextChanged += new System.EventHandler(this.CalcTextBox_TextChanged);
        this.CalcTextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.CalcTextBox_KeyUp);
        // 
        // ResultTextBox
        // 
        this.ResultTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
        | System.Windows.Forms.AnchorStyles.Right)));
        this.ResultTextBox.BackColor = System.Drawing.Color.DimGray;
        this.ResultTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
        this.ResultTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.ResultTextBox.ForeColor = System.Drawing.Color.LightSkyBlue;
        this.ResultTextBox.Location = new System.Drawing.Point(12, 458);
        this.ResultTextBox.Name = "ResultTextBox";
        this.ResultTextBox.Size = new System.Drawing.Size(528, 22);
        this.ResultTextBox.TabIndex = 1;
        this.ResultTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
        // 
        // RefreshTimer
        // 
        this.RefreshTimer.Enabled = true;
        this.RefreshTimer.Interval = 10;
        this.RefreshTimer.Tick += new System.EventHandler(this.RefreshTimer_Tick);
        // 
        // SaveFilesListBox
        // 
        this.SaveFilesListBox.FormattingEnabled = true;
        this.SaveFilesListBox.Location = new System.Drawing.Point(0, 225);
        this.SaveFilesListBox.Name = "SaveFilesListBox";
        this.SaveFilesListBox.Size = new System.Drawing.Size(192, 264);
        this.SaveFilesListBox.TabIndex = 3;
        this.SaveFilesListBox.Visible = false;
        this.SaveFilesListBox.DoubleClick += new System.EventHandler(this.SaveFilesListBox_DoubleClick);
        // 
        // tabControl1
        // 
        this.tabControl1.BackColor = System.Drawing.Color.DimGray;
        this.tabControl1.Dock = System.Windows.Forms.DockStyle.Top;
        this.tabControl1.Location = new System.Drawing.Point(0, 0);
        this.tabControl1.Margin = new System.Windows.Forms.Padding(0);
        this.tabControl1.Name = "tabControl1";
        this.tabControl1.Size = new System.Drawing.Size(552, 27);
        this.tabControl1.TabIndex = 2;
        // 
        // HelpLabel
        // 
        this.HelpLabel.Location = new System.Drawing.Point(198, 225);
        this.HelpLabel.Name = "HelpLabel";
        this.HelpLabel.Size = new System.Drawing.Size(100, 264);
        this.HelpLabel.TabIndex = 4;
        this.HelpLabel.Text = "Save\r\nPress F2 to open a saved calc.\r\nThe Calc will be saved automatically when P" +
"arse is closed.\r\n\r\nVariables\r\nSet a variable by setting the variable name the th" +
"e value. Like so.\r\nTestVar = 1234\r\n";
        this.HelpLabel.Visible = false;
        // 
        // Form1
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.BackColor = System.Drawing.Color.DimGray;
        this.ClientSize = new System.Drawing.Size(552, 490);
        this.Controls.Add(this.HelpLabel);
        this.Controls.Add(this.SaveFilesListBox);
        this.Controls.Add(this.tabControl1);
        this.Controls.Add(this.ResultTextBox);
        this.Controls.Add(this.CalcTextBox);
        this.Name = "Form1";
        this.Text = "Parse";
        this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
        this.Load += new System.EventHandler(this.Form1_Load);
        this.ResizeEnd += new System.EventHandler(this.Form1_ResizeEnd);
        this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
        this.Leave += new System.EventHandler(this.Form1_Leave);
        this.ResumeLayout(false);
        this.PerformLayout();
    }

    #endregion

    private System.Windows.Forms.TextBox CalcTextBox;
    private System.Windows.Forms.TextBox ResultTextBox;
    private System.Windows.Forms.Timer RefreshTimer;
    private TabControl tabControl1;
    private System.Windows.Forms.ListBox SaveFilesListBox;
    private System.Windows.Forms.Label HelpLabel;
}
