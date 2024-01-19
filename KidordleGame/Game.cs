using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KidordleGame;

public class Game
{
    public event Action? GameUpdated;
    public event Action? InputChanged;
    char[] _guess;
    int bufferPos = 0;
    char[] _answer;
    List<WordResult> _results = new();
    public readonly Dictionary<char, short> GuessedLetters = new();

    public List<WordResult> Results { get => _results; }
    public string Answer { get => string.Join("", _answer); }
    public bool IsLose { get; set; }
    public bool IsWin { get; set; }
    public char BlankChar
    {
        get
        {
            return ' ';
        }
    }
    public int GuessesRemaining => Constants.MAX_GUESSES - Results.Count;

    public int MaxGuesses => Constants.MAX_GUESSES;

    public Game(char[] answer)
    {
        _answer = answer;
        _guess = new char[_answer.Length];
        for (int i = 0; i < _guess.Length; i++)
        {
            _guess[i] = ' ';
        }
    }
    public Game(string answer) : this(answer.ToUpper().ToCharArray())
    {
    }

    public Game()
    {
        var r = new Random();
        var randomLineNumber = r.Next(0, Constants.WORD_LIST.Length - 1);
        var line = Constants.WORD_LIST[randomLineNumber];

        _answer = line.ToUpper().ToCharArray();
        _guess = new char[_answer.Length];
        for (int i = 0; i < _guess.Length; i++)
        {
            _guess[i] = ' ';
        }
    }

    public void GuessWord(char[] guess)
    {
        var result = CheckWord(guess);
        if (result != null)
        {
            for (int i = 0; i < guess.Length; i++)
            {
                short btnValue;
                GuessedLetters.TryGetValue(guess[i], out btnValue);
                if (btnValue == 1)
                {
                    continue;
                }
                if (btnValue == 2 && result.letterResult[i] == 0)
                {
                    continue;
                }
                GuessedLetters[guess[i]] = result.letterResult[i];
            }
            _results.Add(result);

            IsWin = result.IsWin;
            IsLose = !result.IsWin && result.IsLastGuess;
        }
        GameUpdated?.Invoke();
    }

    public WordResult CheckWord(char[] guess)
    {

        if (guess.Length != _answer.Length)
        {
            throw new ArgumentException("Guess Length is not the same as the answer length");
        }

        short[] results = new short[_answer.Length];
        bool[] answerUsed = new bool[_answer.Length];
        bool[] guessUsed = new bool[_answer.Length];

        for (int i = 0; i < _answer.Length; i++)
        {
            if (guess[i] == _answer[i] && !answerUsed[i])
            {
                answerUsed[i] = true;
                guessUsed[i] = true;
                results[i] = 1;
            }
        }

        for (int i = 0; i < _answer.Length; i++)
        {
            if (guessUsed[i])
            {
                continue;
            }
            for (int j = 0; j < _answer.Length; j++)
            {
                if (answerUsed[j])
                {
                    continue;
                }
                if (guess[i] == _answer[j])
                {
                    results[i] = 2;
                    guessUsed[i] = true;
                    answerUsed[j] = true;
                    break;
                }
            }
        }
        var hasWon = true;
        for (int i = 0; i < results.Length; i++)
        {
            if (results[i] != 1)
            {
                hasWon = false;
                break;
            }

        }

        var wasLastGuess = Results.Count == Constants.MAX_GUESSES - 1;

        return new WordResult(guess, results, hasWon, wasLastGuess);

    }

    public void Input(char letter)
    {
        if (bufferPos == _guess.Length) return;

        _guess[bufferPos++] = letter;
        InputChanged?.Invoke();
    }
    public void Back()
    {
        if (bufferPos == 0) return;

        _guess[bufferPos - 1] = ' ';
        bufferPos--;
        InputChanged?.Invoke();
    }

    public bool CanFlush()
    {
        return bufferPos == _guess.Length;
    }

    public char[] Flush()
    {
        char[] output = string.Join("", _guess).ToUpper().ToCharArray();
        for (int i = 0; i < _guess.Length; i++)
        {
            _guess[i] = ' ';
        }
        bufferPos = 0;

        return output;
    }

    public char GetInputAt(int idx)
    {
        if (idx <= bufferPos)
        {
            return _guess[idx];
        }

        return ' ';
    }

    public bool HasLetterBeenGuessed(char letter)
    {
        if (GuessedLetters.ContainsKey(letter))
        {
            return true;
        }
        return false;
    }

}
