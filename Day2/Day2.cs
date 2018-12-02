using System;
using System.IO;
using System.Collections;

class MainClass
{
  public static void Main (string[] args) 
  {
    Console.WriteLine("the checksum for my list of box IDs is: "+ listOfBoxIDschecksum());
    Console.WriteLine("the common letters between the two correct box IDs are: "+longestCommonLetters());
  }

  public static int listOfBoxIDschecksum()
  {
    int towTimesAppearance = 0;
    int threeTimesAppearance = 0;
    using(StreamReader sr = new StreamReader("input"))
    {
      string line;
      while ((line = sr.ReadLine()) != null)
      {
        int[] appearances = countTowAndTreeTimesAppearance(line);
        towTimesAppearance+=appearances[0];
        threeTimesAppearance+=appearances[1];
      }
      return towTimesAppearance*threeTimesAppearance;
    }
  }

  public static int[] countTowAndTreeTimesAppearance(string input)
  {
    Hashtable appearances = new Hashtable();
    bool twice = false;
    bool threeTimes = false;
    for(int i=0; i<input.Length; i++)
    {
      char c = input[i];
      if(appearances.ContainsKey(c))
      {
        appearances[c] = (int)appearances[c]+1;
      }
      else
      {
        appearances.Add(c,1);
      }
    }
    twice = appearances.ContainsValue(2);
    threeTimes = appearances.ContainsValue(3);
    return new int[]{Convert.ToInt32(twice),Convert.ToInt32(threeTimes)};
  }

  public static string longestCommonLetters()
  {
    string[] input = File.ReadAllLines("input");
    string maxSim = "";
    for(int i=0; i<input.Length ; i++){
      for(int j=i+1; j<input.Length ; j++){
        string sim = Sim(input[i],input[j]);
        maxSim = (sim.Length>maxSim.Length) ? sim : maxSim;
      }
    }
    return maxSim;
  }

  public static string Sim(string w1,string w2)
  {
    string sim = "";
    for(int i=0; i<w1.Length; i++)
    {
      if(w1[i]==w2[i]) sim+=w1[i];
    }
    return sim;
  }

}