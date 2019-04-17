using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class PacketTransform
{
    public static void Deserialize<T>(byte[] from, T to)
    {
        // utf decode to string
        // read data to get values
        // return as a class
    }

    public static void Serialize(string data)
    {
        //utf 8 endcode
        /**
         * Send data like this
         *  1 13912 0
            {type}{value}{seperator}
         */
    }
}
