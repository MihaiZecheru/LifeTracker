namespace EntryEditor;

public partial class EntryEditor : Form
{
    private int SHORT_SUMMARY_MAX_LENGTH = 115;
    private bool RequestSentPreviously = false;

    /// <summary>
    /// For creating a new Entry
    /// </summary>
    public EntryEditor(string stringdate)
    {
        InitializeComponent();
        date_display.Text = stringdate;
        date_display.Location = new Point((this.Width - date_display.Width) / 2, date_display.Location.Y);
        ToolTips();
    }

    /// <summary>
    /// For editing an existing Entry
    /// </summary>
    public EntryEditor(string short_summary, string detailed_summary, string stringdate)
    {
        InitializeComponent();
        summary_entry.Text = short_summary;
        details_entry.Text = detailed_summary;
        date_display.Text = stringdate;
        date_display.Location = new Point((this.Width - date_display.Width) / 2, date_display.Location.Y);
        ToolTips();
    }

    /// <summary>
    /// Setup all tooltips on the form
    /// </summary>
    private void ToolTips()
    {
        ToolTip toolTip = new ToolTip();
        toolTip.SetToolTip(cancelbtn, "Cancel (Esc)");
        toolTip.SetToolTip(savebtn, "Save (Ctrl + S)");
        toolTip.SetToolTip(summary_entry, "Summary - what do you want to remember doing today?");
        toolTip.SetToolTip(details_entry, "Details - describe your day");
        toolTip.SetToolTip(summary_chars_counter, "Max characters for summary");
        toolTip.SetToolTip(date_display, "Date of entry");
    }

    private void EntryEditor_Load(object sender, EventArgs e)
    {
        this.BringToFront();
        this.Activate();
    }

    private void short_summary_entry_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Enter)
        {
            details_entry.Focus();
        }
    }

    private async void cancelbtn_Click(object sender, EventArgs e)
    {
        WaitScreen();
        await HttpServer.SendCancel();
        RequestSentPreviously = true;
        this.Close();
    }

    private async void savebtn_Click(object sender, EventArgs e)
    {
        if (summary_entry.Text.Length == 0)
        {
            summary_entry.Focus();
            return;
        }

        WaitScreen();

        await HttpServer.SendEntry(summary_entry.Text, details_entry.Text);
        RequestSentPreviously = true;
        this.Close();
    }

    private async void EntryEditor_FormClosed(object sender, FormClosedEventArgs e)
    {
        if (!RequestSentPreviously) await HttpServer.SendCancel();
    }

    private void cancelbtn_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Enter)
        {
            cancelbtn.PerformClick();
        }
    }

    private void savebtn_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Enter)
        {
            savebtn.PerformClick();
        }
    }

    private void short_summary_entry_TextChanged(object sender, EventArgs e)
    {
        summary_chars_counter.Text = summary_entry.TextLength + "/" + SHORT_SUMMARY_MAX_LENGTH;
    }

    private void WaitScreen()
    {
        Cursor = Cursors.WaitCursor;

        for (int i = 0; i < Controls.Count; i++)
        {
            Controls[i].Enabled = false;
        }
    }

    private void short_summary_entry_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.S && e.Control)
        {
            savebtn.PerformClick();
            e.Handled = true;
        }

        if (e.KeyCode == Keys.Escape)
        {
            cancelbtn.PerformClick();
            e.Handled = true;
        }
    }

    private void detailed_summary_entry_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.S && e.Control)
        {
            savebtn.PerformClick();
            e.SuppressKeyPress = true;
        }

        if (e.KeyCode == Keys.Escape)
        {
            cancelbtn.PerformClick();
            e.SuppressKeyPress = true;
        }
    }

    private void short_summary_entry_Enter(object sender, EventArgs e)
    {
        summary_entry.SelectionStart = summary_entry.TextLength;
    }

    private void detailed_summary_entry_Enter(object sender, EventArgs e)
    {
        details_entry.SelectionStart = details_entry.TextLength;
    }
}
