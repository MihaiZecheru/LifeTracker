namespace EntryEditor;

public partial class EntryEditor : Form
{
    private int SHORT_SUMMARY_MAX_LENGTH = 115;
    private int DETAILED_SUMMARY_MAX_LINE_LENGTH = 115;
    private int DETAILED_SUMMARY_MAX_LINES = 14;
    private bool RequestSentPreviously = false;

    /// <summary>
    /// For creating a new Entry
    /// </summary>
    public EntryEditor()
    {
        InitializeComponent();
    }

    /// <summary>
    /// For editing an existing Entry
    /// </summary>
    public EntryEditor(string short_summary, string detailed_summary)
    {
        InitializeComponent();
        short_summary_entry.Text = short_summary;
        detailed_summary_entry.Text = detailed_summary;
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
            detailed_summary_entry.Focus();
        }
    }

    private void detailed_summary_entry_KeyPress(object sender, KeyPressEventArgs e)
    {
        for (int i = 0; i < detailed_summary_entry.Lines.Length; i++)
        {
            if (detailed_summary_entry.Lines[i].Length > DETAILED_SUMMARY_MAX_LINE_LENGTH)
            {
                detailed_summary_entry.Text = detailed_summary_entry.Text.Remove(detailed_summary_entry.GetFirstCharIndexFromLine(i) + detailed_summary_entry.Lines[i].Length - 1);
                detailed_summary_entry.SelectionStart = detailed_summary_entry.GetFirstCharIndexFromLine(i) + detailed_summary_entry.Lines[i].Length;
            }
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
        if (short_summary_entry.Text.Length == 0)
        {
            short_summary_entry.Focus();
            return;
        }

        WaitScreen();

        await HttpServer.SendEntry(short_summary_entry.Text, detailed_summary_entry.Text);
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
        short_summary_chars_counter.Text = short_summary_entry.TextLength + "/" + SHORT_SUMMARY_MAX_LENGTH;
    }

    private void detailed_summary_entry_TextChanged(object sender, EventArgs e)
    {
        detailed_summary_chars_counter.Text = detailed_summary_entry.Lines.Length + "/" + DETAILED_SUMMARY_MAX_LINES;
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

        if (e.KeyCode == Keys.Enter && detailed_summary_entry.Lines.Length >= DETAILED_SUMMARY_MAX_LINES)
        {
            e.SuppressKeyPress = true;
        }
    }
}
