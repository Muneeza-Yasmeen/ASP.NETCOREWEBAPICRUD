using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace YourTestProject.Mocks
{
    public class DbSetMock<T> : Mock<DbSet<T>> where T : class
    {
        private readonly List<T> _data;

        public DbSetMock(List<T> data)
        {
            _data = data;
            var queryableData = data.AsQueryable();

            As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryableData.Provider);
            As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryableData.Expression);
            As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryableData.ElementType);
            As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(queryableData.GetEnumerator());
        }

        public void SetupFindAsync<TKey>(Func<T, TKey> keySelector) where TKey : notnull
        {
            Setup(m => m.FindAsync(It.IsAny<object[]>()))
                .ReturnsAsync((object[] ids) => _data.SingleOrDefault(d => EqualityComparer<TKey>.Default.Equals(keySelector(d), (TKey)ids[0])));
        }
    }
}
