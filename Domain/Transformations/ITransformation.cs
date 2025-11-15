using TransformApi.Domain.Model;

namespace TransformApi.Domain.Transformations;

public interface ITransformation<T> where T : ITransformable<T>
{
    T Transform(T payload);
}
