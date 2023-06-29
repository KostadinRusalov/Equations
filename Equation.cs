using System;
using System.Linq;
using System.Collections.Generic;
using RealNumbers;

namespace E.R.I.S
{
    public static class Equation
    {
        private static string output = "";
        private static List<Fraction> solutions;

        public static string Output() => output;
        public static string Solutions()
        {
            string sol = "";
            for (int i = 0; i < solutions.Count; i++)
            {
                sol += $"x{i + 1} = {solutions[i]} \n";
            }
            return sol;
        }

        public static void QuadraticEquation(Polynomial p)
        {
            // nqkoi slunchev den napravi tozi metod po dobur:
            // taka che da moje da se vika ot HigherDegree()
            if (p.Degree == 1)
            {
                int a = p.Coef[0];
                int b = p.Coef[1];
                output = a == 0 ?
                    string.Format(" 0{0} = {1} has no solution :(", p.Indeterminate, b)
                    : string.Format(" {0} = {1}", p.Indeterminate, new Fraction(-b, a));
            }
            else
            {
                int a = p.Coef[0];
                int b = p.Coef[1];
                int c = p.Coef[2];
                if (a == 0)
                {
                    output = b == 0 ?
                        string.Format(" 0{0} = {1} has no solution :(", p.Indeterminate, b)
                        : string.Format(" {0} = {1}", p.Indeterminate, new Fraction(-c, b));
                }
                else
                {
                    int D = b * b - 4 * a * c;
                    string d = b < 0 ? $"(-{-b})^2 - 4({a})({c})" : $"{b}^2 - 4({a})({c})";
                    if (D < 0)
                    {
                        output = string.Format(" D = {0} = {1} < 0 => no real solutions :(", d, D);
                    }
                    else if (D == 0)
                    {
                        string x = b < 0 ? $"-({b})/2({a})" : $"{-b}/2({a})";
                        output = string.Format(" D = {1} = 0 = > {0}1 = {1}2 = {2} = {3}", p.Indeterminate, d, x, new Fraction(-b, 2 * a));
                    }
                    else
                    {
                        string x = b < 0 ? $"(-({b}) ± √{D})/2({a})" : $"(-{b} ± √{D})/2({a})";
                        output = string.Format(" D = {1} = {2} > 0 => {0}1,2 = {3}", p.Indeterminate, d, D, x);
                        output += "\n";
                        output += string.Format("     {0}1 = {1} ", p.Indeterminate, new Fraction(new Number(-b, new Irrational(D)), 2 * a));
                        output += "\n";
                        output += string.Format("     {0}2 = {1}", p.Indeterminate, new Fraction(new Number(-b, -new Irrational(D)), 2 * a));
                    }
                }
            }
        }
        public static void BiquadraticEquation(Polynomial P)
        {
            int a = P.Coef[0];
            int b = P.Coef[2];
            int c = P.Coef[4];
            List<int> Tcoef = new List<int> { a, b, c };
            Polynomial T = new Polynomial(Tcoef, "t");
            Console.WriteLine(T + " = 0");
            output += " Let x^2 = t, t ≥ 0";
            int D = b * b - 4 * a * c;
            string d = b < 0 ? $"(-{-b})^2 - 4({a})({c})" : $"{b}^2 - 4({a})({c})";
            if (D < 0)
            {
                output += "\n";
                output += string.Format(" D = {0} = {1} < 0 => no real solutions :(", d, D);
            }
            else if (D == 0)
            {
                output += "\n";
                string t = b < 0 ? $"-({b})/2({a})" : $"{-b}/2({a})";
                output += string.Format(" D = {1} = 0{4} = > {0}1 = {1}2 = {2} = {3}", T.Indeterminate, d, t, new Fraction(-b, 2 * a), "\n");
                if (new Fraction(-b, 2 * a) < 0)
                {
                    output += "< 0 => no sulution :(";

                }
                else
                {
                    output += "> 0";
                    output += "\n";
                    Fraction ts = new Fraction(-b, 2 * a);
                    Fraction xs = ts.Sqrt();
                    output += string.Format("     {0}1 = {0}2 = √{1} = {2}", P.Indeterminate, ts, xs);
                }
            }
            else
            {
                string t = b < 0 ? $"(-({b}) ± √{D})/2({a})" : $"(-{b} ± √{D})/2({a})";
                Fraction t1 = new Fraction(new Number(-b, new Irrational(D)), 2 * a);
                Fraction t2 = new Fraction(new Number(-b, -new Irrational(D)), 2 * a);
                output = string.Format(" D = {1} = {2} > 0 => {0}1,2 = {3}", T.Indeterminate, d, D, t);
                output += "\n";
                if (t1.Numerator.Integer < 0 && t2.Numerator.Integer < 0)
                {
                    output += string.Format("     {0}1 = {1} < 0        {0}2 = {2} < 0", T.Indeterminate, t1, t2);
                    output += "\n" + " => no solutuions :(";
                }
                else if (t1.Numerator.Integer > 0 && t2.Numerator.Integer < 0)
                {
                    Fraction x1 = t1.Sqrt();
                    output += string.Format("     {0}1 = {1} ≥ 0        {0}2 = {2} < 0", T.Indeterminate, t1, t2);
                    output += "\n";
                    output += string.Format("     {0}1,2 = ±{1}", P.Indeterminate, x1);
                }
                else if (t1.Numerator.Integer < 0 && t2.Numerator.Integer > 0)
                {
                    Fraction x1 = t2.Sqrt();
                    output += string.Format("     {0}1 = {1} > 0        {0}2 = {2} < 0", T.Indeterminate, t2, t1);
                    output += "\n";
                    output += string.Format("     {0}1,2 = ±{1}", P.Indeterminate, x1);
                }
                else
                {
                    Fraction x1 = t1.Sqrt();
                    Fraction x2 = t2.Sqrt();
                    output += string.Format("     {0}1 = {1} ≥ 0        {0}2 = {2} ≥ 0", T.Indeterminate, t1, t2);
                    output += "\n";
                    output += string.Format("     {0}1,2 = ±{1}         {0}3,4 = ±{2}", P.Indeterminate, x1, x2);
                }
            }
        }
        public static void HigherDegree(Polynomial P)
        {
            List<Fraction> eq = P.Coef.Select(x => new Fraction(x)).ToList();
            int deg = eq.Count - 1;

            List<Fraction> possibleSolutions = PossibleSolutions(eq[0].Numerator.Integer, eq[eq.Count - 1].Numerator.Integer);

            int trys = -1;
            Dictionary<int, Fraction> possSol = new Dictionary<int, Fraction>();
            Dictionary<int, List<Fraction>> rows = new Dictionary<int, List<Fraction>>();

            solutions = new List<Fraction>();

            for (int i = 0; i < possibleSolutions.Count; i++)
            {
                Fraction p = possibleSolutions[i];
                List<Fraction> coef = new List<Fraction>().Append(eq[0]).ToList();
                for (int j = 1; j < eq.Count; j++)
                {
                    coef.Add(p * coef[j - 1] + eq[j]);
                }
                trys++;
                possSol.Add(trys, p);
                rows.Add(trys, coef);
                if (coef[coef.Count - 1] == 0)
                {
                    eq = coef;
                    eq.RemoveAt(eq.Count - 1);
                    i--;
                    solutions.Add(p);
                    if (solutions.Count == deg) break;
                }
            }
            output = HornerTable(P.Coef.Select(x => new Fraction(x)).ToList(), rows, possSol);
        }

