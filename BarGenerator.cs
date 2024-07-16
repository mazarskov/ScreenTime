using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenTime
{
    public class BarGenerator
    {
        public string Generate(int minutes)
        {
            
            //TODO: fix this garbage hard-coding fix
            if (minutes > 60)
            {
                minutes = 60;
            }
            
            int remainder = (minutes % 10);
            int ten_intervals = (minutes - remainder) / 10;
            string finmin = "";
            string complete = "";


            for (int i = 0; i < ten_intervals; ten_intervals--) 
            {
                //complete += "||||||||||||||||||||||||||||||\n";
                complete += "||||||||||||||||||||\n";
            }

            if (remainder > 0)
            {
                while(remainder > 0)
                {
                    //finmin += "||";
                    finmin += "||";
                    remainder--;
                }
                complete = string.Join("\n", finmin, complete);
                complete = complete.Substring(0, complete.Length - 1);
            }
            else
            {
                complete = complete.Substring(0, complete.Length - 1);
            }

            if (!((60 - minutes) <= 9))
            {
                int leftos = 60 - minutes;
                int tenIntervalsOfLeftos = (leftos - (leftos % 10)) / 10;
                for(int j = 0; j < tenIntervalsOfLeftos; tenIntervalsOfLeftos--)
                {
                    complete = string.Join("\n", "", complete);
                }

            }
            return complete;
        }
    }
}
