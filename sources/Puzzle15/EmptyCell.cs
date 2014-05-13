using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Puzzle15
{
    public class EmptyCell
    {
        public EmptyCell()
        {
            ContactSides = new bool[4];
            _position = 0;
            _lastNum = 6;
        }

        private int _position;
        private int _lastNum;
        //private int _contactSides;

        public int Position
        {
            get { return _position; }
            set
            {
                for (int i = 0; i < ContactSides.Length; i++)
                {
                    ContactSides[i] = false;
                }

                value -= 1;
                var vertical = value%4;
                var horizontal = (int)value/4;

                ContactSides[0] = horizontal != 0;
                ContactSides[1] = vertical != 3;
                ContactSides[2] = horizontal != 3;
                ContactSides[3] = vertical != 0;

                //_lastPosition = _position;
                _position = value+1;
            }
        }

        

        public bool[] ContactSides { get; set; }
        //public bool Left { get; private set; }

        //public bool Top { get; private set; }
        //public bool Right { get; private set; }
        //public bool Bottom { get; private set; }


        public int GetRandomContactSide(int num)
        {
            while (true)
            {

                if (!ContactSides[num])
                {
                    var rnd = new MotherOfAll();
                    num = rnd.GetRandom(4);
                    continue;
                }
                //if (num == _lastNum+2 || num == _lastNum -2)
                //{
                //    continue;
                //}
                //_lastNum = num;
                switch (num)
                {
                    case 0:
                        return _position - 4;
                    case 1:
                        return _position + 1;
                    case 2:
                        return _position + 4;
                    case 3:
                        return _position - 1;
                }
                break;
            }
            return 0;
        }
    }
}
