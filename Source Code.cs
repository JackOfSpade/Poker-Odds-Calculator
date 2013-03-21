//Poker-Odds-Calculator
//By Jack Wu
//Note: This source code is written in C# using Microsoft Visual C# 2010 Express.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Check();            
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }
        public void Check()
        {
            int intCom = 0;
            int intDiff = 0;
            int intLeastN = 0;
            int intPosition = 0;
            int intCount = 0;
            int intCardsOnTable = 0;
            int intGreatestSameValue = 0;
            int intLeastSameValue = 0;
            int intGreatestSameSuit = 0;
            int intLeastSameSuit = 0;
            int intSameHandValue = 0;
            int intSameHandSuit = 0;
            double dblCall = 0;
            double dblPot = 0;
            string[] strTemp = new string[7] { txtYV1.Text, txtYV2.Text, txtFV1.Text, txtFV2.Text, txtFV3.Text, txtTV.Text, txtRV.Text };
            string[] strCardSuit = new string[7] { txtYS1.Text, txtYS2.Text, txtFS1.Text, txtFS2.Text, txtFS3.Text, txtTS.Text, txtRS.Text };
            int[] intCardValue = new int[7];
            int[] intSameSuit = new int[2];
            int[] intSameValue = new int[2];
            int[] intStraight = new int[20];
            int[] intStraightNum = new int[10];
            int[] intN = new int[20];
            int[] intStraightArray = new int[25];    
      
            //Empty text exception
            if (txtCall.Text.Length != 0)
            {
                dblCall = Double.Parse(txtCall.Text);
            }

            if (txtPOT.Text.Length != 0)
            {
                dblPot = Double.Parse(txtPOT.Text);
            }
            
            txtCall.Text = "";
            txtPOT.Text = "";
            

            for (int a = 0; a < strTemp.Length; a++)
            {
                //New method to check null
                if (strTemp[a].Length != 0)
                {
                    if (strTemp[a] == "J")
                    {
                        intCardValue[a] = 11;
                    }
                    else if (strTemp[a] == "Q")
                    {
                        intCardValue[a] = 12;
                    }
                    else if (strTemp[a] == "K")
                    {
                        intCardValue[a] = 13;
                    }
                    else if (strTemp[a] == "A")
                    {
                        intCardValue[a] = 1;
                    }
                    else
                    {
                        intCardValue[a] = Int32.Parse(strTemp[a]);
                    }
                }
            }
            //Check identical suits and values (one/two/three/foour pair(s), flush, full house)
            for (int b = 0; b < 2; b++)
            {
                for (int c = 0; c < 7; c++)
                {
                    //If the card suit is not null then obviously card value is entered
                    if (c != 0 &&c!=1&& strCardSuit[b].Length != 0 && strCardSuit[c].Length != 0)
                    {
                        intCom = string.Compare(strCardSuit[b], strCardSuit[c]);
                        if (intCom == 0)
                        {
                            intSameSuit[b]++;
                        }
                        
                        if (intCardValue[b] == intCardValue[c])
                        {
                            intSameValue[b]++;
                        }
                    }
                    else if (b == 0 && c == 1 && strCardSuit[b].Length != 0 && strCardSuit[c].Length != 0)
                    {
                        intCom = string.Compare(strCardSuit[b], strCardSuit[c]);
                        if (intCom == 0)
                        {
                            intSameHandSuit++;
                        }

                        if (intCardValue[b] == intCardValue[c])
                        {
                            intSameHandValue++;
                        }
                    }
                }
            }
            //Ordering the # of identical values/suits of each card
            if (intSameValue[0] > intSameValue[1])
            {
                intGreatestSameValue = intSameValue[0];
                intLeastSameValue = intSameValue[1];
            }
            else if (intSameValue[1] > intSameValue[0])
            {
                intGreatestSameValue = intSameValue[1];
                intLeastSameValue = intSameValue[0];
            }
            else if(intSameValue[1] == intSameValue[0])
            {
                intGreatestSameValue = intSameValue[1];
                intLeastSameValue = intSameValue[0];
            }

            if (intSameSuit[0] > intSameSuit[1])
            {
                intGreatestSameSuit = intSameSuit[0];
                intLeastSameSuit = intSameSuit[1];
            }
            else if (intSameSuit[1] > intSameSuit[0])
            {
                intGreatestSameSuit = intSameSuit[1];
                intLeastSameSuit = intSameSuit[0];
            }
            else if (intSameSuit[1] == intSameSuit[0])
            {
                intGreatestSameSuit = intSameSuit[1];
                intLeastSameSuit = intSameSuit[0];
            }
            //Check for cards on table (doesn't matter which card you use)
            for (int c = 0; c < 7; c++)
            {
                if (strCardSuit[c].Length != 0)
                {
                    intCardsOnTable++;
                }
            }

            if (OnePair(intCardsOnTable, intGreatestSameValue, intLeastSameValue, intSameHandValue) != 99)
            {
                lblOP.Text = Convert.ToString(Math.Round(OnePair(intCardsOnTable, intGreatestSameValue, intLeastSameValue, intSameHandValue) * 100.00, 2, MidpointRounding.AwayFromZero)) + " %";
                lblOPEV.Text = Convert.ToString(Math.Round(-dblCall * (1 - OnePair(intCardsOnTable, intGreatestSameValue, intLeastSameValue, intSameHandValue)) + dblPot * OnePair(intCardsOnTable, intGreatestSameValue, intLeastSameValue, intSameHandValue), 2, MidpointRounding.AwayFromZero));
            }
            else
            {
                lblOP.Text = "N/A";
                lblOPEV.Text = "N/A";
            }

            if (TwoPair(intCardsOnTable, intGreatestSameValue, intLeastSameValue, intSameHandValue) != 99)
            {
                lblTP.Text = Convert.ToString(Math.Round(TwoPair(intCardsOnTable, intGreatestSameValue, intLeastSameValue, intSameHandValue) * 100.00, 2, MidpointRounding.AwayFromZero)) + " %";
                lblTPEV.Text = Convert.ToString(Math.Round(-dblCall * (1 - TwoPair(intCardsOnTable, intGreatestSameValue, intLeastSameValue, intSameHandValue)) + dblPot * TwoPair(intCardsOnTable, intGreatestSameValue, intLeastSameValue, intSameHandValue), 2, MidpointRounding.AwayFromZero));
            }
            else
            {
                lblTP.Text = "N/A";
                lblTPEV.Text = "N/A";
            }

            if (ThreeofaKind(intCardsOnTable, intGreatestSameValue, intLeastSameValue, intSameHandValue) != 99)
            {
                lblTK.Text = Convert.ToString(Math.Round(ThreeofaKind(intCardsOnTable, intGreatestSameValue, intLeastSameValue, intSameHandValue)* 100.00, 2, MidpointRounding.AwayFromZero)) + " %";
                lblTKEV.Text = Convert.ToString(Math.Round(-dblCall * (1 - ThreeofaKind(intCardsOnTable, intGreatestSameValue, intLeastSameValue, intSameHandValue)) + dblPot * ThreeofaKind(intCardsOnTable, intGreatestSameValue, intLeastSameValue, intSameHandValue), 2, MidpointRounding.AwayFromZero));
            }
            else
            {
                lblTK.Text = "N/A";
                lblTKEV.Text = "N/A";
            }

            if (FourofaKind(intCardsOnTable, intGreatestSameValue, intLeastSameValue, intSameHandValue) != 99)
            {
                lblFK.Text = Convert.ToString(Math.Round(FourofaKind(intCardsOnTable, intGreatestSameValue, intLeastSameValue, intSameHandValue)* 100.00, 2, MidpointRounding.AwayFromZero)) + " %";
                lblFKEV.Text = Convert.ToString(Math.Round(-dblCall * (1 - FourofaKind(intCardsOnTable, intGreatestSameValue, intLeastSameValue, intSameHandValue)) + dblPot * FourofaKind(intCardsOnTable, intGreatestSameValue, intLeastSameValue, intSameHandValue), 2, MidpointRounding.AwayFromZero));
            }
            else
            {
                lblFK.Text = "N/A";
                lblFKEV.Text = "N/A";
            }


            if (FullHouse(intCardsOnTable, intGreatestSameValue, intLeastSameValue, intSameHandValue) != 99)
            {
                lblFH.Text = Convert.ToString(Math.Round(FullHouse(intCardsOnTable, intGreatestSameValue, intLeastSameValue, intSameHandValue)* 100.00, 2, MidpointRounding.AwayFromZero)) + " %";
                lblFHEV.Text = Convert.ToString(Math.Round(-dblCall * (1 - FullHouse(intCardsOnTable, intGreatestSameValue, intLeastSameValue, intSameHandValue)) + dblPot * FullHouse(intCardsOnTable, intGreatestSameValue, intLeastSameValue, intSameHandValue), 2, MidpointRounding.AwayFromZero));
            }
            else
            {
                lblFH.Text = "N/A";
                lblFHEV.Text = "N/A";
            }

            if (Flush(intCardsOnTable, intGreatestSameValue, intLeastSameValue, intSameHandSuit) != 99)
            {
                lblF.Text = Convert.ToString(Math.Round(Flush(intCardsOnTable, intGreatestSameSuit, intLeastSameSuit,intSameHandSuit)* 100.00, 2, MidpointRounding.AwayFromZero)) + " %";
                lblFEV.Text = Convert.ToString(Math.Round(-dblCall * (1 - Flush(intCardsOnTable, intGreatestSameValue, intLeastSameValue, intSameHandSuit)) + dblPot * Flush(intCardsOnTable, intGreatestSameValue, intLeastSameValue, intSameHandSuit), 2, MidpointRounding.AwayFromZero));
            }
            else
            {
                lblF.Text = "N/A";
                lblFEV.Text = "N/A";
            }

            //Checking for straight (ＡＳＳＵＭＩＮＧ　ＺＥＲＯ　ＩＳ　ＴＨＥ　ＤＥＦＡＵＬＴ　ＩＮＴ)
            //Quick Sort
            Array.Copy(QuickSort(intCardValue), intCardValue, 7);            
            
            
            //Removing identicals by adding 0 as placeholder
            for (int d = 0; d < intCardValue.Length - 1; d++)
            {
                if (intCardValue[d] == intCardValue[d + 1])
                {
                    intCardValue[d] = 0;
                }
            }

            //Re-Quick Sort
            Array.Copy(QuickSort(intCardValue), intCardValue, 7);

            //Adding "n"
            for (int f = 6; f >0; f--)
            {
                intDiff = (intCardValue[f] - intCardValue[f - 1]);
                if (intDiff > 1)
                {
                    intStraight[intCardValue[f]] = intCardValue[f];
                    for (int g = 1; g < intDiff; g++)
                    {
                        //Adds "n" to everything after [f-1]
                        intStraight[intCardValue[f - 1] + g] = 99;
                    }
                }
                else if (intDiff == 1)
                {
                    intStraight[intCardValue[f]] = intCardValue[f];
                }

                if (f == 1)
                {
                    intStraight[intCardValue[f-1]] = intCardValue[f-1];
                }
            }

            //5 frame “n" straight checker
            for (int h = 0; h < intStraight.Length - 5; h++)
            {
                //If there are less than 5 numbers due to duplicates, intN[20] will all be 0 and will trigger the actions for the exception after this.
                if (intStraight[h] != 0 && intStraight[h+1] != 0 && intStraight[h+2] != 0 && intStraight[h+3] != 0 && intStraight[h+4] != 0)
                {
                    if (intStraight[h] == 99)
                    {
                        intN[h]++;
                        intStraightArray[h] = h;
                    }
                    else
                    {
                        intStraightArray[h] = h;
                    }

                    if (intStraight[h + 1] == 99)
                    {
                        intN[h]++;
                    }

                    if (intStraight[h + 2] == 99)
                    {
                        intN[h]++;
                    }

                    if (intStraight[h + 3] == 99)
                    {
                        intN[h]++;
                    }

                    if (intStraight[h + 4] == 99)
                    {
                        intN[h]++;
                    }
                }
            }

            //Finding least "n" with Quick Sort
            if (intN[0] != 0 || intN[1] != 0 || intN[2] != 0 || intN[3] != 0 || intN[4] != 0 || intN[5] != 0 || intN[6] != 0 || intN[7] != 0 || intN[8] != 0 || intN[9] != 0 || intN[10] != 0 || intN[11] != 0 || intN[12] != 0 || intN[13] != 0 || intN[14] != 0 || intN[15] != 0 || intN[16] != 0 || intN[17] != 0 || intN[18] != 0 || intN[19] != 0)
            {
                //Quick Sort
                Array.Copy(QuickSort(intN), intN, 20);
                //intStraightArray stores the the array # of intStraight that starts the quick-sorted least=N-5-cards
                Array.Copy(QuickSort(intStraightArray), intStraightArray, 25);  

                //Least "n" should be at the front (excluding zeros).
                for (int x = 0; x < intN.Length; x++)
                {
                    if (intN[x] != 0)
                    {
                        intLeastN = intN[x];
                        intPosition = x;
                        break;
                    }
                }

                //Rearranging to a new array
                
                    for (int x = intStraightArray[intPosition]; x < intStraightArray[intPosition] + 5; x++)
                    {
                        intStraightNum[intCount] = intStraight[x];
                        intCount++;
                    }
            }
            else
            {
                for (int x = 0; x < intStraight.Length; x++)
                {
                    if (intStraight[x] != 0)
                    {
                        intDiff++;
                    }
                }

                if (intDiff < 3)
                {
                    intLeastN = 3;
                }
                else if (intDiff == 3)
                {
                    intLeastN = 2;

                    //Rearranging to a new array
                    for (int x = 0; x < intStraight.Length; x++)
                    {
                        if (intStraight[x] != 0)
                        {
                            intStraightNum[intCount] = intStraight[x];
                            intCount++;
                        }
                    }
                }
                else if (intDiff == 4)
                {
                    intLeastN = 1;

                    //Rearranging to a new array
                    for (int x = 0; x < intStraight.Length; x++)
                    {
                        if (intStraight[x] != 0)
                        {
                            intStraightNum[intCount] = intStraight[x];
                            intCount++;
                        }
                    }
                }
                else if (intDiff > 4)
                {
                    intLeastN = 0;
                }
            }


           
            //Adding "n" to front
            //First one will be first # from the 5-section
            if (intStraightNum[0] != 99)
            {
                for (int x = 9; x >0; x--)
                {
                    intStraightNum[x] = intStraightNum[x-1];
                }
                intStraightNum[0] = 99;
            }

            //Adding "n" to back
            //First one will be first # from the 5-section
            for (int x = 0; x < intStraightNum.Length; x++)
            {
                if (intStraightNum[x] == 0 && intStraightNum[x - 1] == 99)
                {
                    break;
                }
                else if (intStraightNum[x] == 0 && intStraightNum[x - 1] != 99)
                {
                    intStraightNum[x] = 99;
                    break;
                }
            }

            if (Straight(intCardsOnTable, intStraightNum, intLeastN) != 99)
            {
                lblS.Text = Convert.ToString(Math.Round(Straight(intCardsOnTable, intStraightNum, intLeastN)* 100.00, 2, MidpointRounding.AwayFromZero)) + " %";
                lblSEV.Text = Convert.ToString(Math.Round(-dblCall * (1 - Straight(intCardsOnTable, intStraightNum, intLeastN)) + dblPot * Straight(intCardsOnTable, intStraightNum, intLeastN), 2, MidpointRounding.AwayFromZero));
            }
            else
            {
                lblS.Text = "N/A";
                lblSEV.Text = "N/A";
            }
        }
