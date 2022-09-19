/*
calculator.cs

Copyright(c) 2022 S'(s1gnsgrfu)

This software is released under the MIT License.
see https://github.com/s1gnsgrfu/calculator_cs/blob/master/LICENSE
*/

using System.Text.RegularExpressions;

namespace Calculator
{
    class calm
    {
        private static int? culflg = 0, flag = 0, culflg2 = 0;
        private static int prod1 = 0, prod2 = 0, c_cnt1 = 0, c_pcnt1 = 0, c_pcnt2 = 0, c_cnt2 = 0, carr1 = 0, carr2 = 0, tmpcf = 0, err = 0, cntp1 = 0, cntp2 = 0, res = 0;
        private static decimal va1 = 0, va2 = 0, result = 0, ctmp1_1, ctmp1_2 = 0, ctmp2_1 = 0, ctmp2_2 = 0, pow = 1;
        private static string tmp;
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
                flag = c1.Stsp(Console.ReadLine());

                if (flag == null)
                {
                    return 0;
                }else if (flag == 2)
                {
                    if (res == 0)
                    {
                        
                        Console.WriteLine("----------");
                        Console.WriteLine(result);
                        Console.WriteLine("----------");

                        culflg = 0;
                        c_cnt1 = c_pcnt1 = carr1 = tmpcf = err = cntp1 = cntp2 = tmpcf = 0;
                        va1 = result = 0;
                    }
                    else if (res == 1)
                    {
                        va1 = result;
                        tmpcf = c_cnt1 = 1;
                        //Console.WriteLine("va1 --> " + va1);
                    }
                    flag = 0;
                    carr2 = c_pcnt2 = c_cnt2 = 0;
                    ctmp2_1 = ctmp2_2 = va2 = ctmp1_1 = ctmp1_2 = 0;

                    //処理終了初期化
                }
                else
                {
                    ;
                }
            }
        }

        private int? Stsp(in string str)
        {
            calm c1 = new calm();

            culflg2 = prod1 = prod2 = cntp1 = cntp2 = 0;
            pow = 1;

            if ((va1 == 0) && (err == 1)) 
            {
                c_cnt1 = c_pcnt1 = carr1 = 0;
            }
            else if ((va1 != 0) && (err == 1)) 
            {
                c_cnt2 = c_pcnt2 = carr2 = 0;
            }

            err = 0;

            foreach (char tmp in str)
            {
                if (va1 == 0)
                {
                    c_cnt1++;               //c_cntに-1すればピリオド前の数値数
                                            //c_cntからc_pcntを引けばピリオド後の数値数
                    if (tmp == '.')
                    {
                        if (prod1 == 0)
                        {
                            prod1 = 1;
                            cntp1++;
                            c_pcnt1 = c_cnt1;
                        }
                        else
                        {
                            cntp1++;
                        }
                        
                    }
                }
                else if ((c_cnt1 != 0) && (tmpcf == 1)) 
                {
                    c_cnt2++;

                    if (tmp == '.')
                    {
                        if (prod2 == 0)
                        {
                            prod2 = 1;
                            cntp2++;
                            c_pcnt2 = c_cnt2;
                        }
                        else
                        {
                            cntp2++;
                        }
                    }
                }

                switch (tmp)
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
                //Console.WriteLine(culflg);
            }
            /*
            Console.WriteLine("c_cnt1  --> "+c_cnt1);
            Console.WriteLine("c_pcnt1 -->"+c_pcnt1);
            Console.WriteLine("c_cnt2  --> " + c_cnt2);
            Console.WriteLine("c_pcnt2  -->" + c_pcnt2);
            Console.WriteLine("prod1 --> " + prod1);
            Console.WriteLine("pro21 --> " + prod2);
            */
            if (Regex.IsMatch(str, "^--"))
            {

                switch (str[2..^0])
                {
                    case "help":
                        Console.WriteLine("help");
                        return 0;
                    case "version":
                        Console.WriteLine("version");
                        Console.WriteLine("v1.0.0");
                        return 0;
                    default:
                        Console.WriteLine("This command is not defined");
                        return 0;
                }
            } else if ((prod1 == 1) && (culflg2 == 0) && (cntp1 == 1))
            {
                carr1 = c_cnt1 - c_pcnt1;                       //小数点後の数値の数
                tmp = str.Substring(c_pcnt1, carr1);            //小数点後の数値（文字列）
                ctmp1_1 = decimal.Parse(str[0..^carr1]);        //小数点前の数値をdecimal型に変換
                ctmp1_2 = decimal.Parse(tmp);                   //小数点後の数値をdecimal型に変換

                for (int i = 0; i < carr1; i++)
                {
                    pow = Decimal.Multiply(pow, 0.1m);
                }
                va1 = ctmp1_1 + ctmp1_2 * pow;                  //小数点前後の数値を結合
                return 0;
            } else if ((prod2 == 1) && (culflg2 == 0) && (va2 == 0) && (cntp2 == 1))
            {
                carr2 = c_cnt2 - c_pcnt2;                       //小数点後の数値の数
                tmp = str.Substring(c_pcnt2, carr2);            //小数点後の数値（文字列）
                ctmp2_1 = decimal.Parse(str[0..^carr2]);        //小数点前の数値をdecimal型に変換
                ctmp2_2 = decimal.Parse(tmp);                   //小数点後の数値をdecimal型に変換

                for (int i = 0; i < carr2; i++)
                {
                    pow = Decimal.Multiply(pow, 0.1m);
                }
                va2 = ctmp2_1 + ctmp2_2 * pow;                  //小数点前後の数値を結合
                return 0;
            }
            else if (Regex.IsMatch(str, "^([0-9])+$"))
            {
                if ((culflg2 == 0) && (va1 == 0) && (res == 0))
                {
                    va1 = decimal.Parse(str);
                }
                else if ((culflg2 == 0) && (va1 != 0) && (culflg != 0))
                {
                    va2 = decimal.Parse(str);
                } else if ((culflg2 == 0) && (res == 1)) 
                {
                    va2 = decimal.Parse(str);
                }
                else
            {
                Console.WriteLine("please enter the operator");
            }
                return 0;
            }
            else if (str == "exit")
            {
                return null;
            }
            else
            {
                res = 0;
                switch (str)
                {
                    case "+":
                    case "q":
                        if (tmpcf == 1)
                        {
                            Eq();
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
                            Eq();
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
                            Eq();
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
                            Eq();
                            res = 1;
                            culflg = 4;

                            return 2;
                        }
                        culflg = 4;
                        tmpcf = 1;
                        break;
                    case "=":
                    case "a":
                        Eq();
                        return 2;
                    default:
                        if ((va1 != 0) && (culflg == 0)) 
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
        }
        
        private int? Eq()
        {
            calm c1 = new calm();

            switch (culflg)
            {
                case 1:
                    result = va1 + va2;
                    break;
                case 2:
                    result = va1 - va2;
                    break;
                case 3:
                    result = va1 * va2;
                    break;
                case 4:
                    result = va1 / va2;
                    break;
            }
            return 0;
        }
    }
}
