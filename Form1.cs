using System;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Drawing.Printing;
using System.Windows.Forms;
using wordsearch.Properties;
using Microsoft.VisualBasic;

namespace wordsearch
{
  public partial class Form1 : Form
  {
    Random random = new Random();
    System.Windows.Forms.Button[,] btnArray; // wordsearch grid but just the buttons // REMEMBER ITS ROW NUMBER THEN COLUMN NUMBER
    System.Windows.Forms.Label[] labelArray; // shows the list of words u wanna find
		int intRows = 0;
    int intColumns = 0;
		int intHowManyWords = 0;
    bool isMouseDown = false; // dw abt it
		Font wordSearchFont = new System.Drawing.Font("Trebuchet MS", 20F);
    Font listFont = new System.Drawing.Font("Trebuchet MS", 14F);
    Color randomColor;
		string[] wordsUsed; // words acc in the wordsearch grid
    List<int> buttonsClicked = new List<int>(); // letters that have been selected on the grid
    int[] firstButtonClicked = new int[3];
		int[] currentButtonClicked = new int[3]; // helps make the draggable part
		Bitmap memoryImage; // printy bit

    public Form1()
    {
      InitializeComponent();
    }

    void sizeOfGrid()
    {
			string rows; // how many rows in the wordsearch
      // int intRows = 0; need to be global now
			do
			{
				rows = Microsoft.VisualBasic.Interaction.InputBox("How many rows would you like in your wordsearch?\nChoose between 10-20", "Wordsearch", "15");
				try
				{
          intRows = Convert.ToInt32(rows);
					if (intRows < 10 || intRows > 20)
					{
						MessageBox.Show("Input not within specified range, try again", "Wordsearch");
					}
				}
				catch
				{
					if (rows == null || rows == "") // basos idc if u wanna play or not ur gonna make a board
					{
						intRows = 10;
					}
					else
					{
						MessageBox.Show("Input not a number, try again", "Wordsearch");
					}
				}
			} while (intRows < 10 || intRows > 20);

			string columns;// how many columns in the wordsearch
			// int intColumns = 0; need to be global now
			do
			{
				columns = Microsoft.VisualBasic.Interaction.InputBox("How many columns would you like in your wordsearch?\nChoose between 10-20", "Wordsearch", "15");
				try
				{
					intColumns = Convert.ToInt32(columns);
					if (intColumns < 10 || intColumns > 20)
					{
						MessageBox.Show("Input not within specified range, try again", "Wordsearch");
					}
				}
				catch
				{
					if (columns == null || columns == "") // basos idc if u wanna play or not ur gonna make a board
					{
						intColumns = 10;
					}
					else
					{
						MessageBox.Show("Input not a number, try again", "Wordsearch");
					}
				}
			} while (intColumns < 10 || intColumns > 20);
		}

    void HowManyWords()
    {
      string stringHowManyWords;
      int maxWordsAllowed;
			if (intRows * intColumns > 300) // dont wanna crash cuz u cant overfill a board yk
			{
				maxWordsAllowed = 20;
			}
			else if (intRows * intColumns > 200)
			{
				maxWordsAllowed = 15;
			}
			else if (intRows * intColumns > 150)
      {
        maxWordsAllowed = 10;
      }
      else
      {
        maxWordsAllowed = 8;
      }
      do
      {
        stringHowManyWords = Microsoft.VisualBasic.Interaction.InputBox($"How many words would you like in your wordsearch?\nChoose between 5-{maxWordsAllowed.ToString()}", "Wordsearch", "5");
        try
        {
          intHowManyWords = Convert.ToInt32(stringHowManyWords);
          if (intHowManyWords < 5 || intHowManyWords > maxWordsAllowed)
          {
            MessageBox.Show("Input not within specified range, try again", "Wordsearch");
          }
        }
        catch
        {
          if (stringHowManyWords == null || stringHowManyWords == "") // basos idc if u wanna play or not ur gonna make a wordsearch
          {
            intHowManyWords = 5;
          }
          else
          {
            MessageBox.Show("Input not a number, try again", "Wordsearch");
          }
        }
      } while (intHowManyWords < 5 || intHowManyWords > maxWordsAllowed);
    }
    
