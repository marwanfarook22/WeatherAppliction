namespace WeatherAppliction
{
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
            flowLayoutPanel1 = new FlowLayoutPanel();
            title = new Label();
            CityNameBox = new ComboBox();
            addCityButton = new Button();
            flowLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.BackColor = SystemColors.ActiveCaption;
            flowLayoutPanel1.Controls.Add(title);
            flowLayoutPanel1.Controls.Add(CityNameBox);
            flowLayoutPanel1.Controls.Add(addCityButton);
            flowLayoutPanel1.Dock = DockStyle.Top;
            flowLayoutPanel1.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanel1.Location = new Point(0, 0);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(1360, 54);
            flowLayoutPanel1.TabIndex = 0;
            // 
            // title
            // 
            title.AutoSize = true;
            title.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            title.Location = new Point(0, 0);
            title.Margin = new Padding(0);
            title.Name = "title";
            title.Size = new Size(468, 54);
            title.TabIndex = 1;
            title.Text = "🌤️ Weather Dashboard";
            // 
            // CityNameBox
            // 
            CityNameBox.BackColor = Color.White;
            CityNameBox.FormattingEnabled = true;
            CityNameBox.Location = new Point(471, 3);
            CityNameBox.Name = "CityNameBox";
            CityNameBox.Size = new Size(516, 28);
            CityNameBox.Sorted = true;
            CityNameBox.TabIndex = 0;
            CityNameBox.Text = "City Names";
            CityNameBox.TextChanged += CityNameBox_TextChanged;
            // 
            // addCityButton
            // 
            addCityButton.BackColor = Color.FromArgb(76, 175, 80);
            addCityButton.Cursor = Cursors.Hand;
            addCityButton.FlatStyle = FlatStyle.Flat;
            addCityButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            addCityButton.ForeColor = Color.White;
            addCityButton.Location = new Point(993, 3);
            addCityButton.Name = "addCityButton";
            addCityButton.Size = new Size(167, 30);
            addCityButton.TabIndex = 4;
            addCityButton.Text = "Search city";
            addCityButton.UseVisualStyleBackColor = false;
            addCityButton.Click += addCityButton_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            AutoSize = true;
            ClientSize = new Size(1360, 921);
            Controls.Add(flowLayoutPanel1);
            Name = "Form1";
            Text = "Form1";
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private FlowLayoutPanel flowLayoutPanel1;
        private Label title;
        private Button addCityButton;
        private ComboBox CityNameBox;
    }
}
