using WeatherAppliction.CardSection;
using WeatherAppliction.ForcastingDataClass;

namespace WeatherAppliction
{
    partial class ForcastingForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            flowLayoutPanel1 = new FlowLayoutPanel();
            SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Dock = DockStyle.Fill;
            flowLayoutPanel1.Location = new Point(0, 0);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.AutoScroll = true;
            flowLayoutPanel1.TabIndex = 0;
            // 
            // ForcastingForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1198, 672);
            Controls.Add(flowLayoutPanel1);
            Name = "ForcastingForm";
            Text = "ForcastingForm";
            ResumeLayout(false);




        }


        private Button button1;
        #endregion

        private void LoadData(IReadOnlyList<List> lists)
        {
            // Find the FlowLayoutPanel in the form's controls
            var flowLayoutPanel = this.Controls.OfType<FlowLayoutPanel>().FirstOrDefault();
            if (flowLayoutPanel == null) return;

            foreach (var item in lists)
            {
                var card = new SimpleForecastCard();
                card.CreateCard(item);
                flowLayoutPanel.Controls.Add(card); // Add to FlowLayoutPanel
            }

        }
        private FlowLayoutPanel flowLayoutPanel1;
    }
}