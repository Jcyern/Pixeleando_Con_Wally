
using ArbolSintaxisAbstracta;
using Parseando;

namespace IParseo
{
    public interface IParse
    {
        public AstNode Parse(Parser parser );
    }
}