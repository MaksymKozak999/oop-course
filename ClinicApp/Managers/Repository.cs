using System.Collections.Generic;
using ClinicApp.Interfaces;

namespace ClinicApp.Managers;

public class Repository<T> where T : IIdentifiable
{
    private List<T> _items = new List<T>();

    public int Count => _items.Count;

    public void Add(T item)
    {
        _items.Add(item);
    }

    public T? GetById(int id)
    {
        foreach (T item in _items)
        {
            if (item.Id == id)
            {
                return item;
            }
        }
        return default; // повертає null для посилальних типів
    }

    public T[] GetAll()
    {
        return _items.ToArray();
    }

    public bool Remove(int id)
    {
        T? item = GetById(id);
        if (item != null)
        {
            _items.Remove(item);
            return true;
        }
        return false;
    }
}