//-----------------------------------------------------------------------------------------------------
//-----------------------------------------------------------------------------------------------------
        int[] QuickSort(int[] intArrayValue)
        {
            int intIndex;
            int intPivot;
            int[] intArrayCombined;
            List<int> listLess = new List<int>();
            List<int> listGreater = new List<int>();
            List<int> listCombined = new List<int>();

            if (intArrayValue.Length <= 1)
            {
                //returns the array since no sorting is necessary.
                return intArrayValue;
            }

            intIndex = (intArrayValue.Length - 1) / 2; ;
            intPivot = intArrayValue[intIndex];

            for (int x = 0; x < intArrayValue.Length; x++)
            {
                if (x != intIndex)
                {
                    if (intArrayValue[x] <= intPivot)
                    {
                        listLess.Add(intArrayValue[x]);
                    }
                    else
                    {
                        listGreater.Add(intArrayValue[x]);
                    }
                }
            }

            listCombined.AddRange(QuickSort(listLess.ToArray()).Cast<int>().ToList());
            listCombined.Add(intPivot);
            listCombined.AddRange(QuickSort(listGreater.ToArray()).Cast<int>().ToList());
            intArrayCombined = listCombined.ToArray();

            return intArrayCombined;
        }

//-----------------------------------------------------------------------------------------------------
//-----------------------------------------------------------------------------------------------------
        double OnePair(int intCardsOnTable, int intGreatestSameValue, int intLeastSameValue, int intSameHandValue)
        {
            double dblProbability = 0;

            if (intCardsOnTable == 5)
            {
                if ((intGreatestSameValue == 1 && intLeastSameValue == 0 && intSameHandValue == 0) || (intGreatestSameValue == 0 && intLeastSameValue == 0 && intSameHandValue == 1))
                {
                    dblProbability = 1;
                }
                else if (intGreatestSameValue == 0 && intLeastSameValue == 0 &&intSameHandValue==0)
                {
                    dblProbability = 246.0 / 1081.0;
                }
                else
                {
                    //99 means "N/A"
                    dblProbability = 99;
                }
            }
            else if (intCardsOnTable == 6)
            {
                if ((intGreatestSameValue == 1 && intLeastSameValue == 0 && intSameHandValue == 0) || (intGreatestSameValue == 0 && intLeastSameValue == 0 && intSameHandValue == 1))
                {
                    dblProbability = 1;
                }
                else if (intGreatestSameValue == 0 && intLeastSameValue == 0 && intSameHandValue == 0)
                {
                    dblProbability = 6.0 / 46.0;
                }
                else
                {
                    //99 means "N/A"
                    dblProbability = 99;
                }
            }
            else
            {
                if ((intGreatestSameValue == 1 && intLeastSameValue == 0 && intSameHandValue == 0) || (intGreatestSameValue == 0 && intLeastSameValue == 0 && intSameHandValue == 1))
                {
                    dblProbability = 1;
                }
                else if (intGreatestSameValue == 0 && intLeastSameValue == 0 && intSameHandValue == 0)
                {
                    dblProbability = 0;
                }
                else
                {
                    //99 means "N/A"
                    dblProbability = 99;
                }
            }

            return dblProbability;
        }

