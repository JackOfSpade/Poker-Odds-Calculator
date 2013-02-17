# Poker Odds Calculator

Poker Odds Calculator is an easy way to calculates the chance that a given pair of cards will achieve one of the rank of hands. It is written for Texas Hold'em by Jack Wu. The goal is to keep it simple and fast because every online poker table only gives a time limit of 1.5 minutes for each turn. Its functionality also includes the calculation of expected values to help the player make better choices. The source code and its algorithm is completely original.

## Usage

Scenario: the flop has just been dealt (with a 10 of diamonds, 2 of clubs and 3 of hearts) and the total pot is $50. Your opponent bets $50. Now it is your turn and you have a 9 of diamonds and 4 of clubs. What will you do?

### 1. Input the Community Cards on the Table:

    //Under "Flop Cards", 
    //Enter the 1st card with a suit of "d" for diamonds and "10" as its value.
    //Enter the 2nd card with a suit of "c" for clubs and "2" as its value.
    //Enter the 3rd card with a suit of "h" for hearts and "3" as its value.
    //Leave the "Turn Card" and "River Card" blank because they haven't been dealt yet.

### 2. Input the Potential Wins and Potential Losses:

    //Under "Expected Value",
    //Enter the total pot of $100 in the middle of the table, which includes the last bet.
    //Enter $50 as the amount you need to call.

### 3. Input Your Cards:

    //Under "Your Cards",
    //Enter your 1st card with a suit of "d" for diamonds and "9" as its value.
    //Enter your 2nd card with a suit of "c" for clubs and "4" as its value.

### 4. Done!
    //Press "Calculate" and analyse the results!
    //In this case, we notice that all of our expected value for the rank of hands are negative, so the best course of action is to fold.
