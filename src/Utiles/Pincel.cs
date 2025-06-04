
using System.Drawing;
using System.Security.Authentication.ExtendedProtection;

namespace Pincel_
{
    public class Pincel
    {
        public static int Size = 0;

        public static string Color = "";

        public static void ChangeColor(string color)
        {
            Color = color;
            System.Console.WriteLine($"Se cambio el color del pincel a {Color} ");
        }
        public static void ChangeSize(int size)
        {
            Size = size;
        }





    }

    public enum Colores
    {
        //Colores   //colores disponible para el metodo Color 
        Color_Red,
        Color_Blue,
        Color_Green,
        Color_Yellow,
        Color_Orange,
        Color_Purple,
        Color_Black,
        Color_White,
        Color_Transparent,
    }



}

