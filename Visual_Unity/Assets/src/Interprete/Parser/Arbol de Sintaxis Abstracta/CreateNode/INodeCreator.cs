using Expresion;
namespace INodeCreador
{
    public interface INodeCreator
    {
        public Expression? CreateNode(Expression? Left, Token Operator, Expression? Right);

    
    }
    


    //para mejorar la extensibilidad y poder agrupar  la creacion de nodos en un solo lugar en este caso lo  mas general son los binarios que es lo  que me pide este lenguaje , y  los unarios serias un caso particular de este 
}