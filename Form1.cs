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
    Font wordSearchFont = new System.Drawing.Font("Trebuchet MS", 20F);
    Font listFont = new System.Drawing.Font("Trebuchet MS", 14F);
    Color randomColor;
		string[] wordsUsed; // words acc in the wordsearch grid
    int[] orientations = { -16, -15, -14, -1, 1, 14, 15, 16 }; // all the orientations that the word could be in
    List<int> buttonsClicked = new List<int>(); // letters that have been selected on the grid
    Bitmap memoryImage; // printy bit
    public Form1()
    {
      InitializeComponent();
    }
    void sizeOfGrid()
    {
			string rows; // how many rows in the wordsearch
      int intRows = 0;
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
			int intColumns = 0;
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
      btnArray = new System.Windows.Forms.Button[intRows, intColumns];
			this.Size = new System.Drawing.Size((40*intColumns) + 20, (40*intRows) + 230);
		}
    void HowManyWords()
    {
      string input;
      int intInput = 0;
      int maxWordsAllowed;
			if (btnArray.Length > 300)
			{
				maxWordsAllowed = 20;
			}
			else if (btnArray.Length > 200)
			{
				maxWordsAllowed = 15;
			}
			else if (btnArray.Length > 150)
      {
        maxWordsAllowed = 10;
      }
      else
      {
        maxWordsAllowed = 8;
      }
      do
      {
        input = Microsoft.VisualBasic.Interaction.InputBox($"How many words would you like in your wordsearch?\nChoose between 5-{maxWordsAllowed.ToString()}", "Wordsearch", "5");
        try
        {
          intInput = Convert.ToInt32(input);
          if (intInput < 5 || intInput > maxWordsAllowed)
          {
            MessageBox.Show("Input not within specified range, try again", "Wordsearch");
          }
        }
        catch
        {
          if (input == null || input == "") // basos idc if u wanna play or not ur gonna make a wordsearch
          {
            intInput = 5;
          }
          else
          {
            MessageBox.Show("Input not a number, try again", "Wordsearch");
          }
        }
      } while (intInput < 5 || intInput > maxWordsAllowed);
      wordsUsed = new string[intInput];
      labelArray = new System.Windows.Forms.Label[intInput];
    }
    
    void BoardSetUp()
    {
      int xPos = 0;
      int yPos = 25; // toolbar thingy innit
      for (int i = 0; i < btnArray.GetLength(0); i++) // rows
      {
				for (int j = 0; j < btnArray.GetLength(1); j++) // columns
        {
          btnArray[i, j] = new System.Windows.Forms.Button();
          btnArray[i, j].Tag = (i*15)+j;
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
          btnArray[i, j].MouseDown += new System.Windows.Forms.MouseEventHandler(ClickButton);
				}
        xPos = 0;
        yPos += 40;
      }
    }

    void InsertWords()
    {
      string[] wordArray = Resources.words.Split("\n");
      string randomWord;
      int orientation, startRow, startColumn, currentRow, currentColumn;
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
          orientation = orientations[random.Next(orientations.Length)];
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
            currentRow += orientation % 15;
            currentColumn += orientation / 15;
          }
        } while (accWorks == false);
				currentRow = startRow;
				currentColumn = startColumn;
				foreach (char letter in wordsUsed[i]) // acc shove it onto the board
        {
          btnArray[currentRow, currentColumn].Text = letter.ToString().ToUpper();
					currentRow += orientation % 15;
					currentColumn += orientation / 15;
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

    public void ClickButton(Object sender, MouseEventArgs e)
    {
      Button btn = (Button)sender;
      int btnNum = Convert.ToInt32(btn.Tag);
      switch (btn.Image.Tag)
      {
        case true: // unselect letter
          buttonsClicked.Remove(btnNum);
          btn.Image = imageList1.Images[0];
          btn.Image.Tag = false;
          break;
        case false: // select letter
          buttonsClicked.Add(btnNum);
          btn.Image = imageList1.Images[1];
          btn.Image.Tag = true;
          buttonsClicked.Sort();
          break;
      }
      this.ActiveControl = null;
      if (buttonsClicked.Count > 1) // cant check words below 1 letter
      {
        if (orientations.Contains(buttonsClicked[1] - buttonsClicked[0]))
        {
          bool buttonsInALine = true;
          int orientation = buttonsClicked[1] - buttonsClicked[0];
          for (int i = 1; i < buttonsClicked.Count; i++)
          {
            if (buttonsClicked[i] - buttonsClicked[i - 1] != orientation)
            {
              buttonsInALine = false;
              break;
            }
          }
          if (buttonsInALine) // only checks if word is in list if theyre alr in a line
          {
            List<string> lettersHighlighted = new List<string>();
            foreach (int buttonClicked in buttonsClicked)
            {
              lettersHighlighted.Add(btnArray[buttonClicked / 15, buttonClicked % 15].Text.ToLower());
            }
            IEnumerable<string> lettersHighlightedReversed = lettersHighlighted.AsEnumerable().Reverse(); // reversable words (reversing a list doesnt return anything in c# its so weird)
            if (wordsUsed.Contains(string.Join("", lettersHighlighted)) || wordsUsed.Contains(string.Join("", lettersHighlightedReversed))) // if an actual word is found
            {
              string wordFound; // word acc found in the wordsearch; can be in 2 directions
              if (wordsUsed.Contains(string.Join("", lettersHighlighted))) { wordFound = string.Join("", lettersHighlighted); }
              else { wordFound = string.Join("", lettersHighlightedReversed); }
              randomColor = Extensions.GetColour();
              foreach (int buttonClicked in buttonsClicked) // highlights word and unclicks them
              {
                btnArray[buttonClicked / 15, buttonClicked % 15].ForeColor = randomColor;
                btnArray[buttonClicked / 15, buttonClicked % 15].Image = imageList1.Images[0];
                btnArray[buttonClicked / 15, buttonClicked % 15].Image.Tag = false;
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
        }
      }
    }

    private void printButton_Click(object sender, EventArgs e)
    {
      this.BackColor = Color.White;
      Graphics myGraphics = this.CreateGraphics();
      Size s = this.Size;
      memoryImage = new Bitmap(s.Width - 20, s.Height - 70, myGraphics);
      Graphics memoryGraphics = Graphics.FromImage(memoryImage);
      if (printDialog1.ShowDialog() == DialogResult.OK)
      {
        memoryGraphics.CopyFromScreen(this.Location.X + 10, this.Location.Y + 48, 0, 0, s); // too much maffs dont talk abt it
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
      sizeOfGrid();
			HowManyWords();
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