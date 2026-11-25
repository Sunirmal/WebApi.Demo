namespace TesdtApplication.Caching
{
    public class LRUCache<Tkey, TValue>
    {
        private readonly int _capacity;
        private readonly Dictionary<Tkey, LinkedListNode<(Tkey Key, TValue Value)>> _cache = new();
        private readonly LinkedList<(Tkey Key, TValue Value)> _list;
        private readonly object _lock = new();

        public LRUCache(int capacity)
        {
            _capacity = capacity;
            _list = new();
        }
            
        public TValue Get(Tkey key)
        {
            lock (_lock)
            {
                if (!_cache.TryGetValue(key, out var node))
                    return default;

                MoveToFront(node);
                return node.Value.Value;
            }
        }

        public void Put(Tkey key, TValue value)
        {
            lock (_lock)
            {
                if (_cache.TryGetValue(key, out var node))
                {
                    MoveToFront(node);
                    node.Value = (key, value);
                    return;
                }

                if (_cache.Count >= _capacity && _list.Last is { } lastNode)
                {
                    _cache.Remove(lastNode.Value.Key);
                    _list.Remove(lastNode);
                }

                var newNode = _list.AddFirst((key, value));
                _cache[key] = newNode;
            }
        }

        private void MoveToFront(LinkedListNode<(Tkey Key, TValue Value)> node)
        {
            _list.Remove(node);
            _list.AddFirst(node);
        }
    }
}
