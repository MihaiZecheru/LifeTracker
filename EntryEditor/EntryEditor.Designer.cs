namespace EntryEditor
{
    partial class EntryEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EntryEditor));
            summary_entry = new TextBox();
            details_entry = new TextBox();
            cancelbtn = new Button();
            savebtn = new Button();
            date_display = new Label();
            summary_chars_counter = new Label();
            SuspendLayout();
            // 
            // summary_entry
            // 
            summary_entry.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            summary_entry.Location = new Point(12, 41);
            summary_entry.MaxLength = 115;
            summary_entry.Name = "summary_entry";
            summary_entry.PlaceholderText = "Summary - what do you want to remember doing today?";
            summary_entry.Size = new Size(776, 22);
            summary_entry.TabIndex = 0;
            summary_entry.TextChanged += short_summary_entry_TextChanged;
            summary_entry.Enter += short_summary_entry_Enter;
            summary_entry.KeyDown += short_summary_entry_KeyDown;
            summary_entry.KeyPress += short_summary_entry_KeyPress;
            // 
            // details_entry
            // 
            details_entry.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            details_entry.Location = new Point(12, 69);
            details_entry.Multiline = true;
            details_entry.Name = "details_entry";
            details_entry.PlaceholderText = "Details - describe your day";
            details_entry.Size = new Size(776, 332);
            details_entry.TabIndex = 1;
            details_entry.Enter += detailed_summary_entry_Enter;
            details_entry.KeyDown += detailed_summary_entry_KeyDown;
            // 
            // cancelbtn
            // 
            cancelbtn.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            cancelbtn.Location = new Point(12, 407);
            cancelbtn.Name = "cancelbtn";
            cancelbtn.Size = new Size(385, 31);
            cancelbtn.TabIndex = 2;
            cancelbtn.Text = "Cancel";
            cancelbtn.UseVisualStyleBackColor = true;
            cancelbtn.Click += cancelbtn_Click;
            cancelbtn.KeyPress += cancelbtn_KeyPress;
            // 
            // savebtn
            // 
            savebtn.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            savebtn.Location = new Point(403, 407);
            savebtn.Name = "savebtn";
            savebtn.Size = new Size(385, 31);
            savebtn.TabIndex = 3;
            savebtn.Text = "Save";
            savebtn.UseVisualStyleBackColor = true;
            savebtn.Click += savebtn_Click;
            savebtn.KeyPress += savebtn_KeyPress;
            // 
            // date_display
            // 
            date_display.AutoSize = true;
            date_display.Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Regular, GraphicsUnit.Point);
            date_display.Location = new Point(345, 9);
            date_display.Name = "date_display";
            date_display.Size = new Size(110, 24);
            date_display.TabIndex = 4;
            date_display.Text = "May 4, 2023";
            // 
            // summary_chars_counter
            // 
            summary_chars_counter.AutoSize = true;
            summary_chars_counter.Location = new Point(12, 23);
            summary_chars_counter.Name = "summary_chars_counter";
            summary_chars_counter.Size = new Size(36, 15);
            summary_chars_counter.TabIndex = 5;
            summary_chars_counter.Text = "0/115";
            // 
            // EntryEditor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(801, 450);
            Controls.Add(summary_chars_counter);
            Controls.Add(date_display);
            Controls.Add(savebtn);
            Controls.Add(cancelbtn);
            Controls.Add(details_entry);
            Controls.Add(summary_entry);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "EntryEditor";
            Text = "Entry Editor";
            FormClosed += EntryEditor_FormClosed;
            Load += EntryEditor_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox summary_entry;
        private TextBox details_entry;
        private Button cancelbtn;
        private Button savebtn;
        private Label date_display;
        private Label summary_chars_counter;
    }
}
