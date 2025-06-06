
using Evalua;

namespace Ievalua
{
    public interface IEvaluate
    {
        public object? Evaluate(Evaluator? evaluador = null);
    }
}