    void BoardSetUp()
    {
			this.Size = new System.Drawing.Size((40 * intColumns) + 20, (40 * intRows) + 230); // this stuff is all here for aesthetics yk
			btnArray = new System.Windows.Forms.Button[intRows, intColumns];
			wordsUsed = new string[intHowManyWords];
			labelArray = new System.Windows.Forms.Label[intHowManyWords];
			int xPos = 0;
      int yPos = 25; // toolbar thingy innit
      for (int i = 0; i < btnArray.GetLength(0); i++) // rows
      {
				for (int j = 0; j < btnArray.GetLength(1); j++) // columns
        {
          btnArray[i, j] = new System.Windows.Forms.Button();
          btnArray[i, j].Tag = (i * intColumns) + j;
          btnArray[i, j].Image = imageList1.Images[0];
          btnArray[i, j].Image.Tag = false; // not selected
          btnArray[i, j].Text = "";
          btnArray[i, j].Width = 40; // width of button
          btnArray[i, j].Height = 40; // height of button
          btnArray[i, j].Left = xPos;
          btnArray[i, j].Top = yPos;
          btnArray[i, j].BackColor = Color.LightGray;
          btnArray[i, j].TabStop = false;
          btnArray[i, j].Font = wordSearchFont;
          this.Controls.Add(btnArray[i, j]); // add button to form
          xPos += btnArray[i, j].Width;
					btnArray[i, j].MouseDown += new System.Windows.Forms.MouseEventHandler(btn_MouseDown);
          btnArray[i, j].MouseMove += new System.Windows.Forms.MouseEventHandler(btn_MouseMove);
          btnArray[i, j].MouseUp += new System.Windows.Forms.MouseEventHandler(btn_MouseUp);
				}
				xPos = 0;
        yPos += 40;
      }
    }

    int[] FindOrientation() // its so bougie ik but we live
    {
      int[] orientation = new int[2];
      switch (random.Next(8))
      {
        case (0):
          orientation = new [] { -1, -1 };
          break;
				case (1):
					orientation = new[] { -1, 0 };
					break;
				case (2):
					orientation = new[] { -1, 1 };
					break;
				case (3):
					orientation = new[] { 0, -1 };
					break;
				case (4):
					orientation = new[] { 0, 1 };
					break;
				case (5):
					orientation = new[] { 1, -1 };
					break;
				case (6):
					orientation = new[] { 1, 0 };
					break;
				case (7):
					orientation = new[] { 1, 1 };
					break;
			}
      return orientation;
    }

    void InsertWords()
    {
      string[] wordArray = Resources.words.Split("\n");
      string randomWord;
      int[] orientation;
      int startRow, startColumn, currentRow, currentColumn;
      bool accWorks;
      for (int i = 0; i < wordsUsed.Length; i++)
      {
        do // find a word
        {
          randomWord = wordArray[random.Next(wordArray.Length)].Trim(); // imagine newlines
        } while (wordsUsed.Contains(randomWord));
        wordsUsed[i] = randomWord;
        do // put the word in the board
        {
          accWorks = true;
          orientation = FindOrientation();
					startRow = random.Next(btnArray.GetLength(0));
					startColumn = random.Next(btnArray.GetLength(1));
          currentRow = startRow;
          currentColumn = startColumn;
          foreach (char letter in wordsUsed[i]) // checks if word can fit in right
          {
            if (currentRow < 0 || currentRow > btnArray.GetLength(0)-1 || currentColumn < 0 || currentColumn > btnArray.GetLength(1)-1) // if out of bounds
            {
              accWorks = false;
              break;
            }
            else if (btnArray[currentRow, currentColumn].Text != letter.ToString().ToUpper() && btnArray[currentRow, currentColumn].Text != "") // if letter in square u want is unusable
            {
              accWorks = false;
              break;
            }
            currentRow += orientation[0];
            currentColumn += orientation[1];
          }
        } while (accWorks == false);
				currentRow = startRow;
				currentColumn = startColumn;
				foreach (char letter in wordsUsed[i]) // acc shove it onto the board
        {
          btnArray[currentRow, currentColumn].Text = letter.ToString().ToUpper();
					currentRow += orientation[0];
					currentColumn += orientation[1];
				}
      }
      foreach (Button btn in btnArray)
      {
        if (btn.Text == "")
        {
          btn.Text = Extensions.GetLetter();
        }
      }
    }

