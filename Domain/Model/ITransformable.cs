using TransformApi.Domain.Transformations;

namespace TransformApi.Domain.Model;
public interface ITransformable<T> where T : ITransformable<T>
{
    T TransformBy(T payload, IEnumerable<ITransformation<T>> transformations);
}
