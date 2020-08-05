using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TibaExam.Logic;

namespace TibaExam.Models
{
    public class Common
    {
        // This class is made to hold a static object of BL for easier access to it
        // from throughout the application, while not making BL itself static which 
        // might make future features such as unit tests more complicated to achieve...
        public static BL BL { get; set; }
    }
}
