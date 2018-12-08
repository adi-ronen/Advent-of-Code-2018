using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

class MainClass 
{
  public static string[] input;
  public static int metadata = 0;
  public static int index = 0;
  public static int rootSum = 0;

  public static void Main (string[] args) 
  {
    metadataSum();
	//Part One
	Console.WriteLine("The sum of all metadata entries: "+metadata);
	//Part Two
    Console.WriteLine("The value of the root node: " +rootSum);
  }

  public static void metadataSum()
  {
    input = File.ReadAllLines("input")[0].Split(' ');
    buildRecursion(new Node());
  }

  public static void buildRecursion(Node node)
  {
    if(node.children==null)
    {
      node.children = new Dictionary<int,Node>();
      node.childrenTargetLength = Int32.Parse(input[index++]);
      node.dataTargetLength = Int32.Parse(input[index++]);
    }
    while(node.children.Count != node.childrenTargetLength)
    {
      Node newNode = new Node();
      node.children.Add(node.children.Count+1,newNode);
      buildRecursion(newNode);
    }
    node.data = new int[node.dataTargetLength];
    for(int i=0;i<node.dataTargetLength;i++)
    {
      int number = Int32.Parse(input[index++]);
      node.data[i]=number;
      metadata+=number;
    }
    if(index == input.Length)
    {
      rootSum = nodeSum(node);
    }
  }

  public static int nodeSum(Node node)
  {
    int sum = 0;
    if(node.childrenTargetLength==0)
    {
      return node.data.Sum();
    }
    foreach(int i in node.data)
    {
      if(node.children.Count>=i)
      {
        sum += nodeSum(node.children[i]);
      }
    }
    return sum;
  }

  public class Node
  {
    public int[] data;
    public int dataTargetLength;
    public Dictionary<int,Node> children;
    public int childrenTargetLength;
  }
}