//-----------------------------------------------------------------------------------------------------
//-----------------------------------------------------------------------------------------------------
        double TwoPair(int intCardsOnTable, int intGreatestSameValue, int intLeastSameValue, int intSameHandValue)
        {
            double dblProbability = 0;

            if (intCardsOnTable == 5)
            {
                if (intGreatestSameValue == 1 && intLeastSameValue == 1&&intSameHandValue==0)
                {
                    dblProbability = 1;
                }
                else if (intGreatestSameValue == 0 && intLeastSameValue == 0&&intSameHandValue==0)
                {
                    dblProbability = 9.0 / 1081.0;
                }
                else if ((intGreatestSameValue == 1 && intLeastSameValue == 0 && intSameHandValue == 0) || (intGreatestSameValue == 0 && intLeastSameValue == 0 && intSameHandValue == 1))
                {
                    dblProbability = 126.0 / 1081.0;
                }
                else
                {
                    dblProbability = 99;
                }
            }
            else if (intCardsOnTable == 6)
            {
                if (intGreatestSameValue == 1 && intLeastSameValue == 1 && intSameHandValue == 0)
                {
                    dblProbability = 1;
                }
                else if (intGreatestSameValue == 0 && intLeastSameValue == 0 && intSameHandValue == 0)
                {
                    dblProbability = 0;
                }
                else if ((intGreatestSameValue == 1 && intLeastSameValue == 0 && intSameHandValue == 0) || (intGreatestSameValue == 0 && intLeastSameValue == 0 && intSameHandValue == 1))
                {
                    dblProbability = 3.0 / 46.0;
                }
                else
                {
                    dblProbability = 99;
                }

            }
            else
            {
                if (intGreatestSameValue == 1 && intLeastSameValue == 1 && intSameHandValue == 0)
                {
                    dblProbability = 1;
                }
                else if (intGreatestSameValue == 0 && intLeastSameValue == 0 && intSameHandValue == 0)
                {
                    dblProbability = 0;
                }
                else if ((intGreatestSameValue == 1 && intLeastSameValue == 0 && intSameHandValue == 0) || (intGreatestSameValue == 0 && intLeastSameValue == 0 && intSameHandValue == 1))
                {
                    dblProbability = 0;
                }
                else
                {
                    dblProbability = 99;
                }
            }

            return dblProbability;
        }
