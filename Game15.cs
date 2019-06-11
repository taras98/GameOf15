using System;

namespace game
{
    class Game15
    {
        int size;
        int[,] map;
        private int spaceX;
        private int spaceY;
        public int SpaceX { get; }
        public int SpaceY { get; }
        static Random rand = new Random();
        public Game15(int Size = 4)
        {
            size = Size;
            map = new int[size, size];
        }

        public void start()
        {
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    map[x, y] = coordsToPosition(x, y) + 1;
                }
            }
            spaceX = size - 1;
            spaceY = size - 1;
            map[spaceX, spaceY] = 0;
        }
        public int shift(int position)
        {
            int x, y, res;

            positionToCoords(position, out x, out y);
            if (Math.Abs(spaceX - x) + Math.Abs(spaceY - y) != 1) return -1;
            map[spaceX, spaceY] = map[x, y];//!
            map[x, y] = 0;
            spaceX = x;
            spaceY = y;
            return position;
        }
        public void shiftBack(int position)
        {
            if (position != -1 || position != 16)
            {
                int x, y;
                positionToCoords(position, out x, out y);
                map[spaceX, spaceY] = map[x, y];//!
                map[x, y] = 0;
                spaceX = x;
                spaceY = y;
            }


        }
        public string shuffler()
        {
            //shift(rand.Next(0, size * size));
            int a = rand.Next(0, 4);
            int x = spaceX;
            int y = spaceY;
            int direction=0;
            string separator = ",";
            switch (a)
            {
                case 0:
                    x++;
                    direction = 1;
                    break; // ліво
                case 1:
                    x--;
                    direction = 2;
                    break; //право
                case 2:
                    y++;
                    direction = 3;
                    break; // верх
                case 3:
                    y--;
                    direction = 4;
                    break;  //низ
            }
            //shift(coordsToPosition(x, y));

            return shift(coordsToPosition(x, y)).ToString() + separator + direction.ToString();
            //return (coordsToPosition(x, y) + 1).ToString() + ". " + direction;

        }
        public void solver(int a)
        {
            int x = spaceX;
            int y = spaceY;
            switch (a)
            {
                case 0:
                    x--;
                    break; // ліво
                case 1:
                    x++;
                    break; //право
                case 2:
                    y--;
                    break; // вниз
                case 3:
                    y++;
                    break; //вверх
            }
            shift(coordsToPosition(x, y));
        }
        public bool check_numbers()
        {
            if (!(spaceX == size - 1 && spaceX == size - 1)) return false;
            for (int x = 0; x < size; x++)
                for (int y = 0; y < size; y++)
                    if (!(x == size - 1 && y == size - 1))
                        if (map[x, y] != coordsToPosition(x, y) + 1)
                            return false;
            return true;


        }
        public int getNumber(int position)
        {
            int x, y;
            positionToCoords(position, out x, out y);
            if (x < 0 || x >= size) return 0;
            if (y < 0 || y >= size) return 0;
            return map[x, y];
        }

        private int coordsToPosition(int x, int y)
        {
            if (x < 0) x = 0;
            if (x > size - 1) x = size - 1;
            if (y < 0) y = 0;
            if (y > size - 1) y = size - 1;
            return y * size + x;
        }
        private void positionToCoords(int position, out int x, out int y)
        {
            if (position < 0) position = 0;
            if (position > size * size - 1) position = (size * size) - 1;
            x = position % size;
            y = position / size;
        }
    }
}
