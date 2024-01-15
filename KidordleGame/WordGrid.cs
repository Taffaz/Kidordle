namespace KidordleGame;

public class WordGrid(int maxGuesses)
{

    char[][] guessGrid = new char[maxGuesses][];
    int currentGuess = 0;

    public char[][] GuessGrid
    {
        get
        {
            return guessGrid;
        }
    }

    public int CurrentGuess { get => currentGuess; }

    public void AddGuess(char[] guess)
    {
        guessGrid[currentGuess++] = guess;
    }

    public char[] GetGuess(int i)
    {
        return guessGrid[i];
    }



}
