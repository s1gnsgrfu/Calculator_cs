/*
calculator.cs

Copyright(c) 2022 S'(s1gnsgrfu)

This software is released under the MIT License.
see https://github.com/s1gnsgrfu/calculator_cs/blob/master/LICENSE
*/

using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace Calculator
{
    class calm
    {
        private static int? culflg = 0, flag = 0, culflg2 = 0;
        private static int prod1 = 0, prod2 = 0, tmpcf = 0, err = 0, res = 0, pi_flag = 0;
        private static decimal result = 0, pow = 1;
        //private static string? tmp;
        private int[] c_cnt = new int[2];
        private int[] c_pcnt = new int[2];
        private int[] carr = new int[2];
        private int[] cntp = new int[2];
        private decimal[] va = new decimal[2];
        private decimal[] ctmp = new decimal[4];

        private readonly decimal pi = 3.14159265359m;


        //culflg -> 1:+ , 2:- , 3:* , 4:/
        //flag -> null:exit , 2:result
        //operator --> q:+ , w:- , e:* , r:/ , a:=
        static int Main()
        {
            calm c1 = new calm();

            Console.WriteLine("Calculator");
            Console.WriteLine("If you need help , type --help");

            while (true)
            {
                Console.WriteLine("");
                Console.Write(">>");
                flag = c1.Identification_string(Console.ReadLine());

                if (flag == null)
                {
                    return 0;
                } else if (flag == 2)
                {
                    if (res == 0)
                    {

                        Console.WriteLine("----------");
                        Console.WriteLine(result);
                        Console.WriteLine("----------");

                        culflg = 0;
                        c1.c_cnt[0] = c1.c_pcnt[0] = c1.carr[0] = tmpcf = err = c1.cntp[0] = c1.cntp[1] = tmpcf = 0;
                        c1.va[0] = result = 0;
                    }
                    else if (res == 1)
                    {
                        c1.va[0] = result;
                        tmpcf = c1.c_cnt[0] = 1;
                    }
                    flag = 0;
                    c1.carr[1] = c1.c_pcnt[1] = c1.c_cnt[1] = 0;
                    c1.ctmp[2] = c1.ctmp[3] = c1.va[1] = c1.ctmp[0] = c1.ctmp[1] = 0;


                    //処理終了初期化
                }
                else
                {
                    ;
                }
            }
        }

        private int? Identification_string(in string str)
        {
            calm c1 = new calm();



            //エラー時のロールバック
            if ((va[0] == 0) && (err == 1))
            {
                c_cnt[0] = c_pcnt[0] = carr[0] = 0;
            }
            else if ((va[0] != 0) && (err == 1))
            {
                c_cnt[1] = c_pcnt[1] = carr[1] = 0;
            }

            culflg2 = prod1 = prod2 = cntp[0] = cntp[1] = 0;
            pow = 1;
            err = 0;

            foreach (char tmpc in str)
            {
                String_analyze(tmpc);

            }
            /*
            debug
            Console.WriteLine("c_cnt1  --> " + c_cnt[0]);
            Console.WriteLine("c_pcnt1 -->"+c_pcnt[0]);
            Console.WriteLine("c_cnt2  --> " + c_cnt[1]);
            Console.WriteLine("c_pcnt2  -->" + c_pcnt[1]);
            Console.WriteLine("prod1 --> " + prod1);
            Console.WriteLine("pro21 --> " + prod2);
            Console.WriteLine("culflg --> " + culflg);
            Console.WriteLine("culflg2 --> " + culflg2);
            */

            if (Regex.IsMatch(str, "^--"))
            {
                Disp_option(str);
                return 0;
            }
            else if (str == ".")
            {
                Console.WriteLine("Error");
                err = 1;
                return 0;
            }
            else if ((prod1 == 1) && (culflg2 == 0) && (cntp[0] == 1))
            {
                Decimal_coupling(0, str);
                return 0;

            }
            else if ((prod2 == 1) && (culflg2 == 0) && (va[1] == 0) && (cntp[1] == 1))
            {
                Decimal_coupling(1, str);
                return 0;

            }
            else if (Regex.IsMatch(str, "^([0-9])+$"))      //数字のみ
            {
                Numeric_assignment(str);
                return 0;
            }
            else if (str == "exit")
            {
                return null;
            }else if (str == "pi")
            {
                pi_flag = 1;
                Numeric_assignment(str);
                pi_flag = 0;
                return 0;
            }
            else
            {
                flag = Identification_operator(str);
                return flag;

            }
            //return 0;
        }
    
        private void String_analyze(char tmpc)
        {
            if (va[0] == 0)
            {
                c_cnt[0]++;               //c_cntに-1すればピリオド前の数値数
                                          //c_cntからc_pcntを引けばピリオド後の数値数
                if (tmpc == '.')
                {
                    
                    if (prod1 == 0)
                    {
                        prod1 = 1;
                        cntp[0]++;
                        c_pcnt[0] = c_cnt[0];
                    }
                    else
                    {
                        cntp[0]++;
                    }

                }
            }
            else if ((c_cnt[0] != 0) && (tmpcf == 1))
            {
                c_cnt[1]++;

                if (tmpc == '.')
                {
                    if (prod2 == 0)
                    {
                        prod2 = 1;
                        cntp[1]++;
                        c_pcnt[1] = c_cnt[1];
                    }
                    else
                    {
                        cntp[1]++;
                    }
                }
            }

            switch (tmpc)
            {
                case '+':
                    culflg2 = 1;
                    break;
                case '-':
                    culflg2 = 2;
                    break;
                case '*':
                    culflg2 = 3;
                    break;
                case '/':
                    culflg2 = 4;
                    break;
            }
        }

        private void Disp_option(string str)
        {
            switch (str[2..^0])
            {
                case "help":
                    Console.WriteLine("help");
                    break;
                case "version":
                    Console.WriteLine("version");
                    Console.WriteLine("v1.0.0");
                    break;
                default:
                    Console.WriteLine("This command is not defined");
                    break;
            }
            return;
        }

        private void Decimal_coupling(int ind, string str)
        {
            calm c1 = new calm();

            //int ind2 = ind;
            //string tmp;

            va[ind] = decimal.Parse(str);

            /*
            debug
            Console.WriteLine("ind --> " + ind);
            Console.WriteLine("str --> " + str);
            */
            /*
            carr[ind] = c_cnt[ind] - c_pcnt[ind];                       //小数点後の数値の数
            tmp = str.Substring(c_pcnt[ind], carr[ind]);            //小数点後の数値（文字列）
            //Console.WriteLine("tmp --> " + tmp);
            if (ind == 1)
            {
                ind2 += 1;
            }
            ctmp[ind2] = decimal.Parse(str[0..^carr[ind]]);        //小数点前の数値をdecimal型に変換
            ctmp[ind2+1] = decimal.Parse(tmp);                   //小数点後の数値をdecimal型に変換

            for (int i = 0; i < carr[ind]; i++)
            {
                pow = Decimal.Multiply(pow, 0.1m);
            }
            va[ind] = ctmp[ind2] + ctmp[ind2+1] * pow;                  //小数点前後の数値を結合
            */
            return;
        }

        private void Numeric_assignment(string str)
        {
            if ((culflg2 == 0) && (va[0] == 0) && (res == 0))
            {
                if (pi_flag == 1)
                {
                    va[0] = pi;
                }
                else
                {
                    va[0] = decimal.Parse(str);
                }
            }
            else if ((culflg2 == 0) && (va[0] != 0) && (culflg != 0))
            {
                if (pi_flag == 1)
                {
                    va[1] = pi;
                }
                else
                {
                    va[1] = decimal.Parse(str);
                }
            }
            else if ((culflg2 == 0) && (res == 1))
            {
                if (pi_flag == 1)
                {
                    va[1] = pi;
                }
                else
                {
                    va[1] = decimal.Parse(str);
                }
            }
            else
            {
                Console.WriteLine("please enter the operator");
            }
            return;
        }

        private int Identification_operator(string str)
        {
            calm c1 = new calm();
            //Console.WriteLine("str --> " + str);      //debug
            res = 0;
            switch (str)
            {
                case "+":
                case "q":
                    if (tmpcf == 1)
                    {
                        Calculation();
                        res = 1;
                        culflg = 1;
                        return 2;
                    }
                    culflg = 1;
                    tmpcf = 1;
                    break;
                case "-":
                case "w":
                    if (tmpcf == 1)
                    {
                        Calculation();
                        res = 1;
                        culflg = 2;
                        return 2;
                    }
                    culflg = 2;
                    tmpcf = 1;
                    break;
                case "*":
                case "e":
                    if (tmpcf == 1)
                    {
                        Calculation();
                        res = 1;
                        culflg = 3;
                        return 2;
                    }
                    culflg = 3;
                    tmpcf = 1;
                    break;
                case "/":
                case "r":
                    if (tmpcf == 1)
                    {
                        Calculation();
                        res = 1;
                        culflg = 4;

                        return 2;
                    }
                    culflg = 4;
                    tmpcf = 1;
                    break;
                case "=":
                case "a":
                    //Console.WriteLine("culflg --> " + culflg);    //debug
                    Calculation();
                    return 2;
                default:
                    if ((va[0] != 0) && (culflg == 0))
                    {
                        Console.WriteLine("please enter the operator");
                    }
                    else
                    {
                        Console.WriteLine("Error");
                        err = 1;
                    }
                    break;
            }
            return 0;
        }

        private void Calculation()
        {
            calm c1 = new calm();

            switch (culflg)
            {
                case 1:
                    result = va[0] + va[1];
                    break;
                case 2:
                    result = va[0] - va[1];
                    break;
                case 3:
                    result = va[0] * va[1];
                    break;
                case 4:
                    try
                    {
                        result = va[0] / va[1];
                    }
                    catch(DivideByZeroException)
                    {
                        Console.WriteLine("Error");
                        res = 1;
                    }
                    break;
            }
        }
    }
}
