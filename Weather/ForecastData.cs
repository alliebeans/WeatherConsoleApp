using System;
using System.Collections;

namespace WeatherApp.Weather
{
    public class ForecastData : ICollection<WeatherData>
    {
        protected WeatherData[] _items;
        protected int _count = 0;
        protected int _capacity = 8;
        public int Count => _count;
        public bool IsReadOnly => true;

        public ForecastData()
        {
            _items = new WeatherData[8];
        }

        public WeatherData this[int index]
        {
            get
            {
                CheckBoundaries(index);
                return _items[index];
            }
            set
            {
                CheckBoundaries(index);
                _items[index] = value;
            }
        }

        public void Add(WeatherData item)
        {
            if (_count == _capacity)
                throw new InvalidOperationException();
            _items[_count] = item;
            _count++;
        }

        public void Clear() => _count = 0;

        public bool Contains(WeatherData item)
        {
            return this.Any(x => object.Equals(x, item));
        }

        public void CopyTo(WeatherData[] array, int arrayIndex)
        {
            var _newLength = _items.Length - arrayIndex;
            if (arrayIndex < 0)
                throw new ArgumentOutOfRangeException();
            if (array == null)
                throw new ArgumentNullException();
            if (array.Length < _newLength)
                throw new ArgumentException();

            var i = arrayIndex;
            for (int j = 0; i < _newLength; j++, i++)
                array[j] = _items[i];
        }

        public IEnumerator<WeatherData> GetEnumerator()
        {
            for (var i = 0; i < _count; i++)
                yield return _items[i];
        }

        public bool Remove(WeatherData item)
        {
            var i = Array.IndexOf(_items, item);
            if (i == -1)
                return false;

            for (int j = i + 1; j < _count; j++)
                _items[j - 1] = _items[j];
            
            _count--;
            return true;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private void CheckBoundaries(int index)
        {
            if (index >= _count)
                throw new IndexOutOfRangeException();
        }
    }
}