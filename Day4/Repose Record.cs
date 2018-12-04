using System;
using System.IO;
using System.Collections.Generic;

class MainClass 
{
  public static void Main (string[] args) 
  {
    int[] strategy = mostCommonSleepMin();
    Console.WriteLine ("Strategy 1: "+strategy[0]);
    Console.WriteLine ("Strategy 2: "+strategy[1]);
  }

  public static int[] mostCommonSleepMin()
  {
     string[] input = File.ReadAllLines("input");
     Array.Sort(input);
     Dictionary<int, int>[] minuts = new Dictionary<int, int>[60];
     Dictionary<int, int> grardSleepTime = new Dictionary<int, int>();
     //For Part One
     int sleeperGuardID =0;
     int maxSleepTime = 0;
     int maxSleepMin = 0;
     int maxSleepInMin=0;
     //For Part Two
     int guardID = 0;
     int lastAsleep = 0;
     int maxTime = 0;
     int maxTimeMin = 0;

     for(int i=0; i<input.Length; i++)
     {
       string[] details = input[i].Split(new Char[]{'[',']','#',' '}, StringSplitOptions.RemoveEmptyEntries);
       int minut = Int32.Parse(details[1].Split(':')[1]);
       switch(details[2])
       {
        case "Guard":
          guardID = Int32.Parse(details[3]);
          lastAsleep = 0;
          break;
        case "falls":
          lastAsleep = minut;
          addSleepTime(guardID, minut ,ref minuts);
          break;
        case "wakes":
          AddToGard(guardID,minut-lastAsleep, ref grardSleepTime, ref maxSleepTime, ref sleeperGuardID);
          for(int t=lastAsleep+1; t<minut; t++)
          {
            addSleepTime(guardID, t ,ref minuts); 
          }
          break;
       }
     }
     CalcStategies(ref maxSleepInMin, ref maxTime, ref minuts, ref guardID, ref maxTimeMin, ref sleeperGuardID, ref maxSleepMin);
     return new int[]{sleeperGuardID*maxSleepMin, guardID*maxTimeMin};
  } 

  public static void CalcStategies( ref int maxSleepInMin, ref int maxTime, ref Dictionary<int,int>[] minuts, ref int guardID, ref int maxTimeMin, ref int sleeperGuardID, ref int maxSleepMin)
  {
    for(int i =0; i<60; i++)
     {
       Dictionary<int, int> min = minuts[i];
       if(min!=null)
       {
         foreach(KeyValuePair<int,int> guard in min)
         {
           if(guard.Value>maxTime)
           {
             guardID = guard.Key;
             maxTime = guard.Value;
             maxTimeMin = i;
           }
           if(guard.Key == sleeperGuardID && guard.Value>maxSleepInMin)
           {
             maxSleepInMin = guard.Value;
             maxSleepMin = i;
           }
         }
       }
     }
  }

  public static void addSleepTime(int guardID, int minut, ref Dictionary<int, int>[] minuts)
  {
    if(minuts[minut]==null)
    {
      minuts[minut] = new Dictionary<int,int>();
    }
    if(minuts[minut].ContainsKey(guardID))
    {
      minuts[minut][guardID]++;
    }
    else
    {
      minuts[minut].Add(guardID,1);
    }
  }

  public static void AddToGard(int guardID, int minuts, ref Dictionary<int, int> grardSleepTime, ref int maxSleepTime, ref int sleeperGuardID)
  {
    if(!grardSleepTime.ContainsKey(guardID))
    {
      grardSleepTime.Add(guardID, 0);
    }
    grardSleepTime[guardID]=grardSleepTime[guardID]+minuts;
    if(grardSleepTime[guardID]>maxSleepTime)
    {
     maxSleepTime = grardSleepTime[guardID]; 
     sleeperGuardID = guardID;
    }
  }
}