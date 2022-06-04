using DapperUow.Dal.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace DapperUow.Dal
{
    //Başında I olanlar interface
    public interface IUnitOfWork: IDisposable
    {

        // Repositorylerimizi ekliyoruz bu alana 
        IProductRepository ProductRepository { get;  }

        void Commit();

       // IUnitOfWork bu alanı oluşturduk sonrada UnitOfWork oluşturduk. projeye sağ tıklayıp ekliyoruz sınıflarımızı. 

    }
}
