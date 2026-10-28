namespace ClinicApp.Models;

public class WaitingQueue<T>
{
    Queue<T> _patients = new Queue<T>();
    public int Count => _patients.Count;

    public bool IsEmpty()
    {
        if (Count == 0)
            return true;
        else
            return false;
        
    }

    public void Enqueue(T item)
    {
        _patients.Enqueue(item);
    }

    public T Dequeue()
    {
        if (IsEmpty() == true)
        {
            throw new InvalidOperationException("Черга порожня.");
        }
        return _patients.Dequeue();
        
    }

    public T Peek()
    {
        if (IsEmpty() == true)
        {
            throw new InvalidOperationException("Черга порожня.");
        }
        return _patients.Peek();
    }
}