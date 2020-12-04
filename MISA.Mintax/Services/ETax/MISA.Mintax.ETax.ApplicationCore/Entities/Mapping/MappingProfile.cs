using AutoMapper;
using MISA.Mintax.ETax.ApplicationCore.Entities.DTO;
using MISA.Mintax.ETax.ApplicationCore.Entities.ETax;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
namespace MISA.Mintax.ETax.ApplicationCore.Entities.Mapping
{
    public class MappingProfile :Profile
    {
        public MappingProfile()
        {


            CreateMap<Employee, EmployeeForList>();
            CreateMap<EmployeeForList, Employee>();

        }
    }
}
