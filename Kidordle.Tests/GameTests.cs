using KidordleGame;

namespace Kidordle.Tests;

public class GameTests
{


    [Theory]
    [MemberData(nameof(GetData), parameters: 13)]
    public void TestCheckWorkWithDifferentGuesses(char[] guess, char[] answer, short[] expected)
    {
        //Arrange
        Game game = new Game(answer);
        WordResult wordResult;

        //Act
        wordResult = game.CheckWord(guess);

        //Assert
        for (int i = 0; i < answer.Length; i++)
        {
            Assert.Equal(expected[i], wordResult.LetterResult[i]);
        }
    }

    public static IEnumerable<object[]> GetData(int numTests)
    {
        var allData = new List<object[]>
        {
            new object[] {new char[]{ 'T', 'E', 'A', 'R', 'S' }, new char[]{ 'T', 'E', 'A', 'R', 'S' }, new short[] { 1,1,1,1,1 } },
            new object[] {new char[]{ 'S', 'T', 'A', 'R', 'S' }, new char[]{ 'T', 'E', 'A', 'R', 'S' }, new short[] { 0,2,1,1,1 } },
            new object[] {new char[]{ 'C', 'H', 'E', 'S', 'T' }, new char[]{ 'T', 'E', 'A', 'R', 'S' }, new short[] { 0,0,2,2,2 } },
            new object[] {new char[]{ 'T', 'E', 'A', 'R', 'S' }, new char[]{ 'L', 'U', 'N', 'C', 'H' }, new short[] { 0,0,0,0,0 } },
            new object[] {new char[]{ 'P', 'O', 'U', 'N', 'D' }, new char[]{ 'L', 'U', 'N', 'C', 'H' }, new short[] { 0,0,2,2,0 } },
            new object[] {new char[]{ 'F', 'U', 'N', 'G', 'I' }, new char[]{ 'L', 'U', 'N', 'C', 'H' }, new short[] { 0,1,1,0,0 } },
            new object[] {new char[]{ 'L', 'U', 'N', 'C', 'H' }, new char[]{ 'L', 'U', 'N', 'C', 'H' }, new short[] { 1,1,1,1,1 } },
            new object[] {new char[]{ 'A', 'X', 'A', 'X', 'A' }, new char[]{ 'X', 'A', 'X', 'A', 'X' }, new short[] { 2,2,2,2,0 } },
            new object[] {new char[]{ 'A', 'X', 'A', 'X', 'A' }, new char[]{ 'A', 'A', 'X', 'A', 'X' }, new short[] { 1,2,2,2,2 } },
            new object[] {new char[]{ 'T', 'E', 'A', 'R' }, new char[]{ 'T', 'E', 'A', 'R' }, new short[] { 1,1,1,1 } },
            new object[] {new char[]{ 'T', 'E', 'A', 'R' }, new char[]{ 'B', 'E', 'A', 'R' }, new short[] { 0,1,1,1 } },
            new object[] {new char[]{ 'T', 'E', 'A', 'R' }, new char[]{ 'A', 'R', 'T', 'E' }, new short[] { 2,2,2,2 } },
            new object[] {new char[]{ 'T', 'E', 'A', 'R' }, new char[]{ 'B', 'B', 'B', 'B' }, new short[] { 0,0,0,0 } },
        };

        return allData.Take(numTests);
    }

}