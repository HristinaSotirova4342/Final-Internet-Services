using AutoMapper;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace BankApplication.Tests.Internal
{
    public static class AutoMapperModule
    {
        private static MapperConfiguration configuration;
        private static IMapper mapper;

        public static IMapper CreateMapper()
        {
            if (mapper == null)
            {
                mapper = new Mapper(CreateMapperConfiguration());
            }

            return mapper;
        }

        public static MapperConfiguration CreateMapperConfiguration()
        {
            if (configuration == null)
            {
                configuration = new MapperConfiguration(cfg =>
                {
                    cfg.AddMaps(Assembly.Load("BankApplication.Data"));

                   
                });
            }

            return configuration;
        }
    }
}

