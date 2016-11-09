using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeMapV2
{
    public class UserPoint
    {
        public double  X { get; set; }
        public double Y { get; set; }

        public double GetPoint(int axis)
        {
            return axis == 0 ? X : Y;
        }

        public void SetPoint(int axis, double number)
        {
            if (axis == 0)
                X = number;
            else
                Y = number;
        }
    }
}
