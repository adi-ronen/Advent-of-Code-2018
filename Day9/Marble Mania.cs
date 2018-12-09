using System;
using System.Collections.Generic;

class MainClass {
  //431 players; last marble is worth 70950 points
  static Dictionary<uint,uint> playersScore = new Dictionary<uint,uint>();

  public static void Main (string[] args) {
    //Part One
    Console.WriteLine ("The winning Elf's score: " + winningElfsScore(431,70950));
    //Part Two
    Console.WriteLine ("The new winning Elf's score be if the number of the last marble were 100 times larger: "+winningElfsScore(431,70950*100));
  }

  public static uint winningElfsScore (uint numberOfPlayers, uint lastMarbleWorth) {
    LinkedList<uint> circle = new LinkedList<uint>();
    uint playingPlayer = 1;
    LinkedListNode<uint> currentNode = new  LinkedListNode<uint>(0);
    circle.AddFirst(currentNode);
    for(uint marbleNumber=1; marbleNumber<=lastMarbleWorth; marbleNumber++)
    {
      if(marbleNumber%23==0)
      {
        for(int i = 1; i< 7; i++)
        {
          if(currentNode==circle.First)
          {
            currentNode = circle.Last;
          }
          else
          {
            currentNode = currentNode.Previous;
          }
        }
        if(!playersScore.ContainsKey(playingPlayer))
        {
          playersScore.Add(playingPlayer,0);
        }
        playersScore[playingPlayer] += marbleNumber+currentNode.Previous.Value;
        circle.Remove(currentNode.Previous);
      }
      else
      {
        currentNode=next(currentNode, ref circle);
        circle.AddAfter(currentNode,marbleNumber);   
        currentNode=next(currentNode, ref circle);
      }
      playingPlayer++;
      if(playingPlayer>numberOfPlayers)
      {
        playingPlayer=1;
      }
    }
    uint bestScore = 0;
    foreach(uint playerScore in playersScore.Values)
    { 
      bestScore = Math.Max(bestScore,playerScore);
    }
    return bestScore;
  }

  public static LinkedListNode<uint> next(LinkedListNode<uint> currentNode, ref LinkedList<uint> circle)
  {
    if(currentNode==circle.Last)
    {
      return circle.First;
    }
    return currentNode.Next;
  }
}