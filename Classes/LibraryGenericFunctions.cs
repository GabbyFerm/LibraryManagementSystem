using LibraryManagementSystem.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Classes
{
  public class LibraryGenericFunctions<T> where T : class, IIdentifiable
  {
    private List<T> _libraryItem;

    public LibraryGenericFunctions(List<T> listItems)
    {
        _libraryItem = listItems;
    }

        public List<T> ListAll()
        {
            return _libraryItem.ToList();
        }
        public void Add(T item)
        {
            _libraryItem.Add(item);
        }
        public void Update(T item)
        {
            var index = _libraryItem.FindIndex(item => (item as IIdentifiable)!.Id == (item as IIdentifiable)!.Id);
            if (index >= 0)
            {
                _libraryItem[index] = item;
            }
        }
        public T Fetch(int id)
        {
            return _libraryItem.FirstOrDefault(item => item.Id == id)!;
        }
        public void Remove(int id)
        {
            var item = Fetch(id);
            if (item != null)
            {
                _libraryItem.Remove(item);
            }
        }
        public void RemoveByCondition(Func<T, bool> condition)
        {
            var itemToRemove = _libraryItem.FirstOrDefault(condition);
            if (itemToRemove != null)
            {
                _libraryItem.Remove(itemToRemove);
                Console.WriteLine($"{typeof(T).Name} with ID {itemToRemove.Id} has been removed successfully.");
            }
            else
            {
                Console.WriteLine($"{typeof(T).Name} not found.");
            }
        }
    }
}
