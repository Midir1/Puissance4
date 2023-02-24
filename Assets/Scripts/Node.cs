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
        Opponent,
        Opponent1
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
        for (int i = 0; i < 7; i++)
        {
             for (int j = 0; j < 6; j++)
             {
                 //Horizontal Alignment
                 if (i < 4)
                 {
                     if (Board[j, i] != Tile.Empty 
                         && Board[j, i] == Board[j, i + 1] 
                         && Board[j, i] == Board[j, i + 2] 
                         && Board[j, i] == Board[j, i + 3])
                         return Board[j, i];
                 }

                 //Vertical Alignment
                 if (j < 3)
                 {
                     if (Board[j, i] != Tile.Empty 
                         && Board[j, i] == Board[j + 1, i] 
                         && Board[j, i] == Board[j + 2, i] 
                         && Board[j, i] == Board[j + 3, i])
                         return Board[j, i];
                 }

                 //Diagonal Alignment
                 if (i < 4 && j < 3)
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
        }

        return Tile.Empty;
    }

    private Tile IsAligned(ref int _value)
    {
        for (int i = 0; i < 7; i++)
        {
             for (int j = 0; j < 6; j++)
             {
                 //Horizontal Alignment
                 if (i < 4)
                 {
                     if (Board[j, i] != Tile.Empty)
                     {
                         if (Board[j, i] == Board[j, i + 1])
                         {
                             if (Board[j, i] == Tile.AI) _value += 10;
                             else _value -= 10;
                             
                             if (Board[j, i] == Board[j, i + 2])
                             {
                                 if (Board[j, i] == Tile.AI) _value += 25;
                                 else _value -= 25;
                                 
                                 if(Board[j, i] == Board[j, i + 3]) return Board[j, i];
                             }
                         }
                     }
                     
                 }

                 //Vertical Alignment
                 if (j < 3)
                 {
                     if (Board[j, i] != Tile.Empty)
                     {
                         if (Board[j, i] == Board[j + 1, i])
                         {
                             if (Board[j, i] == Tile.AI) _value += 10;
                             else _value -= 10;
                             
                             if (Board[j, i] == Board[j + 2, i])
                             {
                                 if (Board[j, i] == Tile.AI) _value += 25;
                                 else _value -= 25;
                                 
                                 if(Board[j, i] == Board[j + 3, i]) return Board[j, i];
                             }
                         }
                     }
                 }

                 //Diagonal Alignment
                 if (i < 4 && j < 3)
                 {
                     if (Board[j, i] != Tile.Empty)
                     {
                         if (Board[j, i] == Board[j + 1, i + 1])
                         {
                             if (Board[j, i] == Tile.AI) _value += 10;
                             else _value -= 10;
                             
                             if (Board[j, i] == Board[j + 2, i + 2])
                             {
                                 if (Board[j, i] == Tile.AI) _value += 25;
                                 else _value -= 25;
                                 
                                 if(Board[j, i] == Board[j + 3, i + 3]) return Board[j, i];
                             }
                         }
                     }
                     
                     if (Board[j, 6 - i] != Tile.Empty)
                     {
                         if (Board[j, 6 - i] == Board[j + 1, 5 - i]) 
                         {
                             if (Board[j, 6 - i] == Tile.AI) _value += 10;
                             else _value -= 10;
                             
                             if (Board[j, 6 - i] == Board[j + 2, 4 - i] )
                             {
                                 if (Board[j, 6 - i] == Tile.AI) _value += 25;
                                 else _value -= 25;
                                 
                                 if(Board[j, 6 - i] == Board[j + 3, 3 - i]) return Board[j, 6 - i];
                             }
                         }
                     }
                 }
             }
        }

        return Tile.Empty;
    }

    public int Evaluate()
    {
        Tile result = IsAligned(ref Value);
        
        if(result == Tile.AI) Value = 10000;
        else if (result == Tile.Opponent) Value = -10000;

        return Value;
    }
}