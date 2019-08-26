using CalculatorServices.Interfaces;
using CalculatorUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CalculatorServices.Services
{
    public class CalculatorService : ICalculatorService
    {
        #region Properties

        public bool AllowNegativeNumbers { get; set; }

        public double UpperBounds { get; set; } = 1000;

        public List<string> CustomDelimiters { get; set; }

        private int _operator;

        private List<string> _argumentsArray;

        private List<Double> _argumentValues;

        private string _defaultDelimiter;
        public string DefaultDelimiter
        {
            get
            {
                return !String.IsNullOrEmpty(_defaultDelimiter) ?
                    _defaultDelimiter :
                    "\\n";
            }
            set
            {
                _defaultDelimiter = value;
            }
        }

        private string _operatorCharater
        {
            get
            {
                switch (_operator)
                {
                    case 2:
                        return "-";
                    case 3:
                        return "*";
                    case 4:
                        return "/";
                    default:
                        return "+";
                }
            }
        }

        #endregion

        #region Constructors

        public CalculatorService()
        {
            CustomDelimiters = new List<string>();
        }

        #endregion

        #region Public Methods

        public string GetArgumentsString()
        {
            var displayString = new StringBuilder(_argumentValues.First().ToString());

            for(int index = 1; index < _argumentValues.Count(); index++)
            {
                displayString.AppendFormat("{0}{1}", _operatorCharater, _argumentValues[index]);
            }

            displayString.AppendFormat(" = {0}", Calculate());

            return displayString.ToString();
        }

        public void UpdateAllowNegativeNumbers(char inputValue)
        {
            AllowNegativeNumbers = char.ToUpper(inputValue) == 'Y' ?
                true :
                false;
        }

        public void UpdateUpperBoundary(string inputValue)
        {
            Double converted;
            if(Double.TryParse(inputValue.ToCharArray(), out converted))
            {
                UpperBounds = converted;

                return;
            }

            Console.WriteLine("Blank or invalid value provided. Using default.");
        }

        public void UpdateOperator(char inputValue)
        {
            _operator = 1;

            int updateValue;
            if (!int.TryParse(inputValue.ToString(), out updateValue) ||
                updateValue < 0 ||
                updateValue > 5)
            {
                Console.WriteLine(Environment.NewLine);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("!!! Error. Invalid operator provided. Defaulting to addition.");
            }

            _operator = updateValue;
        }

        public Double Calculate()
        {
            switch(_operator)
            {
                case 2:
                    return _argumentValues.Subtract();
                case 3:
                    return _argumentValues.Multiply();
                case 4:
                    return _argumentValues.Divide();
                default:
                    return _argumentValues.Sum();
            }         
        }

        public void ValidateInput(string args)
        {
            var negativeValues = new List<Double>();
            _argumentValues = new List<Double>();

            //Requirement 1, Bullet #1
            var argsArray = args.Split(',');
            _argumentsArray = new List<string>(argsArray);

            //Requirement #6,7,8
            foreach (var customDelimiter in CustomDelimiters)
            {
                ParseDelimiter(argsArray, customDelimiter);
                argsArray = _argumentsArray.ToArray();
            }

            //Requirement #3
            ParseDelimiter(argsArray, DefaultDelimiter);
 
            //Requirement #2
            foreach (var inputArg in _argumentsArray)
            {
                Double converted;

                if (!Double.TryParse(inputArg.ToCharArray(), out converted))
                {
                    //Requirement #1, bullet #2
                    converted = 0.0;
                }

                if (!AllowNegativeNumbers && converted < 0)
                {
                    //Requirement #4
                    negativeValues.Add(converted);
                    continue;
                }

                //Requirement #5
                if (converted <= UpperBounds)
                {
                    _argumentValues.Add(converted);
                }
            }

            //Requirement #4
            if (negativeValues.Count > 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("!!! Warning. Negative entries provided! They will be ignored.");

                negativeValues.ForEach(x => Console.WriteLine(String.Format("{0} ", x)));
            }
        }

        #endregion

        #region Private Methods

        private void ParseDelimiter(string[] argsArray, string delimiter)
        {
            argsArray.Where(x => x.Contains(delimiter))
                .All(x =>
                {
                    var splitVals = x.Split(delimiter);

                    _argumentsArray.Remove(x);
                    _argumentsArray.AddRange(splitVals);

                    return true;
                });
        }

        #endregion
    }
}
