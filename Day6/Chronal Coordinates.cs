using System;
using System.IO;
using System.Collections.Generic;

class MainClass {
  static int maxX = Int32.MinValue;
  static int maxY = Int32.MinValue;
  static int minX = Int32.MaxValue;
  static int minY = Int32.MaxValue;
  static int distanceCounter = 0;

  public static void Main (string[] args) {
    //Part One
    Console.WriteLine("The size of the largest area that isn't infinite: "+largestArea());
    //Part Two
    Console.WriteLine("The size of the region containing all locations which have a total distance to all given coordinates of less than 10000: "+ distanceCounter/2);
  }

  public static int largestArea()
  {
    int pointCounter = 0; 
    Dictionary<Point,int> points1 = new Dictionary<Point,int>();
    Dictionary<Point,int> points2 = new Dictionary<Point,int>();
    using(StreamReader sr = new StreamReader("input"))
    {
      string line;
      while ((line = sr.ReadLine()) != null)
      {
        Point p = new Point();
        string[] cordinates = line.Split(',');
        p.X = Int32.Parse(cordinates[0]);
        p.Y = Int32.Parse(cordinates[1]);
        p.ID = pointCounter++;
        maxX = Math.Max(p.X, maxX);
        maxY = Math.Max(p.Y, maxY);
        minX = Math.Min(p.X, minX);
        minY = Math.Min(p.Y, minY);
        points1.Add(p,0);
        points2.Add(p,0);
      }
    }
    calcDistencesMatrix(ref points1, 0);
    calcDistencesMatrix(ref points2, 10);
    return biggestDistance(ref points1, ref points2);
  }

  public static int biggestDistance(ref Dictionary<Point,int> points1, ref Dictionary<Point,int> points2)
  {
    int max = 0;
    foreach(Point p in points1.Keys)
    {
      if(points1[p]==points2[p])
      {
        max = Math.Max(points1[p], max);
      }
    }
    return max;
  }

  public static void calcDistencesMatrix(ref Dictionary<Point,int> points, int space)
  {
    for(int i=minX-space; i<maxX+space; i++)
    {
      for(int j=minY-space; j<maxY+space; j++)
      {
        closestPoint(i, j, ref points);
      }
    }
  }

  public static void closestPoint(int x, int y , ref Dictionary<Point,int> points)
  {
    double minDistance = Int32.MaxValue;
    bool twoInMinDist = true;
    Point current = new Point();
    current.X = x;
    current.Y = y;
    Point closest = new Point(); 
    int TotalDistance = 0;
    foreach(Point p in points.Keys)
    {
      int distance = taxicabGeometry(p, current);
      TotalDistance+=distance;
      if(distance<minDistance)
      {
        minDistance = distance;
        closest = p;
        twoInMinDist = false;
      }
      else if(distance==minDistance)
      {
        twoInMinDist = true;
      }
    }
    if(TotalDistance<10000)
    {
      distanceCounter++;
    }
    if(twoInMinDist)
    {
      return;
    }
    points[closest]++;
  }

  public static int taxicabGeometry(Point p1, Point p2)
  {
    return Math.Abs(p1.X-p2.X)+Math.Abs(p1.Y-p2.Y);
  }

  public struct Point 
  {
    public int ID {get;set;}
    public int X {get;set;}
    public int Y {get;set;}
  }
}