        private static List<Fraction> PossibleSolutions(int aN, int a0)
        {
            List<int> divisorsOfa0 = Divisors(a0);
            List<int> divisorsOfaN = Divisors(aN);
            List<Fraction> possibleRationalSolutions = new List<Fraction>();
            foreach (int denominator in divisorsOfaN)
            {
                foreach (int numerator in divisorsOfa0)
                {
                    possibleRationalSolutions.Add(new Fraction(numerator, denominator));
                }
            }
            return Fraction.Unique(possibleRationalSolutions);
        }

        private static List<int> Divisors(int number)
        {
            int a = Math.Abs(number);
            List<int> divisors = new List<int>();
            for (int i = 1; i <= a; i++)
            {
                if (a % i == 0)
                {
                    divisors.Add(i);
                    divisors.Add(-i);
                }
            }
            return divisors;
        }

        private static int MaxLenth(Dictionary<int, List<Fraction>> row, Dictionary<int, Fraction> posSol)
        {
            int max = new int();
            for (int i = 0; i < row.Count; i++)
            {
                char[] lp = posSol[i].ToString().ToCharArray();
                max = lp.Length > max ? lp.Length : max;
                foreach (Fraction f in row[i])
                {
                    char[] lr = f.ToString().ToCharArray();
                    max = lr.Length > max ? lr.Length : max;
                }
            }
            return max;
        }

        private static string HornerTable(List<Fraction> coef, Dictionary<int, List<Fraction>> rows, Dictionary<int, Fraction> sols)
        {
            int max = MaxLenth(rows, sols) + 1;
            string row = new string(' ', max) + " |";

            for (int i = 0; i < coef.Count; i++)
            {
                char[] l = coef[i].ToString().ToCharArray();
                row += new string(' ', max - l.Length) + coef[i];
            }
            int e = coef.Count;
            row += "\n" + new string('-', max + 1) + "+" + new string('-', (max - 2) * e + 7) + "\n";
            for (int i = 0; i < rows.Count; i++)
            {
                char[] pS = sols[i].ToString().ToCharArray();
                row += new string(' ', max - pS.Length) + sols[i] + " |";

                foreach (Fraction f in rows[i])
                {
                    char[] lR = f.ToString().ToCharArray();
                    row += new string(' ', max - lR.Length) + f;
                }
                if (e > rows[i].Count)
                {
                    e = rows[i].Count;
                    row += new string(' ', max - 1) + "0";
                    row += "\n" + new string('-', max + 1) + "+" + new string('-', (max - 2) * e + 4) + "\n";
                }
                else
                {
                    row += "\n";
                }
            }
            return row;
        }
    }
}