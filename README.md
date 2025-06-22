
# Pixeleando con Wally 
![imagen espacial][def]
## Segundo Proyecto de Programación Matcom



### Descripción :
En este proyecto se interpreta un lenguaje creado y analizado por los profesores de Programación de la Facultad de Matemática y Computación de la asignatura de Programación . Se divide el string , creando tokens , que luego se interpretan , y se analizan sintácticamente , creando una estructura , la cual se analiza semánticamente   y luego se evalúan las expresiones.

#### Visual : Unity
Usamos la herramienta tan poderosa que ofrece unity que es los Tilemap , para pintar casillas , las cuales representan píxeles. Además creamos  varios tiles de colores para poder tener una variadad , y permitir la creación de un arte visual. 


### Lógica básica del Proyecto :
- **Lexer** : Divide  el codigo pasado el cual es una estructura de string , en varias palabras claves , las cuales llamaremos tokens. 

- **Parser** : Analiza todos los tokens que me forma el Lexer , y los va uniendo en estructuras sintácticas definidas por el autor 
- **Semántica** : En la Semántica , coge esos  Nodos creados por el parser  que serían las estructura  sintácticas definidas , y verifica que la semántica de cada una de los nodos sea correcta  , osea q si tengo un método Pintar que recibe un int , en la semántico se verifica que lo pasado por el usuario sea un int . Entre otras especificaciones , las cuales se aprecian dentro del proyeto
- **Evaluación** : Este seria el último paso que recorre los nodos los cuales están bien definidos y los evalúa. Lo que provoca que cada estructura haga la acción específica a la cual esta definida 


###  Comandos del Lenguaje 
| **Comando**      | **Descripción**|
|:------------------:|:----------------:|
|**Spawn (int x ,int y)** | Se coloca en la posición (x,y) del tablero|
|**Size (int size)**     | Cambia el tamaño del Pincel , si es un número par , lo convierte al impar más cercano|
|**Color ( "Blue" )**     | Cambia el color del Pincel al color pasado |
|**DrawLine  ( int dirx , int dir y , int distancia )** | Dibuja una línea desde la posición actual en la dirección (dirX,dirY) a la distancia especificada |
|**DrawCircle(int dirX , int dirY , int radius)**|Crea un círculo de ese radio y el centro del círculo se ubica a una distancia igual que el radio y en la dirección (dirX , dirY) con respecto a al posición actual |
|**DrawRectangle(int dirX , int dirY , int distancia , int ancho , int altura)** |Similar al círculo ,coloca el centro del rectángulo en a la distancia dada en la dirección (dirX, int dirY) |
|**Fill()**|Pinta con el color de brocha actual todos los pı́xeles del color de la posición actual que son alcanzables sin tener que caminar sobre algún otro color|
|**x <- Expression**| Asignación de variables , a x se le asigna Expression|
|**GetActualX()**|Retorna el valor X de la posición actual |
|**GetActualY()**|Retorna el valor Y de la posición actual|
|**GetCanvasSize()**|Retorna tamaño largo y ancho del canvas en un tupla<int,int>|
|**GetColorCount (string color, int x1, int y1, int x2, int y2)**|Retorna la cantidad de casillas con color  que hay en el rectángulo formado por las posiciones x1,y1 (una esquina) y x2, y2(la otra esquina). Si cualquiera de las esquinas cae fuera de las dimensiones del canvas, debe retornar 0|
|**IsBrushColor(string color)**| Retorna 1 si el color de la brocha actual es string color, 0 en caso contrario|
|**IsBrushSize(int size)**| Retorna 1 si el tamaño de la brocha actual es size, 0 en caso contrario|
|**IsCanvasColor(string color, int vertical, int horizontal)**|Retorna 1 si la casilla señalada está pintada del parámetro color, 0 en caso contrario. La casilla en  cuestión se determina por la posición actual de Wall-E (X, Y) y se calcula como: (X + horizontal, Y + vertical). Note que si tanto horizontal como vertical son 0, se verifica entonces la casilla actual de Wall-E. Si la posición cae fuera del canvas debe retornar false|
|**GoTo [label] (condition)**| Si la condición es verdadera va a la posición del label |


[def]: photo/espacio.jpg