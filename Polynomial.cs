using System.Collections.Generic;

namespace E.R.I.S
{
    public class Polynomial
    {
        public List<int> Coef { get; set; }
        public int Degree { get; set; }
        public string Indeterminate { get; set; }
        public Polynomial(List<int> coefficients)
        {
            Coef = coefficients;
            Degree = Coef.Count - 1;
            Indeterminate = "x";
        }
        public Polynomial(List<int> coefficients, string indenterminate)
        {
            Coef = coefficients;
            Degree = Coef.Count - 1;
            Indeterminate = indenterminate;
        }
        public override string ToString()
        {
            if (Degree == 0)
            {
                return Coef[0].ToString();
            }
            if (Degree == 1)
            {
                return $"{FirstCoefString(Coef[0])}{Indeterminate} {LastCoef(Coef[1])}";
            }
            if (Degree == 2)
            {
                return $"{FirstCoefString(Coef[0])}{Indeterminate}^2 {CoefString(Coef[1])}{Indeterminate} {LastCoef(Coef[2])}";
            }
            string P = FirstCoefString(Coef[0]) + Indeterminate + "^" + Degree;
            for (int i = 1; i <= Degree - 2; i++)
            {
                P += $"{CoefString(Coef[i])}{Indeterminate}^{Degree - i} ";
            }
            P += $"{CoefString(Coef[Degree - 1])}{Indeterminate} {LastCoef(Coef[Degree])}";
            return P;
        }
        public bool AllCoefZero()
        {
            for (int i = 0; i <= Degree; i++)
            {
                if (Coef[i] != 0)
                {
                    return false;
                }
            }
            return true;
        }
        private string FirstCoefString(int a)
        {
            if (a == -1)
            {
                return "- ";
            }
            if (a < 0)
            {
                return $"- {-a}";
            }
            if (a == 1)
            {
                return " ";
            }
            return $" {a}";
        }
        private string CoefString(int a)
        {
            if (a == -1)
            {
                return "- ";
            }
            if (a < 0)
            {
                return $"- {-a}";
            }
            if (a == 1)
            {
                return "+ ";
            }
            return $"+ {a}";
        }
        private string LastCoef(int a) => a < 0 ? $"- {-a}" : $"+ {a}";

    }
}

