using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoSzomszediIszony
{
    class rosszSzamformatum: Exception
    {
        public rosszSzamformatum(): base("A beírt számformátum nem megfelelő. Csak pozitív egész számokat használj 0-tól 100-ig!")
        {

        }
    }
}
