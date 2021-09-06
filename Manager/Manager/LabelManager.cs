﻿using System;
using System.Collections.Generic;
using System.Text;
using Manager.Interface;
using Models.Models;
using Repository.Interface;

namespace Manager.Manager
{
    public class LabelManager : ILabelManager
    {
        private readonly ILabelRepository repository;

        public LabelManager(ILabelRepository repository)
        {
            this.repository = repository;
        }
        public bool AddLabeltoNote(LabelModel labelData)
        {
            try
            {
                return this.repository.AddLabeltoNote(labelData);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