//-----------------------------------------------------------------------------------------------------
//-----------------------------------------------------------------------------------------------------
        double ThreeofaKind(int intCardsOnTable, int intGreatestSameValue, int intLeastSameValue, int intSameHandValue)
        {
            double dblProbability = 0;

            if (intCardsOnTable == 5)
            {   //intLeastSameValue must == 1 since it is the same as intGreatestValue
                if ((intGreatestSameValue == 2 && intLeastSameValue == 0 && intSameHandValue == 0) || (intGreatestSameValue == 1 && intLeastSameValue == 1 && intSameHandValue == 1))
                {
                    dblProbability = 1;
                }
                else if (intGreatestSameValue == 0 && intLeastSameValue == 0 && intSameHandValue==0)
                {
                    dblProbability = 6.0 / 1081.0;
                }
                else if ((intGreatestSameValue == 1 && intLeastSameValue == 0 && intSameHandValue == 0) || (intGreatestSameValue == 0 && intLeastSameValue == 0 && intSameHandValue == 1))
                {
                    dblProbability = 84.0 / 1081.0;
                }
                else
                {
                    dblProbability = 99;
                }
            }
            else if (intCardsOnTable == 6)
            {
                if ((intGreatestSameValue == 2 && intLeastSameValue == 0 && intSameHandValue == 0) || (intGreatestSameValue == 1 && intLeastSameValue == 1 && intSameHandValue == 1))
                {
                    dblProbability = 1;
                }
                else if (intGreatestSameValue == 0 && intLeastSameValue == 0 && intSameHandValue == 0)
                {
                    dblProbability = 0;
                }
                else if ((intGreatestSameValue == 1 && intLeastSameValue == 0 && intSameHandValue == 0) || (intGreatestSameValue == 0 && intLeastSameValue == 0 && intSameHandValue == 1))
                {
                    dblProbability = 2.0/46.0;
                }
                else
                {
                    dblProbability = 99;
                }
            }
            else
            {
                if ((intGreatestSameValue == 2 && intLeastSameValue == 0 && intSameHandValue == 0) || (intGreatestSameValue == 1 && intLeastSameValue == 1 && intSameHandValue == 1))
                {
                    dblProbability = 1;
                }
                else if (intGreatestSameValue == 0 && intLeastSameValue == 0 && intSameHandValue == 0)
                {
                    dblProbability = 0;
                }
                else if ((intGreatestSameValue == 1 && intLeastSameValue == 0 && intSameHandValue == 0) || (intGreatestSameValue == 0 && intLeastSameValue == 0 && intSameHandValue == 1))
                {
                    dblProbability = 0;
                }
                else
                {
                    dblProbability = 99;
                }
            }

            return dblProbability;
        }
