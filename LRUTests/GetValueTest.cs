using System;
using System.Collections.Generic;
using System.Linq;
using LRU.Entity;
using LRU.Facade;
using LRU.Factory;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LRUTests
{
    /// <summary>
    /// Summary description for CodedUITest1
    /// </summary>
    [CodedUITest]
    public class GetValueTest
    {
        private Random rand = new Random();
        private Random _rand1;
        private readonly LRUFactory _factory;
        readonly IList<KeyValuePair<int, int>> _valuesToCache = new List<KeyValuePair<int, int>>();
        private IList<int?> _cacheValList;
        private IList<int?> _tocacheValList;
        private IList<int?> _cachedValues;
        private IList<int> _delCacheVal;
        private int _delCount=10; // init to capacity to account for initial fill
        private int _loops;
        private IList<int?> _currCacheVal;
        private IList<int?> _newCacheVal;

        public GetValueTest()
        {
            _factory = LRUFacade.GetFactory();
        }

        [TestMethod]
        public void GetValueTestMethod()
        {
            var capacity = 10;
            _factory.GetLruCache(capacity);  // init static content to null as singleton
            _rand1 = new Random();
            for (int i = 0; i < 100; i++)
            {
                _valuesToCache.Add(new KeyValuePair<int, int>(rand.Next(), _rand1.Next()));
            }
            _loops = rand.Next(10, 100);
            for (int i = 0; i < _loops; i++) // exercise cache
            {
                _currCacheVal = LRUCache.ValuesT.Cast<int?>().Where(s => s != null).ToList();
                
                LRUCache.Set(_valuesToCache[i].Key, _valuesToCache[i].Value);
                _newCacheVal = LRUCache.ValuesT.Cast<int?>().Where(s => s != null).ToList();
                if (_currCacheVal.Count == 10 && _newCacheVal.Count == 10)
                {
                    _delCacheVal = _currCacheVal.Except(_newCacheVal).Cast<int>().ToList();
                    if (_delCacheVal.Count == 1) _delCount++;
                }
            }
            _cacheValList = LRUCache.ValuesT.Cast<int?>().ToList();
            _tocacheValList = _valuesToCache.Select(s => s.Value).Cast<int?>().ToList();
            _cachedValues = _cacheValList.Intersect(_tocacheValList).ToList();
            Assert.AreEqual(_cacheValList.Count, _cachedValues.Count);
            Assert.AreEqual(_loops, _delCount);
        }
    }
}
