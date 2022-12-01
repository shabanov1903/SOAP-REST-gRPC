﻿using LibraryService.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using LibraryService.API.Properties;

namespace LibraryService.Services.Impl
{
    public class LibraryDatabaseContext : ILibraryDatabaseContextService
    {
        private IList<Book> _libraryDatabase;

        public IList<Book> Books => _libraryDatabase;

        public LibraryDatabaseContext()
        {
            Initialize();
        }

        private void Initialize()
        {
           _libraryDatabase =
                (List<Book>)JsonConvert.DeserializeObject(Encoding.UTF8.GetString(Resources.books), typeof(List<Book>));
        }
    }
}