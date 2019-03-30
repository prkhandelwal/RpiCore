using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpiUNO
{
    public sealed class PinValue
    {
        private static readonly byte _High = 1;
        private static readonly byte _Low = 0;

        public static byte High
        {
            get
            {
                return _High;
            }
        }
        public static byte Low
        {
            get
            {
                return _Low;
            }
        }
    }
}
