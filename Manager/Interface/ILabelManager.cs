﻿using Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Manager.Interface
{
    public interface ILabelManager
    {
        bool AddLabeltoNote(LabelModel labelData);
    }
}