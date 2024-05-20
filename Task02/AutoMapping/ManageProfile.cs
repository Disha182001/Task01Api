using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Persistence.Entities.Model;
using Persistence.Entities.ViewModel;
namespace Persistence.AutoMapping;



    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Student, StudentVM>().ReverseMap();
            
        }
    }

