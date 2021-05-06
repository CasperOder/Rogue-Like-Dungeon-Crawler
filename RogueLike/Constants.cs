using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueLike
{
    public static class Constants
    {
        public static int tileSize, roomWidth, roomHeight, startRoomCoords, minimumNumberOfRooms, windowWidth, windowHeight, itemSize, weaponItemSize;

        //800 room width. 20 tilesize, 40 tiles.

        public static void LoadConstants()
        {
            tileSize = 32;
            roomWidth = tileSize * 16;
            roomHeight = tileSize * 16;
            startRoomCoords = 4;
            //minimumNumberOfRooms = 45;
            windowWidth = 1850;
            windowHeight = 1000;
            itemSize = 24;
            weaponItemSize = 32;
        }

    }
}
