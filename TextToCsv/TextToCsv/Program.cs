using System;

namespace TextToCsv
{
    public class Program
    {
        private static void Main(string[] args)
        {
            foreach (var food in args)
            {
                var foodLwr = food.ToLower();

                if (foodLwr == "kaas" || foodLwr == "worst" || foodLwr == "oude kaas")
                {
                    Console.WriteLine($"Hallo allemaal, {foodLwr} met mosterd");
                }
                else if (foodLwr == "komkommer" || foodLwr == "paprika" || foodLwr == "oude kaas")
                {
                    Console.WriteLine($"Hallo allemaal, {foodLwr} heksenkaas");
                }
                else
                {
                    Console.WriteLine($"{foodLwr} zonder mosterd en heksenkaas");
                }
            }
        }
    }
}
