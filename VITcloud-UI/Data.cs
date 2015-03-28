using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VITcloud_UI
{
    class Data
    {
        public String Hostel;
        public String Room;
        public String Block;
        public String[] Files;

        public Data(String Hostel, String Block, String Room, String[] Files)
        {
            this.Hostel = Hostel;
            this.Block = Block;
            this.Room = Room;
            this.Files = Files;
        }
    }
}