    void InsertWordsToFind() // basos the list of words you wanna find at the bottom of a wordsearch yk
    {
      int xPos = 5;
      int yPos = 30 + (40*btnArray.GetLength(0));
      for (int n = 0; n < labelArray.Length; n++)
      {
        labelArray[n] = new System.Windows.Forms.Label();
        labelArray[n].Text = wordsUsed[n];
        labelArray[n].Width = 150;
        labelArray[n].Height = 30;
        labelArray[n].Left = xPos;
        labelArray[n].Top = yPos;
        labelArray[n].TabStop = false;
        labelArray[n].Font = listFont;
        yPos += labelArray[n].Height;
        if ((n + 1) % 5 == 0) // 5 words a column so it doesnt go off the app
        {
          yPos = 30 + (40 * btnArray.GetLength(0));
          xPos += labelArray[n].Width;
        }
        this.Controls.Add(labelArray[n]);
      }
    }

    private void Form1_Load(object sender, EventArgs e) // FORM LOAD RIGHT HERE
    {
      MessageBox.Show("Welcome to my objectively bad wordsearch", "Wordsearch");
      sizeOfGrid();
      HowManyWords();
      BoardSetUp();
      InsertWords();
      InsertWordsToFind(); // i was gonna just redirect this to the generate button but its too much faff lmao
    }

		public void btn_MouseDown(Object sender, MouseEventArgs e)
		{
			randomColor = Extensions.GetColour();
			isMouseDown = true;
			Button btn = (Button)sender;
			btn.BackColor = randomColor;
			firstButtonClicked[0] = Convert.ToInt32(btn.Tag);
			firstButtonClicked[1] = Convert.ToInt32(btn.Tag) / intColumns;
			firstButtonClicked[2] = Convert.ToInt32(btn.Tag) % intColumns;
			buttonsClicked.Add(firstButtonClicked[0]);
			Control control = (Control)sender;
      label1.Text = firstButtonClicked[0].ToString();
			if (control.Capture)
			{
				control.Capture = false;
			}
			this.ActiveControl = null;
		}

		public void btn_MouseMove(Object sender, MouseEventArgs e)
		{
			if (isMouseDown == true)
			{
				Button btn = (Button)sender;
        currentButtonClicked[0] = Convert.ToInt32(btn.Tag);
				label1.Text = currentButtonClicked[0].ToString();
				currentButtonClicked[1] = Convert.ToInt32(btn.Tag) / intColumns;
				currentButtonClicked[2] = Convert.ToInt32(btn.Tag) % intColumns;
				if (currentButtonClicked[0] != buttonsClicked.Last() && !buttonsClicked.Contains(currentButtonClicked[0]) && buttonsClicked.Count > 0) // gain a button
				{
					int rowDiff = currentButtonClicked[1] - firstButtonClicked[1];
					int columnDiff = currentButtonClicked[2] - firstButtonClicked[2];
          int orientation = 0;
					if (rowDiff == 0) // horizontal
          {
            orientation = 1;
					}
          else if (columnDiff == 0) // vertical
          {
						orientation = intColumns;
					}
          else if (rowDiff == columnDiff) // negative diagonal
          {
						orientation = intColumns + 1;
					}
          else if (rowDiff == -columnDiff) // positive diagonal
          {
						orientation = intColumns - 1;
					}
          if (orientation != 0)
					{
            foreach (int buttonClicked in buttonsClicked)
            {
              btnArray[buttonClicked / intColumns, buttonClicked % intColumns].BackColor = Color.LightGray;
            }
						buttonsClicked.Clear();
						if (currentButtonClicked[0] > firstButtonClicked[0])
						{
							for (int i = firstButtonClicked[0]; i <= currentButtonClicked[0]; i += orientation)
							{
                btnArray[i / intColumns, i % intColumns].BackColor = randomColor;
                buttonsClicked.Add(i);
							}
						}
						else
						{
							for (int i = firstButtonClicked[0]; i >= currentButtonClicked[0]; i -= orientation)
							{
								btnArray[i / intColumns, i % intColumns].BackColor = randomColor;
								buttonsClicked.Add(i);
							}
						}
					}
				}
				else if (currentButtonClicked[0] != buttonsClicked.Last() && buttonsClicked.Contains(currentButtonClicked[0]) && buttonsClicked.Count > 1)
				{
					int index = buttonsClicked.IndexOf(currentButtonClicked[0]) + 1;
					while (buttonsClicked.Count > index)
					{
						btnArray[buttonsClicked.Last() / intColumns, buttonsClicked.Last() % intColumns].BackColor = Color.LightGray;
						buttonsClicked.Remove(buttonsClicked.Last());
					}
				}
			}
		}
		public void btn_MouseUp(Object sender, MouseEventArgs e)
    {
			isMouseDown = false;
      foreach (int buttonClicked in buttonsClicked) // highlights word and unclicks them
      {
        btnArray[buttonClicked / intColumns, buttonClicked % intColumns].BackColor = Color.LightGray;
      }
			List<string> lettersHighlighted = new List<string>();
			foreach (int buttonClicked in buttonsClicked)
			{
				lettersHighlighted.Add(btnArray[buttonClicked / intColumns, buttonClicked % intColumns].Text.ToLower());
			}
			IEnumerable<string> lettersHighlightedReversed = lettersHighlighted.AsEnumerable().Reverse(); // reversable words (reversing a list doesnt return anything in c# its so weird)
			if (wordsUsed.Contains(string.Join("", lettersHighlighted)) || wordsUsed.Contains(string.Join("", lettersHighlightedReversed))) // if an actual word is found
			{
				string wordFound; // word acc found in the wordsearch; can be in 2 directions
				if (wordsUsed.Contains(string.Join("", lettersHighlighted))) { wordFound = string.Join("", lettersHighlighted); }
				else { wordFound = string.Join("", lettersHighlightedReversed); }
				foreach (int buttonClicked in buttonsClicked) // highlights word and unclicks them
				{
					btnArray[buttonClicked / intColumns, buttonClicked % intColumns].ForeColor = randomColor;
					btnArray[buttonClicked / intColumns, buttonClicked % intColumns].Image = imageList1.Images[0];
					btnArray[buttonClicked / intColumns, buttonClicked % intColumns].Image.Tag = false;
				}
				buttonsClicked.Clear();
				int index = Array.IndexOf(wordsUsed, wordFound);
				labelArray[index].ForeColor = Color.Red;
				wordsUsed[index] = "";
				bool allWordsFound = true;
				foreach (string word in wordsUsed)
				{
					if (word != "")
					{
						allWordsFound = false;
						break;
					}
				}
				if (allWordsFound) // checks if all words have been found
				{
					MessageBox.Show("You found all the words!", "Wordsearch");
				}
			}
		}

