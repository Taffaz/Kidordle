using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KidordleGame;

public record class WordResult(char[] guess, short[] letterResult, bool isWin, bool isLastGuess)
{
    public char[] Guess
    {
        get
        {
            return guess;
        }
    }

    public short[] LetterResult
    {
        get
        {
            return letterResult;
        }
    }

    public bool IsWin { get { return isWin; } }
    public bool IsLastGuess {  get { return isLastGuess; } }

}
