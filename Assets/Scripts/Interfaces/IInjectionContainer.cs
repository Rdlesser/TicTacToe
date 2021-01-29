public interface IInjectionContainer<T> where T : class 
{
    void Inject(T injection);
}