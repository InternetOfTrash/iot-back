using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iot_backend
{
    class LevelPredictor
    {
        public DateTime GetExpectedFillDate(DateTime secondLastDate, int SecondLastLevel, DateTime lastDate, int lastLevel) {   

            double timeDiff = lastDate.Subtract(secondLastDate).TotalSeconds;   //secondsbetween dates
            double levelDiff = lastLevel - SecondLastLevel;                     //difference in level between measurements
            double increasePerSecond = levelDiff / timeDiff;                    //percentage increase per second
            double percentLeft = 100 - lastLevel;                               //percentage left untill filled
            double secondsLeft = percentLeft / increasePerSecond;               //seconds left untill filled
            DateTime expectedFull = DateTime.Now.AddSeconds(secondsLeft);       //expected date before container is full

            return expectedFull;

        }
    }
}
