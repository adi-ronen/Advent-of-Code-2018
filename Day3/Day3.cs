using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

class MainClass
{
  public static void Main (string[] args)
  {
    int[] ans = overlapPicesOfFabric();
    //Part One
    Console.WriteLine("Square inches of fabric are within two or more claims: "+ans[0]);
    //Part Two
    Console.WriteLine("The ID of the only claim that doesn't overlap: "+ans[1]);
  }

  public static int[] overlapPicesOfFabric()
  {
    //example inmput --> #1 @ 871,327: 16x20
    int squares = 0; 
    int dosentOverlapID = 0;
    using(StreamReader sr = new StreamReader("input"))
    {
      string line;
      Hashtable fabric = new Hashtable();
      HashSet<string> IDs = new HashSet<string>();
      while ((line = sr.ReadLine()) != null)
      {
        string[] prms = line.Split(new char[]{'#','@',':'}, StringSplitOptions.RemoveEmptyEntries);
        squares += overlapSquare(ref fabric, prms[0], prms[1], prms[2], ref IDs);
      }
      if(IDs.Count==1)
      {
        foreach(string id in IDs)//only way to get the element from hashset in csharp... 
          dosentOverlapID = Int32.Parse(id);
      }
      return new int[]{squares,dosentOverlapID};
    }
  }

  public static int overlapSquare(ref Hashtable fabric,string ID, string shift, string prms, ref HashSet<string> IDs)
  {
    int ans= 0;
    int fromLeft = Int32.Parse(shift.Split(',')[0]);
    int fromTop = Int32.Parse(shift.Split(',')[1]);
    int width = Int32.Parse(prms.Split('x')[0]);
    int height = Int32.Parse(prms.Split('x')[1]);

    IDs.Add(ID);
    bool overlap = false;

    for(int i=fromTop; i<fromTop+height; i++)
    {
      for(int j=fromLeft; j<fromLeft+width; j++)
      {
        if(fabric.Contains(i+","+j))
        {
          string squarID = i+","+j;
          string[] values = ((string)fabric[squarID]).Split(',');
          //ID,0 if once claimed ID,1 if overlap (ID is the ID of the one that first claimed this squar)
          if(values[1]=="0")
          {
            fabric[i+","+j]=values[0]+",1";
            ans++;
          }
          if(IDs.Contains(values[0]))
          {
            IDs.Remove(values[0]);
          }
          overlap = true;
        }
        else
        {
          //first claim of fabric squar
          fabric.Add(i+","+j, ID+",0");
        }
      }
    }
    if(overlap)
    {
      IDs.Remove(ID);
    }
    return ans;
  }
}