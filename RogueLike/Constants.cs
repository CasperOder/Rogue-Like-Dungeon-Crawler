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



        public static void LoadConstants()
        {
            tileSize = 50;
            roomWidth = tileSize * 10;
            roomHeight = tileSize * 8;
            startRoomCoords = 4;
            //minimumNumberOfRooms = 45;
            windowWidth = 1850;
            windowHeight = 1000;
            itemSize = 16;
            weaponItemSize = 32;
        }

    }
}
