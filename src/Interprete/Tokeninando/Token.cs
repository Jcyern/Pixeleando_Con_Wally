
public enum TypeToken
{
    //Aritmetico
    Numero,          // 1 ,2 ,3 ,4 ,5 ,6 ,7 ,8 ,9 ,0

    Operador,        // + , - , *, %

    OpenParenthesis, // (

    CloseParenthesis, // )

    Identificador,   

    Asignacion,     //  <-

    //Simbolos
    Coma,   // ,
    
    Punto, // .

    String,   // "


    //Instrucciones
    Spawn,      //posicionar al wally  

    Color,      // cambiar el color del pincel 

    Size,       //modificar el tamano del pincel

    DrawLine,

    DrawCircle,

    DrawRectangle,

    Fill,




    //Expresiones Booleanas
    And,    //&&

    Or,     // ||

    Comparacion,  // == , <= , >=  , < , >



    //Funciones 
    GetActualX,

    GetActualY,

    GetCanvasSize,

    GetColorCount,

    IsBrushColor,

    IsBrushSize,

    IsCanvasColor,


    //Etiquetas
    Label,


    //Condicionales 
    GoTo,


    //InvalidToken
    InvalidToken,


    //Colores   //colores disponible para el metodo Color 
    Color_Red,
    Color_Blue,
    Color_Green,
    Color_Yellow,
    Color_Orange,
    Color_Purple,
    Color_Black,
    Color_White,
    Color_Transparent




}



public class Token 
{
    //pos del token
    public int fila ;
    public int columna ;


    public TypeToken type ;

    public string value ;  


    //se le pasa la pos del token en el texto  
    //el valor y el tipo de token 
    public Token (TypeToken type , string value , int fila , int columna )
    {
        this.fila = fila ;
        this.columna = columna ;

        this.type = type;
        this.value = value;
    }


}


