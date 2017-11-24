using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace Yahtzee
{
    public partial class YahtzeeMainForm : Form
    {

        #region Declarations
        Random rngeesus;
        Image[] diceImages;
        int[] diceResults;
        int[] dice;
        bool player1;
        bool player2;
        int rollCounter;
        int[] savedDice;

        int pBBox;

        bool onePair, twoPair, threeKind, fullHouse, lowStraight, highStraight, fourKind, yahtzee, ones, twos, threes, fours, fives, sixes, chance;


        #endregion

        public YahtzeeMainForm()
        {
            InitializeComponent();            
            rngeesus = new Random();
            diceImages = new Image[8];
            dice = new int[5] { 0, 0, 0, 0, 0 };
            savedDice = new int[6] { 0, 0, 0, 0, 0, 0 };
            diceResults = new int[6] { 0, 0, 0, 0, 0, 0 };
            LoadImages();
            player1 = true;
            player2 = false;
            rollCounter = 0;

            #region Setting all the bool values of possible combinations to false
            bool onePair = false; bool twoPair = false; bool threeKind = false; bool fullHouse = false; bool lowStraight = false; bool highStraight = false; bool fourKind = false;
            bool yahtzee = false; bool ones = false; bool twos = false; bool threes = false; bool fours = false; bool fives = false; bool sixes = false; bool chance = false;
            #endregion
        }

        #region LoadImages() loads images into diceImages[]
        private void LoadImages()
        {
            diceImages[0] = Properties.Resources._0;
            diceImages[1] = Properties.Resources._1;
            diceImages[2] = Properties.Resources._2;
            diceImages[3] = Properties.Resources._3;
            diceImages[4] = Properties.Resources._4;
            diceImages[5] = Properties.Resources._5;
            diceImages[6] = Properties.Resources._6;
            diceImages[7] = Properties.Resources._7;
        }
        #endregion

        #region RollTheDice() rolls the dice and sets the corresponding images in pictureboxes 1-5
        private void RollTheDice()
        {
            for (int i = 0; i < dice.Length; i++)
            {
                dice[i] = rngeesus.Next(1, 7);
            }

            if (pictureBox6.Image == null)
                pictureBox1.Image = diceImages[dice[0]];
            if (pictureBox7.Image == null)
                pictureBox2.Image = diceImages[dice[1]];
            if (pictureBox8.Image == null)
                pictureBox3.Image = diceImages[dice[2]];
            if (pictureBox9.Image == null)
                pictureBox4.Image = diceImages[dice[3]];
            if (pictureBox10.Image == null)
                pictureBox5.Image = diceImages[dice[4]];
        }
        #endregion

        #region Counter() Ensures only 3 rolls can be done
        private void Counter()
        {
            if (rollCounter < 2)
                rollCounter++;
            else
            {
                rollDiceBtn.Hide();
                nextPlayerBtn.Show();
            }
        }
        #endregion

        #region NextPlayer() Sets and clears the playingfield for the next player
        private void NextPlayer()
        {

            if(player1 == true)
            {
                player2 = true;
                player1 = false;
                currentPlayerLbl.Text = "Current Player: 2";
            }
            else if (player2 == true)
            {
                player1 = true;
                player2 = false;
                currentPlayerLbl.Text = "Current Player: 1";
            }
            pictureBox6.Image = null;
            pictureBox7.Image = null;
            pictureBox8.Image = null;
            pictureBox9.Image = null;
            pictureBox10.Image = null;
            Reset();
            rollCounter = 0;

        }
        #endregion
        #region Reset() resets the corresponding int[]'s for a fresh roll
        private void Reset()
        {
            for (int i = 0; i < dice.Length; i++)
            {
                dice[i] = 0;
            }
            
            for (int i = 0; i < diceResults.Length; i++)
            {
                diceResults[i] = 0;
            }
        }
        #endregion

        #region InsertRollsIntoResults() adds the 'saved' dice into the diceResults[]
        private void InsertRollsIntoResults()
        {

        }
        #endregion

        #region DiceCombination() sets possible combination to true
        private void DiceCombination()
        {
            for(int i = 0; i < diceResults.Length; i++)
            {
                if(diceResults[i] == 2)
                {
                    onePair = true;
                    for (int j = i+1; j < diceResults.Length; j++) // loops through the diceResults[] again, starting at the index the pair was found (i+1)
                    {
                        if(diceResults[j] == 2)
                        {
                            twoPair = true;
                        }
                    }
                }

                if(diceResults[i] == 3)
                {
                    threeKind = true;
                    for(int j = 0; j < diceResults.Length; j++) // loops through the diceResults[] again, starting at [0] looking for a pair
                    {
                        if(diceResults[j] == 2)
                        {
                            fullHouse = true;
                        }
                    }
                }
                if(diceResults[i] == 5)
                {
                    yahtzee = true;
                }
            }
                    // Possible combinations for the low straight
            if (diceResults[0] == 1 && diceResults[1] == 1 && diceResults[2] == 1 && diceResults[3] == 1)
            {
                lowStraight = true;
            }
            else if (diceResults[4] == 1 && diceResults[1] == 1 && diceResults[2] == 1 && diceResults[3] == 1)
            {
                lowStraight = true;
            }
            else if (diceResults[2] == 1 && diceResults[3] == 1 && diceResults[4] == 1 && diceResults[5] == 1)
            {
                lowStraight = true;
            }
                    // Possible combinations for the high straight
            if(diceResults[0] == 1 && diceResults[1] == 1 && diceResults[2] == 1 && diceResults[3] == 1 && diceResults[4] == 1)
            {
                highStraight = true;
            }
            else if (diceResults[5] == 1 && diceResults[1] == 1 && diceResults[2] == 1 && diceResults[3] == 1 && diceResults[4] == 1)
            {
                highStraight = true;
            }
        }
        #endregion

        #region DiceScore() updates the diceScoreLbl
        private void DiceScore()
        {
            if (onePair == true)
                diceScoreLbl.Text += "One pair ";
            if (twoPair == true)
                diceScoreLbl.Text += "Two pair ";
            if (threeKind == true)
                diceScoreLbl.Text += "Three of a kind";
            if (lowStraight == true)
                diceScoreLbl.Text += "Low straight";
            if (highStraight == true)
                diceScoreLbl.Text += "High straight";
            if (fullHouse == true)
                diceScoreLbl.Text += "Full House";
            if (fourKind == true)
                diceScoreLbl.Text += "Four of a kind";
            if (yahtzee == true)
                diceScoreLbl.Text += "Yahtzee!";


        }
        #endregion

        


        private void rollDiceBtn_Click(object sender, EventArgs e)
        {
            diceScoreLbl.Text = "Dice Combo ";
            RollTheDice();
            Counter();
        }

        private void nextPlayerBtn_Click(object sender, EventArgs e)
        {

            if (pictureBox1.Image == null && pictureBox2.Image == null && pictureBox3.Image == null && pictureBox4.Image == null && pictureBox5.Image == null)
            {
                InsertRollsIntoResults();
                DiceCombination();
                DiceScore();
                nextPlayerBtn.Hide();
                rollDiceBtn.Show();

                NextPlayer();
            }
            else
            {
                MessageBox.Show("Please select all the dice");
            }
        }
        #region All the pictureBox click events still have to come up with a way to turn this into a function
        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            for (int i = 0; i < diceImages.Length; i++)
            {
                if (pictureBox1.Image == diceImages[i])
                {
                    pictureBox6.Image = diceImages[i];
                    pictureBox1.Image = null;
                }
            }
        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < diceImages.Length; i++)
            {
                if (pictureBox2.Image == diceImages[i])
                {
                    pictureBox7.Image = diceImages[i];
                    pictureBox2.Image = null;
                }
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < diceImages.Length; i++)
            {
                if (pictureBox3.Image == diceImages[i])
                {
                    pictureBox8.Image = diceImages[i];
                    pictureBox3.Image = null;
                }
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < diceImages.Length; i++)
            {
                if (pictureBox4.Image == diceImages[i])
                {
                    pictureBox9.Image = diceImages[i];
                    pictureBox4.Image = null;
                }
            }
        }
        private void pictureBox5_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < diceImages.Length; i++)
            {
                if (pictureBox5.Image == diceImages[i])
                {
                    pictureBox10.Image = diceImages[i];
                    pictureBox5.Image = null;
                }
            }
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < diceImages.Length; i++)
            {
                if (pictureBox6.Image == diceImages[i])
                {
                    pictureBox1.Image = diceImages[i];
                    pictureBox6.Image = null;
                }
            }
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < diceImages.Length; i++)
            {
                if (pictureBox7.Image == diceImages[i])
                {
                    pictureBox2.Image = diceImages[i];
                    pictureBox7.Image = null;
                }
            }
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < diceImages.Length; i++)
            {
                if (pictureBox8.Image == diceImages[i])
                {
                    pictureBox3.Image = diceImages[i];
                    pictureBox8.Image = null;
                }
            }
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < diceImages.Length; i++)
            {
                if (pictureBox9.Image == diceImages[i])
                {
                    pictureBox4.Image = diceImages[i];
                    pictureBox9.Image = null;
                }
            }
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < diceImages.Length; i++)
            {
                if (pictureBox10.Image == diceImages[i])
                {
                    pictureBox5.Image = diceImages[i];
                    pictureBox10.Image = null;
                }
            }
        }
        #endregion



    }
}
