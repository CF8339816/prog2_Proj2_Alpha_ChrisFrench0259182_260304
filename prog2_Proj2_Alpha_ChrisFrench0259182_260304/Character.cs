using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prog2_Proj2_Alpha_ChrisFrench0259182_260304
{
    public class Character
    {

        public string _name { get; set; }
        public int _x { get; set; }
        public int _y { get; set; }
        public int _attack { get; set; }
        public char _symbol { get; protected set; }
        public int _health { get; set; }

        public ConsoleColor _color;

        public (int, int) _min_max_x = (1, 55);
        public (int, int) _min_max_y = (1, 24);
        protected Character(string Name, int x, int y, int attack, char symbol, int hp, ConsoleColor color)
        {
            _name = Name;
            _x = x;
            _y = y;
            _attack = attack;
            _symbol = symbol;
            _health = hp;
            _color = color;
        }


    }
}
