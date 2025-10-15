using System;

namespace MazeGame
{
    class Player
    {
        public int X { get; set; }
        public int Y { get; set; }
    }

    class Game
    {
        private int[,] _grid;
        private Player _player;
        private Player _goal;

        public Game(int[,] grid, Player player, Player goal)
        {
            _grid = grid;
            _player = player;
            _goal = goal;
        }

        public int[,] GetGrid()
        {
            return _grid;
        }

        private bool CanMove(int NewX, int newY)
        {
            //calculate how many rows and cols the grid has
            int rows = _grid.GetLength(0);
            int cols = _grid.GetLength(1);

            //check for out of bounds
            if (newX < 0 || newY < 0 || newX >= cols || newY >= rows)
                return false;
            //check for walls (1 = wall)
            return _grid[newY, newX] != 1;          
        }
        //Move player up 1 cell if valid                
        public void MovePlayerUp()
        {
            int newX = _player.X; 
            int newY = _player.Y - 1;

            if(CanMove(newX, newY))
            {
                _player.Y = newY;
            }            
        }
        //Move player down 1 cell if valid   
        public void MovePlayerDown()
        {
            int newX = _player.X; 
            int newY = _player.Y + 1;

            if(CanMove(newX, newY))
            {
                _player.Y = newY;
            }    
        }
        //Move player left 1 cell if valid
        public void MovePlayerLeft()
        {
            int newX = _player.X - 1; 
            int newY = _player.Y;

            if(CanMove(newX, newY))
            {
                _player.X = newX;
            }  
        }
        //Move player right 1 cell if valid
        public void MovePlayerRight()
        {
            int newX = _player.X + 1; 
            int newY = _player.Y;

            if(CanMove(newX, newY))
            {
                _player.X = newX;
            }  
        }
        
        public bool IsPlayerAtGoal()
        {
            return _player.X == _goal.X && _player.Y == _goal.Y;
        }

        public bool IsValidPath()
        {
            //calculate how many rows and cols the grid has
            int rows = _grid.GetLength(0);
            int cols = _grid.GetLength(1);

            bool[,] visit = new bool[rows, cols];

            return SearchGrid(_player.X, _player.Y, visit);
        }

            private bool SearchGrid(int x, int y, bool[,] visit)
                         {
                            //Check for out of bounds
                            if(x < 0 || y < 0 || x >= _grid.GetLength(1) || y >= grid.GetLength(0))
                               {
                                   return false;
                               }

                                if x == goal.X && y == _goal.Y
                                {
                                    return true;
                                }

                               //wall of already visited check
                               if(_grid[y, x] == 1 || visit[y, x])
                               {
                                    return false;
                               }

                                visit[y, x] = true;

                               //Search in all four directions
                               return SearchGrid(x + 1, y, visit) || //right
                                      SearchGrid(x - 1, y, visit) || //left
                                      SearchGrid(x, y + 1, visit) || //down
                                      SearchGrid(x, y - 1, visit); // up                            
                         }
             public Player GetPlayer()
        {
            return _player;
        }
    }

   

    class Program
    {
        static void Main(string[] args)
        {
            test_initial_state();
            test_move_player_right();
            test_move_player_left();
            test_move_player_up();
            test_move_player_down();
            test_move_player_through_wall();
            test_move_player_off_grid();
            test_win_game();
            test_is_valid_path();
            test_is_valid_path_1();
            test_is_not_valid_path();
            test_is_not_valid_path_1();
            Console.WriteLine("{\"test\": \"all unit tests\", \"result\": \"passed\"}");
        }

        static void test_initial_state()
        {
            int[,] grid = {
                {0, 0, 0, 0, 0},
                {0, 1, 1, 1, 0},
                {0, 1, 0, 0, 0},
                {0, 1, 1, 1, 0},
                {0, 0, 0, 2, 0},
            };
            Player player = new Player { X = 2, Y = 2 };
            Player goal = new Player { X = 4, Y = 3 };
            Game game = new Game(grid, player, goal);
            if (game.IsPlayerAtGoal())
            {
                throw new Exception("Error: Player is at the goal initially");
            }
        }

        static void test_move_player_right()
        {
            int[,] grid = {
                {0, 0, 0, 0, 0},
                {0, 1, 1, 1, 0},
                {0, 1, 0, 0, 0},
                {0, 1, 1, 1, 0},
                {0, 0, 0, 2, 0},
            };
            Player player = new Player { X = 2, Y = 2 };
            Player goal = new Player { X = 4, Y = 3 };
            Game game = new Game(grid, player, goal);
            game.MovePlayerRight();
            if (player.X != 3 || player.Y != 2)
            {
                throw new Exception("Error: Player did not move right");
            }
        }

        static void test_move_player_left()
        {
            int[,] grid = {
                {0, 0, 0, 0, 0},
                {0, 1, 1, 1, 0},
                {0, 1, 0, 0, 0},
                {0, 1, 1, 1, 0},
                {0, 0, 0, 2, 0},
            };
            Player player = new Player { X = 2, Y = 2 };
            Player goal = new Player { X = 4, Y = 3 };
            Game game = new Game(grid, player, goal);
            game.MovePlayerRight();
            game.MovePlayerLeft();
            if (player.X != 2 || player.Y != 2)
            {
                throw new Exception("Error: Player did not move left");
            }
        }

