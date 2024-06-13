namespace wordsearch
{
	partial class Form1
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
			components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
			imageList1 = new ImageList(components);
			printDocument1 = new System.Drawing.Printing.PrintDocument();
			printDialog1 = new PrintDialog();
			toolStrip1 = new ToolStrip();
			generateButton = new ToolStripButton();
			printButton = new ToolStripButton();
			helpButton = new ToolStripButton();
			toolStripDropDownButton1 = new ToolStripDropDownButton();
			regularToolStripMenuItem = new ToolStripMenuItem();
			animalsToolStripMenuItem = new ToolStripMenuItem();
			foodToolStripMenuItem = new ToolStripMenuItem();
			fruitsToolStripMenuItem = new ToolStripMenuItem();
			vegetablesToolStripMenuItem = new ToolStripMenuItem();
			countriesToolStripMenuItem = new ToolStripMenuItem();
			toolStrip1.SuspendLayout();
			SuspendLayout();
			// 
			// imageList1
			// 
			imageList1.ColorDepth = ColorDepth.Depth32Bit;
			imageList1.ImageStream = (ImageListStreamer)resources.GetObject("imageList1.ImageStream");
			imageList1.TransparentColor = Color.Transparent;
			imageList1.Images.SetKeyName(0, "notChosen.png");
			imageList1.Images.SetKeyName(1, "chosen.png");
			// 
			// printDocument1
			// 
			printDocument1.PrintPage += printDocument1_PrintPage;
			// 
			// printDialog1
			// 
			printDialog1.UseEXDialog = true;
			// 
			// toolStrip1
			// 
			toolStrip1.BackColor = SystemColors.ActiveBorder;
			toolStrip1.Items.AddRange(new ToolStripItem[] { generateButton, printButton, helpButton, toolStripDropDownButton1 });
			toolStrip1.Location = new Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new Size(600, 25);
			toolStrip1.TabIndex = 1;
			toolStrip1.Text = "toolStrip1";
			// 
			// generateButton
			// 
			generateButton.BackColor = SystemColors.ButtonShadow;
			generateButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
			generateButton.ImageTransparentColor = Color.Magenta;
			generateButton.Margin = new Padding(2, 1, 2, 1);
			generateButton.Name = "generateButton";
			generateButton.Size = new Size(124, 23);
			generateButton.Text = "Generate Wordsearch";
			generateButton.ToolTipText = "Generate a new wordsearch";
			generateButton.Click += generateButton_Click;
			// 
			// printButton
			// 
			printButton.BackColor = SystemColors.ButtonShadow;
			printButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
			printButton.ImageTransparentColor = Color.Magenta;
			printButton.Margin = new Padding(2, 1, 2, 1);
			printButton.Name = "printButton";
			printButton.Size = new Size(102, 23);
			printButton.Text = "Print Wordsearch";
			printButton.ToolTipText = "Print out your wordsearch";
			printButton.Click += printButton_Click;
			// 
			// helpButton
			// 
			helpButton.BackColor = SystemColors.ButtonShadow;
			helpButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
			helpButton.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
			helpButton.ForeColor = SystemColors.ControlText;
			helpButton.ImageTransparentColor = Color.Magenta;
			helpButton.Margin = new Padding(2, 1, 2, 1);
			helpButton.Name = "helpButton";
			helpButton.Size = new Size(36, 23);
			helpButton.Text = "Help";
			helpButton.ToolTipText = "Help and instructions";
			helpButton.Click += helpButton_Click;
			// 
			// toolStripDropDownButton1
			// 
			toolStripDropDownButton1.DisplayStyle = ToolStripItemDisplayStyle.Text;
			toolStripDropDownButton1.DropDownItems.AddRange(new ToolStripItem[] { regularToolStripMenuItem, animalsToolStripMenuItem, foodToolStripMenuItem, fruitsToolStripMenuItem, vegetablesToolStripMenuItem, countriesToolStripMenuItem });
			toolStripDropDownButton1.Image = (Image)resources.GetObject("toolStripDropDownButton1.Image");
			toolStripDropDownButton1.ImageTransparentColor = Color.Magenta;
			toolStripDropDownButton1.Name = "toolStripDropDownButton1";
			toolStripDropDownButton1.Size = new Size(100, 22);
			toolStripDropDownButton1.Text = "Word Category";
			// 
			// regularToolStripMenuItem
			// 
			regularToolStripMenuItem.Name = "regularToolStripMenuItem";
			regularToolStripMenuItem.Size = new Size(180, 22);
			regularToolStripMenuItem.Text = "Regular";
			regularToolStripMenuItem.Click += regularToolStripMenuItem_Click;
			// 
			// animalsToolStripMenuItem
			// 
			animalsToolStripMenuItem.Name = "animalsToolStripMenuItem";
			animalsToolStripMenuItem.Size = new Size(180, 22);
			animalsToolStripMenuItem.Text = "Animals";
			animalsToolStripMenuItem.Click += animalsToolStripMenuItem_Click;
			// 
			// foodToolStripMenuItem
			// 
			foodToolStripMenuItem.Name = "foodToolStripMenuItem";
			foodToolStripMenuItem.Size = new Size(180, 22);
			foodToolStripMenuItem.Text = "Food";
			foodToolStripMenuItem.Click += foodToolStripMenuItem_Click;
			// 
			// fruitsToolStripMenuItem
			// 
			fruitsToolStripMenuItem.Name = "fruitsToolStripMenuItem";
			fruitsToolStripMenuItem.Size = new Size(180, 22);
			fruitsToolStripMenuItem.Text = "Fruits";
			fruitsToolStripMenuItem.Click += fruitsToolStripMenuItem_Click;
			// 
			// vegetablesToolStripMenuItem
			// 
			vegetablesToolStripMenuItem.Name = "vegetablesToolStripMenuItem";
			vegetablesToolStripMenuItem.Size = new Size(180, 22);
			vegetablesToolStripMenuItem.Text = "Vegetables";
			vegetablesToolStripMenuItem.Click += vegetablesToolStripMenuItem_Click;
			// 
			// countriesToolStripMenuItem
			// 
			countriesToolStripMenuItem.Name = "countriesToolStripMenuItem";
			countriesToolStripMenuItem.Size = new Size(180, 22);
			countriesToolStripMenuItem.Text = "Countries";
			countriesToolStripMenuItem.Click += countriesToolStripMenuItem_Click;
			// 
			// Form1
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(600, 797);
			Controls.Add(toolStrip1);
			ForeColor = SystemColors.ControlText;
			FormBorderStyle = FormBorderStyle.Fixed3D;
			Icon = (Icon)resources.GetObject("$this.Icon");
			MaximizeBox = false;
			Name = "Form1";
			Text = "Wordsearch";
			Load += Form1_Load;
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private ImageList imageList1;
		private System.Drawing.Printing.PrintDocument printDocument1;
		private PrintDialog printDialog1;
		private ToolStrip toolStrip1;
		private ToolStripButton printButton;
		private ToolStripButton generateButton;
		private ToolStripButton helpButton;
		private ToolStripDropDownButton toolStripDropDownButton1;
		private ToolStripMenuItem regularToolStripMenuItem;
		private ToolStripMenuItem animalsToolStripMenuItem;
		private ToolStripMenuItem foodToolStripMenuItem;
		private ToolStripMenuItem fruitsToolStripMenuItem;
		private ToolStripMenuItem vegetablesToolStripMenuItem;
		private ToolStripMenuItem countriesToolStripMenuItem;
	}
}