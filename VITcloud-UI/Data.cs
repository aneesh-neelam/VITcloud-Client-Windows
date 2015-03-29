/*
 *  VITcloud: Windows Desktop Client Application
 *  Copyright (C) 2015  Aneesh Neelam <neelam.aneesh@gmail.com>
 *
 *  This program is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation, either version 3 of the License, or
 *  (at your option) any later version.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

using System;

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
