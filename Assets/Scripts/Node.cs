using System.Collections.Generic;

public class Node
{
    public bool AITurn = true;
    public readonly List<Node> Children = new();
    public int Value;
    
    public enum Tile
    {
        Empty,
        AI,
        Opponent
    }

    public readonly Tile[,] Board = 
    {
        { Tile.Empty, Tile.Empty, Tile.Empty, Tile.Empty, Tile.Empty, Tile.Empty, Tile.Empty },
        { Tile.Empty, Tile.Empty, Tile.Empty, Tile.Empty, Tile.Empty, Tile.Empty, Tile.Empty },
        { Tile.Empty, Tile.Empty, Tile.Empty, Tile.Empty, Tile.Empty, Tile.Empty, Tile.Empty },
        { Tile.Empty, Tile.Empty, Tile.Empty, Tile.Empty, Tile.Empty, Tile.Empty, Tile.Empty },
        { Tile.Empty, Tile.Empty, Tile.Empty, Tile.Empty, Tile.Empty, Tile.Empty, Tile.Empty },
        { Tile.Empty, Tile.Empty, Tile.Empty, Tile.Empty, Tile.Empty, Tile.Empty, Tile.Empty }
    };
    
    public Tile IsAligned()
    {
        //Horizontal Vertical Alignment
         for (int i = 0; i < 4; i++)
         {
             if (Board[i, 0] != Tile.Empty 
                 && Board[i, 0] == Board[i, 1] 
                 && Board[i, 0] == Board[i, 2] 
                 && Board[i, 0] == Board[i, 3])
                 return Board[i, 0];
             
             if (Board[0, i] != Tile.Empty 
                 && Board[0, i] == Board[1, i] 
                 && Board[0, i] == Board[2, i] 
                 && Board[0, i] == Board[3, i])
                 return Board[0, i];
         }

        //Diagonal Alignment
         for (int j = 0; j < 3; j++)
         {
             for (int i = 0; i < 4; i++)
             {
                 if (Board[j, i] != Tile.Empty 
                     && Board[j, i] == Board[j + 1, i + 1] 
                     && Board[j, i] == Board[j + 2, i + 2] 
                     && Board[j, i] == Board[j + 3, i + 3]) 
                     return Board[j, i];
             
                 if (Board[j, 6 - i] != Tile.Empty 
                     && Board[j, 6 - i] == Board[j + 1, 5 - i] 
                     && Board[j, 6 - i] == Board[j + 2, 4 - i] 
                     && Board[j, 6 - i] == Board[j + 3, 3 - i] ) 
                     return Board[j, 6 - i];
             }
         }

        return Tile.Empty;
    }

    public int Evaluate()
    {
        Tile result = IsAligned();
        
        if(result == Tile.AI) Value = 100;
        else if (result == Tile.Opponent) Value = - 100;
        else Value = 0;
           
        return Value;
    }

    public override string ToString()
    {
        string sOut = "";
        for(int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                switch (Board[i,j])
                {
                    case Tile.Empty:
                        sOut += "_ ";
                        break;
                    case Tile.AI:
                        sOut += "O ";
                        break;
                    case Tile.Opponent:
                        sOut += "X ";
                        break;
                }
            }
            sOut += "\n";
        }
        return sOut;
    }
}