using TransformApi.Transformations;

namespace TransformApi.Model;
public interface ITransformable<T> where T : ITransformable<T>
{
    T TransformBy(T payload, IEnumerable<ITransformation<T>> transformations);
}