//-----------------------------------------------------------------------------------------------------
//-----------------------------------------------------------------------------------------------------
        double FourofaKind(int intCardsOnTable, int intGreatestSameValue, int intLeastSameValue, int intSameHandValue)
        {
            double dblProbability = 0;

            if (intCardsOnTable == 5)
            {
                if ((intGreatestSameValue == 3 && intSameHandValue == 0) || (intLeastSameValue == 3 && intSameHandValue == 0)||(intGreatestSameValue==2&&intLeastSameValue==2&&intSameHandValue==1))
                {
                    dblProbability = 1;
                }
                else if (intGreatestSameValue == 0 && intLeastSameValue == 0&&intSameHandValue==0)
                {
                    dblProbability = 0;
                }
                else if ((intGreatestSameValue == 1 && intLeastSameValue == 0 && intSameHandValue == 0) || (intGreatestSameValue == 0 && intLeastSameValue == 0 && intSameHandValue == 1))
                {
                    dblProbability = 1.0 / 1081.0;
                }
                else if ((intGreatestSameValue == 2 && intSameHandValue == 0) || (intGreatestSameValue == 1 && intSameHandValue == 1))
                {
                    dblProbability = 2.0 / 47.0;
                }
                else if ((intGreatestSameValue == 1 && intLeastSameValue == 1 && intSameHandValue == 0))
                {
                    dblProbability = 2.0 / 1081.0;
                }
                else
                {
                    dblProbability = 99;
                }
            }
            else if (intCardsOnTable == 6)
            {
                if ((intGreatestSameValue == 3 && intSameHandValue == 0) || (intLeastSameValue == 3 && intSameHandValue == 0) || (intGreatestSameValue == 2 && intLeastSameValue == 2 && intSameHandValue == 1))
                {
                    dblProbability = 1;
                }
                else if (intGreatestSameValue == 0 && intLeastSameValue == 0 && intSameHandValue == 0)
                {
                    dblProbability = 0;
                }
                else if ((intGreatestSameValue == 1 && intLeastSameValue == 0 && intSameHandValue == 0) || (intGreatestSameValue == 0 && intLeastSameValue == 0 && intSameHandValue == 1))
                {
                    dblProbability = 0;
                }
                else if ((intGreatestSameValue == 2 && intSameHandValue == 0) || (intGreatestSameValue == 1 && intSameHandValue == 1))
                {
                    dblProbability = 1.0 / 46.0;
                }
                else if ((intGreatestSameValue == 1 && intLeastSameValue == 1 && intSameHandValue == 0))
                {
                    dblProbability = 0;
                }
                else
                {
                    dblProbability = 99;
                }
            }
            else
            {
                if ((intGreatestSameValue == 3 && intSameHandValue == 0) || (intLeastSameValue == 3 && intSameHandValue == 0) || (intGreatestSameValue == 2 && intLeastSameValue == 2 && intSameHandValue == 1))
                {
                    dblProbability = 1;
                }
                else if (intGreatestSameValue == 0 && intLeastSameValue == 0 && intSameHandValue == 0)
                {
                    dblProbability = 0;
                }
                else if ((intGreatestSameValue == 1 && intLeastSameValue == 0 && intSameHandValue == 0) || (intGreatestSameValue == 0 && intLeastSameValue == 0 && intSameHandValue == 1))
                {
                    dblProbability = 0;
                }
                else if ((intGreatestSameValue == 2 && intSameHandValue == 0) || (intGreatestSameValue == 1 && intSameHandValue == 1))
                {
                    dblProbability = 0;
                }
                else if ((intGreatestSameValue == 1 && intLeastSameValue == 1 && intSameHandValue == 0))
                {
                    dblProbability = 0;
                }
                else
                {
                    dblProbability = 99;
                }
            }

            return dblProbability;
        }
