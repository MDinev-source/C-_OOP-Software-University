﻿using EasterRaces.Models.Drivers.Contracts;
using EasterRaces.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasterRaces.Repositories.Entities
{
    public class DriverRepository : IRepository<IDriver>
    {
        private readonly List<IDriver> drivers;
        public DriverRepository()
        {
            this.drivers = new List<IDriver>();
        }
        public IReadOnlyCollection<IDriver> Drivers
            => this.drivers.AsReadOnly();

        public void Add(IDriver model)
        {
            this.drivers.Add(model);
        }

        public IReadOnlyCollection<IDriver> GetAll()
        {
            return Drivers;
        }

        public IDriver GetByName(string name)
        {
            return drivers.FirstOrDefault(x => x.Name == name);
        }

        public bool Remove(IDriver model)
        {
            return this.drivers.Remove(model);
        }
    }
}
