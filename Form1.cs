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
    System.Windows.Forms.Button[] btnArray = new System.Windows.Forms.Button[225]; // wordsearch grid
    System.Windows.Forms.Label[] labelArray; // shows words to find
    string[,] wordsearchLetterGrid = new string[15, 15]; // actual letters per button
    Font wordSearchFont = new System.Drawing.Font("Trebuchet MS", 20F);
    Font listFont = new System.Drawing.Font("Trebuchet MS", 14F);
    string[] wordsUsed; // words acc in the wordsearch grid
    int[] orientations = { -16, -15, -14, -1, 1, 14, 15, 16 }; // all the orientations that the word could be in
    List<int> buttonsClicked = new List<int>(); // letters that have been selected on the grid
    Bitmap memoryImage; // printy bit
    public Form1()
    {
      InitializeComponent();
    }

    void HowManyWords()
    {
      string input;
      int intInput = 0;
      do
      {
        input = Microsoft.VisualBasic.Interaction.InputBox("How many words would you like in your wordsearch?\nChoose between 5-15", "Wordsearch", "10");
        try
        {
          intInput = Convert.ToInt32(input);
          if (intInput < 5 || intInput > 15)
          {
            MessageBox.Show("Input not within specified range, try again", "Wordsearch");
          }
        }
        catch
        {
          if (input == null || input == "") // basos idc if u wanna play or not ur gonna make a board
          {
            intInput = 10;
          }
          else
          {
            MessageBox.Show("Input not a number, try again", "Wordsearch");
          }
        }
      } while (intInput < 5 || intInput > 15);
      wordsUsed = new string[intInput];
      labelArray = new System.Windows.Forms.Label[intInput];
    }
    void BoardSetUp()
    {
      int xPos = 0;
      int yPos = 25;
      for (int n = 0; n < 225; n++)
      {
        btnArray[n] = new System.Windows.Forms.Button();
        btnArray[n].Tag = n;
        btnArray[n].Image = imageList1.Images[0];
        btnArray[n].Image.Tag = false; // not selected
        btnArray[n].Text = "";
        btnArray[n].Width = 40; // width of button
        btnArray[n].Height = 40; // height of button
        if (n % 15 == 0 && n != 0) // newline every 15 buttons
        {
          xPos = 0;
          yPos += btnArray[n].Height;
        }
        btnArray[n].Left = xPos;
        btnArray[n].Top = yPos;
        btnArray[n].BackColor = Color.LightGray;
        btnArray[n].TabStop = false;
        btnArray[n].Font = wordSearchFont;
        this.Controls.Add(btnArray[n]); // add button to form
        xPos += btnArray[n].Width;
        btnArray[n].MouseDown += new System.Windows.Forms.MouseEventHandler(ClickButton);
      }
    }

    void InsertWords()
    {
      string[] wordArray = Resources.words.Split("\n");
      string randomWord = "";
      int orientation = 0;
      int startPoint = 0;
      int currentPoint = 0;
      int currentXPos = 0, currentYPos = 0;
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
          startPoint = random.Next(btnArray.Length);
          currentPoint = startPoint;
          currentXPos = currentPoint % 15;
          currentYPos = currentPoint / 15;
          foreach (char letter in wordsUsed[i]) // checks if word can fit in right
          {
            if (currentXPos < 0 || currentXPos > 14 || currentYPos < 0 || currentYPos > 14) // if out of bounds
            {
              accWorks = false;
              break;
            }
            else if (btnArray[currentPoint].Text != letter.ToString().ToUpper() && btnArray[currentPoint].Text != "") // if letter in square u want is unusable
            {
              accWorks = false;
              break;
            }
            currentPoint += orientation;
            currentXPos += orientation % 15;
            currentYPos += orientation / 15;
          }
        } while (accWorks == false);
        currentPoint = startPoint;
        foreach (char letter in wordsUsed[i]) // acc shove it onto the board
        {
          btnArray[currentPoint].Text = letter.ToString().ToUpper();
          currentPoint += orientation;
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
      int xPos = 20;
      int yPos = 630;
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
          yPos = 630;
          xPos += labelArray[n].Width;
        }
        this.Controls.Add(labelArray[n]);
      }
    }

    private void Form1_Load(object sender, EventArgs e) // FORM LOAD RIGHT HERE
    {
      MessageBox.Show("Welcome to my objectively bad wordsearch", "Wordsearch");
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
              lettersHighlighted.Add(btnArray[buttonClicked].Text.ToLower());
            }
            IEnumerable<string> lettersHighlightedReversed = lettersHighlighted.AsEnumerable().Reverse(); // reversable words (reversing a list doesnt return anything in c# its so weird)
            if (wordsUsed.Contains(string.Join("", lettersHighlighted)) || wordsUsed.Contains(string.Join("", lettersHighlightedReversed))) // if an actual word is found
            {
              string wordFound;
              if (wordsUsed.Contains(string.Join("", lettersHighlighted))) { wordFound = string.Join("", lettersHighlighted); }
              else { wordFound = string.Join("", lettersHighlightedReversed); }
              Color randomColor = Extensions.GetColour();
              foreach (int button in buttonsClicked)
              {
                btnArray[button].ForeColor = randomColor; // highlights letter
                btnArray[button].Image = imageList1.Images[0];
                btnArray[button].Image.Tag = false;
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
        memoryGraphics.CopyFromScreen(this.Location.X + 10, this.Location.Y + 58, 0, 0, s); // too much maffs dont talk abt it
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
      for (int i = 0; i < btnArray.Length; i++)
      {
        this.Controls.Remove(btnArray[i]);
      }
      for (int i = 0; i < labelArray.Length; i++)
      {
        this.Controls.Remove(labelArray[i]);
      }
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
