using System;

class MainClass {

  public static int GSN = 2866;
  public static Cell[,] matrix; 

  public static void Main (string[] args) {

    matrix = new Cell[300,300];
    int length = matrix.GetLength(0);

    for(int x=0; x<length; x++) {
      for(int y=0; y<length; y++) {
        Cell c = new Cell(x+1, y+1);
        matrix[x,y] = c;
      }
    }
    //Part One
    Cell cellLTP =  matrix[0,0];
    for(int x=0; x<length; x++) {
      for(int y=0; y<length; y++) {
        Cell current = matrix[x,y].CalcLTP(3,length);
        cellLTP =current.LTP>cellLTP.LTP ? current : cellLTP;
      }
    }
    Console.WriteLine("The X,Y coordinate of the top-left fuel cell of the 3x3 square with the largest total power: "+cellLTP.X+","+cellLTP.Y); 

    //Part Two
    cellLTP =  matrix[0,0];
    for(int x=0; x<length; x++) {
      for(int y=0; y<length; y++) {
        Cell current = matrix[x,y].CalcLTP(length, length);
        cellLTP =current.LTP>cellLTP.LTP ? current : cellLTP;
      }
    }
    
    Console.WriteLine("The X,Y,size identifier of the square with the largest total power: "+cellLTP.X+","+cellLTP.Y+","+(cellLTP.size+1)); 
  }
 
  public class Cell {
    public int RackID;
    public int X;
    public int Y;
    public int PowerLevel;
    public int LTP;
    public int size;

    public Cell(int x,int y) {
      this.X = x;
      this.Y = y;
      this.PowerLevel = CalcPowerLevet();
      this.LTP = 0;
      this.size = 0;
    }

    public int CalcPowerLevet() {
      this.RackID = this.X + 10;
      return (int)(((((this.RackID * this.Y) + GSN ) * this.RackID)/100)%10) - 5;
    }

    public Cell CalcLTP(int num, int length) {
      int newLTP = 0;
      for(int i=0; i<num && i<= length-this.X && i<= length-this.Y; i++){
        for(int j=0; j<i; j++) {
            newLTP+=matrix[this.X+j-1,this.Y+i-1].PowerLevel+matrix[this.X-1+i,this.Y+j-1].PowerLevel;
        }
        newLTP+=matrix[this.X+i-1,this.Y+i-1].PowerLevel;
        if(newLTP>this.LTP){
          this.LTP = newLTP;
          this.size = i;
        }
      }
      return this;
    }
  }
}