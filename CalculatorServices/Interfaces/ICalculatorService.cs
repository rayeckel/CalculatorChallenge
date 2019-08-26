using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorServices.Interfaces
{
    public interface ICalculatorService
    {
        bool AllowNegativeNumbers { get; set; }

        string DefaultDelimiter { get; set; }

        List<string> CustomDelimiters { get; set; }

        double UpperBounds { get; set; }

        void UpdateAllowNegativeNumbers(char inputValue);

        void UpdateUpperBoundary(string inputValue);

        void UpdateOperator(char inputValue);

        string GetArgumentsString();

        void ValidateInput(string args);

        Double Calculate();
    }
}