//-----------------------------------------------------------------------------------------------------
//-----------------------------------------------------------------------------------------------------
        double FullHouse(int intCardsOnTable, int intGreatestSameValue, int intLeastSameValue, int intSameHandValue)
        {
            double dblProbability = 0;

            if (intCardsOnTable == 5)
            {
                if (intGreatestSameValue == 2 && intLeastSameValue == 1&&intSameHandValue==0)
                {
                    dblProbability = 1;
                }
                else if ((intGreatestSameValue == 1 && intLeastSameValue == 0 && intSameHandValue == 0)||(intGreatestSameValue == 0 && intLeastSameValue == 0 && intSameHandValue == 1))
                {
                    dblProbability = 6.0 / 1081.0;
                }
                else if (intGreatestSameValue == 1 && intLeastSameValue == 1 && intSameHandValue == 0)
                {
                    dblProbability = 178.0 / 1081.0;
                }
                else if ((intGreatestSameValue == 2 && intLeastSameValue == 0 && intSameHandValue == 0) || (intGreatestSameValue == 1 && intLeastSameValue == 1 && intSameHandValue == 1))
                {
                    dblProbability = 267.0 / 2162.0;
                }
                else if (intGreatestSameValue == 0 && intLeastSameValue == 0&&intSameHandValue==0)
                {
                    dblProbability = 0;
                }
                else
                {
                    dblProbability = 99;
                }
            }
            else if (intCardsOnTable == 6)
            {
                if (intGreatestSameValue == 2 && intLeastSameValue == 1 && intSameHandValue == 0)
                {
                    dblProbability = 1;
                }
                else if ((intGreatestSameValue == 1 && intLeastSameValue == 0 && intSameHandValue == 0) || (intGreatestSameValue == 0 && intLeastSameValue == 0 && intSameHandValue == 1))
                {
                    dblProbability = 0;
                }
                else if (intGreatestSameValue == 1 && intLeastSameValue == 1 && intSameHandValue == 0)
                {
                    dblProbability = 2.0 / 23.0;
                }
                else if ((intGreatestSameValue == 2 && intLeastSameValue == 0 && intSameHandValue == 0) || (intGreatestSameValue == 1 && intLeastSameValue == 1 && intSameHandValue == 1))
                {
                    dblProbability = 3.0 / 46.0;
                }
                else if (intGreatestSameValue == 0 && intLeastSameValue == 0 && intSameHandValue == 0)
                {
                    dblProbability = 0;
                }
                else
                {
                    dblProbability = 99;
                }
            }
            else
            {
                if (intGreatestSameValue == 2 && intLeastSameValue == 1 && intSameHandValue == 0)
                {
                    dblProbability = 1;
                }
                else if ((intGreatestSameValue == 1 && intLeastSameValue == 0 && intSameHandValue == 0) || (intGreatestSameValue == 0 && intLeastSameValue == 0 && intSameHandValue == 1))
                {
                    dblProbability = 0;
                }
                else if (intGreatestSameValue == 1 && intLeastSameValue == 1 && intSameHandValue == 0)
                {
                    dblProbability = 0;
                }
                else if ((intGreatestSameValue == 2 && intLeastSameValue == 0 && intSameHandValue == 0) || (intGreatestSameValue == 1 && intLeastSameValue == 1 && intSameHandValue == 1))
                {
                    dblProbability = 0;
                }
                else if (intGreatestSameValue == 0 && intLeastSameValue == 0 && intSameHandValue == 0)
                {
                    dblProbability = 0;
                }
                else
                {
                    dblProbability = 99;
                }
            }

            return dblProbability;
        }
