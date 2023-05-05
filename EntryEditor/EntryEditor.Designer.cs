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
            short_summary_entry = new TextBox();
            detailed_summary_entry = new TextBox();
            cancelbtn = new Button();
            savebtn = new Button();
            date_display = new Label();
            short_summary_chars_counter = new Label();
            detailed_summary_chars_counter = new Label();
            SuspendLayout();
            // 
            // short_summary_entry
            // 
            short_summary_entry.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            short_summary_entry.Location = new Point(12, 41);
            short_summary_entry.MaxLength = 115;
            short_summary_entry.Name = "short_summary_entry";
            short_summary_entry.PlaceholderText = "One sentence summary - what was the main thing you did today?";
            short_summary_entry.Size = new Size(776, 22);
            short_summary_entry.TabIndex = 0;
            short_summary_entry.TextChanged += short_summary_entry_TextChanged;
            short_summary_entry.Enter += short_summary_entry_Enter;
            short_summary_entry.KeyDown += short_summary_entry_KeyDown;
            short_summary_entry.KeyPress += short_summary_entry_KeyPress;
            // 
            // detailed_summary_entry
            // 
            detailed_summary_entry.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            detailed_summary_entry.Location = new Point(12, 69);
            detailed_summary_entry.Multiline = true;
            detailed_summary_entry.Name = "detailed_summary_entry";
            detailed_summary_entry.PlaceholderText = "Details - include anything you might want to remember";
            detailed_summary_entry.Size = new Size(776, 332);
            detailed_summary_entry.TabIndex = 1;
            detailed_summary_entry.TextChanged += detailed_summary_entry_TextChanged;
            detailed_summary_entry.Enter += detailed_summary_entry_Enter;
            detailed_summary_entry.KeyDown += detailed_summary_entry_KeyDown;
            detailed_summary_entry.KeyPress += detailed_summary_entry_KeyPress;
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
            // short_summary_chars_counter
            // 
            short_summary_chars_counter.AutoSize = true;
            short_summary_chars_counter.Location = new Point(12, 23);
            short_summary_chars_counter.Name = "short_summary_chars_counter";
            short_summary_chars_counter.Size = new Size(36, 15);
            short_summary_chars_counter.TabIndex = 5;
            short_summary_chars_counter.Text = "0/115";
            // 
            // detailed_summary_chars_counter
            // 
            detailed_summary_chars_counter.AutoSize = true;
            detailed_summary_chars_counter.Location = new Point(758, 23);
            detailed_summary_chars_counter.Name = "detailed_summary_chars_counter";
            detailed_summary_chars_counter.Size = new Size(30, 15);
            detailed_summary_chars_counter.TabIndex = 6;
            detailed_summary_chars_counter.Text = "0/14";
            detailed_summary_chars_counter.TextAlign = ContentAlignment.TopRight;
            // 
            // EntryEditor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(801, 450);
            Controls.Add(detailed_summary_chars_counter);
            Controls.Add(short_summary_chars_counter);
            Controls.Add(date_display);
            Controls.Add(savebtn);
            Controls.Add(cancelbtn);
            Controls.Add(detailed_summary_entry);
            Controls.Add(short_summary_entry);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "EntryEditor";
            Text = "Entry Editor";
            FormClosed += EntryEditor_FormClosed;
            Load += EntryEditor_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox short_summary_entry;
        private TextBox detailed_summary_entry;
        private Button cancelbtn;
        private Button savebtn;
        private Label date_display;
        private Label short_summary_chars_counter;
        private Label detailed_summary_chars_counter;
    }
}
