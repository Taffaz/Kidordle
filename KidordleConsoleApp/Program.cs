using KidordleGame;
using Spectre.Console;

var game = new Game("tear");
Grid grid;
int guessCounter = 0;
bool hasWon = false;

while(guessCounter < Constants.MAX_GUESSES && !hasWon)
{
    grid = new Grid();
    var guess = GetGuess();
    AnsiConsole.Clear();
    guessCounter++;

    game.GuessWord(guess.ToUpper().ToCharArray());

    for (int i = 0; i < Constants.NUM_LETTERS; i++)
    {
        grid.AddColumn();
    }


    for (int j = 0; j < Constants.MAX_GUESSES; j++)
    {
        if(j < game.Results.Count)
        {
            var res = game.Results[j];
            hasWon = res.IsWin;

            string resultString = "";

            for (int i = 0; i < res.guess.Length; i++)
            {
                var colourTag = "[white]";
                if (res.LetterResult[i] == 1)
                {
                    colourTag = "[green]";
                }else if(res.LetterResult[i] == 2)
                {
                    colourTag = "[yellow]";
                }
                resultString += colourTag + res.guess[i] + "[/]";
            }
            grid.AddRow(resultString.ToUpper());

        }
    }

    AnsiConsole.Write(grid);

}

var gameOverString = hasWon ? "Game Won" : "Game Lost";
AnsiConsole.WriteLine(gameOverString);

static string GetGuess()
{
    const int numLettersAllowed = Constants.NUM_LETTERS;

    var guessPrompt = AnsiConsole.Prompt(
        new TextPrompt<string>("What is your guess?")
            .ValidationErrorMessage("[red]Not a Valid Guess[/]")
            .Validate(guess =>
            {
                return guess.Length switch
                {
                    < numLettersAllowed => ValidationResult.Error("[red]Not enough letters[/]"),
                    > numLettersAllowed => ValidationResult.Error("[red]Too many letters[/]"),
                    _ => ValidationResult.Success(),
                } ;
            })
        );
    return guessPrompt;
}