using MyColor.Domain.Entities;
using System.Collections.Generic;

namespace MyColor.Infra.Data.Interfaces
{
    public interface IFileReader
    {
        /// <summary>
        /// Load some values from a file to the database
        /// </summary>
        IEnumerable<Person> LoadDataFromFile();
    }
}
