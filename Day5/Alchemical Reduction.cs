using System;
using System.IO;
using System.Collections;

class MainClass 
{//Alchemical Reduction
  public static void Main (string[] args)
  {
    string input = File.ReadAllLines("input")[0];
    //Part One
    Console.WriteLine (Reduction(input)+" units remain after fully reacting the polymer i scanned");
    //Part Two
    Console.WriteLine ("The length of the shortest polymer i can produce: "+Reduction2(input));
  }

  public static int Reduction(string input)
  {
    return doReduction(input).Length;
  }

  public static string doReduction(string input)
  {
    Stack letters = new Stack();
    for(int i=0; i<input.Length;i++)
    {
      char current = input[i];
      if(letters.Count != 0)
      {
        char topLetter = (char)letters.Peek();
        if(Char.IsUpper(current) && !Char.IsUpper(topLetter))
        {
          if(Char.ToLower(current)==topLetter)
          {
            letters.Pop();
            continue;
          }
        }
        else if(!Char.IsUpper(current) && Char.IsUpper(topLetter))
        {
          if(Char.ToUpper(current)==topLetter)
          {
            letters.Pop();
            continue;
          }
        }
      }
      letters.Push(current);
    }
    return ToString(letters);
  }

  public static string ToString(Stack stk)
  {
    string str ="";
    while(stk.Count!=0)
    {
      str+=stk.Pop();
    }
    return str;
  }

  public static int Reduction2(string input)
  {
    string newInput = doReduction(input);
    int minReaction = newInput.Length;
    for(char delete = 'a' ; delete <= 'z'; delete++)
    {
      string temp = newInput.Replace(""+delete, string.Empty).Replace(""+Char.ToUpper(delete),string.Empty);
      string ans = doReduction(temp);
      minReaction = Math.Min(minReaction, ans.Length);
    }
    return minReaction;
  }
}