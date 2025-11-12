using TransformApi.Model;

namespace TransformApi.Transformations;

public interface ITransformation<T> where T : ITransformable<T>
{
    T Transform(T payload);
}
