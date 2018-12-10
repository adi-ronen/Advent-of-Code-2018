using System;
using System.IO;
using System.Collections.Generic;

class MainClass {
  public static void Main (string[] args) {
    message();
  }
  
  public static void message()
  {
    string[] input = File.ReadAllLines("input");
    List<Point> points = new List<Point>();
    foreach(string p in input)
    {
      string[] factors = p.Split(new char[]{'<',',','>'});
      points.Add(new Point(
        Int32.Parse(factors[1].Trim()),
        Int32.Parse(factors[2].Trim()),
        Int32.Parse(factors[4].Trim()),
        Int32.Parse(factors[5].Trim())
        ));
    }
    ExtreamValues matrixSize = findExtreamValues(points);
    ExtreamValues newMatrixSize = findExtreamValues(points);
    int seconds = 0; 
    while(newMatrixSize.bottom<=matrixSize.bottom)
    {
      matrixSize = newMatrixSize;
      foreach(Point p in points)
      {
        p.Move();
      } 
      seconds++;
      newMatrixSize = findExtreamValues(points);
    }
    foreach(Point p in points)
    {
      p.MoveBack();
    } 
    seconds--;
    char[,] matrix = new char[newMatrixSize.bottom-newMatrixSize.top+1, newMatrixSize.right - newMatrixSize.left+1];
    foreach(Point p in points)
    {
       p.Normalize(newMatrixSize);
       matrix[p.Y_position,p.X_position] = '#';
    }
    print(matrix,seconds);
  }

  public static void print(char[,] matrix,int seconds)
  {
    int rowLength = matrix.GetLength(0);
    int colLength = matrix.GetLength(1);

    for (int i = 0; i < rowLength; i++)
    {
      for (int j = 0; j < colLength; j++)
      {
        if(matrix[i, j]=='#')
        {
          Console.Write('#');
        }
        else
        {
          Console.Write('.');
        }
      }
      Console.WriteLine();
    }

    Console.WriteLine("exactly "+ seconds + " seconds you would have wait for that message to appear");
  }

  public static ExtreamValues findExtreamValues(List<Point> points)
  {
    ExtreamValues matrixSize= new ExtreamValues();
    matrixSize.right = matrixSize.bottom = Int32.MinValue;
    matrixSize.left = matrixSize.top = Int32.MaxValue;
    foreach(Point p in points)
    {
      matrixSize.right = Math.Max(matrixSize.right, p.X_position);
      matrixSize.bottom = Math.Max(matrixSize.bottom, p.Y_position);
      matrixSize.left = Math.Min(matrixSize.left, p.X_position);
      matrixSize.top = Math.Min(matrixSize.top, p.Y_position);
    }
    return matrixSize;
  }

  public class Point
  {
    public int X_position {get;set;}
    public int Y_position {get;set;}
    public int X_velocity {get;set;}
    public int Y_velocity {get;set;}
    
    public Point(int x, int y, int vx, int vy)
    {
      this.X_position = x;
      this.Y_position = y;
      this.X_velocity = vx;
      this.Y_velocity = vy;
    }

    public void Move()
    {
      X_position = X_position + X_velocity;
      Y_position = Y_position + Y_velocity;
    }
    public void MoveBack()
    {
      X_position = X_position - X_velocity;
      Y_position = Y_position - Y_velocity;
    }
    
    public void Normalize(ExtreamValues ex)
    {
      X_position = X_position - ex.left;
      Y_position = Y_position - ex.top;
    }
  }

  public struct ExtreamValues
  {
    public int right {get;set;}
    public int left {get;set;}
    public int top {get;set;}
    public int bottom {get;set;}
  }
}