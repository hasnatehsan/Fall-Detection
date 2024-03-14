using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fall_fall.Model
{
    public class PersonModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        //From the drop down, the position of the Age group
        public int AgeGroup { get; set; }
    }
}
