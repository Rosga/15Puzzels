using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzle15
{
    public class MerseneTwister
    {
        private long m;
        private long m0;

        public MerseneTwister()
        {
            GetBeginNumber();
        }

        public int GetRandom(int upper)
        {
            m = 1812433253 * (m0 ^ (m0 >> 30) + 1) & 0xffffffff;
            m0 = m;

            return (int)Math.Abs(m%upper);
        }

        public void Random(int upper, int count)
        {
            GetBeginNumber();
            for (int i = 0; i < count; i++)
            {
                m = 1812433253 * (m0 ^ (m0 >> 30) + i + 1) & 0xffffffff;

                m0 = m;

                Console.Write("{0}, ", m % upper);
            }

            Console.WriteLine();
            Console.WriteLine();
        }

        private void GetBeginNumber()
        {
            m0 = Math.Abs(DateTime.Now.Ticks);
        }
    }
}
