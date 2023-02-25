using Mapster;
using Studentby.App.Data.Database.Entities;
using Studentby.App.Logic.Employers.Models;
using Studentby.App.Logic.Students.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studentby.App.Logic.Common.Mapper;
internal class UserMappingRegistration : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Student, StudentRes>()
            .Map(dest => dest.Email, src => src.User.Email);

        config.NewConfig<Employer, EmployerRes>()
            .Map(dest => dest.Email, src => src.User.Email);
    }
}
