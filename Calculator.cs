/*
calculator.cs

Copyright(c) 2022 S'(s1gnsgrfu)

This software is released under the MIT License.
see https://github.com/s1gnsgrfu/calculator_cs/blob/master/LICENSE
*/

using System;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace Calculator
{
    class calm
    {
        public static int? count = 0, va1 = 0, va2 = 0, culflg = 0, result = 0, flag = 0;
        //culflg -> 1:+ , 2:- , 3:* , 4:/
        //flag -> null:exit , 2:result
        //private static Dictionary<キーの型/クラス, > f = new Dictionary<キーの型/クラス, 値の型/クラス>();
        static int Main()
        {
            //List <string> ma = new List <string>();
            //int? result, flag;
            string str1, str2, str3;
            //int[] flag = new int[2];
            calm c1 = new calm();

            Console.WriteLine("Calculator");
            Console.WriteLine("If you need help , type --help");
            while (true)
            {

                Console.Write(">>");
                flag = c1.Stsp(Console.ReadLine());
                if (flag == null)
                {
                    return 0;
                }else if (flag == 2)
                {
                    Console.WriteLine("----------");
                    Console.WriteLine(result);
                    Console.WriteLine("----------");
                    count = va1 = va2 = culflg = result = flag = 0;
                }
                else
                {
                    ;
                }
                //c1.Stsp(str1);
            }
        }

        private int? Stsp(string str)
        {
            calm c1 = new calm();
            //int count, va1, va2;
            Console.WriteLine("1 -- typed --> " + str);
            if (Regex.IsMatch(str, "^--"))
            {
                Console.WriteLine(true);
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
            }
            else if (Regex.IsMatch(str, "^[0-9]+$"))  //すべて数字（1回目）
            {
                if (count == 0)
                {
                    count++;
                    Console.WriteLine("int-" + count);
                    va1 = int.Parse(str);
                    Console.WriteLine("va1 -> " + va1);
                    //return 0;
                }
                else if ((count == 1)&&(culflg != 0))
                {
                    count++;
                    Console.WriteLine("int-" + count);
                    va2 = int.Parse(str);
                    Console.WriteLine("va2 -> " + va2);
                    //return 0;
                }
                else
                {
                    Console.WriteLine("ennzannsi kure");
                    
                }
                return 0;
            }/*
            else if (str == "=")
            {
                if (count > 0)
                {
                    Console.WriteLine("eq");
                    //return 0;
                }
                else
                {
                    Console.WriteLine("Error");
                    //return 0;
                }
                return 0;
            }*/
            else if (str == "exit")
            {
                return null;
            }
            else
            {
                switch (str)
                {
                    case "+":
                        Console.WriteLine("sw-+");
                        culflg = 1;
                        Console.WriteLine(culflg);
                        break;
                    case "-":
                        Console.WriteLine("sw--");
                        culflg = 2;
                        break;
                    case "*":
                        Console.WriteLine("sw-*");
                        culflg = 3;
                        break;
                    case "/":
                        Console.WriteLine("sw-/");
                        culflg = 4;
                        break;
                    case "=":
                        Console.WriteLine("sw-=");
                        //culflg = 5; //計算専用関数に飛べばいいかも
                        Eq();
                        return 2;
                        //break;
                    default:
                        if (count == 1)
                        {
                            Console.WriteLine("ennzannsi kure");
                        }
                        else
                        {
                            Console.WriteLine("Error");
                        }
                        break;
                }
                //Console.WriteLine("else");
                return 0;
            }
            /*
            //sf -> 演算、被演算数値フラグ
            //i -> 文字数
            //cf -> 1:+ , 2:- , 3:* , 4:/ , 5:=
            int sf = 0, i = 0, ic1 = 0, ic2 = 0, cf = 0, ex = 0;
            double res;
            double[] d = new double[2];
            List<int> cd1 = new List<int>();
            List<int> cd2 = new List<int>();

            foreach (char c in ma)
            {
                //Console.WriteLine(c);
                

                //string isNumeric = int.TryParse(c, out n);
                if (char.IsNumber(c))
                {
                    if (sf == 0)
                    {
                        ic1++;  //1--
                        cd1.Add(c);
                    }else if(sf == 1)
                    {
                        ic2++;  //1--
                        cd2.Add(c);
                    }
                    Console.Write(c + " --> ");
                    Console.WriteLine("digit");
                    Console.WriteLine(ma);

                }
                else
                {
                    if(sf == 0)
                    {
                        switch (c)
                        {
                            case '+':
                                cf = 1;
                                break;
                            case '-':
                                cf = 2;
                                break;
                            case '*':
                                cf = 3;
                                break;
                            case '/':
                                cf = 4;
                                break;
                            case '=':
                                cf = 5;
                                break;
                            default:
                                Console.WriteLine("Error");
                                ex = 1;
                                break;
                        }
                    }else if ((sf == 1) || (sf == 9))   //2回目および=
                    {
                        d = Digi(cd1, cd2, ic1, ic2);
                        res = Caltion();
                    }
                    
                    Console.Write(c + " --> ");
                    Console.WriteLine("No digit");
                    Console.WriteLine(ma);
                    sf = 1;
                }

                i++;    //0--
            */
            //}
        }
        
        private int? Eq()
        {
            calm c1 = new calm();
            Console.WriteLine("clu->" + culflg);
            
            //Console.WriteLine("----------");
            
            //Console.Write(va1+" ");
            switch (culflg)
            {
                case 1:
                    //Console.WriteLine("++++++++++++" + va1 + "==" + va2);
                    result = va1 + va2;
                    break;
                case 2:
                    Console.WriteLine("--------" + va1 + "==" + va2);
                    result = va1 - va2;
                    break;
                case 3:
                    result = va1 * va2;
                    break;
                case 4:
                    result = va1 / va2;
                    break;
            }
            //Console.Write(va2 + " ");


            //Console.WriteLine()
            
            return 0;
        }
    /*
        //private double List<int> cd1 = new List<int>();
        private double[] Digi(IReadOnlyCollection<int> cd1,IReadOnlyCollection<int> cd2,int ic1,int ic2)
        {
            foreach(double c1 in cd1)
            {
                if (ic1 == 1)
                {
                    double res1 = c1;
                }
            }
        }
        private void Caltion()
        {

        }*/


    }

}
