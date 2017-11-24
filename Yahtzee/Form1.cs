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
        Image PBTop;
        Image PBBottom;

        bool onePair, twoPair, threeKind, fullHouse, lowStraight, highStraight, fourKind, yahtzee, ones, twos, threes, fours, fives, sixes, chance;


        #endregion


        public YahtzeeMainForm()
        {
            InitializeComponent();            
            rngeesus = new Random();
            diceImages = new Image[8];
            dice = new int[5] { 0, 0, 0, 0, 0 };
            diceResults = new int[6] { 0, 0, 0, 0, 0, 0 };
            LoadImages();
            player1 = true;
            player2 = false;
            rollCounter = 0;

            #region Setting all the bool values of possible conditions to false
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

        #region NextPlayer() Sets p1/p2 to true/false and updates the currentPlayerLbl to the current player.
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

        }
        #endregion

        #region InsertRollsIntoResults() adds the 'saved' dice into the diceResults[]
        private void InsertRollsIntoResults()
        {
            if (player1 == true)
            {
                for (int i = 0; i < dice.Length; i++)
                {
                    if (dice[i] == 1)
                        diceResults[0]++;
                    else if (dice[i] == 2)
                        diceResults[1]++;
                    else if (dice[i] == 3)
                        diceResults[2]++;
                    else if (dice[i] == 4)
                        diceResults[3]++;
                    else if (dice[i] == 5)
                        diceResults[4]++;
                    else if (dice[i] == 6)
                        diceResults[5]++;
                }
            }
            else if (player2 == true)
            {
                for (int i = 0; i < dice.Length; i++)
                {
                    if (dice[i] == 1)
                        diceResults[0]++;
                    else if (dice[i] == 2)
                        diceResults[1]++;
                    else if (dice[i] == 3)
                        diceResults[2]++;
                    else if (dice[i] == 4)
                        diceResults[3]++;
                    else if (dice[i] == 5)
                        diceResults[4]++;
                    else if (dice[i] == 6)
                        diceResults[5]++;
                }
            }
        }
#endregion

        private void CheckResultsForCombinations()
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
            }
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
        }

        private void SwapToBottomPictureBox()
        {
            for (int i = 0; i < diceImages.Length; i++)
            {
                if (PBTop == diceImages[i])
                {
                    PBBottom = diceImages[i];
                    PBTop = null;
                }
            }
        }

        private void rollDiceBtn_Click(object sender, EventArgs e)
        {
            RollTheDice();
        }

        private void nextPlayerBtn_Click(object sender, EventArgs e)
        {
            InsertRollsIntoResults();
            NextPlayer();
        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            PBTop = pictureBox1.Image;
            PBBottom = pictureBox6.Image;
            SwapToBottomPictureBox();
        }
    }
}
