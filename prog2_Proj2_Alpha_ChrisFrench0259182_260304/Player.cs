using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prog2_Proj2_Alpha_ChrisFrench0259182_260304
{
    public class Player : Character
    {
        public Player(string Name, int x, int y, int attack, char symbol, int hp, ConsoleColor color) : base(Name, x, y, attack: 20, symbol: '!', hp: 50, color: ConsoleColor.Blue)
        {


        }


        //private static Random plRando = new Random();



        public void Move(int plMoveX, int plMoveY)// sets up move for input
        {
            int newPlMoveX = _x + plMoveX;
            int newPlMoveY = _y + plMoveY;


            if (newPlMoveX >= _min_max_x.Item1 && newPlMoveX <= _min_max_x.Item2) _x = newPlMoveX;
            if (newPlMoveY >= _min_max_y.Item1 && newPlMoveY <= _min_max_y.Item2) _y = newPlMoveY;
        }



    }



}
