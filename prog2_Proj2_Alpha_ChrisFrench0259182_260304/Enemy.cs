using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prog2_Proj2_Alpha_ChrisFrench0259182_260304
{
    public class Enemy : Character
    {

        public Enemy(string Name, int x, int y, int attack, char symbol, int hp, ConsoleColor color) : base(Name, x, y, attack, symbol, hp, color)
        {
        }


        private static Random enRando = new Random();


    }





}
