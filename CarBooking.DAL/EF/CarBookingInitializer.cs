﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace CarBooking.DAL.EF
{
    class CarBookingInitializer : DropCreateDatabaseAlways<CarBookingContex>
    {
        protected override void Seed(CarBookingContex context)
        {
            
        }
    }
}
