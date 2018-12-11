using System;
using System.IO;
using System.Collections.Generic;

internal class MainClass
{
  public static void Main (string[] args) 
  {
    //Part One
    Console.WriteLine("the resulting frequency after all of the changes in frequency have been applied is: "+ sumFrequency());
    //Part Two
    Console.WriteLine("the first frequency the device reaches twice is: "+ FirstFrequencyTwice);
  }

  public static int sumFrequency()
  {
    int sum=0;
    using(StreamReader sr = new StreamReader("input"))
    {
      string line;
      while ((line = sr.ReadLine()) != null)
      {
        sum = AddStringToSum(line,sum);
      }
      return sum;
    }
  }

    public static int FirstFrequencyTwice
    {
        get
        {
            HashSet<int> frequencies = new HashSet<int>();
            int sum = 0;
            while (true)
            {
                using (StreamReader sr = new StreamReader("input"))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        sum = AddStringToSum(line, sum);
                        if (frequencies.Contains(sum))
                        {
                            return sum;
                        }
                        else
                        {
                            frequencies.Add(sum);
                        }
                    }
                }
            }
        }
    }

    static int AddStringToSum(string str, int sum)
  {
     if(str.Length<2) return sum;
     string s_number = str.Substring(1);
        if (Int32.TryParse(s_number, out int n_number))
        {
            sum = (str[0] == '-') ? sum - n_number : sum + n_number;
        }
        return sum;
  }
}