/*
Calculator.cs

Copyright(c) 2022 S'(s1gnsgrfu)

This software is released under the MIT License.
see https://github.com/s1gnsgrfu/Calculator_cs/blob/master/LICENSE
*/

using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.IO;
using System.Text;
using System.Runtime.ExceptionServices;
using System.Diagnostics;

namespace Calculator
{
    class calm
    {
        private static int? culflg = 0, flag = 0;
        private static int tmpcf = 0, res = 0, count = 0, re = 0;
        private static decimal result = 0;
        private int[] err = new int[2];
        private decimal[] va = new decimal[3];
        string[] split_str = new string[99];        //100項まで

        private readonly decimal pi = 3.14159265359m;
        private readonly string version = "v1.0.0";

        //culflg -> 1:+ , 2:- , 3:* , 4:/
        //flag -> null:exit , 2:result
        private void Variable_initialization(int reset_type)
        {
            if (reset_type == 0)        //全変数初期化
            {
                culflg = flag = 0;
                tmpcf = res =  count = 0;
                result = 0;
                for (int i = 0; i < 2; i++)
                {
                    err[i] = 0;
                    va[i] = 0;
                }
                for (int i = 0; i < 99; i++)
                {
                    split_str[i] = "";
                }
            }
            else if (reset_type == 1)       //第3項以降の変数初期化
            {
                tmpcf = 1;
                flag = 0;
            }
            return;
        }

        static void Main()
        {
            Console.WriteLine("Calculator");
            Console.WriteLine("If you need help , type --help");
            InOutput();
        }
        static int InOutput()
        {
            calm c1 = new calm();

            while (true)
            {
                c1.Variable_initialization(0);
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

                        c1.Variable_initialization(0);
                    }
                    else if (res == 1)      //第3項以降 --> 連続処理に要変更
                    {
                        c1.Continuous_process();
                    }
                }else if(flag == 3)     //100項を超えたとき または 全変数初期化
                {
                    c1.Variable_initialization(0);
                }
                else
                {
                    ;
                }
            }
        }

        private void Continuous_process()
        {
            va[0] = result;
            va[1] = 0;
            count = 1;
            Variable_initialization(1);
            return;
        }

        private int? Identification_string(in string str)
        {
            calm c1 = new calm();
            if (Regex.IsMatch(str, "^--"))
            {
                Disp_option(str);
                return 3;
            }
            else if (str == "exit")
            {
                return null;
            }
            Split_string(str);
            if (err[1] == 1)        //100項を超えたとき
            {
                return 3;
            }

            err[0] = 0;
            
            for (int i = 0; i < split_str.Length; i++) {
                String_analyze(split_str[i]);
                if (re == 4)
                {
                    res = 0;
                    return 2;
                }else if(re == 6){
                    break;
                }
            }
            return 3;
        }
        
        private void Split_string(string str)
        {
            int count = 0, count_blank = 0, blank_flag = 0, pre_blank = 0, next_str_flag = 0;
            count = count_blank = blank_flag = pre_blank = next_str_flag = 0;

            foreach(char c in str)
            {
                if (c == ' ')
                {
                    if (count == 0)      //先頭文字の空白削除
                    {
                        ;
                    }
                    else if (blank_flag == count)       //二回目以降の空白繰り返し無視
                    {
                        ;
                    }
                    else if (count_blank == pre_blank)      //文字列代入後初回
                    {
                        count_blank++;
                        blank_flag = count;
                        next_str_flag = 1;
                        
                    }
                }
                else
                {
                    count++;
                    try
                    {
                        split_str[count_blank] += c;
                    }
                    catch (System.IndexOutOfRangeException)
                    {
                        Console.WriteLine("Both numbers and operators must be limited to 100.");
                        err[1] = 1;
                        return;
                    }
                    pre_blank = count_blank;
                    next_str_flag = 0;
                }
            }

            if(next_str_flag == 1)      //文字列末尾の空白判定によるcount_blank増加のロールバック
            {
                count_blank--;
            }
            return;
        }
        private void String_analyze(string str)
        {
            re = 0;
            try
            {
                if (decimal.TryParse(str, out va[count]))
                {
                    count++;
                }
                else if (str == "pi")
                {
                    va[count] = pi;
                    count++;
                }
                else
                {
                    switch (str)
                    {
                        case "+":
                        case "-":
                        case "*":
                        case "/":
                        case "=":
                            re = Identification_operator(str);
                            if (re == 2)
                            {
                                Continuous_process();
                                re = 0;
                            }
                            else if (re == 4)
                            {
                                return;
                            }
                            break;
                    }
                }
            }
            catch(System.IndexOutOfRangeException)
            {
                Console.WriteLine("Error");
                re = 6;
            }
        }

        private void Disp_option(string str)
        {
            string path;
            switch (str)
            {
                case "--help":
                    path = "../../../option//HELP.txt";
                    //path = "option//HELP.txt";
                    var text = File.ReadAllText(path);
                    Console.WriteLine(text);
                    break;
                case "--version":
                    Console.WriteLine("Calculator_cs version "+version);
                    break;
                default:
                    Console.WriteLine("This command is not defined");
                    break;
            }
            return;
        }


        private int Identification_operator(string str)
        {
            calm c1 = new calm();
            res = 0;
            switch (str)
            {
                case "+":
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
                    Calculation();
                    return 4;
                default:
                    Console.WriteLine("Error");
                    err[0] = 1;
                    c1.Variable_initialization(0);
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
                        c1.Variable_initialization(0);
                    }
                    break;
            }
        }


    }
}
