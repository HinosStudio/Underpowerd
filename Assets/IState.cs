public interface IState<T>
{
    void OnEnter(T source);
    void OnExit(T source);
    void OnUpdate(T source);
}


