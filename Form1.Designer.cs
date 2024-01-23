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
      printButton = new Button();
      printDocument1 = new System.Drawing.Printing.PrintDocument();
      printDialog1 = new PrintDialog();
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
      // printButton
      // 
      printButton.BackColor = SystemColors.Control;
      printButton.Location = new Point(513, 726);
      printButton.Name = "printButton";
      printButton.Size = new Size(75, 23);
      printButton.TabIndex = 0;
      printButton.TabStop = false;
      printButton.Text = "Print";
      printButton.UseVisualStyleBackColor = false;
      printButton.Click += printButton_Click;
      // 
      // printDocument1
      // 
      printDocument1.PrintPage += printDocument1_PrintPage;
      // 
      // printDialog1
      // 
      printDialog1.UseEXDialog = true;
      // 
      // Form1
      // 
      AutoScaleDimensions = new SizeF(7F, 15F);
      AutoScaleMode = AutoScaleMode.Font;
      ClientSize = new Size(600, 761);
      Controls.Add(printButton);
      ForeColor = SystemColors.ControlText;
      FormBorderStyle = FormBorderStyle.Fixed3D;
      Icon = (Icon)resources.GetObject("$this.Icon");
      Name = "Form1";
      Text = "Wordsearch";
      Load += Form1_Load;
      ResumeLayout(false);
    }

    #endregion

    private ImageList imageList1;
    private Button printButton;
    private System.Drawing.Printing.PrintDocument printDocument1;
    private PrintDialog printDialog1;
  }
}