using System;
using System.Collections.Generic;
using System.Text;
using Zino.Domain.SeedWork;
using Zino.Service.Attributes;

namespace Zino.Service.Enums
{

    /// <summary>
    /// farz bar in ke in mavared dar DB ijad shode va ma az Db Get kardim
    /// </summary>
    public enum UnitTypes
    {
        [UnitAttributes(
            Name = "Inch",
            Code = "In",
            Unit = 96)]
        In,
        [UnitAttributes(
            Name = "Millimeter",
            Code = "mm",
            Unit = 0.001)]
        mm,
        [UnitAttributes(
            Name = "Meter",
            Code = "m",
            Unit = 1)]
        m,
        [UnitAttributes(
            Name = "Centimeter",
            Code = "cm",
            Unit = 0.01)]
        cm,
        [UnitAttributes(
            Name = "Kilometer",
            Code = "km",
            Unit = 1000)]
        km,


        [UnitAttributes(
             Name = "Milligram",
             Code = "mg",
             Unit = 0.001)]
        mg,
        [UnitAttributes(
            Name = "Kilogram",
            Code = "kg",
            Unit = 1000)]
        kg,
        [UnitAttributes(
            Name = "Tone",
            Code = "ton",
            Unit = 1000000)]
        ton,
    }

    public struct Unit
    {
        public Unit(double value, UnitTypes type)
        {
            this._value = value;

            this._type = type;
        }
        private double _value;
        public double Value
        {
            get { return _value; }
            set { _value = value; }
        }
        private UnitTypes _type;
        public UnitTypes Type
        {
            get { return _type; }
            set
            {
                this.Value = this.To(value).Value;

                _type = value;
            }
        }

        public Unit To(UnitTypes unitType)
        {
            return new Unit((this.Value * this.GetPixelPer(this.Type)) / this.GetPixelPer(unitType), unitType);
        }

        public double GetPixelPer(UnitTypes unitType)
        {
            return unitType switch
            {
                UnitTypes.cm => this.GetPixelPer(UnitTypes.In) * 2.54F,
                UnitTypes.In => 96,
                UnitTypes.mm => this.GetPixelPer(UnitTypes.m) / 1000,
                UnitTypes.m => this.GetPixelPer(UnitTypes.cm) * 100,
                UnitTypes.km => this.GetPixelPer(UnitTypes.m) * 1000,
                _ => 1,
            };
        }
    }
    public interface IUnitFurmola 
    {
        double Eval(string expression);
    }
    public class UnitFurmola : IUnitFurmola
    {
        private string[] _operators = { "-", "+", "/", "*" };
        private Func<double, double, double>[] _operations = {
        (a1, a2) => a1 - a2,
        (a1, a2) => a1 + a2,
        (a1, a2) => a1 / a2,
        (a1, a2) => a1 * a2,
        (a1, a2) => Math.Pow(a1, a2)};

        public double Eval(string expression)
        {
            List<string> tokens = getTokens(expression);
            Stack<double> operandStack = new Stack<double>();
            Stack<string> operatorStack = new Stack<string>();
            int tokenIndex = 0;

            while (tokenIndex < tokens.Count)
            {
                string token = tokens[tokenIndex];
                if (token == "(")
                {
                    string subExpr = getSubExpression(tokens, ref tokenIndex);
                    operandStack.Push(Eval(subExpr));
                    continue;
                }
                if (token == ")")
                {
                    throw new ArgumentException("Mis-matched parentheses in expression");
                }
                //If this is an operator  
                if (Array.IndexOf(_operators, token) >= 0)
                {
                    while (operatorStack.Count > 0 && Array.IndexOf(_operators, token) < Array.IndexOf(_operators, operatorStack.Peek()))
                    {
                        string op = operatorStack.Pop();
                        double arg2 = operandStack.Pop();
                        double arg1 = operandStack.Pop();
                        operandStack.Push(_operations[Array.IndexOf(_operators, op)](arg1, arg2));
                    }
                    operatorStack.Push(token);
                }
                else
                {
                    operandStack.Push(double.Parse(token));
                }
                tokenIndex += 1;
            }

            while (operatorStack.Count > 0)
            {
                string op = operatorStack.Pop();
                double arg2 = operandStack.Pop();
                double arg1 = operandStack.Pop();
                operandStack.Push(_operations[Array.IndexOf(_operators, op)](arg1, arg2));
            }
            return operandStack.Pop();
        }

        private string getSubExpression(List<string> tokens, ref int index)
        {
            StringBuilder subExpr = new StringBuilder();
            int parenlevels = 1;
            index += 1;
            while (index < tokens.Count && parenlevels > 0)
            {
                string token = tokens[index];
                if (tokens[index] == "(")
                {
                    parenlevels += 1;
                }

                if (tokens[index] == ")")
                {
                    parenlevels -= 1;
                }

                if (parenlevels > 0)
                {
                    subExpr.Append(token);
                }

                index += 1;
            }

            if ((parenlevels > 0))
            {
                throw new ArgumentException("Mis-matched parentheses in expression");
            }
            return subExpr.ToString();
        }

        private List<string> getTokens(string expression)
        {
            string operators = "()^*/+/-";
            List<string> tokens = new List<string>();
            StringBuilder sb = new StringBuilder();

            foreach (char c in expression.Replace(" ", string.Empty))
            {
                if (operators.IndexOf(c) >= 0)
                {
                    if ((sb.Length > 0))
                    {
                        tokens.Add(sb.ToString());
                        sb.Length = 0;
                    }
                    tokens.Add(c.ToString());
                }
                else
                {
                    sb.Append(c);
                }
            }

            if ((sb.Length > 0))
            {
                tokens.Add(sb.ToString());
            }
            return tokens;
        }

        public double Convert(string requets)
        {
            string pattern = @"(\d+)\s+([-+*/])\s+(\d+)";
            foreach (var expression in requets)
                foreach (System.Text.RegularExpressions.Match m in System.Text.RegularExpressions.Regex.Matches(requets, pattern))
                {
                    int value1 = Int32.Parse(m.Groups[1].Value);
                    int value2 = Int32.Parse(m.Groups[3].Value);

                    return (m.Groups[2].Value) switch
                    {
                        "+" => value1 + value2,
                        "-" => value1 - value2,
                        "/" => value1 / value2,
                        _ => 0,
                    };
                }
            return 1;
        }

    }
}
