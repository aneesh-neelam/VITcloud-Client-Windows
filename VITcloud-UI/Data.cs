using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VITcloud_UI
{
    class Data
    {
        private String hostel;
        private String block;
        private String room;
        private String[] directories;

        public Data(String hostel, String block, String room, String[] directories)
        {
            this.hostel = hostel;
            this.block = block;
            this.room = room;
            this.directories = directories;
        }

        public void Scan()
        {

        }
    }
}
