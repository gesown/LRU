using System;
using System.Linq;
using LRU.Entity;
using LRU.Facade;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LRUTests
{
    [TestClass]
    public class CheckCacheValueGet
    {
        [TestMethod]
        public void ValueGetterTest()
        {
            int capacity=700;
            LRUFacade.InitCacheRandom(capacity);
            int j = 0;
            for (int i = 0; i < LRUCache.KeysT.Length; i++)
            {
                if (LRUCache.KeysT[i] == null) break;
                var keyObj = LRUCache.KeysT[i];
                var checkVal = LRUCache.Get<KeyObjectType, ValueObjectType>((KeyObjectType)keyObj);
                if (checkVal != null) j++;
            }
            Assert.AreEqual(j, LRUCache.WhenLastUsedTicks.Where(s=>s!=null).Count()); // cache may not be full depending on key/value pair count < capacity
        }
    }
}