//-----------------------------------------------------------------------------------------------------
//-----------------------------------------------------------------------------------------------------
        double Flush(int intCardsOnTable, int intGreatestSameSuit, int intLeastSameSuit, int intSameHandSuit)
        {
            double dblProbability = 0;

            if (intCardsOnTable == 5)
            {
                if (intGreatestSameSuit < 2 && intSameHandSuit==0)
                {
                    dblProbability = 0;
                }
                else if ((intGreatestSameSuit > 3 && intSameHandSuit == 0) || (intGreatestSameSuit > 2 && intSameHandSuit == 1))
                {
                    dblProbability = 1;
                }
                else if ((intGreatestSameSuit == 2 && intSameHandSuit == 0) || (intGreatestSameSuit == 1 && intSameHandSuit == 1))
                {
                    dblProbability = 45.0 / 1081.0;
                }
                else if ((intGreatestSameSuit == 3 && intSameHandSuit == 0) || (intGreatestSameSuit == 2 && intSameHandSuit == 1))
                {
                    dblProbability = 378.0 / 1081.0;
                }
                else
                {
                    dblProbability = 99;
                }
            }
            else if (intCardsOnTable == 6)
            {
                if (intGreatestSameSuit < 2 && intSameHandSuit == 0)
                {
                    dblProbability = 0;
                }
                else if ((intGreatestSameSuit > 3 && intSameHandSuit == 0) || (intGreatestSameSuit > 2 && intSameHandSuit == 1))
                {
                    dblProbability = 1;
                }
                else if ((intGreatestSameSuit == 2 && intSameHandSuit == 0) || (intGreatestSameSuit == 1 && intSameHandSuit == 1))
                {
                    dblProbability = 0;
                }
                else if ((intGreatestSameSuit == 3 && intSameHandSuit == 0) || (intGreatestSameSuit == 2 && intSameHandSuit == 1))
                {
                    dblProbability = 9.0/46.0;
                }
                else
                {
                    dblProbability = 99;
                }
            }
            else
            {
                if (intGreatestSameSuit < 2 && intSameHandSuit == 0)
                {
                    dblProbability = 0;
                }
                else if ((intGreatestSameSuit > 3 && intSameHandSuit == 0) || (intGreatestSameSuit > 2 && intSameHandSuit == 1))
                {
                    dblProbability = 1;
                }
                else if ((intGreatestSameSuit == 2 && intSameHandSuit == 0) || (intGreatestSameSuit == 1 && intSameHandSuit == 1))
                {
                    dblProbability = 0;
                }
                else if ((intGreatestSameSuit == 3 && intSameHandSuit == 0) || (intGreatestSameSuit == 2 && intSameHandSuit == 1))
                {
                    dblProbability = 0;
                }
                else
                {
                    dblProbability = 99;
                }
            }

            return dblProbability;
        }
