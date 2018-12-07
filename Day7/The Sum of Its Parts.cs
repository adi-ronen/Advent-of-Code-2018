using System;
using System.IO;
using System.Collections.Generic;

class MainClass {
  static char lastLetter;
  static string charsOrder ="";
  static int workCounter = 0; 
  static string finished ="";
  
  public static void Main (string[] args) {
    //Part One
    Console.WriteLine ("The order should the steps in the instructions be completed: "+instructionsOrder());
    //Part Two
    Console.WriteLine (workTime()+" secounds will take to complete all of the steps");
  }

  public static string instructionsOrder()
  {
    Dictionary<string, List<string>> orders = fillOrder();
    //print(orders);
    buildRecursive(ref orders);
    return charsOrder;
  }

  public static int workTime()
  {
    Dictionary<string, List<string>> orders = fillOrder();
    //print(orders);
    List<Stack<char>> workers = new List<Stack<char>>();
    Stack<char> w1 = new Stack<char>();
    Stack<char> w2 = new Stack<char>();
    Stack<char> w3 = new Stack<char>();
    Stack<char> w4 = new Stack<char>();
    Stack<char> w5 = new Stack<char>();
    workers.Add(w1);
    workers.Add(w2);
    workers.Add(w3);
    workers.Add(w4);
    workers.Add(w5);
    splitWork(ref orders, ref workers);
    return workCounter;
  }

  public static void splitWork(ref Dictionary<string, List<string>> orders, ref List<Stack<char>> workers)
  {
    while(finished.Length != charsOrder.Length)
    {
     foreach(Stack<char> worker in workers)
      {
        if(worker.Count == 0)
        { 
          char toAdd = addJob(worker, ref orders);
          if(toAdd!='.')
          {
            for(int i=0; i<86-('Z'-toAdd); i++)
            {
              worker.Push(toAdd);
            }
          }
          else
          {
            worker.Push(toAdd);
          }
        }
      }
      doWork(ref workers);
    }
  }

  public static void doWork(ref List<Stack<char>> workers)
  {
    foreach(Stack<char> worker in workers)
    {
      if(worker.Count==1 && worker.Peek()!='.')
      {
        finished += worker.Peek();
      }
      worker.Pop();
    }
    workCounter++;
  }

  public static char addJob(Stack<char> worker, ref Dictionary<string, List<string>> orders)
  {
    for(char c = 'A'; c<=lastLetter; c++) 
    {
      if(orders[""+c] == null)
       {
         continue;
       }
      List<string> operationsBefore = new List<string>(orders[""+c]);
      foreach(string operation in operationsBefore)
      {
        if(finished.Contains(operation))
        {
          orders[""+c].Remove(operation);
        }
      }
      if(orders[""+c].Count == 0)
      {
        orders[""+c] = null;
        return c;
      }
    }
    return '.';
  }
   
  public static void buildRecursive(ref Dictionary<string, List<string>> orders)
  {
    for(char c = 'A'; c<=lastLetter; c++)
    {
      if(orders[""+c] == null)
      {
        continue;
      }
      List<string> operationsBefore = new List<string>(orders[""+c]);
      foreach(string operation in operationsBefore)
      {
        if(orders[operation]==null)
        {
          orders[""+c].Remove(operation);
        }
      }
      if(orders[""+c].Count == 0)
      {
        charsOrder+=c;
        orders[""+c] = null;
        buildRecursive(ref orders);
      }
    }
  }

  public static void print(Dictionary<string, List<string>> orders)
  {
    for(char c = 'A'; c<=lastLetter; c++)
    {
      Console.Write(c+" ---> ");
      foreach(string s in orders[""+c])
      {
        Console.Write(s+" ");
      }
      Console.WriteLine();
    }
  }

  public static Dictionary<string, List<string>> fillOrder()
  {
    string[] input = File.ReadAllLines("input");
    Array.Sort(input);
    Dictionary<string, List<string>> orders = new Dictionary<string, List<string>>();
    foreach(string line in input)
    {
      string[] split = line.Split(' ');
      if(!orders.ContainsKey(split[7]))
      {
        orders.Add(split[7], new List<string>());
      }
      if(!orders.ContainsKey(split[1]))
      {
        orders.Add(split[1], new List<string>());
      }
      orders[split[7]].Add(split[1]);
      if(lastLetter<split[1][0])
      {
        lastLetter = split[1][0];
      }
      if(lastLetter<split[7][0])
      {
        lastLetter = split[1][0];
      }
    }
     return orders;
  }
}