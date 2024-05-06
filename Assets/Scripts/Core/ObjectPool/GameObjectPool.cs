using System;
using System.Collections.Generic;

namespace BoxBoyGame.Core.ObjectPool 
{
    public class GameObjectPool<T>
    {
        private readonly List<T> currentStock;
        private readonly Func<T> factoryMethod;

        private readonly bool isDynamic;
        private readonly Action<T> turnOnCallback;
        private readonly Action<T> turnOffCallback;

        #region Public Method
        public GameObjectPool(Func<T> factoryMethod, Action<T> turnOnCallback, Action<T> turnOffCallback, int initialStock = 0, bool isDynamic = true)
        {
            this.factoryMethod = factoryMethod;
            this.isDynamic = isDynamic;

            this.turnOffCallback = turnOffCallback;
            this.turnOnCallback = turnOnCallback;

            currentStock = new List<T>();

            CreatePool(initialStock);
        }

        public void TurnOnObject()
        {
            var result = default(T);
            if (currentStock.Count > 0)
            {
                result = currentStock[0];
                currentStock.RemoveAt(0);
            }
            else if (isDynamic)
                result = factoryMethod();

            if (result != null) 
            {
                turnOnCallback?.Invoke(result);
            }
        }

        public void TurnOffObject(T o)
        {
            turnOffCallback?.Invoke(o);
            currentStock.Add(o);
        }
        #endregion

        #region Private Methods
        private void CreatePool(int initialStock = 0) 
        {
            for (var i = 0; i < initialStock; i++)
            {
                var o = this.factoryMethod();
                this.turnOffCallback?.Invoke(o);
                currentStock.Add(o);
            }
        }
        #endregion
    }
}