        static void test_move_player_up()
        {
            int[,] grid = {
                {0, 0, 0, 0, 0},
                {0, 1, 1, 1, 0},
                {0, 1, 0, 0, 0},
                {0, 1, 1, 1, 0},
                {0, 0, 0, 2, 0},
            };
            Player player = new Player { X = 2, Y = 2 };
            Player goal = new Player { X = 4, Y = 3 };
            Game game = new Game(grid, player, goal);
            game.MovePlayerRight();
            game.MovePlayerRight();
            game.MovePlayerUp();
            if (player.X != 4 || player.Y != 1)
            {
                throw new Exception("Error: Player did not move up");
            }
        }

        static void test_move_player_down()
        {
            int[,] grid = {
                {0, 0, 0, 0, 0},
                {0, 1, 1, 1, 0},
                {0, 1, 0, 0, 0},
                {0, 1, 1, 1, 0},
                {0, 0, 0, 2, 0},
            };
            Player player = new Player { X = 2, Y = 2 };
            Player goal = new Player { X = 4, Y = 3 };
            Game game = new Game(grid, player, goal);
            game.MovePlayerRight();
            game.MovePlayerRight();
            game.MovePlayerDown();
            if (player.X != 4 || player.Y != 3)
            {
                throw new Exception("Error: Player did not move down");
            }
        }

        static void test_move_player_through_wall()
        {
            int[,] grid = {
                {0, 0, 0, 0, 0},
                {0, 1, 1, 1, 0},
                {0, 1, 0, 0, 0},
                {0, 1, 1, 1, 0},
                {0, 0, 0, 2, 0},
            };
            Player player = new Player { X = 2, Y = 2 };
            Player goal = new Player { X = 4, Y = 3 };
            Game game = new Game(grid, player, goal);
            game.MovePlayerLeft();
            if (player.X != 2 || player.Y != 2)
            {
                throw new Exception("Error: Player moved through a wall");
            }
        }

        static void test_move_player_off_grid()
        {
            int[,] grid = {
                {0, 0, 0, 0, 0},
                {0, 1, 1, 1, 0},
                {0, 1, 0, 0, 0},
                {0, 1, 1, 1, 0},
                {0, 0, 0, 2, 0},
            };
            Player player = new Player { X = 2, Y = 2 };
            Player goal = new Player { X = 4, Y = 3 };
            Game game = new Game(grid, player, goal);
            game.MovePlayerRight();
            game.MovePlayerRight();
            game.MovePlayerRight();
            if (player.X != 4 || player.Y != 2)
            {
                throw new Exception("Error: Player moved off the grid");
            }
        }

        static void test_win_game()
        {
            int[,] grid = {
                {0, 0, 0, 0, 0},
                {0, 1, 1, 1, 0},
                {0, 1, 0, 0, 0},
                {0, 1, 1, 1, 0},
                {0, 0, 0, 2, 0},
            };
            Player player = new Player { X = 2, Y = 2 };
            Player goal = new Player { X = 3, Y = 4 };
            Game game = new Game(grid, player, goal);
            game.MovePlayerRight();
            game.MovePlayerRight();
            game.MovePlayerDown();
            game.MovePlayerDown();
            game.MovePlayerLeft();
            if (!game.IsPlayerAtGoal())
            {
                throw new Exception("Error: Player did not win the game");
            }
        }

        static void test_is_valid_path()
        {
            int[,] grid = {
                {0, 0, 0, 0, 0},
                {0, 1, 1, 1, 0},
                {0, 1, 0, 0, 0},
                {0, 1, 1, 1, 0},
                {0, 0, 0, 2, 0},
            };
            Player player = new Player { X = 2, Y = 2 };
            Player goal = new Player { X = 4, Y = 3 };
            Game game = new Game(grid, player, goal);
            if (!game.IsValidPath())
            {
                throw new Exception("Error: Path is not valid");
            }
        }

        static void test_is_valid_path_1()
        {
            int[,] grid = {
                {0, 1, 0, 0, 0},
                {0, 1, 0, 1, 0},
                {0, 1, 0, 1, 0},
                {0, 1, 0, 1, 0},
                {0, 0, 0, 1, 2},
            };
            Player player = new Player { X = 0, Y = 0 };
            Player goal = new Player { X = 4, Y = 3 };
            Game game = new Game(grid, player, goal);
            if (!game.IsValidPath())
            {
                throw new Exception("Error: Path is valid, but isValidPath returned false");
            }
        }

        static void test_is_not_valid_path_1()
        {
            int[,] grid = {
                {0, 1, 0, 1, 0},
                {0, 1, 0, 1, 0},
                {0, 1, 0, 1, 0},
                {0, 1, 0, 1, 0},
                {0, 0, 0, 1, 2},
            };
            Player player = new Player { X = 0, Y = 0 };
            Player goal = new Player { X = 4, Y = 3 };
            Game game = new Game(grid, player, goal);
            if (game.IsValidPath())
            {
                throw new Exception("Error: Path is not valid, but isValidPath returned true");
            }
        }

        static void test_is_not_valid_path()
        {
            int[,] grid = {
                {0, 1, 0, 0, 0},
                {0, 1, 0, 1, 0},
                {0, 1, 0, 1, 0},
                {0, 1, 0, 1, 1},
                {0, 0, 0, 1, 2},
            };
            Player player = new Player { X = 0, Y = 0 };
            Player goal = new Player { X = 4, Y = 3 };
            Game game = new Game(grid, player, goal);
            if (game.IsValidPath())
            {
                throw new Exception("Error: Path is not valid, but isValidPath returned true");
            }
        }
    }
}
