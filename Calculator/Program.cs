using CalculatorServices.Interfaces;
using CalculatorServices.Services;
using System;

namespace Calculator
{
    public class Program
    {
        #region Properties

        private readonly ICalculatorService _calculatorService;

        #endregion

        #region Constructors

        public Program(ICalculatorService calculatorService)
        {
            _calculatorService = calculatorService;
        }

        #endregion

        #region Public Methods

        public static void Main()
        {
            var program = new Program(new CalculatorService());

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("*****       Lets Calculate Some Numbers!      *****");
            Console.WriteLine("*****       (press Ctrl-C to end)      *****");
            Console.WriteLine(Environment.NewLine);

            program.DoAdd();
        }

        public void DoAdd()
        {
            _calculatorService.CustomDelimiters
                .Clear();

            var defaultDelim = _calculatorService.DefaultDelimiter;

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"Provide default delimiter: (default is {defaultDelim}. Press enter to accept default.) ");
            Console.ForegroundColor = ConsoleColor.White;

            _calculatorService.DefaultDelimiter = Console.ReadLine();

            Console.WriteLine(Environment.NewLine);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"Allow negative numbers? (Y/N)");
            Console.ForegroundColor = ConsoleColor.White;

            _calculatorService.UpdateAllowNegativeNumbers(Console.ReadKey().KeyChar);

            var upperBoundary = _calculatorService.UpperBounds;

            Console.WriteLine(Environment.NewLine);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"Override Upper Boundary? (default is {upperBoundary}. Press enter to accept default.) ");
            Console.ForegroundColor = ConsoleColor.White;

            _calculatorService.UpdateUpperBoundary(Console.ReadLine());

            Console.WriteLine(Environment.NewLine);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Provide any custom delimiters: (comma delimited. Leave blank for none) ");
            Console.ForegroundColor = ConsoleColor.White;

            _calculatorService.CustomDelimiters
                .AddRange(Console.ReadLine().Split(','));

            Console.WriteLine(Environment.NewLine);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("How would you like them calculated?  ");
            Console.WriteLine("    1 = Add  ");
            Console.WriteLine("    2 = Subtract  ");
            Console.WriteLine("    3 = Multiply  ");
            Console.WriteLine("    4 = Divide  ");
            Console.ForegroundColor = ConsoleColor.White;

            _calculatorService.UpdateOperator(Console.ReadKey().KeyChar);

            Console.WriteLine(Environment.NewLine);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Please provide the values to be calculated:  ");
            Console.ForegroundColor = ConsoleColor.White;

            _calculatorService.ValidateInput(Console.ReadLine());

            Console.WriteLine(Environment.NewLine);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("*** The sum is:");
            Console.ForegroundColor = ConsoleColor.Blue;

            Console.WriteLine(_calculatorService.Calculate());

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("*** Formula used:");
            Console.ForegroundColor = ConsoleColor.Blue;

            Console.WriteLine(_calculatorService.GetArgumentsString());

            Console.WriteLine(Environment.NewLine);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("*****       Lets Calculate Some More!      *****");
            Console.WriteLine("*****       (press Ctrl-C to end)      *****");
            Console.WriteLine(Environment.NewLine);

            DoAdd();
        }

        #endregion
    }
}
