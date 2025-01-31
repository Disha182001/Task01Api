﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Commands
{
    public class CreateStudentCommand : IRequest<int>
    {

        public string Name { get; set; }
        public int Age { get; set; }
    }
}