//-----------------------------------------------------------------------------------------------------
//-----------------------------------------------------------------------------------------------------
        double Straight(int intCardsOnTable, int[] intStraightNum, int intLeastN)
        {
            int intPosition = 0;
            double dblSeries = 0;
            double dblCount = 0;
            double dblTen = 10;
            double dblProbability = 0;

            for (int x = 9; x >= 0; x--)
            {
                if (intStraightNum[x] != 0)
                {
                    if (intStraightNum[x] == 99)
                    {
                        dblCount++;
                    }
                    else
                    {
                        dblSeries += Math.Pow(dblTen, dblCount);
                        dblCount++;
                    }
                }
            }

            if (intLeastN==2&&dblSeries > 1000 && dblSeries < 10000)
            {
                intPosition = 1;
            }
            else if (intLeastN == 2 && dblSeries > 10000 && dblSeries < 100000)
            {
                intPosition = 2;
            }
            else if (intLeastN == 2 && dblSeries > 100000 && dblSeries < 1000000)
            {
                intPosition = 3;
            }
            else if (intLeastN == 1 && dblSeries > 10000 && dblSeries < 100000)
            {
                intPosition = 1;
            }
            else if (intLeastN == 1 && dblSeries > 100000 && dblSeries < 1000000)
            {
                intPosition = 2;
            }


            if (intCardsOnTable == 5)
            {
                if (intLeastN == 0)
                {
                    dblProbability = 1;
                }
                else if (intLeastN > 2)
                {
                    dblProbability = 0;
                }
                else if (intLeastN == 2 && (intPosition == 1 || intPosition == 3))
                {
                    dblProbability = 16.0 / 1081.0;
                }
                else if(intLeastN == 2 && intPosition == 2)
                {
                    dblProbability = 32.0 / 1081.0;
                }
                else if (intLeastN == 1 && intPosition == 1)
                {
                    dblProbability = 340.0 / 1081.0;
                }
                else if (intLeastN == 1 && intPosition == 2)
                {
                    dblProbability = 178.0 / 1081.0;
                }
                else
                {
                    dblProbability = 99;
                }
            }
            else if (intCardsOnTable == 6)
            {
                if (intLeastN == 0)
                {
                    dblProbability = 1;
                }
                else if (intLeastN > 1)
                {
                    dblProbability = 0;
                }              
                else if (intLeastN == 1)
                {
                    dblProbability = 4.0/46.0;
                }
                else
                {
                    dblProbability = 99;
                }
            }
            else
            {
                if (intLeastN == 0)
                {
                    dblProbability = 1;
                }
                else if (intLeastN > 1)
                {
                    dblProbability = 0;
                }
                else if (intLeastN == 1)
                {
                    dblProbability = 0;
                }
                else
                {
                    dblProbability = 99;
                }
            }

            return dblProbability;
        }           
    }
}
