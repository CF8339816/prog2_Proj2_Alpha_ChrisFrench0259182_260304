using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace prog2_Proj2_Alpha_ChrisFrench0259182_260304
{
    class Program
    {
        static string Name;
        
        // static Player player = new Player("Hero", 3, 3, 15, '!', 50, ConsoleColor.Blue);
        static Player player = new Player( " " , 3, 3, 15, '!', 50, ConsoleColor.Blue);
        static List<Enemy> enemies = new List<Enemy>();

        static LoadMap map = new LoadMap();
        static bool isPlaying = true;
        //static int output_X = 61;
        //static int output_Y = 1;
        static int nextX ;
        static int nextY ;
        
        static int plMaxHP = 50;
  


        static int gold = 0;
        static bool goldTreasure = true;
        static Random goldPileSpawn = new Random();
        static (int, int) PlPosition = (player._x, player._y);
        static (int, int) goldLoc = (treasure_x_pos, treasure_y_pos);
        static int treasure_x_pos;
        static int treasure_y_pos;
        static (int, int) treasure_min_max_x = (9, 45);
        static (int, int) treasure_min_max_y = (7, 20);
        static int loot = 15;

        static void Main(string[] args)
        {
            Console.SetCursorPosition(0, 0);
            alias();
            Console.Clear();
            Console.CursorVisible = false;

            Console.CursorVisible = false;
            map.MapLoader();
            Console.WriteLine("Press any Key to start... Use W,A,S,D  or arrow keys to move around the map...Press 'Q' to exit...\nFight enemies '&' by manouvering to them or try to avoid them... Lava '%' will damage you ");

            enemies.Add(new Enemy("Gobbo", 50, 4, 10, '&', 25, ConsoleColor.Green));
            enemies.Add(new Enemy("Slobbo", 20, 24, 10, '&', 25, ConsoleColor.Green));
            enemies.Add(new Enemy("Zobbo", 15, 12, 12, '&', 25, ConsoleColor.Green));
            enemies.Add(new Enemy("Hobbo", 15, 12, 12, '&', 40, ConsoleColor.DarkYellow));



            while (isPlaying)
            {
                player._name = Name;
                MovePlayer();
                if (map.Maps[player._y][player._x] == 'G')
                {
                    isPlaying = false; 
                    continue; 
                }
                foreach (var e in enemies)
                {
                    MoveEnemy(e);
                }
                DrawEntities();
                DrawGold();


            }
           if ((map.Maps[player._y][player._x] == 'G') || (player._health == 0))
              {
               if (player._health == 0)
                {
                    Console.SetCursorPosition(60, 23);// outputs player death and end of game prompts to exit
                    Console.WriteLine($" {player._name} has {player._health} health, {player._name} has died with {gold} golds on them");
                    Console.ReadKey(true);
                }
           
                if (map.Maps[player._y][player._x] == 'G')
                {
                    isPlaying = false;
                    Console.SetCursorPosition(60, 22);// outputs player death and end of game prompts to exit
                    Console.WriteLine($" {player._name} has reached the goal with {player._health} health, ");
                    Console.SetCursorPosition(60, 23);
                    Console.WriteLine($"{player._name} is safe with {gold} golds on them");
                    Console.ReadKey(true);
                }
            }
            Console.SetCursorPosition(60, 24);
            Console.WriteLine(" please come back soon");
            Console.ReadKey(true);
            Console.SetCursorPosition(60, 25);
            Console.WriteLine(" please press any key to exit");
            Console.ReadKey(true);
            Console.WriteLine("\n\n\n\n\n\n");

        }

        public static void MovePlayer()
        {

            int plX = 0, plY = 0;
            ConsoleKey input = Console.ReadKey(true).Key;
            // move player with W,A,S,D or optional arrow keys 
            if (input == ConsoleKey.LeftArrow) plX = -1;
            if (input == ConsoleKey.A) plX = -1;
            if (input == ConsoleKey.RightArrow) plX = 1;
            if (input == ConsoleKey.D) plX = 1;
            if (input == ConsoleKey.UpArrow) plY = -1;
            if (input == ConsoleKey.W) plY = -1;
            if (input == ConsoleKey.DownArrow) plY = 1;
            if (input == ConsoleKey.S) plY = 1;

            if (input == ConsoleKey.Q) isPlaying = false; //Quit the 'is playing' loop

            int nextX = player._x + plX;
            int nextY = player._y + plY;

            //if (map.CanMoveTo(nextX, nextY))// checks map loader for forbidden tiles
            //{
            //    Console.SetCursorPosition(player._x, player._y);
            //    char oldTile = map.Maps[player._y][player._x];
            //    WriteTileWithColor(oldTile);
            //    player._x = nextX;
            //    player._y = nextY;

            bool hitEnemy = false;
            foreach (var e in enemies)
            {
                if (nextX == e._x && nextY == e._y)
                {
                    // COMBAT LOGIC: Deal and take damage instead of moving
                    int damageToEnemy = 10;
                    int damageToPlayer = 5;

                    e._health -= damageToEnemy;
                    player._health -= damageToPlayer;

                    Console.SetCursorPosition(60, 14);
                    Console.WriteLine($" {e._name} takes {player._attack} points of combat damage");
                    Console.SetCursorPosition(60, 15);
                    Console.WriteLine($" {e._name} has {e._health} health...");

                    Console.SetCursorPosition(60, 17);
                    Console.WriteLine($" {player._name} takes {e._attack} points of combat damage");
                    Console.SetCursorPosition(60, 18);
                    Console.WriteLine($" {player._name} has {player._health} health...");

                    if (player._health <= 0 || e._health <= 0)
                    {
                        if (player._health <= 0)
                        {
                            player._health = 0;
                            Console.SetCursorPosition(60, 20);
                            Console.WriteLine($" {player._name} has {player._health} health, {player._name} has died");
                            isPlaying = false;
                        }
                        if (e._health <= 0)
                        {
                            e._health = 0;
                            Console.SetCursorPosition(60, 21);
                            Console.WriteLine($" {e._name} has {e._health} health, {e._name} has died");
                            isPlaying = true;
                        }
                    }
                }

                if (!hitEnemy && map.CanMoveTo(nextX, nextY))
                {
                    Console.SetCursorPosition(player._x, player._y);
                    char oldTile = map.Maps[player._y][player._x];
                    WriteTileWithColor(oldTile);

                    player._x = nextX;
                    player._y = nextY;

                    if ((player._x, player._y) == (treasure_x_pos, treasure_y_pos))// applies lootable gold 
                    {
                        gold += 15;
                        Console.SetCursorPosition(60, 5);
                        Console.WriteLine($" {player._name} loots 15 amounts of golds! ");
                        Console.SetCursorPosition(60, 6);
                        Console.WriteLine($"{player._name} now has {gold} gold...woooo!");
                        goldTreasure = true;
                        DrawGold();
                    }
                    if (map.Maps[player._y][player._x] == '%')// applies lava damage 
                    {
                        player._health -= 30;

                        if (player._health < 0)
                        {
                            player._health = 0;
                        }
                        Console.SetCursorPosition(60, 8);
                        Console.WriteLine($" {player._name} takes 30 points of lava damage");
                        Console.SetCursorPosition(60, 9);
                        Console.WriteLine($" {player._name} now has {player._health} HP");
                        if (player._health == 0) isPlaying = false;
                    }
                    if (map.Maps[player._y][player._x] == 'w')// applies spring water healing
                    {
                        player._health += 20;
                        if (player._health > plMaxHP)
                        {
                            player._health = plMaxHP;
                        }
                        Console.SetCursorPosition(60, 11);
                        Console.WriteLine($" {player._name} Finds cool refreshing sparkling mineral");
                        Console.SetCursorPosition(60, 12);
                        Console.WriteLine($" water and is healed for 20 pts {player._name} now has {player._health} HP");
                    }
                }
            }
        }

        static void WriteTileWithColor(char tile) //colours the map tiles and writes them to screen
        {
            if (tile == '%') Console.ForegroundColor = ConsoleColor.Red;
            else if (tile == 'w') Console.ForegroundColor = ConsoleColor.DarkCyan;
            else if (tile == '#') Console.ForegroundColor = ConsoleColor.DarkGray;
            else Console.ForegroundColor = ConsoleColor.White;

            Console.Write(tile);
            Console.ResetColor();
        }

        static void MoveEnemy(Enemy e)
        {
            Thread.Sleep(75);
            int nextX = e._x;
            int nextY = e._y;
            Random _rando = new Random();
            int nextRandX = e._x + _rando.Next(-1, 2); //randomises mocve on x
            int nextRandY = e._y + _rando.Next(-1, 2); // randomises moves on y
            nextX = nextRandX;
            nextY = nextRandY;

            // tells enemy how  to move when player is close
            if (player._x > (e._x + 4)) nextX++;
            else if (player._x < (e._x - 4)) nextX--;

            if (player._y > (e._y + 4)) nextY++;
            else if (player._y < (e._y - 4)) nextY--;

            char targetTile = map.Maps[nextY][nextX];

            //if (map.CanMoveTo(nextX, nextY) && targetTile != '%' && targetTile != (player._x, player._y))// defines  the lava and the player as non traversable for the enemy  lava works player seems not to
            //if (map.CanMoveTo(nextX, nextY) && targetTile != '%' && targetTile != (player._symbol))// defines  the lava and the player as non traversable for the enemy  lava works player seems not to

            if (map.CanMoveTo(nextX, nextY) && targetTile != '%' && (nextX != player._x || nextY != player._y))

            {
                Console.SetCursorPosition(e._x, e._y);
                Console.Write(" ");

                e._x = nextX;
                e._y = nextY;
            }
            else
            {
                nextX = 0;
                nextY = 0;
            }
        }

        static void DrawEntities()// draws the player and the enemy symbols/ sprites
        {

            foreach (var e in enemies)
            {
                if (e._health > 0) // Only draw if alive
                {
                    Console.SetCursorPosition(e._x, e._y);
                    Console.ForegroundColor = e._color;
                    Console.Write(e._symbol);
                }
            }




            //Console.SetCursorPosition(e._x, enemy._y);
            //Console.ForegroundColor = enemy._color;
            //Console.Write(enemy._symbol);

            Console.SetCursorPosition(player._x, player._y);
            Console.ForegroundColor = player._color;
            Console.Write(player._symbol);
            Console.ResetColor();
        }
        static void DrawGold()
        {
            if (goldTreasure)
            {
                bool clearGoldSpawn = false;
                while (!clearGoldSpawn)
                {
                    treasure_x_pos = goldPileSpawn.Next(treasure_min_max_x.Item1, treasure_min_max_x.Item2 + 1);
                    treasure_y_pos = goldPileSpawn.Next(treasure_min_max_y.Item1, treasure_min_max_y.Item2 + 1);
                    if (map.CanMoveTo(treasure_x_pos, treasure_y_pos))
                    {
                        if (treasure_x_pos != player._x || treasure_y_pos != player._y) //checks for player
                        {
                            clearGoldSpawn = true;
                        }
                    }
                }

                goldLoc = (treasure_x_pos, treasure_y_pos);
                Console.SetCursorPosition(treasure_x_pos, treasure_y_pos);
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write("$");
                Console.ResetColor();
                goldTreasure = false;
               
            }
            Console.ResetColor();
        }

        static void alias()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine("What is your character's name");
            Console.ForegroundColor = ConsoleColor.Blue;
            Name = Console.ReadLine();
        }

    }

    }


