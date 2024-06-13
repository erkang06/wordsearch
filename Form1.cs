using System.Drawing.Printing;
using wordsearch.Properties;

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
		Size wordsearchSize;
		string wordSet; // list of words used in wordsearch


		public Form1()
		{
			InitializeComponent();
		}

		int sizeOfGrid(string dimension)
		{
			string value; // how many rows in the wordsearch
			int intValue = 0;
			do
			{
				value = Microsoft.VisualBasic.Interaction.InputBox($"How many {dimension} would you like in your wordsearch?\nChoose between 10-20", "Wordsearch", "15"); // https://stackoverflow.com/questions/10797774/messagebox-with-input-field
				try
				{
					intValue = Convert.ToInt32(value);
					if (intValue < 10 || intValue > 20)
					{
						MessageBox.Show("Input not within specified range, try again", "Wordsearch");
					}
				}
				catch
				{
					if (value == null || value == "") // basos idc if u wanna play or not ur gonna make a board
					{
						intValue = 10;
					}
					else
					{
						MessageBox.Show("Input not a number, try again", "Wordsearch");
					}
				}
			} while (intValue < 10 || intValue > 20);
			return intValue;
		}

		void HowManyWords()
		{
			string stringHowManyWords;
			int maxWordsAllowed;
			if (intRows * intColumns >= 300) // dont wanna crash cuz u cant overfill a board yk
			{
				maxWordsAllowed = 20;
			}
			else if (intRows * intColumns >= 200)
			{
				maxWordsAllowed = 15;
			}
			else if (intRows * intColumns >= 150)
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
					btnArray[i, j].Image = imageList1.Images[0]; // unselected
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
					orientation = [-1, -1];
					break;
				case (1):
					orientation = [-1, 0];
					break;
				case (2):
					orientation = [-1, 1];
					break;
				case (3):
					orientation = [0, -1];
					break;
				case (4):
					orientation = [0, 1];
					break;
				case (5):
					orientation = [1, -1];
					break;
				case (6):
					orientation = [1, 0];
					break;
				case (7):
					orientation = [1, 1];
					break;
			}
			return orientation;
		}

		void InsertWords()
		{
			string[] wordArray = wordSet.Split("\n");
			string randomWord;
			int[] orientation;
			int startRow, startColumn, currentRow, currentColumn;
			bool accWorks = true;
			for (int i = 0; i < wordsUsed.Length; i++)
			{
				do // only here just in case word rly doesnt wanna fit
				{
					do // find a word
					{
						randomWord = wordArray[random.Next(wordArray.Length)].Trim(); // imagine newlines
					} while (wordsUsed.Contains(randomWord));
					int numberOfTries = 0;
					do // put the word in the board
					{
						accWorks = true;
						orientation = FindOrientation();
						startRow = random.Next(btnArray.GetLength(0));
						startColumn = random.Next(btnArray.GetLength(1));
						currentRow = startRow;
						currentColumn = startColumn;
						foreach (char letter in randomWord) // checks if word can fit in right
						{
							if (currentRow < 0 || currentRow > btnArray.GetLength(0) - 1 || currentColumn < 0 || currentColumn > btnArray.GetLength(1) - 1) // if out of bounds
							{
								accWorks = false;
								numberOfTries++;
								break;
							}
							else if (btnArray[currentRow, currentColumn].Text != letter.ToString().ToUpper() && btnArray[currentRow, currentColumn].Text != "") // if square u want is unusable
							{
								accWorks = false;
								numberOfTries++;
								break;
							}
							currentRow += orientation[0];
							currentColumn += orientation[1];
						}
					} while (accWorks == false || numberOfTries < 50); // if number of tries is greater than 50 itll find a new word instead of trying it again
				} while (accWorks == false);
				wordsUsed[i] = randomWord; // finally put word into list now it acc fits fine
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
				if (btn.Text == "") // shove a letter in if blank 
				{
					btn.Text = Extensions.GetLetter();
				}
			}
		}

		void InsertWordsToFind() // basos the list of words you wanna find at the bottom of a wordsearch yk
		{
			int xPos = 5;
			int yPos = 30 + (40 * btnArray.GetLength(0));
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
			wordSet = Resources.regular;
			WordsearchSetUp("new"); // completely from scratch
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
			btn.Image = imageList1.Images[1]; // selected
			Control control = (Control)sender;
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
						foreach (int buttonClicked in buttonsClicked) // unselect currently selected buttons
						{
							btnArray[buttonClicked / intColumns, buttonClicked % intColumns].BackColor = Color.LightGray;
							btnArray[buttonClicked / intColumns, buttonClicked % intColumns].Image = imageList1.Images[0]; // not selected
						}
						buttonsClicked.Clear();
						if (currentButtonClicked[0] > firstButtonClicked[0]) // front to back
						{
							for (int i = firstButtonClicked[0]; i <= currentButtonClicked[0]; i += orientation)
							{
								btnArray[i / intColumns, i % intColumns].BackColor = randomColor;
								buttonsClicked.Add(i);
								btnArray[i / intColumns, i % intColumns].Image = imageList1.Images[1]; // selected
							}
						}
						else // back to front
						{
							for (int i = firstButtonClicked[0]; i >= currentButtonClicked[0]; i -= orientation)
							{
								btnArray[i / intColumns, i % intColumns].BackColor = randomColor;
								buttonsClicked.Add(i);
								btnArray[i / intColumns, i % intColumns].Image = imageList1.Images[1]; // selected
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
						btnArray[buttonsClicked.Last() / intColumns, buttonsClicked.Last() % intColumns].Image = imageList1.Images[0]; // not selected
						buttonsClicked.Remove(buttonsClicked.Last());
					}
				}
			}
		}

		public void btn_MouseUp(Object sender, MouseEventArgs e)
		{
			isMouseDown = false;
			foreach (int buttonClicked in buttonsClicked) // unhighlights word and unclicks them
			{
				btnArray[buttonClicked / intColumns, buttonClicked % intColumns].BackColor = Color.LightGray;
				btnArray[buttonClicked / intColumns, buttonClicked % intColumns].Image = imageList1.Images[0]; // not selected

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
			buttonsClicked.Clear(); // no matter what that silly list has to be cleared
		}

		private void printButton_Click(object sender, EventArgs e)
		{
			this.BackColor = Color.White;
			Graphics myGraphics = this.CreateGraphics();
			wordsearchSize = this.Size;
			memoryImage = new Bitmap(wordsearchSize.Width - 20, wordsearchSize.Height - 72, myGraphics);
			Graphics memoryGraphics = Graphics.FromImage(memoryImage);
			if (printDialog1.ShowDialog() == DialogResult.OK)
			{
				memoryGraphics.CopyFromScreen(this.Location.X + 10, this.Location.Y + 57, 0, 0, wordsearchSize); // too much maffs dont talk abt it
				if (memoryImage.Width >= 800)
				{
					memoryImage = new Bitmap(memoryImage, new Size(780, Convert.ToInt32((double)memoryImage.Height / memoryImage.Width * 780.0))); // resize if too big to fit on page
				}
				try { printDocument1.Print(); }
				catch { MessageBox.Show("Wordsearch wasn't able to print", "Wordsearch"); }
			}
			this.BackColor = SystemColors.Control;
			this.ActiveControl = null;
		}

		private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
		{
			int xPos = (800 - memoryImage.Width) / 2;
			e.Graphics.DrawImage(memoryImage, xPos, 100);
		}

		private void helpButton_Click(object sender, EventArgs e)
		{
			MessageBox.Show(Resources.helpsheet, "Wordsearch");
			this.ActiveControl = null;
		}

		private void generateButton_Click(object sender, EventArgs e)
		{
			WordsearchSetUp("generate"); // pressing the generate button
		}

		void WordsearchSetUp(string type)
		{
			intRows = sizeOfGrid("rows");
			intColumns = sizeOfGrid("columns");
			HowManyWords();
			if (type == "generate")
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
			}
			BoardSetUp();
			InsertWords();
			InsertWordsToFind();
		}

		private void regularToolStripMenuItem_Click(object sender, EventArgs e)
		{
			wordSet = Resources.regular;
			MessageBox.Show("Wordsearch will load in with regular wordset on next generation", "Wordsearch");
		}

		private void animalsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			wordSet = Resources.animals;
			MessageBox.Show("Wordsearch will load in with animals wordset on next generation", "Wordsearch");
		}

		private void foodToolStripMenuItem_Click(object sender, EventArgs e)
		{
			wordSet = Resources.food;
			MessageBox.Show("Wordsearch will load in with food wordset on next generation", "Wordsearch");
		}

		private void fruitsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			wordSet = Resources.fruits;
			MessageBox.Show("Wordsearch will load in with fruits wordset on next generation", "Wordsearch");
		}

		private void vegetablesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			wordSet = Resources.vegetables;
			MessageBox.Show("Wordsearch will load in with vegetables wordset on next generation", "Wordsearch");
		}

		private void countriesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			wordSet = Resources.countries;
			MessageBox.Show("Wordsearch will load in with countries wordset on next generation", "Wordsearch");
		}
	}

	static class Extensions
	{
		static Random random = new Random();
		static string[] colours = Resources.colours.Split("\n");
		public static string GetLetter() // https://stackoverflow.com/questions/15249138/pick-random-char
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