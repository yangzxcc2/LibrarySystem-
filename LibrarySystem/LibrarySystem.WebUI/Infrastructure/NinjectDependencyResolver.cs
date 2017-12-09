using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LibrarySystem.Domain.Abstract;
using LibrarySystem.Domain.Entities;
using Moq;
using Ninject;

namespace LibrarySystem.WebUI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }
        private void AddBindings()
        {
            Mock<IBooksRepository> mock = new Mock<IBooksRepository>();
            mock.Setup(m => m.book).Returns(new List<Book>
            {
                new Book {BookName = "Percy Jackson", BookID = 1},
                new Book {BookName = "Prince of Persia", BookID = 2},
                new Book {BookName = "Divergent", BookID = 3}
            });
            kernel.Bind<IBooksRepository>().ToConstant(mock.Object);
        }
    }
}