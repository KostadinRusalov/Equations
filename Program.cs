using System;
using System.Linq;
using System.Collections.Generic;

namespace E.R.I.S
{
    // E.R.I.S.2 is the second verson of "highEr degRee equatIon Solving" program
    // which shows how to solve quadratic, biquadratic and higher degree equations. 
    // by: Kostadin Roussalov

    /*
       E.R.I.S.2.1 was finishied on 25.11.2020
       2.1 finds real solutions of quadratic and biquadratic equations and rational ones of higher degree equations.
       Version 2.2 is expected to find real solutions also of higher degree equations.
    */

    class MainClass
    {
        public static void Main(string[] args)
        {
            string[] commands = Console.ReadLine().Split().ToArray();
            while (commands[0].ToLower() != "end")
            {
                try
                {

                    List<int> coef = commands.Select(int.Parse).ToList();
                    Polynomial P = new Polynomial(coef);
                    if (P.AllCoefZero())
                    {
                        Console.WriteLine("0x = 0 has infinite solutions");
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.WriteLine(P + " = 0");
                        if (P.Degree < 3)
                        {
                            Equation.QuadraticEquation(P);
                            Console.WriteLine(Equation.Output() + "\n");
                        }
                        else if (P.Degree == 4 && P.Coef[1] == 0 && P.Coef[3] == 0 && P.Coef[0] != 0)
                        {
                            Equation.BiquadraticEquation(P);
                            Console.WriteLine(Equation.Output() + "\n");
                        }
                        else
                        {
                            Equation.HigherDegree(P);
                            Console.WriteLine(Equation.Output());
                            Console.WriteLine(Equation.Solutions());

                        }
                    }
                    commands = Console.ReadLine().Split().ToArray();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("error");
                    throw ex;
                }
            }
        }
    }
}