using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using wasteless.Models;
using wasteless.Services;

namespace wasteless.Resolvers
{
    public class NoiseListResolver
    {
        public static NoiseListViewModel GetNoiseListResolver()
        {
            return new NoiseListViewModel { NoiseListDTO = DBService.GetNoises() };
        }
    }
}