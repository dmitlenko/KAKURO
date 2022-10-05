using Kakuro.Engine.Algorithms;
using Kakuro.Engine.Core;

class Program
{
    public static void Main()
    {
        Generator generator = new Generator();

        try
        {
            KakuroBoard board = generator.Generate(8, 8, 3);

        } catch(KakuroException e)
        {
            Console.WriteLine(e.Message);
        }
    }
}