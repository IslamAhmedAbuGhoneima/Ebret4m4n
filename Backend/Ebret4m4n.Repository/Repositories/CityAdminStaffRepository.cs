﻿using Ebret4m4n.Contracts;
using Ebret4m4n.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ebret4m4n.Repository.Repositories
{
    public class CityAdminStaffRepository : BaseRepository<CityAdminStaff>, ICityAdminStaffRepository
    {
        public CityAdminStaffRepository(EbretAmanDbContext context) : base(context)
        {
        }
    }
}
