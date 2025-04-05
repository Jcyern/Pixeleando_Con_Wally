namespace Lector 
{
    public class LectorText
    {
        public string []? Lines {get;private set;}  // las lineas del text 
        public string Route  {get ; private set;}

        //se le pasa la ruta por defecto que tiene el txt al cual se le pasara el codigo 
        public LectorText(string ruta = "/home/juanca/Documentos/Pixeleando_con_Wally/Logic/Lector_de_Texto/Editor.txt" )
        {
            Route = ruta;
            Lines = File.ReadAllLines(ruta);
        }


        public void PrintText()
        {
            if(Lines!= null)
            for (int i =0 ; i <Lines.Length; i ++)
            {
                System.Console.WriteLine($"Line:{i+1} ,Text{Lines[i]}");
            }
            else
            System.Console.WriteLine("No hay lineas de texto que imprimir");
        }
    }
}