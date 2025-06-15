
namespace metodos
{
    public class Metodos
    {
        public static bool InRange(int valor, int min, int max)
        {
            return valor >= min && valor <= max;
        }




        //metodos para conectar con los visualles de unity

        public static int GetActualX_U()
        {
            //metodo de unity  
            return 0;
        }

        public static int GetActualY_U()
        {
            //metodo de UNity 
            return 0;
        }


        public static void DrawLine(int dirx, int diry, int distance)
        {
            //metodo de unity

        }

        public static void DrawCircle(int dirx, int diry, int radius)
        {
            //metood de unity 
        }

        public static void DrawRectangle(int dirx, int diry, int d, int alto, int ancho)
        {
            //metodo a unity
        }


        public static void Fill()
        {
            //metodo  de unity 
        }

        public static void Size(int size)
        {
            //metodo de unity
        }

        public static void Color(string color)
        {
            //metodo de unity 
        }

        public static void Spawn((int x, int y) pos)
        {
            //metood de unity 
        }
    }
}