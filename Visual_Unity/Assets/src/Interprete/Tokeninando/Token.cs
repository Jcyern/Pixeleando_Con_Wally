
public enum TypeToken
{
    //Aritmetico
    Numero,          // 1 ,2 ,3 ,4 ,5 ,6 ,7 ,8 ,9 ,0

    Operador,        // + , - , *, % , ** 

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

    Boolean,
    And,    //&&

    Or,     // ||

    Equals,  // == , <= , >=  , < , >
    NotEquals,

    Less,

    Bigger,

    LessEqual,

    BiggerEqual,



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

    color,

    Fin,

    OpenBraze,

    CloseBraze,




}




public class Token 
{
    //pos del token
    public int fila ;
    public int columna ;


    public TypeToken type ;

    public (int,int) Pos =>(fila,columna);

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


