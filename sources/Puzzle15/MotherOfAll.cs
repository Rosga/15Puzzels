using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzle15
{
    public class MotherOfAll
    {
        private long[] x;
        private long s;

        public MotherOfAll()
        {
            x = new long[5];
            GetBeginNumbers();
        }

        public int GetRandom(int upper)
        {
            s = x[4] * 21111111111 + x[3] * 1429 + x[2] * 1776 + x[1] * 5115 + x[0];

            x[4] = x[3];
            x[3] = x[2];
            x[2] = x[1];
            x[1] = x[0];
            x[0] = s;

            return (int)Math.Abs(s%upper);
        }

        public void Random(int upper, int count)
        {
            GetBeginNumbers();

            for (int i = 0; i < count; i++)
            {
                s = x[4] * 21111111111 + x[3] * 1429 + x[2] * 1776 + x[1] * 5115 + x[0];

                x[4] = x[3];
                x[3] = x[2];
                x[2] = x[1];
                x[1] = x[0];
                x[0] = s;
            }

        }

        private void GetBeginNumbers()
        {
            for (int i = 0; i < x.Count(); i++)
            {
                x[i] = Math.Abs((DateTime.Now.Ticks));
            }
        }
    }
}