    private void printButton_Click(object sender, EventArgs e)
    {
      this.BackColor = Color.White;
      Graphics myGraphics = this.CreateGraphics();
      Size s = this.Size;
      memoryImage = new Bitmap(s.Width - 20, s.Height - 72, myGraphics);
      Graphics memoryGraphics = Graphics.FromImage(memoryImage);
      if (printDialog1.ShowDialog() == DialogResult.OK)
      {
        memoryGraphics.CopyFromScreen(this.Location.X + 10, this.Location.Y + 57, 0, 0, s); // too much maffs dont talk abt it
        try { printDocument1.Print(); }
        catch { MessageBox.Show("Wordsearch wasn't able to print", "Wordsearch"); }
      }
      this.BackColor = SystemColors.Control;
      this.ActiveControl = null;
    }

    private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
    {
      e.Graphics.DrawImage(memoryImage, 100, 100);
    }

    private void helpButton_Click(object sender, EventArgs e)
    {
      MessageBox.Show(Resources.helpsheet, "Wordsearch");
      this.ActiveControl = null;
    }

    private void generateButton_Click(object sender, EventArgs e)
    {
			sizeOfGrid();
			HowManyWords();
			for (int i = 0; i < btnArray.GetLength(0); i++) // rows
			{
        for (int j = 0; j < btnArray.GetLength(1); j++) // columns
        {
					this.Controls.Remove(btnArray[i, j]);
				}
      }
      for (int i = 0; i < labelArray.Length; i++)
      {
        this.Controls.Remove(labelArray[i]);
      }
      BoardSetUp();
      InsertWords();
      InsertWordsToFind();
    }
  }
  static class Extensions
  {
    static Random random = new Random();
    static string[] colours = Resources.colours.Split("\n");
    public static string GetLetter()
    {
      // This method returns a random uppercase letter.
      // ... Between 'a' and 'z' inclusive.
      int num = random.Next(0, 26); // Zero to 25
      char let = (char)('A' + num);
      return let.ToString();
    }

    public static Color GetColour()
    {
      string randomColourString = colours[random.Next(colours.Length)].Trim();
      Color randomColor = Color.FromName(randomColourString);
      return randomColor;
    }
  }
}